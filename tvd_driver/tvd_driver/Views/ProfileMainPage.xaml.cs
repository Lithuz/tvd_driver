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

        private static ProfileMainPage instance;

        public static ProfileMainPage Getinstance()
        {
            if (instance == null)
            {
                instance = new ProfileMainPage();
            }
            return instance;
        }

        public ObservableCollection<ProfileMainPageMenuItem> menulist { get; set; }
        public MainViewModel mainModel;
        public LoginModel UserData { get; set; }
        public ProfileMainPage()
        {
            instance = this;
            InitializeComponent();
            mainModel = MainViewModel.Getinstance();
            UserData = mainModel.Usuario;
            menulist = new ObservableCollection<ProfileMainPageMenuItem>(new[]
                {
                    new ProfileMainPageMenuItem { Id = 0, Title = "Current Trip",Color="Black" },
                    new ProfileMainPageMenuItem { Id = 1, Title = "Sales Available",Color="Black" },
                    new ProfileMainPageMenuItem { Id = 2, Title = "Completed Services",Color="Black" },
                    new ProfileMainPageMenuItem { Id = 3, Title = "Total acomulated(Bonuses):",Color="Black"},
                    new ProfileMainPageMenuItem { Id = 4, Title = "Log Out",Color="Gray" }
                });
            Label prop = (Label)MasterPage.FindByName("userName");
            prop.Text = UserData.Nombre;
            Label propD = (Label)MasterPage.FindByName("memberDate");
            propD.Text = "member since: "+UserData.FechaAlta;

            MasterPage.ListView.ItemsSource = menulist;
            MasterPage.ListView.ItemSelected += ListView_ItemSelectedAsync;
        }

        private async void ListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ProfileMainPageMenuItem;
            if (item == null)
                return;
            var page = (Page)Activator.CreateInstance(item.TargetType);
            if (item.Id == 0)
            {
                var venta = MainViewModel.Getinstance().Venta;
                if (venta != null)
                {
                    page = new MainPage(venta);

                    page.Title = item.Title;

                    Detail = new NavigationPage(page);
                }
                else
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failed operation", "Theres no trip linked to current user.", "Dismiss");
                }
                IsPresented = false;
            }
            if (item.Id == 1)
            {
                mainModel.Ventas = new VentasViewModel();
                ProfileMainPage.Getinstance().Detail = new NavigationPage(new ProfileMainPageDetail());
                //page = new ProfileMainPageDetail();

                //page.Title = item.Title;
                //Detail = new NavigationPage(page);
                //mainModel.Ventas = new VentasViewModel();

                IsPresented = false;
            }
            if (item.Id == 4)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Alert!", "Do you really want to log out?", "Yes", "No");
                    if (result)
                    {
                        Settings.UserId = string.Empty;
                        Settings.UserPass = string.Empty;
                        Xamarin.Forms.Application.Current.MainPage = new LoginPage();
                    }
                });
            }
            MasterPage.ListView.SelectedItem = null;
        }
    }
}