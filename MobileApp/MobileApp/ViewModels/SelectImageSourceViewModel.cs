using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Exceptions;
using MobileApp.Services;
using MobileApp.Views;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class SelectImageSourceViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly ICameraService _cameraService;
        public ICommand TakePhoto { get; }

        public SelectImageSourceViewModel(INavigationService navigationService, IDialogService dialogService, ICameraService cameraService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _cameraService = cameraService;

            TakePhoto = new Command(async () => await TakePhotoAsync());
        }

        private async Task TakePhotoAsync()
        {
            MediaFile photo;
            try
            {
                photo = await _cameraService.TakePhotoAsync();
                if (photo != null)
                {
                    var image = ImageSource.FromStream(() => photo.GetStream());
                    await _navigationService.NavigateTo(new CameraResultPage(), image);
                }
            }
            catch (PermissionDeniedException ex)
            {
                await _dialogService.DisplayAlert("Permission denied", ex.ToString(), "OK");
            }
        }
    }
}
