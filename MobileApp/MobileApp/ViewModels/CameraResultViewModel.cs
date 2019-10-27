using System;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using MobileApp.Services;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class CameraResultViewModel : BaseViewModel, IParameterized<ImageSource>
    {
        private ImageSource _image;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public CameraResultViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        public void HandleParameter(ImageSource parameter)
        {
            Image = parameter;
        }

        public ICommand AddNewReceipt =>
            new Command(async () =>
            {
                var receipt = new Receipt()
                {
                    BruttoSummary = 20,
                    Company = new Company(),
                    CreateDateTime = DateTime.Now,
                    PicturePath = string.Empty
                };
                _dataService.AddReceipt(receipt);
                await _navigationService.NavigateTo(new MainPage());

            });
    }
}
