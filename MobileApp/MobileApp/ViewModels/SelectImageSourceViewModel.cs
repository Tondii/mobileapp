using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Services;
using MobileApp.Views;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class SelectImageSourceViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand TakePhoto { get; }

        public SelectImageSourceViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            TakePhoto = new Command(async () => await TakePhotoAsync());
        }

        private async Task TakePhotoAsync()
        {
            var permission = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (permission != PermissionStatus.Granted)
            {
                var grantedPermission =
                    await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                if (grantedPermission[Permission.Camera] != PermissionStatus.Granted)
                {
                    await _dialogService.DisplayAlert("Access denied", permission.ToString(), "OK");
                    return;
                }
            }

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                var image = ImageSource.FromStream(() => photo.GetStream());
                await _navigationService.NavigateTo(new CameraResultPage(), image);
            }
        }
    }
}
