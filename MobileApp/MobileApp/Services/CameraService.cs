using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace MobileApp.Services
{
    class CameraService : ICameraService
    {
        private readonly IPermissionService _permissionService;

        public CameraService(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<MediaFile> TakePhotoAsync()
        {
            await _permissionService.CheckAndRequestPermission(Permission.Camera);
            return await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
        }
    }
}
