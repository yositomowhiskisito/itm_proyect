using System;
using Java.IO;
using Android.OS;
using Android.Content;
using Android.Provider;
using Android.Database;
using Android.Webkit;
using Android.App;
using Android.Runtime;
using System.Threading;
using System.Threading.Tasks;
using LIBUtilities.Core;
using XAM_ProyectITM.Core;

namespace XAM_ProyectITM.Droid.Core
{
    [Preserve(AllMembers = true)]
    public class FilePickerImplementation : IFilePicker
    {
        private readonly Context _context;
        private int _requestId;
        private TaskCompletionSource<FileData> _completionSource;

        public FilePickerImplementation()
        {
            _context = Application.Context;
        }

        public async Task<FileData> PickFile(string type = "documents/*")
        {
            CacheHelper.Add("FindFiles", type);
            return await TakeMediaAsync(type, Intent.ActionGetContent);
        }

        private Task<FileData> TakeMediaAsync(string type, string action)
        {
            var id = GetRequestId();
            var ntcs = new TaskCompletionSource<FileData>(id);
            if (Interlocked.CompareExchange(ref _completionSource, ntcs, null) != null)
                throw new InvalidOperationException("Only one operation can be active at a time");

            try
            {
                var pickerIntent = new Intent(this._context, typeof(FilePickerActivity));
                pickerIntent.SetFlags(ActivityFlags.NewTask);
                this._context.StartActivity(pickerIntent);

                EventHandler<FilePickerEventArgs> handler = null;
                EventHandler<EventArgs> cancelledHandler = null;

                handler = (s, e) => {
                    var tcs = Interlocked.Exchange(ref _completionSource, null);
                    FilePickerActivity.FilePicked -= handler;
                    //tcs?.SetResult(new FileData(e.FilePath, e.FileName, () => System.IO.File.OpenRead(e.FilePath)));
                    tcs?.SetResult(new FileData(e.FilePath, e.FileName, e.FileByte));
                };

                cancelledHandler = (s, e) => {
                    var tcs = Interlocked.Exchange(ref _completionSource, null);
                    FilePickerActivity.FilePickCancelled -= cancelledHandler;
                    tcs?.SetResult(null);
                };

                FilePickerActivity.FilePickCancelled += cancelledHandler;
                FilePickerActivity.FilePicked += handler;
            }
            catch (Exception)
            {

            }
            return _completionSource.Task;
        }

        private int GetRequestId()
        {
            int id = _requestId;

            if (_requestId == int.MaxValue)
                _requestId = 0;
            else
                _requestId++;

            return id;
        }

        public async Task<bool> SaveFile(FileData fileToSave)
        {
            try
            {
                var myFile = new File(Android.OS.Environment.ExternalStorageDirectory, fileToSave.FileName);

                if (myFile.Exists())
                    return true;

                var fos = new FileOutputStream(myFile.Path);

                fos.Write(fileToSave.DataArray);
                fos.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void OpenFile(File fileToOpen)
        {
            var uri = Android.Net.Uri.FromFile(fileToOpen);
            var intent = new Intent();
            var mime = IOUtil.GetMimeType(uri.ToString());

            intent.SetAction(Intent.ActionView);
            intent.SetDataAndType(uri, mime);
            intent.SetFlags(ActivityFlags.NewTask);

            _context.StartActivity(intent);
        }

        public void OpenFile(string fileToOpen)
        {
            var myFile = new File(Android.OS.Environment.ExternalStorageState, fileToOpen);

            OpenFile(myFile);
        }

        public async void OpenFile(FileData fileToOpen)
        {
            var myFile = new File(Android.OS.Environment.ExternalStorageState, fileToOpen.FileName);

            if (!myFile.Exists())
                await SaveFile(fileToOpen);

            OpenFile(fileToOpen);
        }
    }

    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    [Preserve(AllMembers = true)]
    public class FilePickerActivity : Activity
    {
        private Context context;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            context = Application.Context;
            var intent = new Intent(Intent.ActionGetContent);
            intent.SetType(CacheHelper.GetValue("FindFiles").ToString());
            intent.AddCategory(Intent.CategoryOpenable);
            try
            {
                StartActivityForResult(Intent.CreateChooser(intent, "Select file"), 0);
            }
            catch (Exception exAct)
            {
                System.Diagnostics.Debug.Write(exAct);
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Canceled)
            {
                OnFilePickCancelled();
                Finish();
            }
            else
            {
                System.Diagnostics.Debug.Write(data.Data);
                try
                {
                    var _uri = data.Data;
                    var filePath = IOUtil.getPath(context, _uri);
                    if (string.IsNullOrEmpty(filePath))
                        filePath = _uri.Path;

                    var file = IOUtil.readFile(filePath);
                    var fileName = GetFileName(context, _uri);
                    OnFilePicked(new FilePickerEventArgs(file, fileName, filePath));
                }
                catch (Exception readEx)
                {
                    OnFilePickCancelled();
                    System.Diagnostics.Debug.Write(readEx);
                }
                finally
                {
                    Finish();
                }
            }
        }

        string GetFileName(Context ctx, Android.Net.Uri uri)
        {
            string[] projection = { MediaStore.MediaColumns.DisplayName };
            var cr = ctx.ContentResolver;
            var name = "";
            var metaCursor = cr.Query(uri, projection, null, null, null);
            if (metaCursor != null)
            {
                try
                {
                    if (metaCursor.MoveToFirst())
                    {
                        name = metaCursor.GetString(0);
                    }
                }
                finally
                {
                    metaCursor.Close();
                }
            }
            return name;
        }

        internal static event EventHandler<FilePickerEventArgs> FilePicked;
        internal static event EventHandler<EventArgs> FilePickCancelled;

        private static void OnFilePickCancelled()
        {
            FilePickCancelled?.Invoke(null, null);
        }

        private static void OnFilePicked(FilePickerEventArgs e)
        {
            var picked = FilePicked;
            if (picked != null)
                picked(null, e);
        }
    }

    public class IOUtil
    {
        public static string getPath(Context context, Android.Net.Uri uri)
        {
            bool isKitKat = Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;

            if (isKitKat && DocumentsContract.IsDocumentUri(context, uri))
            {
                if (isExternalStorageDocument(uri))
                {
                    var docId = DocumentsContract.GetDocumentId(uri);
                    string[] split = docId.Split(':');
                    var type = split[0];

                    if ("primary".Equals(type, StringComparison.OrdinalIgnoreCase))
                    {
                        return Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
                    }
                }
                else if (isDownloadsDocument(uri))
                {
                    string id = DocumentsContract.GetDocumentId(uri);
                    Android.Net.Uri contentUri = ContentUris.WithAppendedId(
                            Android.Net.Uri.Parse("content://downloads/public_downloads"), long.Parse(id));

                    return getDataColumn(context, contentUri, null, null);
                }
                else if (isMediaDocument(uri))
                {
                    var docId = DocumentsContract.GetDocumentId(uri);
                    string[] split = docId.Split(':');
                    var type = split[0];

                    Android.Net.Uri contentUri = null;
                    if ("image".Equals(type))
                    {
                        contentUri = MediaStore.Images.Media.ExternalContentUri;
                    }
                    else if ("video".Equals(type))
                    {
                        contentUri = MediaStore.Video.Media.ExternalContentUri;
                    }
                    else if ("audio".Equals(type))
                    {
                        contentUri = MediaStore.Audio.Media.ExternalContentUri;
                    }

                    var selection = "_id=?";
                    var selectionArgs = new string[] {
                        split[1]
                    };

                    return getDataColumn(context, contentUri, selection, selectionArgs);
                }
            }
            else if ("content".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return getDataColumn(context, uri, null, null);
            }
            else if ("file".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return uri.Path;
            }
            return null;
        }

        public static string getDataColumn(Context context, Android.Net.Uri uri, string selection,
            string[] selectionArgs)
        {
            ICursor cursor = null;
            var column = "_data";
            string[] projection = { column };
            try
            {
                cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs,
                        null);
                if (cursor != null && cursor.MoveToFirst())
                {
                    int column_index = cursor.GetColumnIndexOrThrow(column);
                    return cursor.GetString(column_index);
                }
            }
            finally
            {
                if (cursor != null)
                    cursor.Close();
            }
            return null;
        }

        public static bool isExternalStorageDocument(Android.Net.Uri uri)
        {
            return "com.android.externalstorage.documents".Equals(uri.Authority);
        }

        public static bool isDownloadsDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.downloads.documents".Equals(uri.Authority);
        }

        public static bool isMediaDocument(Android.Net.Uri uri)
        {
            return "com.android.providers.media.documents".Equals(uri.Authority);
        }

        public static byte[] readFile(string file)
        {
            try
            {
                return readFile(new File(file));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                return new byte[0];
            }
        }

        public static byte[] readFile(File file)
        {
            var f = new RandomAccessFile(file, "r");
            try
            {
                long longlength = f.Length();
                var length = (int)longlength;

                if (length != longlength)
                    throw new IOException("Filesize exceeds allowed size");
                byte[] data = new byte[length];
                f.ReadFully(data);
                return data;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                return new byte[0];
            }
            finally
            {
                f.Close();
            }
        }

        public static string GetMimeType(string url)
        {
            string type = null;
            var extension = MimeTypeMap.GetFileExtensionFromUrl(url);

            if (extension != null)
            {
                type = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            }
            return type;
        }
    }
}