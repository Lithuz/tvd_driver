using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tvd_driver.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace tvd_driver
{
    public partial class App : Application
    {

        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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
