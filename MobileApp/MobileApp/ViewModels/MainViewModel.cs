using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        protected INavigationService _navigationService;
        protected IDialogService _dialogService;
        private Receipt _selectedReceipt;
        private ObservableCollection<Receipt> _receipts;

        public ICommand SelectImageSource =>
            new Command(async () => await _navigationService.NavigateTo(new SelectImageSourcePage()));
        public ICommand SearchReceipts => new Command(async () => await SearchReceiptsAsync());
        public ICommand FilterReceipts => new Command(async () => await FilterReceiptsAsync());

        public ObservableCollection<Receipt> Receipts
        {
            get => _receipts;
            set
            {
                _receipts = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            _navigationService = App.Container.GetInstance<INavigationService>();
            _dialogService = App.Container.GetInstance<IDialogService>();
            var dataService = App.Container.GetInstance<IDataService>();
            Receipts = new ObservableCollection<Receipt>(dataService.GetAllReceipts().OrderByDescending(x => x.CreateDateTime));
        }
        protected MainViewModel(bool onlyServices)
        {
            _navigationService = App.Container.GetInstance<INavigationService>();
            _dialogService = App.Container.GetInstance<IDialogService>();
        }

        public Receipt SelectedReceipt
        {
            get => _selectedReceipt;
            set
            {
                _selectedReceipt = value;
                RaisePropertyChanged();

                if (_selectedReceipt == null)
                {
                    return;
                }

                ReceiptSelected(_selectedReceipt);
                SelectedReceipt = null;
            }
        }
        private void ReceiptSelected(Receipt receipt)
        {
            _navigationService.NavigateTo(new ReceiptPage(), receipt);
        }

        protected async Task SearchReceiptsAsync()
        {
            var searchResult =
                await _dialogService.DisplaySearchAlert("", "Wyszukaj paragon", "OK", "Anuluj", "Wyszukaj...");
            if (!searchResult.Ok)
            {
                return;
            }

            var searchText = searchResult.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            var searchedReceipts = Receipts.Where(r => r.Company.Name.ToLower().Contains(searchText)
                                                       || r.Comment.ToLower().Contains(searchText)
                                                       || r.Company.City.ToLower().Contains(searchText)).ToList();

            if (searchedReceipts.Any())
            {
                await _navigationService.NavigateTo(new FilteredListPage(),
                    new ObservableCollection<Receipt>(searchedReceipts));
            }
        }
        protected async Task FilterReceiptsAsync()
        {
            var promptResult = await _dialogService.DisplayDatePrompt("Wybierz dzień", "OK", "Anuluj");
            if (!promptResult.Ok)
            {
                return;
            }
            var date = promptResult.SelectedDate;
            var filteredReceipts = Receipts.Where(r => r.SaleDate.GetValueOrDefault(DateTime.MinValue) == date.Date).ToList();

            if (filteredReceipts.Any())
            {
                await _navigationService.NavigateTo(new FilteredListPage(),
                    new ObservableCollection<Receipt>(filteredReceipts));
            }
        }
    }
}
