using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using tvd_driver.Helpers;
using tvd_driver.Services;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMainPageMaster : ContentPage
    {
        public ListView ListView;
        private static ProfileMainPageMaster instance;
        #region Singleton
        public static ProfileMainPageMaster GetInstance()
        {
            if (instance == null)
            {
                instance = new ProfileMainPageMaster();
            }
            return instance;
        }

        #endregion

        public ProfileMainPageMaster()
        {
            instance = this;
            InitializeComponent();
            ListView = MenuItemsListView;
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Be Careful", "Do you really want to log out? \n\nYou will unable to recive notification on every new trip.", "Yes", "No");
                if (result)
                {
                    Settings.UserId = string.Empty;
                    Settings.UserPass = string.Empty;
                    DependencyService.Get<IUnRegisterDevice>().UnregisterDevice();
                    Application.Current.MainPage = new LoginPage();
                }
            });
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.DisplayAlert("The Vitamin Doctors Drive", 
                    "This is an app, part of the family of intravenous therapy apps \"The Vitamin Doctors\"." +
                    "\n\nMore in thevitamindoctors.com.mx\n\nver 1.1(Beta)", "Ok");
            });
        }
    }
}