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
using Firebase.Iid;
using WindowsAzure.Messaging;

[assembly: Dependency(typeof(tvd_driver.Droid.RegistrationDevice))]
namespace tvd_driver.Droid
{
    public class RegistrationDevice : IRegisterDevice,IUnRegisterDevice
    {
        const string TAG = "MyFirebaseIIDService";
        public void RegisterDevice()
        {
            MyFirebaseIIDService ns = MyFirebaseIIDService.Getinstance();
            ns.RegisterDevice(null);
        }

        public  void UnregisterDevice()
        {
            MyFirebaseIIDService ns = MyFirebaseIIDService.Getinstance();
            ns.UnregisterReceiverAsync();
        }
    }
}