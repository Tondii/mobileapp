using System.Threading.Tasks;
using MobileApp.Navigation;
using Xamarin.Forms;

namespace MobileApp.Services
{
    class NavigationService : INavigationService
    {
        private NavigationPage _rootPage;

        public void Init(NavigationPage page)
        {
            _rootPage = page;
        }

        public async Task NavigateWithoutReturnTo(Page page)
        {
            NavigationPage.SetHasBackButton(page, false);
            await _rootPage.PushAsync(page);
        }

        public async Task NavigateTo(Page page)
        {
            await _rootPage.PushAsync(page);
        }

        public async Task BackToPreviousPage()
        {
            await _rootPage.PopAsync();
        }

        public async Task NavigateTo<TParameter>(Page page, TParameter parameter)
        {
            if (page.BindingContext != null && page.BindingContext is IParameterized<TParameter> p)
            {
                p.HandleParameter(parameter);
            }

            await _rootPage.PushAsync(page);
        }
    }
}
