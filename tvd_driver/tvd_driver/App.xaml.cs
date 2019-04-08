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
        private LoginModel user;

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
                    mainModel.Ventas = new VentasViewModel();
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

        private async Task<bool> GetUserAsync(string userName, string userPass)
        {
            var response = await apiServices.LoginUser(userName, userPass);
            if(response != null)
            {
                var mainModel = MainViewModel.Getinstance();
                LoginModel login = new LoginModel()
                {
                    idEnfermero = response.idEnfermero,
                    Nombre = response.Nombre,
                    Telefono = response.Telefono,
                    Usuario = response.Usuario,
                    Correo = response.Correo,
                    Pass = response.Pass,
                    TallaFilipina = response.TallaFilipina,
                    TallaPantalon = response.TallaPantalon,
                    FechaAlta = response.FechaAlta,
                    Activo = response.Activo,
                    Estatus = response.Estatus

                };
                user = login;
                return true;
            }
            else
            {
                return false;
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
