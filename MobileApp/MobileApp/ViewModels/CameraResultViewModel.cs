using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Model.Recognition;
using MobileApp.Navigation;
using MobileApp.Recognition;
using MobileApp.Services;
using MobileApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class CameraResultViewModel : BaseViewModel, IParameterized<byte[]>
    {
        private bool _isProcessing;
        private byte[] _imageBytes;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly IRequestService _requestService;
        private readonly IDialogService _dialogService;
        private Receipt _receipt;


        public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(_imageBytes));
        public bool IsProcessing
        {
            get => _isProcessing;
            private set
            {
                _isProcessing = value;
                RaisePropertyChanged();
            }
        }
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set
            {
                _imageBytes = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddNewReceipt => new Command(async () => await AddNewReceiptAsync());

        public CameraResultViewModel()
        {
            _navigationService = App.Container.GetInstance<INavigationService>();
            _dataService = App.Container.GetInstance<IDataService>();
            _fileService = App.Container.GetInstance<IFileService>();
            _requestService = App.Container.GetInstance<IRequestService>();
            _dialogService = App.Container.GetInstance<IDialogService>();
        }

        public void HandleParameter(byte[] parameter)
        {
            ImageBytes = parameter;
        }

        private async Task AddNewReceiptAsync()
        {
            IsProcessing = true;
            _receipt = new Receipt
            {
                BruttoSummary = 0,
                Company = new Company(),
                CreateDateTime = DateTime.Now,
                PicturePath = _fileService.GenerateImagePath()
            };
            await _fileService.SaveImage(_receipt.PicturePath, _imageBytes, 50);
            try
            {
                await RecognizeElements();
                RecognizeReceipt();
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                await _dialogService.DisplayAlert("Błąd rozpoznawania paragonu", ex.Message, "OK");
                return;
            }
            await _dataService.AddReceiptAsync(_receipt);
            IsProcessing = false;
            await _navigationService.NavigateWithoutReturnTo(new MainPage());
        }

        private async Task RecognizeElements()
        {
            try
            {
                var bytes = await _fileService.OpenFile(_receipt.PicturePath);
                var base64 = Convert.ToBase64String(bytes);
                var googleResponse = await _requestService.GetRecognizedWords(base64);
                var words = WordProcessor.ConvertGoogleResponse(googleResponse);
                _receipt.GoogleResponse = JsonConvert.SerializeObject(words);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Brak dostępu do internetu!", ex.Message, "OK");
            }
        }

        private void RecognizeReceipt()
        {
            var words = JsonConvert.DeserializeObject<List<Word>>(_receipt.GoogleResponse);

            var dateRecognizer = new DateRecognizer(words);
            _receipt.SaleDate = dateRecognizer.GetSaleDate();

            var totalAmountRecognizer = new TotalAmountRecognizer(words);
            _receipt.BruttoSummary = totalAmountRecognizer.GetTotalAmount();

            var companyRecognizer = new CompanyRecognizer(words, _receipt.Company);
            _receipt.Company = companyRecognizer.GetRecognizedCompany();
        }
    }
}
