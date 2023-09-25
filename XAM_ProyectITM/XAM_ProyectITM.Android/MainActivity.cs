using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using XAM_ProyectITM.Core;
using XAM_ProyectITM.Droid.Core;
using System.IO;
using Android.Content;
using System.Threading.Tasks;
using Android.Support.V7.App;
using XAM_ProyectITM.Services;
using XAM_ProyectITM.Droid.Services;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace XAM_ProyectITM.Droid
{
    [Activity(Label = "Proyect ITM", Icon = "@drawable/ITM", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Instance = this;
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            Window.SetNavigationBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossFilePicker.Current = new FilePickerImplementation();
            Rg.Plugins.Popup.Popup.Init(this);
            LoadApplication(new App());
        }

        public static readonly int PickImageId = 1000;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

    }
}