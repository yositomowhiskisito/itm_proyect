using System.IO;
using System.Threading.Tasks;

namespace XAM_ProyectITM.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }

    public interface IStyleService
    {
        void GetStyle(string value);
    }
}
