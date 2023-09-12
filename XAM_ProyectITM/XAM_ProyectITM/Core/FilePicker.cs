using System;
using System.IO;
using System.Threading.Tasks;

namespace XAM_ProyectITM.Core
{
    public class CrossFilePicker
    {
        public static IFilePicker Current;
    }

    public interface IFilePicker
    {
        Task<FileData> PickFile(string type = "");
        Task<bool> SaveFile(FileData fileToSave);
        void OpenFile(string fileToOpen);
        void OpenFile(FileData fileToOpen);
    }

    public class FilePickerEventArgs : EventArgs
    {
        public byte[] FileByte { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public FilePickerEventArgs() { }

        public FilePickerEventArgs(byte[] fileByte)
        {
            FileByte = fileByte;
        }

        public FilePickerEventArgs(byte[] fileByte, string fileName)
            : this(fileByte)
        {
            FileName = fileName;
        }

        public FilePickerEventArgs(byte[] fileByte, string fileName, string filePath)
            : this(fileByte, fileName)
        {
            FilePath = filePath;
        }
    }

    public class FileData : IDisposable
    {
        private string _fileName;
        private string _filePath;
        private bool _isDisposed;
        private byte[] _dataArray;
        private readonly Action<bool> _dispose;
        private readonly Func<Stream> _streamGetter;

        public FileData() { }

        public FileData(string filePath, string fileName, byte[] dataArray, Action<bool> dispose = null)
        {
            _filePath = filePath;
            _fileName = fileName;
            _dispose = dispose;
            _dataArray = dataArray;
        }

        public FileData(string filePath, string fileName, Func<Stream> streamGetter, Action<bool> dispose = null)
        {
            _filePath = filePath;
            _fileName = fileName;
            _dispose = dispose;
            _streamGetter = streamGetter;
        }

        public byte[] DataArray
        {
            get
            {
                if (_dataArray != null)
                    return _dataArray;

                using (var stream = GetStream())
                {
                    var resultBytes = new byte[stream.Length];
                    stream.Read(resultBytes, 0, (int)stream.Length);

                    return resultBytes;
                }
            }
        }

        public string FileName
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(null);

                return _fileName;
            }
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(null);

                _fileName = value;
            }
        }

        public string FilePath
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(null);

                return _filePath;
            }
            set
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(null);

                _filePath = value;
            }
        }

        public Stream GetStream()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(null);

            return _streamGetter();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _dispose?.Invoke(disposing);
        }

        ~FileData()
        {
            Dispose(false);
        }
    }
}