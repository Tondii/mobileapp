using System;
using MobileApp.Services;
using SimpleInjector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    public partial class App : Application
    {
        public static Container Container { get; set; }
        public App(string dbPath)
        {
            InitializeComponent();

            var container = new Container();

            container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
            container.Register<IDataService>(() => new DataService(dbPath), Lifestyle.Transient);

            Container = container;


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
