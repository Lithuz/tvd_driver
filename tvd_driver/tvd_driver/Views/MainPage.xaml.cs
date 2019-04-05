using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainPage(VentasModel ventasItemViewModel)
        {
            InitializeComponent();
            apiServices = new ApiServices();
            mainViewModel = MainViewModel.Getinstance();

            for (int c = 0; c < mainViewModel.relVentasPdcto.Count; c++)
            {
                PickerData.Items.Add(mainViewModel.relVentasPdcto[c].ProductoNombre + "x " + mainViewModel.relVentasPdcto[c].ProductoCantidad);
            }
            geolocatorService = new GeolocatorService();
            NombreCliente.Text = mainViewModel.Venta.NombreCliente;
            DireccionCliente.Text = mainViewModel.Venta.Direccion;
            EmailCliente.Text = mainViewModel.Venta.Correo;
            TelefonoCliente.Text = mainViewModel.Venta.TelefonoCLiente;

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
                        if (unlinkResult)
                        {
                            await Application.Current.MainPage.Navigation.PopAsync();
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
                    if (statusResult)
                    {
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                }
            });
        }
    }
}
