using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private IDataService _dataService;
        public ObservableCollection<Receipt> Receipts { get; }

        public ICommand SelectImageSource => new Command(async () => await _navigationService.NavigateTo(new SelectImageSourcePage()));

        private Receipt _selectedReceipt;
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
                _selectedReceipt = null;
            }
        }

        public MainViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            Receipts = new ObservableCollection<Receipt>(_dataService.GetAllReceipts());
        }

        private void ReceiptSelected(Receipt receipt)
        {
            _navigationService.NavigateTo(new ReceiptDetailsPage(), receipt);
        }
    }
}
