using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace MobileApp.Services
{
    class DialogService : IDialogService
    {
        public async Task<bool> DisplayAgreementAlert(string title, string message, string accept, string cancel)
        {
            return await UserDialogs.Instance.ConfirmAsync(message, title, accept, cancel);
        }

        public async Task DisplayAlert(string title, string message, string buttonText)
        {
            await UserDialogs.Instance.AlertAsync(message, title, buttonText);
        }

        public async Task<string> DisplaySearchAlert(string title, string message, string accept, string cancel,
            string placeholder)
        {
            return (await UserDialogs.Instance.PromptAsync(message, title, accept, cancel, placeholder)).Text;
        }
    }
}
