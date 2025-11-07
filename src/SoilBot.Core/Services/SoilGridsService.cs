using System.Net.Http;
using System.Text.Json;
using Amazon.Runtime;
using SoilBot.Core.Models;

namespace SoilBot.Core.Services
{
    
    public class SoilGridsService
    {
        private readonly HttpClient _http = new HttpClient();

        public async Task<SoilSample?> GetSampleAsync(double lat, double lon)
        {
            string url = $"https://rest.isric.org/soilgrids/v2.0/properties/query?lon={lon}&lat={lat}&property=phh2o,clay,sand,ocd";

            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($" Falha ao consultar SoilGrids: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var layers = doc.RootElement.GetProperty("properties").GetProperty("layers");

            float GetValue (string name)
            {
                try
                {
                    var layer = layers.EnumerateArray().FirstOrDefault(l => l.GetProperty("name").GetString() == name);
                    var firstDepth = layer.GetProperty("depths")[0];
                    return firstDepth.GetProperty("values").GetProperty("mean").GetSingle();
                }

                catch { return 0f; }
            }

            return new SoilSample
            {
                Latitude = lat,
                Longiitude = lon,
                pH = GetValue("phh2o"),
                Clay = GetValue("clay"),
                Sand = GetValue("sand"),
                OrganicCarbon = GetValue("ocd"),
                Source = "SoilGrids v2.0"

            };
        }
    }
}