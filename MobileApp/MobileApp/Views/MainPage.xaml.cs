using System;
using System.ComponentModel;
using MobileApp.Services;
using MobileApp.ViewModels;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Container.GetInstance<MainViewModel>();
        }
    }
}
