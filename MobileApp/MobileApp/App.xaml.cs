using MobileApp.Services;
using MobileApp.Views;
using SimpleInjector;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class App : Application
    {
        public static Container Container { get; set; }
        public App(string dbPath)
        {
            InitializeComponent();

            var container = new Container();

            container.Register<IRequestService, RequestService>();
            container.Register<IPermissionService, PermissionService>();
            container.Register<INavigationService, NavigationService>(Lifestyle.Singleton);
            container.Register<IDataService>(() => new DataService(dbPath));
            container.Register<IDialogService, DialogService>();
            container.Register<ICameraService, CameraService>();
            container.Register<IFileService, FileService>();
            container.Register<IReceiptService, ReceiptService>();

            Container = container;

            var initialPage = new NavigationPage(new MainPage());
            var navigationService = container.GetInstance<INavigationService>();
            navigationService.Init(initialPage);

            MainPage = initialPage;
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
