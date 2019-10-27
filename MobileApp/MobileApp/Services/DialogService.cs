using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Services
{
    class DialogService : IDialogService
    {
        public async Task<bool> DisplayAgreementAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task DisplayAlert(string title, string message, string buttonText)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
        }

    }
}
