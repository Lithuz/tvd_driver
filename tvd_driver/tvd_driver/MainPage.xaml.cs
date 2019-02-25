using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace tvd_driver
{
    public partial class MainPage : ContentPage
    {
        GeolocatorService geolocatorService;

        public MainPage()
        {
            InitializeComponent();
            geolocatorService = new GeolocatorService();
            MoveMapToCurrentPosition();
        }

        async void MoveMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();
            if(geolocatorService.Latitude !=0 || geolocatorService.Longitude != 0)
            {
                var position = new Position(geolocatorService.Latitude, geolocatorService.Longitude);
                MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(.5)));
            }
        }

        private void BtnAlert_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:+5216671540679"));
        }
    }
}
