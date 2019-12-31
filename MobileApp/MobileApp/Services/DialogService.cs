using System;
using System.Threading.Tasks;
using Acr.UserDialogs;

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

        public async Task<PromptResult> DisplaySearchAlert(string title, string message, string accept, string cancel,
            string placeholder)
        {
            return await UserDialogs.Instance.PromptAsync(message, title, accept, cancel, placeholder);
        }

        public async Task<DatePromptResult> DisplayDatePrompt(string title, string accept, string cancel)
        {
            var datePromptConfig = new DatePromptConfig
            {
                Title = title,
                CancelText = cancel,
                OkText = accept,
                SelectedDate = DateTime.Today,
                UnspecifiedDateTimeKindReplacement = DateTimeKind.Local
            };
            return await UserDialogs.Instance.DatePromptAsync(datePromptConfig);
        }
    }
}
