using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using tvd_driver.Models;
using WindowsAzure.Messaging;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECIVE")]

[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.INTERNET")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.WAKE_LOCK")]

namespace tvd_driver.Droid
{
    public class MyBroadCastReciver
    {
        public static string[] SENDER_IDS = new string[] { Constants.SenderID };

        public const string TAG = "MyBroadCastReciver-GCM";
    }

    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public NotificationHub Hub { get; set; }

        public string RegistrationID { get; set; }


        public PushHandlerService() : base(Constants.SenderID)
        {
            Log.Info(MyBroadCastReciver.TAG, "PushHandlerService() constructor");
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Verbose(MyBroadCastReciver.TAG, "GCM Error: " + errorId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(MyBroadCastReciver.TAG, "GCM Message recived");

            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            var message = intent.Extras.GetString("Message");
            var type = intent.Extras.GetString("Type");

            if (!string.IsNullOrEmpty(message))
            {
                var notification = intent.Extras.GetString("Notification");
                createNotification("The Vitamin Doctors", string.Format("{0}", message));
            }
        }
        private void createNotification(string tittle, string desc)
        {
            var notificationmanager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(MainActivity));

            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, tittle);

            notification.Flags = NotificationFlags.AutoCancel;

            notification.SetLatestEventInfo(this, tittle, desc, PendingIntent.GetActivity(this, 0, uiIntent,0));

            notificationmanager.Notify(1, notification);
            dialogNotify(tittle, desc);
        }

        private void dialogNotify(string tittle, string desc)
        {
            var mainActivity = MainActivity.Getinstance();
            mainActivity.RunOnUiThread(()=> {
                AlertDialog.Builder dlg = new AlertDialog.Builder(mainActivity);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(tittle);
                alert.SetButton("Acept", delegate { alert.Dismiss(); });
                alert.SetIcon(Resource.Drawable.notification_template_icon_low_bg);
                alert.SetMessage(desc);
                alert.Show();
            });
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadCastReciver.TAG, "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            Hub = new NotificationHub(Constants.ListenConnectionString, Constants.NotificationHubName, context);

            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadCastReciver.TAG, ex.Message);
            }

            var tags = new List<string>() { };

            var mainModel = LoginModel.GetInstance();
            if (mainModel.Usuario != null)
            {
                var userId = mainModel.idEnfermero;
                tags.Add("userId:" + userId);
            }

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadCastReciver.TAG, ex.Message);
                throw;
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadCastReciver.TAG, "GCM unregistered: " + registrationId);
        }
    }
}