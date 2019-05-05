using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using tvd_driver.Helpers;
using tvd_driver.Services;
using tvd_driver.ViewModels;
using Plugin.Connectivity.Abstractions;

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
                    new ProfileMainPageMenuItem { Id = 0, Title = "Current Trip",Color="Black",VOptions="Start",TextAlign="Left" },
                    new ProfileMainPageMenuItem { Id = 1, Title = "Sales Available",Color="Black",VOptions="Start",TextAlign="Left" },
                    new ProfileMainPageMenuItem { Id = 2, Title = $"Completed Services: {UserData.ViajesTotales}",Color="Black",VOptions="Start",TextAlign="Left" }
                });
            Label prop = (Label)MasterPage.FindByName("userName");
            prop.Text = UserData.Nombre;
            Label propD = (Label)MasterPage.FindByName("memberDate");
            propD.Text = "member since: " + UserData.FechaAlta;

            MasterPage.ListView.ItemsSource = menulist;
            MasterPage.ListView.ItemSelected += ListView_ItemSelectedAsync;

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            verifyTirpInstance();
        }

        private async void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                await DisplayAlert("ERROR", "Theres no connection aviable.", "got it");
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("ERROR", "Theres no connection aviable.", "got it");
            }
        }

        private void verifyTirpInstance()
        {
            if (mainModel.Venta != null && mainModel.Usuario.Estatus == 2)
            {
                string[] values = { "val1", "val2" };
                DisplayActionSheet("PRUEBA", "CANCEL", "CANCEL2", values);
            }
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

                IsPresented = false;
            }
            MasterPage.ListView.SelectedItem = null;
        }
    }
}