using System.Threading.Tasks;
using Acr.UserDialogs;

namespace MobileApp.Services
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string buttonText);
        Task<bool> DisplayAgreementAlert(string title, string message, string accept, string cancel);
        Task<PromptResult> DisplaySearchAlert(string title, string message, string accept, string cancel,
            string placeholder);
        Task<DatePromptResult> DisplayDatePrompt(string title, string accept, string cancel);
    }
}
