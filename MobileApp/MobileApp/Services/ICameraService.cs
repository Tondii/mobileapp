using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace MobileApp.Services
{
    public interface ICameraService
    {
        Task<MediaFile> TakePhotoAsync();
    }
}
