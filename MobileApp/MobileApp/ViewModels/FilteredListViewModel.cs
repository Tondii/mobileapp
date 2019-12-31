using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    internal class FilteredListViewModel : MainViewModel, IParameterized<ObservableCollection<Receipt>>
    {
        public new ICommand SearchReceipts => new Command(async () => await SearchReceiptsAsync());
        public new ICommand FilterReceipts => new Command(async () => await FilterReceiptsAsync());
        public FilteredListViewModel() : base(true)
        {

        }

        public void HandleParameter(ObservableCollection<Receipt> parameter)
        {
            Receipts = parameter;
        }

        protected new async Task SearchReceiptsAsync()
        {
            await _navigationService.BackToPreviousPage();
            await base.SearchReceiptsAsync();
        }
    }
}
