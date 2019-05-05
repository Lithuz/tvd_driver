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
        private IEnumerable<VentasItemViewModel> venta;
        private bool isRefreshing;
        private string filter;
        private List<VentasModel> ventasList;

        public ICommand RefreshVentas { get { return new RelayCommand(LoadVentas); } }
        public ICommand SearchCommand { get { return new RelayCommand(Search); } }

        public ObservableCollection<VentasItemViewModel> Ventas
        {
            get { return this.ventas; }
            set { SetValue(ref this.ventas, value); }
        }

        public IEnumerable<VentasItemViewModel> Venta
        {
            get { return this.venta; }
            set { SetValue(ref this.venta, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set { SetValue(ref this.filter, value); this.Search(); }
        }

        public VentasViewModel()
        {
            this.apiServices = new ApiServices();
            this.LoadVentas();
        }

        public async void LoadVentas()
        {
            var date = DateTime.Now.ToString("MM/dd/yyyy");
            IsRefreshing = true;
            this.ventasList = await apiServices.GetAviableVentas();
            this.Ventas = new ObservableCollection<VentasItemViewModel>(this.ToVentasItemViewModel()
                .Where(v => Convert.ToDateTime(v.Fecha).ToString("MM/dd/yyyy") == date));
            IsRefreshing = false;
        }

        private void Search()
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
            var response = this.ventasList.Select(v => new VentasItemViewModel
            {
                idVenta = v.idVenta,
                NumeroOrden = v.NumeroOrden,
                TotalVenta = v.TotalVenta,
                NombreCliente = v.NombreCliente.ToUpper(),
                Direccion = v.Direccion,
                Correo = v.Correo.ToLower(),
                TelefonoCLiente = v.TelefonoCLiente,
                GeoLattitud = v.GeoLattitud,
                GeoAltitud = v.GeoAltitud,
                Asignado = v.Asignado,
                Enfermero = v.Enfermero,
                Fecha = v.Fecha,
                EstatusFinal = v.EstatusFinal
            });
            venta = response;
            return venta;
        }
    }
}
