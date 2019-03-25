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

namespace tvd_driver.Droid
{
    [Activity(Label = "tvd_driver", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;

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

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation }, 1);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            //FirebaseApp.InitializeApp(Application.Context);
            LoadApplication(new App());

            IsPlayServicesAvailable();
#if DEBUG
            // Force refresh of the token. If we redeploy the app, no new token will be sent but the old one will
            // be invalid.
            //Task.Run(() =>
            //{
            //    // This may not be executed on the main thread.
            //    FirebaseInstanceId.Instance.DeleteInstanceId();
            //    Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
            //});
#endif
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

        // This service handles the device's registration with FCM.
        [Service]
        [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        public class MyFirebaseIIDService : FirebaseInstanceIdService
        {
            public void OnTokenRefreshAsync()
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                Console.WriteLine($"Token received: {refreshedToken}");
                SendRegistrationToServerAsync(refreshedToken);
            }

            public void SendRegistrationToServerAsync(string token)
            {

                //    try
                //    {
                //        //Formats: https://firebase.google.com/docs/cloud-messaging/concept-options
                //        //    The "notification" format will automatically displayed in the notification center if the
                //        //     app is not in the foreground.
                //        const string templateBodyFCM =
                //            "{" +
                //                "\"notification\" : {" +
                //                "\"body\" : \"$(messageParam)\"," +
                //                  "\"title\" : \"Xamarin University\"," +
                //                "\"icon\" : \"myicon\" }" +
                //            "}";

                //        var templates = new JObject();
                //        templates["genericMessage"] = new JObject
                //    {
                //        {"body", templateBodyFCM}
                //    };

                //        var client = new WindowsAzure.Messaging.NotificationHub(;
                //        var push = client.GetPush();

                //        await push.RegisterAsync(token, templates);

                //        // Push object contains installation ID afterwards.
                //        Console.WriteLine(push.InstallationId.ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //        Debugger.Break();
                //    }
            }
        }

        [Service]
        [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
        public class MyFirebaseMessagingService : FirebaseMessagingService
        {
            public override void OnMessageReceived(RemoteMessage message)
            {
                base.OnMessageReceived(message);

                Console.WriteLine("Received: " + message);

                // Android supports different message payloads. To use the code below it must be something like this (you can paste this into Azure test send window):
                // {
                //   "notification" : {
                //      "body" : "The body",
                //                 "title" : "The title",
                //                 "icon" : "myicon
                //   }
                // }
                try
                {
                    var msg = message.GetNotification().Body;
                    MessagingCenter.Send<object, string>(this, App.NotificationRecivedkey, msg);



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error extracting message: " + ex);
                }
            }
        }

        //private void InitFirebaseAuth()
        //{
        //    var options = new FirebaseOptions.Builder()
        //    .SetApplicationId("1:141302426286:android:f5adfca813015f92")
        //    .SetApiKey("AIzaSyAyFthYa3extbXIMx14XKhK7OWK0WNV-ls")
        //    .Build();



        //    if (app == null)
        //        app = FirebaseApp.InitializeApp(this, options, "TheVitaminDoctors");

        //}
    }
}