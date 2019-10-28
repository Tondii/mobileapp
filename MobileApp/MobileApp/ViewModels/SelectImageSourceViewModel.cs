using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Exceptions;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class SelectImageSourceViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly ICameraService _cameraService;
        public ICommand TakePhoto => new Command(async () => await TakePhotoAsync());

        public SelectImageSourceViewModel(INavigationService navigationService, IDialogService dialogService, ICameraService cameraService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _cameraService = cameraService;
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await _cameraService.TakePhotoAsync();
                if (photo != null)
                {
                    await using var ms = new MemoryStream();
                    photo.GetStream().CopyTo(ms);
                    photo.Dispose();
                    await _navigationService.NavigateTo(new CameraResultPage(), ms.ToArray());
                }
            }
            catch (PermissionDeniedException ex)
            {
                await _dialogService.DisplayAlert("Permission denied", ex.ToString(), "OK");
            }
        }
    }
}
