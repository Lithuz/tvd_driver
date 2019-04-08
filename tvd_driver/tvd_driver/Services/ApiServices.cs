using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using tvd_driver.Models;
using tvd_driver.ViewModels;

namespace tvd_driver.Services
{
    public class ApiServices
    {
        string mainApiUri = "http://tvddriverapi.azurewebsites.net/api/";
        public async Task<LoginModel> LoginUser(string username, string password)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"{mainApiUri}Enfermeros?usuario={username}&password={password}");
            
            if(response.Length > 2)
            {
                var json = JsonConvert.DeserializeObject<List<LoginModel>>(response);
                return json[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<List<VentasModel>> GetAviableVentas()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{mainApiUri}Ventas?Asignado=false");
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            { 
                var json = JsonConvert.DeserializeObject<List<VentasModel>>(result);
                //var list = (List<VentasModel>)json;
                return json;//json[0];
            }
            else
            {
                return null;
            }
        }

        internal object LoginUserSync(object userName, object userPass)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{mainApiUri}Enfermeros?usuario={userName}&password={userPass}");

            if (response.Result.Length > 2)
            {
                var json = JsonConvert.DeserializeObject<List<LoginModel>>(response.Result);
                return json[0];
            }
            else
            {
                return null;
            }
        }

        internal async Task<bool> SetStatusAsync(int idEnfermero, int status)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync( new Uri($"{mainApiUri}Enfermeros?idEnfermero={idEnfermero}&status={status}"),null);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal async Task<bool> SaveDisclaimer(int idVenta, string Disclaimer)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(new Uri($"{mainApiUri}Ventas?idVentas={idVenta}&Disclaimer={Disclaimer}"), null);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal async Task<bool> LinkVentaEnfermero(int idVenta, int idEnfermero, bool asignado)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(new Uri($"{mainApiUri}Ventas?idVentas={idVenta}&idEnfermero={idEnfermero}&Asignado={asignado}"), null);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        internal async Task<ObservableCollection<RelVentaPdctoModel>> RelVentasProdcucto(int numeroOrden)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{mainApiUri}Ventas?NumeroOrden={numeroOrden}");
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<ObservableCollection<RelVentaPdctoModel>>(result);
                //var list = (List<VentasModel>)json;
                return json;//json[0];
            }
            else
            {
                return null;
            }
        }
    }
}
