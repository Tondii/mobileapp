using System.Threading.Tasks;
using MobileApp.Exceptions;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace MobileApp.Services
{
    class CameraService : ICameraService
    {
        public async Task<MediaFile> TakePhotoAsync()
        {
            var permission = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (permission != PermissionStatus.Granted)
            {
                var grantedPermission = await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                if (grantedPermission[Permission.Camera] != PermissionStatus.Granted)
                {
                    throw new PermissionDeniedException(grantedPermission[Permission.Camera].ToString());
                }
            }

            return await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
        }
    }
}
