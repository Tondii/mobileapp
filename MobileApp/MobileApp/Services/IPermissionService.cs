using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace MobileApp.Services
{
    interface IPermissionService
    {
        Task CheckAndRequestPermission(Permission permission);
    }
}
