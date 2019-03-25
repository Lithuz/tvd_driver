﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            ActivitySpinner.IsVisible = true;
            ApiServices apiServices = new ApiServices();
            var response = await apiServices.LoginUser(ntyUsername.Text, ntyPassword.Text);
            if (response == null)
            {
                await DisplayAlert("Alert", "Wrong User or Password", "Cancel");
            }
            else
            {
                RegisterDevice();
                Navigation.InsertPageBefore(new ProfileMainPage(response), this);
                await Navigation.PopAsync();
            }
            ActivitySpinner.IsVisible = false;
        }

        public void RegisterDevice()
        {
            var register = DependencyService.Get<IRegisterDevice>();
            register.RegisterDevice();
        }
    }
}