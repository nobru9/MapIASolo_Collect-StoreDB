/*using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using SoilBot.Core.Models;

namespace SoilBot.Core.Services
{
    public class SoilGridsService
    {
        private readonly HttpClient _http = new();

        public async Task<SoilSample?> GetSampleAsync(double lat, double lon)
        {
            var url = $"https://rest.soilgrids.org/soilgrids/v2.0/properties/query?lat={lat}&lon={lon}&property=phh2o&depth=0-5cm";

            try
            {
                var response = await _http.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Falha ao consultar SoilGrids: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var root = JObject.Parse(json);

                double? ph = root["properties"]?["phh2o"]?["values"]?.First?["value"]?.Value<double>();

                if (ph == null)
                    return null;

                return new SoilSample
                {
                    Latitude = lat,
                    Longitude = lon,
                    pH = ph.Value,
                    OrganicCarbon = 0 // SoilGrids não retorna OC nesse endpoint
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro SoilGrids: " + ex.Message);
                return null;
            }
        }
    }
}*/
