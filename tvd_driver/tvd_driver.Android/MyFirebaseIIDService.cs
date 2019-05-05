using Android.App;
using Android.Util;
using WindowsAzure.Messaging;
using Firebase.Iid;
using System.Collections.Generic;
using tvd_driver.Helpers;
using System.Threading.Tasks;
using tvd_driver.ViewModels;

namespace tvd_driver.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        #region Singleton
        private static MyFirebaseIIDService instance;
        private static NotificationHub hub;

        public string Token;

        public static MyFirebaseIIDService Getinstance()
        {
            if (instance == null)
            {
                instance = new MyFirebaseIIDService();
            }
            return instance;
        }
        #endregion

        public MyFirebaseIIDService()
        {
            instance = this;
        }

        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "FCM token: " + refreshedToken);
            SendRegistrationToServerAsync(refreshedToken);
        }

        public void UnregisterReceiverAsync()
        {
            var unregister = Task.Run(() => hub.Unregister());
            Task.WaitAny(unregister);
            if (unregister.IsCompletedSuccessfully)
            {
                Log.Debug(TAG, "Device succesfull unregistered");
            }
        }

        public void RegisterDevice(string Token)
        {            
            OnTokenRefresh();
        }

        void SendRegistrationToServerAsync(string token)
        {
            if (!string.IsNullOrEmpty(Settings.UserId) && !string.IsNullOrEmpty(Settings.UserPass))
            {
                hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString, Application.Context);
                var context = MainActivity.Getinstance().ApplicationContext;
                // Register with Notification Hubs

                var tags = new List<string>() { };

                RegisterDevice(token, hub, tags);
            }
        }

        public void RegisterDevice(string token, NotificationHub hub, List<string> tags)
        {
            var UserModel = MainViewModel.Getinstance().Usuario;
            tags.Add($"user_id:{UserModel.idEnfermero}");
            tags.Add($"status:{UserModel.Estatus}");

            var task = Task.Run(() => hub.Register(token, tags.ToArray()));
            Task.WaitAny(task);
            if (task.IsCompletedSuccessfully)
            {
                var regID = task.Result.RegistrationId;
                Log.Debug(TAG, $"Successful registration of ID {regID}");
            }
        }
    }
}