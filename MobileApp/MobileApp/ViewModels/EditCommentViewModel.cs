using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Database.DTO;
using MobileApp.Navigation;
using MobileApp.Services;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class EditCommentViewModel : BaseViewModel, IParameterized<Receipt>
    {
        private Receipt _receipt;
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public Receipt Receipt
        {
            get => _receipt;
            set
            {
                _receipt = value;
                RaisePropertyChanged();
            }
        }
        public ICommand EditComment => new Command(async () => await EditCommentAsync());

        public EditCommentViewModel()
        {
            _navigationService = App.Container.GetInstance<INavigationService>();
            _dataService = App.Container.GetInstance<IDataService>();
        }

        public void HandleParameter(Receipt parameter)
        {
            Receipt = parameter;
        }

        private async Task EditCommentAsync()
        {
            await _dataService.UpdateReceiptAsync(Receipt);
            MessagingCenter.Send(this, "editedComment");
            await _navigationService.BackToPreviousPage();
        }


    }
}
