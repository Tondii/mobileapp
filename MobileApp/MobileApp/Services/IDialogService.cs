using System.Threading.Tasks;

namespace MobileApp.Services
{
    public interface IDialogService
    {
        Task DisplayAlert(string title, string message, string buttonText);
        Task<bool> DisplayAgreementAlert(string title, string message, string accept, string cancel);
    }
}
