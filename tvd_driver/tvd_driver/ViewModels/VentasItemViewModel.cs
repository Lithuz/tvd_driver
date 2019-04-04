using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using tvd_driver.Models;
using Xamarin.Forms;
using tvd_driver.Views;

namespace tvd_driver.ViewModels
{
    public class VentasItemViewModel : VentasModel
    {
        public ICommand SelectSaleCommand { get { return new RelayCommand(LoadAceptedSaleAsync); } }

        private async void LoadAceptedSaleAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage(this));
        }
    }
}
