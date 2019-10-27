using System;
using System.IO;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using MobileApp.Services;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class CameraResultViewModel : BaseViewModel, IParameterized<Stream>
    {
        private Stream _imageStream;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        public ImageSource Image => ImageSource.FromStream(() => _imageStream);

        public CameraResultViewModel(INavigationService navigationService, IDataService dataService, IFileService fileService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _fileService = fileService;
        }

        public Stream ImageStream
        {
            get => _imageStream;
            set
            {
                _imageStream = value;
                RaisePropertyChanged();
            }
        }

        public void HandleParameter(Stream parameter)
        {
            ImageStream = parameter;
        }

        public ICommand AddNewReceipt =>
            new Command(async () =>
            {
                var receipt = new Receipt()
                {
                    BruttoSummary = 0,
                    Company = new Company(),
                    CreateDateTime = DateTime.Now,
                    PicturePath = _fileService.GenerateImagePath()
                };
                await _fileService.SaveFile(receipt.PicturePath, _imageStream);
                await _dataService.AddReceiptAsync(receipt);
                await _navigationService.NavigateTo(new MainPage());
            });
    }
}
