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
    public class LoginViewModel : BaseViewModel
    {

        private ApiServices apiServices;
        private string email;
        private string passWord;
        private bool isRefreshing;
        private bool isRemembered;
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }


        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsRemembered
        {
            get { return this.isRemembered; }
            set { SetValue(ref this.isRemembered, value); }
        }


        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PassWord
        {
            get { return this.passWord; }
            set { SetValue(ref this.passWord, value); }
        }


        public LoginViewModel()
        {
            Email = string.Empty;
            PassWord = string.Empty;
            IsRemembered = true;
            this.apiServices = new ApiServices();
            this.Email = string.Empty;
        }

        private async void Login()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PassWord))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Error",
                    "You must enter an eMail and Password",
                    "Dismiss");
            }
            else
            {
                var response = await apiServices.LoginUser(Email, PassWord);

                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Login Error", "Wrong User or Password", "Dismiss");
                    PassWord = string.Empty;
                }
                else
                {
                    if (IsRemembered)
                    {
                        Settings.UserId = Email;
                        Settings.UserPass = PassWord;
                    }
                    var mainModel = MainViewModel.Getinstance();
                    var register = DependencyService.Get<IRegisterDevice>();
                    register.RegisterDevice();
                    mainModel.Usuario = response;
                    mainModel.Ventas = new VentasViewModel();
                    Application.Current.MainPage = new Views.ProfileMainPage();
                }
            }
            IsRefreshing = false;
        }
    }
}
