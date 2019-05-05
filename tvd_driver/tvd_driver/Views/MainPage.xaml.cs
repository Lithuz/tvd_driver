using GalaSoft.MvvmLight.Command;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tvd_driver.Models;
using tvd_driver.Services;
using tvd_driver.ViewModels;
using tvd_driver.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace tvd_driver
{
    public partial class MainPage : ContentPage
    {
        GeolocatorService geolocatorService;
        //public string NombreCliente { get; set; }
        //public string Direccion { get; set; }
        //public string Telefono { get; set; }

        private ApiServices apiServices;
        public LoginModel UserData { get; set; }
        private MainViewModel mainViewModel;
        private string ProductList = string.Empty;

        public MainPage(VentasItemViewModel ventasItemViewModel)
        {
            InitializeComponent();
            apiServices = new ApiServices();
            mainViewModel = MainViewModel.Getinstance();

            for (int c = 0; c < mainViewModel.relVentasPdcto.Count; c++)
            {
                ProductList = ProductList + mainViewModel.relVentasPdcto[c].ProductoNombre.ToUpper() + " - " + mainViewModel.relVentasPdcto[c].ProductoCantidad + "\n";
            }
            geolocatorService = new GeolocatorService();
            NombreCliente.Text = "Client: " + mainViewModel.Venta.NombreCliente.ToUpper();
            DireccionCliente.Text = "Address: " + mainViewModel.Venta.Direccion;
            EmailCliente.Text = "eMail: " + mainViewModel.Venta.Correo;
            TelefonoCliente.Text = "Contact phone: " + mainViewModel.Venta.TelefonoCLiente;

            MoveMapToCurrentPosition();
        }

        void MoveMapToCurrentPosition()
        {
            geolocatorService.Latitude = Convert.ToDouble(mainViewModel.Venta.GeoLattitud);
            geolocatorService.Longitude = Convert.ToDouble(mainViewModel.Venta.GeoAltitud);
            if (geolocatorService.Latitude != 0 || geolocatorService.Longitude != 0)
            {
                var position = new Position(geolocatorService.Latitude, geolocatorService.Longitude);
                MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(.5)));
                var Destino = new Pin();
                Destino.Label = "Client Location";
                Destino.Position = position;
                MainMap.Pins.Add(Destino);
            }
        }

        private void BtnAlert_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:6644848589"));
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to cancel current trip??", "Yes", "No");
                if (result)
                {
                    var statusResult = await apiServices.SetStatusAsync(MainViewModel.Getinstance().Usuario.idEnfermero, 1);
                    if (statusResult)
                    {
                        var unlinkResult = await apiServices.LinkVentaEnfermero(mainViewModel.Venta.idVenta, 0, false);
                        var saleResult = await apiServices.ChangeSaleStatus(MainViewModel.Getinstance().Venta.idVenta, false);
                        if (unlinkResult && saleResult)
                        {
                            mainViewModel.Venta = null;
                            mainViewModel.Ventas = new VentasViewModel();
                            ProfileMainPage.Getinstance().Detail = new NavigationPage(new ProfileMainPageDetail());
                        }
                    }
                }
            });
        }

        private void BtnEndDelivery_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to finish current trip??", "Yes", "No");
                if (result)
                {
                    var statusResult = await apiServices.SetStatusAsync(MainViewModel.Getinstance().Usuario.idEnfermero, 1);
                    var saleResult = await apiServices.ChangeSaleStatus(MainViewModel.Getinstance().Venta.idVenta, true);
                    if (statusResult && saleResult)
                    {
                        mainViewModel.Venta = null;
                        mainViewModel.Ventas = new VentasViewModel();
                        ProfileMainPage.Getinstance().Detail = new NavigationPage(new ProfileMainPageDetail());

                        var tripsCount = apiServices.GetTotales4Loggeduser(MainViewModel.Getinstance().Usuario);
                        var instance = ProfileMainPageMaster.GetInstance();
                        ObservableCollection<ProfileMainPageMenuItem> ListElements = instance.ListView.ItemsSource as ObservableCollection<ProfileMainPageMenuItem>;
                        ListElements[2].Title = $"Completed Services: {tripsCount}";
                    }
                }
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            await Application.Current.MainPage.DisplayAlert("Product List", ProductList, "Dismiss");
        }
        private async void Disclaimer_Clicked(object sender, EventArgs e)
        {

            await Application.Current.MainPage.Navigation.PushModalAsync(new DisclaimerPage());
        }
    }
}
