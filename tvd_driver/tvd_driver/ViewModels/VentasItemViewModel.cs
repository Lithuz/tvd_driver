using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using tvd_driver.Models;
using Xamarin.Forms;
using tvd_driver.Views;
using tvd_driver.Services;

namespace tvd_driver.ViewModels
{
    public class VentasItemViewModel : VentasModel
    {
        private ApiServices apiServices;
        private MainViewModel mainModel;
        public ICommand SelectSaleCommand { get { return new RelayCommand(LoadAceptedSaleAsync); } }

        public VentasItemViewModel()
        {
            apiServices = new ApiServices();
            mainModel = MainViewModel.Getinstance();
        }
        private async void LoadAceptedSaleAsync()
        {
            mainModel.Usuario = await apiServices.LoginUser(mainModel.Usuario.Usuario, mainModel.Usuario.Pass);
            if (mainModel.Usuario.Estatus != 1)
            {
                await Application.Current.MainPage.DisplayAlert("Failed Operation", $"The user {mainModel.Usuario.Nombre} is allready on a trip.", "Dismiss");
            }
            else
            {
                var response = await apiServices.SetStatusAsync(mainModel.Usuario.idEnfermero, 2);
                if (response)
                {
                    mainModel.Venta = this;
                    mainModel.Usuario.Estatus = 2;
                    var linkResponse = await apiServices.LinkVentaEnfermero(mainModel.Venta.idVenta, mainModel.Usuario.idEnfermero, true);
                    if (linkResponse)
                    {
                        
                        mainModel.relVentasPdcto = await apiServices.RelVentasProdcucto(mainModel.Venta.NumeroOrden);

                        ProfileMainPage.Getinstance().Detail = new NavigationPage(new MainPage(mainModel.Venta));
                    }
                }
            }
        }
    }
}
