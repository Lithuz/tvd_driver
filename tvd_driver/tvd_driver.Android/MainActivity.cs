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
using Plugin.PushNotification;

namespace tvd_driver.Droid
{
    [Activity(Label = "The Vitamin Doctors Drive", Icon = "@drawable/TheVitaminDoctorsLogo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;
        public const string TAG = "MainActivity";

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

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation }, 1);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            //FirebaseApp.InitializeApp(Application.Context);
            LoadApplication(new App());
            PushNotificationManager.ProcessIntent(this, Intent);

            IsPlayServicesAvailable();
#if DEBUG
            //Force refresh of the token.If we redeploy the app, no new token will be sent but the old one will
            // be invalid.
            Task.Run(() =>
            {
                // This may not be executed on the main thread.
                FirebaseInstanceId.Instance.DeleteInstanceId();
                Console.WriteLine("Forced token: " + FirebaseInstanceId.Instance.Token);
            });
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



        //// This service handles the device's registration with FCM.
        //[Service]
        //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        //public class MyFirebaseIIDService : FirebaseInstanceIdService
        //{
        //    const string TAG = "MyFirebaseIIDService";
        //    NotificationHub hub;

        //    public override void OnTokenRefresh()
        //    {
        //        var refreshedToken = FirebaseInstanceId.Instance.Token;
        //        Log.Debug(TAG, "FCM token: " + refreshedToken);
        //        SendRegistrationToServer(refreshedToken);
        //    }

        //    void SendRegistrationToServer(string token)
        //    {
        //        // Register with Notification Hubs
        //        hub = new NotificationHub(Constants.NotificationHubName,
        //                                    Constants.ListenConnectionString, this);

        //        var tags = new List<string>() { };
        //        var regID = hub.Register(token, tags.ToArray()).RegistrationId;

        //        Log.Debug(TAG, $"Successful registration of ID {regID}");
        //    }
        //}

        //[Service]
        //[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
        //public class MyFirebaseMessagingService : FirebaseMessagingService
        //{
        //    const string TAG = "MyFirebaseMsgService";
        //    public override void OnMessageReceived(RemoteMessage message)
        //    {
        //        Log.Debug(TAG, "From: " + message.From);

        //        base.OnMessageReceived(message);

        //        try
        //        {
        //            var msg = string.Empty;
        //            if (message.GetNotification() != null)
        //            {
        //                //These is how most messages will be received
        //                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
        //                msg = message.GetNotification().Body;
        //            }
        //            else
        //            {
        //                //Only used for debugging payloads sent from the Azure portal
        //                msg = message.Data.Values.First();
        //            }
        //            SendNotification(msg);
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Debug(TAG, "Error getting message: " + ex);
        //        }

        //    }

        //    void SendNotification(string messageBody)
        //    {
        //        var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
        //        //Create an intent to show ui
        //        var uiIntent = new Intent(this, typeof(MainActivity));

        //        //Use Notification Builder
        //        NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

        //        //Create the notification
        //        //we use the pending intent, passing our ui intent over which will get called
        //        //when the notification is tapped.


        //        var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
        //                .SetSmallIcon(Resource.Drawable.TheVitaminDoctors)
        //                .SetTicker("TITULO")
        //                .SetContentTitle("Tittlwe")
        //                .SetContentText("Mensaje")
        //                //.AddAction(new NotificationCompat.Action())


        //                //Set the notification sound
        //                .SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Notification))

        //                //Auto cancel will remove the notification once the user touches it
        //                .SetAutoCancel(true).Build();

        //        //Show the notification
        //        notificationManager.Notify(1, notification);

        //        //MessagingCenter.Send<object, string>(this, App.NotificationRecivedkey, messageBody);
        //        //var mainActivity = MainActivity.Getinstance();
        //        //mainActivity.RunOnUiThread(() =>
        //        //{
        //        //    AlertDialog.Builder dlg = new AlertDialog.Builder(mainActivity);
        //        //    AlertDialog alert = dlg.Create();
        //        //    alert.SetTitle("The Vitamin Doctor");
        //        //    alert.SetButton("Acept", delegate { alert.Dismiss(); });
        //        //    alert.SetIcon(Resource.Drawable.notification_template_icon_low_bg);
        //        //    alert.SetMessage(messageBody);
        //        //    alert.Show();
        //        //});

        //        //var intent = new Intent(this, typeof(MainActivity));
        //        //intent.AddFlags(ActivityFlags.ClearTop);
        //        //var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

        //        //var notificationBuilder = new Notification.Builder(this)
        //        //            .SetContentTitle("FCM Message")
        //        //            .SetSmallIcon(Resource.Drawable.ic_launcher)
        //        //            .SetContentText(messageBody)
        //        //            .SetAutoCancel(true)
        //        //            .SetContentIntent(pendingIntent);

        //        //var notificationManager = NotificationManager.FromContext(this);

        //        //notificationManager.Notify(0, notificationBuilder.Build());
            //}
        //}
    }
}