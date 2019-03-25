using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tvd_driver.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace tvd_driver
{
    public partial class App : Application
    {

        public static double ScreenHeight;
        public static double ScreenWidth;

        public const string NotificationRecivedkey = "NotificationRecived";
        public const string MobileServiceUrl = "http://tvddriverapi.azurewebsites.net";

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=d22f90dd-9587-4990-b120-d4ca3b5f04c2;" + "uwp={Your UWP App secret here};" + "ios={Your iOS App secret here}", typeof(Analytics), typeof(Crashes), typeof(Push));
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
