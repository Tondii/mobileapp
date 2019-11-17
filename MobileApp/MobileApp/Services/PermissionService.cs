using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Exceptions;
using Plugin.Permissions.Abstractions;

namespace MobileApp.Services
{
    class PermissionService : IPermissionService
    {
        public async Task CheckAndRequestPermission(Permission permission)
        {
            var currentPermission = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(permission);

            if (currentPermission != PermissionStatus.Granted)
            {
                var grantedPermission = await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (grantedPermission[permission] != PermissionStatus.Granted)
                {
                    throw new PermissionDeniedException(grantedPermission[permission].ToString());
                }
            }
        }
    }
}
