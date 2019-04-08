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

        public ApiServices apiServices = new ApiServices();

        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.UserId) || string.IsNullOrEmpty(Settings.UserPass))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var response = apiServices.LoginUserSync(Settings.UserId, Settings.UserPass);
                if (response != null)
                {
                    var mainModel = MainViewModel.Getinstance();
                    var register = DependencyService.Get<IRegisterDevice>();
                    register.RegisterDevice();
                    mainModel.Usuario = (LoginModel)response;
                    if(mainModel.Usuario.Estatus == 2)
                    {
                        mainModel.Ventas = new VentasViewModel();
                        mainModel.Venta = apiServices.GetVentaNosync(mainModel.Usuario.idEnfermero);
                        mainModel.relVentasPdcto = apiServices.RelVentasProdcuctoNoSync(mainModel.Venta.NumeroOrden);
                    }
                    else
                    {
                        mainModel.Ventas = new VentasViewModel();
                    }
                    MainPage = new NavigationPage(new ProfileMainPage());
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Login Error", "Theres an error geting the requested user, please, login again.", "Dismiss");
                    Settings.UserId = string.Empty;
                    Settings.UserPass = string.Empty;
                    MainPage = new NavigationPage(new LoginPage());
                    
                }
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
