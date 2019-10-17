using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Services;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewReceiptMethod : ContentPage
    {
        public NewReceiptMethod()
        {
            InitializeComponent();
        }

        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            var permission = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (permission != PermissionStatus.Granted)
            {
                var grantedPermission =
                    await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                if (grantedPermission[Permission.Camera] != PermissionStatus.Granted)
                {
                    await DisplayAlert("Access denied", permission.ToString(), "OK");
                    return;
                }

            }

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                var image = ImageSource.FromStream(() => photo.GetStream());
                await App.Container.GetInstance<INavigationService>().NavigateTo(new CameraResultPage(), image);
            }
        }
    }
}