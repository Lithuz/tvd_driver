using System;
using System.Collections.Generic;
using System.Text;
using tvd_driver.Models;
using tvd_driver.Services;
using Xamarin.Forms;

namespace tvd_driver.ViewModels
{
    public class MainViewModel
    {

        private static MainViewModel instance;

        #region ViewModels    
        public LoginViewModel Login { get; set; }
        public VentasViewModel Ventas { get; set; }
        #endregion

        public LoginModel Usuario { get; set; }

        #region Consturctores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion


        public static MainViewModel Getinstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }


        public void RegisterDevice()
        {
            var register = DependencyService.Get<IRegisterDevice>();
            register.RegisterDevice();
        }
    }
}
