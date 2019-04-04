using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Models;
using tvd_driver.Services;
using tvd_driver.ViewModels;
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

            private VentasModel ventasModel;

        public LoginModel UserData { get; set; }

        public MainPage(VentasModel ventasItemViewModel)
        {
            InitializeComponent();
            ventasModel = ventasItemViewModel;
            if (ventasItemViewModel != null)
            {
                NombreCliente.Text = ventasItemViewModel.NombreCliente;
                DireccionCliente.Text = "Dirección Pendiente";
                TelefonoCliente.Text = ventasItemViewModel.TelefonoCLiente;
            }
            geolocatorService = new GeolocatorService();
            geolocatorService.Latitude = Convert.ToDouble(ventasModel.GeoLattitud);
            geolocatorService.Longitude = Convert.ToDouble(ventasModel.GeoAltitud);
            MoveMapToCurrentPosition();
        }

        void MoveMapToCurrentPosition()
        {
            geolocatorService.Latitude = Convert.ToDouble(ventasModel.GeoLattitud);
            geolocatorService.Longitude = Convert.ToDouble(ventasModel.GeoAltitud);
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
    }
}
