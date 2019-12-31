using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class ReceiptViewModel : BaseViewModel, IParameterized<Receipt>
    {
        private readonly IDataService _dataService;
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
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
        public ICommand RemoveReceipt => new Command(async () => await RemoveThisReceipt());
        public ICommand EditComment => new Command(async () => await EditCommentAsync());

        public ReceiptViewModel()
        {
            _dataService = App.Container.GetInstance<IDataService>();
            _fileService = App.Container.GetInstance<IFileService>();
            _dialogService = App.Container.GetInstance<IDialogService>();
            _navigationService = App.Container.GetInstance<INavigationService>();
        }

        public void HandleParameter(Receipt parameter)
        {
            Receipt = parameter;
        }

        private async Task RemoveThisReceipt()
        {
            if (!await _dialogService.DisplayAgreementAlert("Usunąć?", "Czy na pewno chcesz usunąć ten paragon?", "Tak",
                "Nie"))
            {
                return;
            }

            await _fileService.DeleteFile(Receipt.PicturePath);
            await _dataService.DeleteReceiptAsync(Receipt.Id);
            await _navigationService.NavigateWithoutReturnTo(new MainPage());
        }

        private async Task EditCommentAsync()
        {
            await _navigationService.NavigateTo(new EditCommentPage(), Receipt);
            MessagingCenter.Subscribe<EditCommentViewModel>(this, "editedComment", model =>
            {
                Receipt = _dataService.GetReceipt(model.Receipt.Id);
                MessagingCenter.Unsubscribe<EditCommentViewModel>(this, "editedComment");
            });
        }
    }
}
