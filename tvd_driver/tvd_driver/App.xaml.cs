using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tvd_driver.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace tvd_driver
{
    using Views;
    using Helpers;
    using Services;
    using tvd_driver.Models;
    using System.Threading.Tasks;
    using tvd_driver.ViewModels;

    public partial class App : Application
    {

        public static double ScreenHeight;
        public static double ScreenWidth;

        public const string NotificationRecivedkey = "NotificationRecived";
        public const string MobileServiceUrl = "http://tvddriverapi.azurewebsites.net";

        public LoginModel User = new LoginModel();

        public App()
        {
            InitializeComponent();
            User = null;

            MainPage = new NavigationPage(new LoginPage());
            //if (string.IsNullOrEmpty(Settings.UserId) || string.IsNullOrEmpty(Settings.UserPass))
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}
            //else
            //{
            //    ReqData();                    
            //}
        }
       
        protected override void OnStart()
        {

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
