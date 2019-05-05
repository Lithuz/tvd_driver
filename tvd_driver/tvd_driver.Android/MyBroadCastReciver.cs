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
using Android.Support.V4.App;
using Android.Graphics;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECIVE")]

[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.INTERNET")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.WAKE_LOCK")]

namespace tvd_driver.Droid
{
    //[BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    //[IntentFilter(new string[]{Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE},Categories =new string[] {"@PACKAGE_NAME@"})]
    //[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    //[IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]

    //public class MyBroadCastReciver:GcmBroadcastReceiverBase<PushHandlerService>
    //{
    //    public static string[] SENDER_IDS = new string[] { Constants.SenderID };

    //    public const string TAG = "MyBroadCastReciver-GCM";
    //}

    //[Service]
    //public class PushHandlerService : GcmServiceBase
    //{
    //    public NotificationHub Hub { get; set; }
    //    public string RegistrationID { get; set; }


    //    public PushHandlerService() : base(Constants.SenderID)
    //    {
    //        Log.Info(MyBroadCastReciver.TAG, "PushHandlerService() constructor");
    //    }

    //    protected override void OnMessage(Context context, Intent intent)
    //    {
    //        Log.Info(MyBroadCastReciver.TAG, "GCM Message recived");

    //        var msg = new StringBuilder();

    //        if (intent != null && intent.Extras != null)
    //        {
    //            foreach (var key in intent.Extras.KeySet())
    //                msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
    //        }

    //        var message = intent.Extras.GetString("message");
    //        var type = intent.Extras.GetString("Type");

    //        if (!string.IsNullOrEmpty(message))
    //        {
    //            var notification = intent.Extras.GetString("Notification");
    //            createNotification("The Vitamin Doctors", message);
    //        }
    //    }

    //    protected override bool OnRecoverableError(Context context, string errorId)
    //    {
    //        Log.Warn(MyBroadCastReciver.TAG, "Recoverable Error" + errorId);
    //        return base.OnRecoverableError(context, errorId);
    //    }

    //    protected override void OnRegistered(Context context, string registrationId)
    //    {
    //        Log.Verbose(MyBroadCastReciver.TAG, "GCM Registered: " + registrationId);
    //        RegistrationID = registrationId;

    //        Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString, context);

    //        try
    //        {
    //            Hub.UnregisterAll(registrationId);
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error(MyBroadCastReciver.TAG, ex.Message);
    //        }

    //        var tags = new List<string>() { };

    //        var mainModel = ViewModels.MainViewModel.Getinstance();
    //        if (mainModel.Usuario != null)
    //        {
    //            var userId = mainModel.Usuario.idEnfermero;
    //            tags.Add("userId:" + userId);
    //        }

    //        try
    //        {
    //            var hubRegistration = Hub.Register(registrationId, tags.ToArray());
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error(MyBroadCastReciver.TAG, ex.Message);
    //        }
    //    }

    //    protected override void OnUnRegistered(Context context, string registrationId)
    //    {
    //        Log.Verbose(MyBroadCastReciver.TAG, "GCM unregistered: " + registrationId);
    //        createNotification("The VItamin Doctors", "The Devices has been unregistered!");
    //    }

    //    private void createNotification(string tittle, string desc)
    //    {
    //        dialogNotify(tittle, desc);
    //    }

       

    //    private void dialogNotify(string tittle, string desc)
    //    {
    //        var resultIntent = new Intent(this, typeof(MainActivity));

    //        // Construct a back stack for cross-task navigation:
    //        var stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
    //        stackBuilder.AddNextIntent(resultIntent);

    //        // Create the PendingIntent with the back stack:
    //        var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

    //        // Build the notification:
    //        var builder = new NotificationCompat.Builder(this, Constants.CHANNEL_ID)
    //                      .SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Notification))
    //                      .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
    //                      .SetLights(Color.Purple, 3000, 3000)
    //                      .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
    //                      .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
    //                      .SetContentTitle(tittle) // Set the title
    //                      //.SetNumber(12) // Display the count in the Content Info    
    //                      .SetSmallIcon(Resource.Drawable.ic_launcher) // This is the icon to display
    //                      .SetContentText(desc); // the message to display.



    //        //PendingIntent contIntent = PendingIntent.GetActivity(this, 1000, resultIntent, PendingIntentFlags.OneShot);
    //        //builder.SetContentIntent(contIntent);
    //        //// Finally, publish the notification:
    //        //NotificationManager notificationP = (NotificationManager)GetSystemService(Context.NotificationService);
    //        //notificationP.Notify(Constants.NOTIFICATION_ID, builder.Build());
    //        var notificationManager = NotificationManagerCompat.From(this);
    //        notificationManager.Notify(Constants.NOTIFICATION_ID, builder.Build());
    //    }

    //    [BroadcastReceiver]
    //    public class CustomActionReceiver : BroadcastReceiver
    //    {
    //        public override void OnReceive(Context context, Intent intent)
    //        {
    //            //Show toast here
    //            Toast.MakeText(context, intent.Action, ToastLength.Short).Show();
    //            var extras = intent.Extras;

    //            if (extras != null && !extras.IsEmpty)
    //            {
    //                NotificationManager manager = context.GetSystemService(Context.NotificationService) as NotificationManager;
    //                var notificationId = extras.GetInt("NotificationIdKey", -1);
    //                if (notificationId != -1)
    //                {
    //                    manager.Cancel(notificationId);
    //                }
    //            }
    //        }
    //    }



    //    protected override void OnError(Context context, string errorId)
    //    {
    //        Log.Verbose(MyBroadCastReciver.TAG, "GCM Error: " + errorId);
    //    }
    //}
}