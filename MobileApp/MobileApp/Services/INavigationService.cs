using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Services
{
    public interface INavigationService
    {
        void Init(NavigationPage page);
        Task NavigateWithoutReturnTo(Page page);
        Task NavigateTo(Page page);
        Task BackToPreviousPage();
        Task NavigateTo<TParameter>(Page page, TParameter parameter);
    }
}
