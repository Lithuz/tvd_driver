using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using tvd_driver.Services;
using Xamarin.Forms;
using Plugin.Settings;
using tvd_driver.Helpers;

namespace tvd_driver.ViewModels
{
    public class LoginViewModel
    {

        private ApiServices apiServices;
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool IsBussy { get; set; }
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }


        public LoginViewModel()
        {
            this.apiServices = new ApiServices();
            this.Email = string.Empty;
            this.IsBussy = false;
        }

        private  async void Login()
        {

            if(string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Error",
                    "You must enter an eMail",
                    "Dismiss");
            }
            if (string.IsNullOrEmpty(PassWord))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Error",
                    "You must enter an Password",
                    "Dismiss");
            }
            var response = await apiServices.LoginUser(Email, PassWord);

            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login Error", "Wrong User or Password", "Dismiss");
            }
            else
            {
                var mainModel = MainViewModel.Getinstance();
                //Settings.UserId = Email;
                //Settings.UserPass = PassWord;
                var register = DependencyService.Get<IRegisterDevice>();
                register.RegisterDevice();
                mainModel.Usuario = response;
                mainModel.Ventas = new VentasViewModel();
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new Views.ProfileMainPage());
            }
        }
    }
}
