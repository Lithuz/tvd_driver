using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Forms;
using Android.Media;
using Android.Support.V4.App;
using Android.OS;

namespace tvd_driver.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        //const string TAG = "FirebaseNotificationService";



        //public override void OnMessageReceived(RemoteMessage message)
        //{

        //    CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
        //    {

        //        System.Diagnostics.Debug.WriteLine("Received");

        //    };

        //    Log.Debug(TAG, "From: " + message.From);

        //    // Pull message body out of the template
        //    var messageBody = message.Data["message"];
        //    if (string.IsNullOrWhiteSpace(messageBody))
        //        return;

        //    Log.Debug(TAG, "Notification message body: " + messageBody);
        //    SendNotification(messageBody);
        //}

        //void SendNotification(string messageBody)
        //{
        //    var intent = new Intent(this, typeof(MainActivity));
        //    intent.AddFlags(ActivityFlags.ClearTop);
        //    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

        //    Notification notify = new Notification();
        //    notify.Defaults = NotificationDefaults.Sound;
        //    notify.Defaults = NotificationDefaults.Vibrate;

        //    NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this)
        //         .SetSmallIcon(Resource.Drawable.TheVitaminDoctorsLogo)
        //         .SetContentTitle("The VItamin Doctors")
        //         .SetContentText(messageBody)
        //         .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
        //         .SetVibrate(new long[] { 1000, 1000 })
        //         .SetLights(12314, 3000, 3000)
        //         .SetAutoCancel(true)
        //         .SetContentIntent(pendingIntent);

        //    NotificationChannel channel = new NotificationChannel("FirebasePushNotificationChannel", "The VItamin Doctors", NotificationImportance.Default);

        //    var notificationManager = NotificationManager.FromContext(this);
        //    notificationManager.CreateNotificationChannel(channel);
        //    notificationManager.Notify(100, notificationBuilder.Build());
        //}

        //const string TAG = "MyFirebaseMsgService";
        //public override void OnMessageReceived(RemoteMessage message)
        //{
        //    Log.Debug(TAG, "From: " + message.From);
        //    if (message.GetNotification() != null)
        //    {
        //        //These is how most messages will be received
        //        Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
        //        SendNotification(message.GetNotification().Body);
        //    }
        //    else
        //    {
        //        //Only used for debugging payloads sent from the Azure portal
        //        SendNotification(message.Data.Values.First());

        //    }
        //}

        //void SendNotification(string messageBody)
        //{
        //    MessagingCenter.Send<object, string>(this, App.NotificationRecivedkey, messageBody);

        //    //var intent = new Intent(this, typeof(MainActivity));
        //    //intent.AddFlags(ActivityFlags.ClearTop);
        //    //var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

        //    //var notificationBuilder = new Notification.Builder(this)
        //    //            .SetContentTitle("FCM Message")
        //    //            .SetSmallIcon(Resource.Drawable.ic_launcher)
        //    //            .SetContentText(messageBody)
        //    //            .SetAutoCancel(true)
        //    //            .SetContentIntent(pendingIntent);

        //    //var notificationManager = NotificationManager.FromContext(this);

        //    //notificationManager.Notify(0, notificationBuilder.Build());
        //}
    }
}