using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using tvd_driver.Models;
using tvd_driver.Services;

namespace tvd_driver.ViewModels
{
    public class VentasViewModel : BaseViewModel
    {

        private ApiServices apiServices;
        private ObservableCollection<VentasItemViewModel> ventas;
        private bool isRefreshing;
        private string filter;
        private List<VentasModel> ventasList;

        public ICommand RefreshVentas { get { return new RelayCommand(LoadVentas); } }
        public ICommand SearchCommand { get { return new RelayCommand(Search); } }
        public ICommand SelectSaleCommand { get { return new RelayCommand(LoadAceptedSale); } }

        public ObservableCollection<VentasItemViewModel> Ventas
        {
            get { return this.ventas; }
            set { SetValue(ref this.ventas, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set { SetValue(ref this.filter, value);this.Search(); }
        }

        public VentasViewModel()
        {
            this.apiServices = new ApiServices();
            this.LoadVentas();
        }

        private async void LoadVentas()
        {
            IsRefreshing = true;
            this.ventasList = await apiServices.GetAviableVentas();
            this.Ventas = new ObservableCollection<VentasItemViewModel>(this.ToVentasItemViewModel());
            IsRefreshing = false;
        }

        private  void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Ventas = new ObservableCollection<VentasItemViewModel>(this.ToVentasItemViewModel());
            }
            else
            {
                this.Ventas = new ObservableCollection<VentasItemViewModel>(
                    this.ToVentasItemViewModel().Where(v => v.NombreCliente.ToLower().Contains(this.Filter.ToLower()) || 
                                          v.NumeroOrden.ToString().Contains(this.Filter.ToLower())));
            }
        }

        private IEnumerable<VentasItemViewModel> ToVentasItemViewModel()
        {
            return this.ventasList.Select(v => new VentasItemViewModel
            {
                idVenta = v.idVenta,
                NumeroOrden = v.NumeroOrden,
                TotalVenta = v.TotalVenta,
                NombreCliente = v.NombreCliente,
                Correo = v.Correo,
                TelefonoCLiente = v.TelefonoCLiente,
                Producto = v.Producto,
                GeoLattitud = v.GeoLattitud,
                GeoAltitud = v.GeoAltitud,
                Asignado = v.Asignado,
                Enfermero = v.Enfermero,
                Fecha = v.Fecha
            });
        }

        private void LoadAceptedSale()
        {
            var mainModel = MainViewModel.Getinstance();
            var user = mainModel.Usuario;
            if(user.Estatus == 1)
            {

            }
        }
    }
}
