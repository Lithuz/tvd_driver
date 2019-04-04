using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android;
using Firebase.Auth;
using Firebase;
using Android.Gms.Common;
using Firebase.Iid;
using Firebase.Messaging;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Android.Util;
using WindowsAzure.Messaging;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.LocalNotifications;

namespace tvd_driver.Droid
{
    [Activity(Label = "The Vitamin Doctors Drive", Icon = "@drawable/TheVitaminDoctorsLogo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string TAG = "MainActivity";
        internal static readonly string COUNT_KEY = "count";

        #region Singleton
        private static MainActivity instance;

        public static MainActivity Getinstance()
        {
            if (instance == null)
            {
                instance = new MainActivity();
            }
            return instance;
        }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CreateNotificationChannel();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation }, 1);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            //FirebaseApp.InitializeApp(Application.Context);
            LoadApplication(new App());
            IsPlayServicesAvailable();
//#if DEBUG
//            //Force refresh of the token.If we redeploy the app, no new token will be sent but the old one will be invalid.
//            Task.Run(() =>
//            {
//                // This may not be executed on the main thread.
//                FirebaseInstanceId.Instance.DeleteInstanceId();
//                //Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
//            });
//#endif
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = "The Vitamin Doctors Notification Channel";
            var description = "Travel Notifications";
            var channel = new NotificationChannel(Constants.CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    // In a real project you can give the user a chance to fix the issue.
                    Console.WriteLine($"Error: {GoogleApiAvailability.Instance.GetErrorString(resultCode)}");
                }
                else
                {
                    Console.WriteLine("Error: Play services not supported!");
                    Finish();
                }
                return false;
            }
            else
            {
                Console.WriteLine("Play Services available.");
                return true;
            }
        }     
    }
}