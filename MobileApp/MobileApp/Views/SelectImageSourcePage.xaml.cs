using MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectImageSourcePage : ContentPage
    {
        public SelectImageSourcePage()
        {
            InitializeComponent();
            BindingContext = App.Container.GetInstance<SelectImageSourceViewModel>();
        }
    }
}