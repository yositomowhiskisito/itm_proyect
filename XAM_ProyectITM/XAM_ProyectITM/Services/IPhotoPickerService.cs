using System.IO;
using System.Threading.Tasks;

namespace XAM_ProyectITM.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
