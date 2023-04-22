using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using AltkamulAlteqani.Entities.ApiModels;

namespace AltkamulAlteqani.Invokers.Invokers
{
    public class BaseInvoker<TEntity, TEntityOut> where TEntity : class where TEntityOut : class
    {
        public static async Task<List<TEntityOut>> Get(string request)
        {
            string BaseApi = ConfigurationManager.AppSettings.Get("StackoverflowApi");
            string urlParameter = request;

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(BaseApi);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.GetAsync(urlParameter);

                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync().Result;
                        var dataObjects = JsonConvert.DeserializeObject<ApiResponse<TEntityOut>>(res);
                        return dataObjects.items;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var stringMesg = response.Content.ReadAsStringAsync().Result;
                        var errorMessage = JsonConvert.DeserializeObject<dynamic>(stringMesg);
                        throw new InvalidDataException(errorMessage);
                    }
                    else
                    {
                        throw new Exception(response.ToString());
                    }
                }
            }
            catch (InvalidDataException Inex)
            {
                throw Inex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
