using System;
using System.Collections.Generic;
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
    }
}
