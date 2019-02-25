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

namespace tvd_driver.Droid
{
    [Activity(Label = "tvd_driver", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation }, 1);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            FirebaseApp.InitializeApp(Application.Context);
            LoadApplication(new App());
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
            .SetApplicationId("1:141302426286:android:f5adfca813015f92")
            .SetApiKey("AIzaSyAyFthYa3extbXIMx14XKhK7OWK0WNV-ls")
            .Build();



            if (app == null)
                app = FirebaseApp.InitializeApp(this, options, "TheVitaminDoctors");

        }
    }
}