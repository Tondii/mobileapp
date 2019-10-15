using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Services
{
    public interface INavigationService
    {
        void Init(NavigationPage page);

        Task NavigateTo(Page page);

        Task NavigateTo<TParameter>(Page page, TParameter parameter);
    }
}
