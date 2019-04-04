using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using Plugin.Settings;
using tvd_driver.Helpers;
using tvd_driver.Services;
using tvd_driver.ViewModels;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMainPage : MasterDetailPage
    {
        public ObservableCollection<ProfileMainPageMenuItem> menulist { get; set; }
        public LoginModel UserData { get; set; }
        public ProfileMainPage()
        {

            InitializeComponent();
            var mainModel = MainViewModel.Getinstance();
            UserData = mainModel.Usuario;
            menulist = new ObservableCollection<ProfileMainPageMenuItem>(new[]
                {
                    new ProfileMainPageMenuItem { Id = 0, Title = $"Status de Viaje" },
                    new ProfileMainPageMenuItem { Id = 1, Title = (UserData.Estatus == 1)?"Disponible":"En Viaje" },
                    new ProfileMainPageMenuItem { Id = 2, Title = $"Miembro desde {UserData.FechaAlta}" },
                    new ProfileMainPageMenuItem { Id = 3, Title = "Viajes Acomulados:" },
                    new ProfileMainPageMenuItem { Id = 4, Title = "Total acomulado:" }
                }); ;

            Label prop = (Label)MasterPage.FindByName("userName");
            prop.Text = UserData.Nombre;

            MasterPage.ListView.ItemsSource = menulist;
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, string>(this, App.NotificationRecivedkey, OnMessageRecived);
        }

        private void OnMessageRecived(object sender, string msg)
        {
            CrossLocalNotifications.Current.Show("Te Vitamin Doctors Driver", msg, 0);

            //CrossPushNotification.Current.RegisterForPushNotifications();

            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    var sd = msg;
            //});
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ProfileMainPageMenuItem;
            if (item == null)
                return;
            var page = (Page)Activator.CreateInstance(item.TargetType);
            if (item.Id == 0)
            {
                page = new MainPage(null);

                page.Title = item.Title;

                Detail = new NavigationPage(page);
            }
            MasterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}