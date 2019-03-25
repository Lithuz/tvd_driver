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
            var KeyValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("usuario",username),
                new KeyValuePair<string, string>("password",password),
            };
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
    }
}
