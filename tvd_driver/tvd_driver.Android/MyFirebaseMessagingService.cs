using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Util;
using Firebase.Messaging;
using Android.OS;
using Android.Support.V4.App;
using Build = Android.OS.Build;
using Android.Content;
using Android.Graphics;

namespace tvd_driver.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var mainModel = ViewModels.MainViewModel.Getinstance();
            string Message = string.Empty;
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                Message = message.GetNotification().Body;
                Log.Debug(TAG, "Notification Message Body: " + Message);

                if (Message == "UpdateVentas")
                {
                    mainModel.Ventas.LoadVentas();
                    return;
                }
                SendNotification(Message);
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                Message = message.Data.Values.First();
                if (Message == "UpdateVentas")
                {
                    mainModel.Ventas.LoadVentas();
                    return;
                }
                SendNotification(Message);

            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this)
                        .SetContentTitle("The Vitamin Doctors Drive")
                        .SetSmallIcon(Resource.Drawable.ic_launcher)
                        .SetContentText(messageBody)
                        .SetTicker(messageBody)
                        .SetAutoCancel(true)
                        .SetContentIntent(pendingIntent);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                notificationBuilder.SetChannelId(Constants.CHANNEL_ID);
            }

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}