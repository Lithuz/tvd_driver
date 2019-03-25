using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using tvd_driver.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tvd_driver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMainPageMaster : ContentPage
    {
        public ListView ListView;
        public ProfileMainPageMaster()
        {
            InitializeComponent();
            ListView = MenuItemsListView;
        }

    }
}