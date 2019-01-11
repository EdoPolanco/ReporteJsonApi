using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace reportes.Negocio
{
    public class Datos
    {
        // aquí hacemos el consumo de los datos de la api
        public static List<Cliente> getCliente()
        {
            List<Cliente> result = new List<Cliente>();
            using (HttpClient api = new HttpClient())
            {

                var response = api.GetAsync("https://my-json-server.typicode.com/HaibuSolutions/prueba-tecnica-sf/user").Result;
                string strResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<List<Cliente>>(strResult);
                }

            }
            return result;
        }
        
    }
}
