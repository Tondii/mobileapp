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
        private readonly IFileService _fileService;
        public ICommand TakePhoto => new Command(async () => await TakePhotoAsync());
        public ICommand PickImage => new Command(async () => await PickImageAsync());

        public SelectImageSourceViewModel()
        {
            _navigationService = App.Container.GetInstance<INavigationService>();
            _dialogService = App.Container.GetInstance<IDialogService>();
            _cameraService = App.Container.GetInstance<ICameraService>();
            _fileService = App.Container.GetInstance<IFileService>();
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
        private async Task PickImageAsync()
        {
            try
            {
                var image = await _fileService.PickImageFromGallery();
                if (image != null)
                {
                    await using var ms = new MemoryStream();
                    image.GetStream().CopyTo(ms);
                    image.Dispose();
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
