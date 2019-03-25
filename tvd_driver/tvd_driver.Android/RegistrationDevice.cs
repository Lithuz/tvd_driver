using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using tvd_driver.Services;
using Xamarin.Forms;
using Xamarin.Android;
using Gcm.Client;
using Android.Util;

[assembly: Dependency(typeof(tvd_driver.Droid.RegistrationDevice))]
namespace tvd_driver.Droid
{
    public class RegistrationDevice : IRegisterDevice
    {
        public void RegisterDevice()
        {
            var mainActivity = MainActivity.Getinstance();
            GcmClient.CheckDevice(mainActivity);
            GcmClient.CheckManifest(mainActivity);

            Log.Info("MainActivity", "Registering...");
            GcmClient.Register(mainActivity, Constants.SenderID);
            var regis = GcmClient.IsRegistered(mainActivity);
            var edos = GcmClient.IsRegisteredOnServer(mainActivity);


        }
    }
}