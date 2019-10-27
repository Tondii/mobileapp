using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace MobileApp.Services
{
    interface ICameraService
    {
        Task<MediaFile> TakePhotoAsync();
    }
}
