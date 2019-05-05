using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                return json;
            }
            else
            {
                return null;
            }
        }

        internal VentasItemViewModel GetVentaNosync(int idEnfermero)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{mainApiUri}Ventas?Enfermero={idEnfermero}");

            if (response.Result.Length > 2)
            {
                var json = JsonConvert.DeserializeObject<List<VentasItemViewModel>>(response.Result);
                return json[0];
            }
            else
            {
                return null;
            }
        }

        internal ObservableCollection<RelVentaPdctoModel> RelVentasProdcuctoNoSync(int numeroOrden)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{mainApiUri}Ventas?NumeroOrden={numeroOrden}");

            if (response.Result.Length > 2)
            {
                var json = JsonConvert.DeserializeObject<ObservableCollection<RelVentaPdctoModel>>(response.Result);
                return json;
            }
            else
            {
                return null;
            }
        }

        internal int GetTotales4Loggeduser(LoginModel UserModel)
        {
            var idUsuario = UserModel.idEnfermero;
            var result = 0;
            var httpClient = new HttpClient();
            var response = httpClient.GetStringAsync($"{mainApiUri}Ventas?idEnfermero={idUsuario}&EstatusFinal=Completado");

            if (response.Result.Length > 2)
            {
                string resulResponse = response.Result;
                JArray json = (JArray)JsonConvert.DeserializeObject(resulResponse);

                if(json != null)
                {
                    result = json.Count;
                }
                return result;
            }
            else
            {
                return result;
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

        internal async Task<bool> ChangeSaleStatus(int idVenta, bool completed)
        {
            var fechaVenta = DateTime.Now.ToString();
            var EstatusFinal = (completed) ? "Completado" : "Cancelado";

            var httpClient = new HttpClient();
            var response = await httpClient.PutAsync(new Uri($"{mainApiUri}Ventas?idVentas={idVenta}&fechaVenta={fechaVenta}&EstatusFinal={EstatusFinal}"), null);
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
                return json;
            }
            else
            {
                return null;
            }
        }



        internal async Task SendUpdateVentasAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(mainApiUri.Replace("/api/",""));
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, mainApiUri.Replace("/api/", ""));
            request.Content = new StringContent("\"UpdateVentas\"",Encoding.UTF8, "application/json");

            
            var result = await httpClient.SendAsync(request);

            if (result.IsSuccessStatusCode)
            {
                Log.Debug("UPDATE VENTAS", "Succesfully updated ventas");
            }
            else
            {
                Log.Debug("UPDATE VENTAS", "Erro updateing ventas");
            }
        }
    }
}
