using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tvd_driver.Models;
using tvd_driver.ViewModels;
using tvd_driver.Helpers;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}