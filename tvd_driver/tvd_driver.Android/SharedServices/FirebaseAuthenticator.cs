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
using Xamarin.Forms;
using tvd_driver.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(IFirebaseAuth))]
namespace tvd_driver.Droid.SharedServices
{
    class FirebaseAuthenticator : IFirebaseAuth
    {
        public async Task<bool> SignIn(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.GetInstance(MainActivity.app).SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}