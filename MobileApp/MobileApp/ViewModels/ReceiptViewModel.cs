using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.DocumentRecognition;
using MobileApp.Navigation;
using MobileApp.Services;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class ReceiptViewModel : BaseViewModel, IParameterized<Receipt>
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;

        private Receipt _receipt;
        public Receipt Receipt
        {
            get => _receipt;
            private set
            {
                _receipt = value;
                RaisePropertyChanged();
            }
        }

        public ICommand GetRecognizedElements => new Command(async () => await RecognizeElements());

        public ReceiptViewModel()
        {
            _dataService = App.Container.GetInstance<IDataService>();
            _fileService = App.Container.GetInstance<IFileService>();
        }

        public void HandleParameter(Receipt parameter)
        {
            Receipt = parameter;
        }

        private async Task RecognizeElements()
        {
            var bytes = await _fileService.OpenImage(Receipt.PicturePath);
            var base64 = Convert.ToBase64String(bytes);
            var recognizer = new ReceiptRecognizer();
            var googleResponse = await recognizer.GetRecognizedText(base64);
            Receipt.GoogleResponse = googleResponse;
            await _dataService.UpdateReceiptAsync(Receipt);
        }
    }
}
