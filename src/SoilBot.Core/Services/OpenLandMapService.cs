using System.Net.Http;
using System.Text.Json;
using SoilBot.Core.Models;

namespace SoilBot.Core.Services
{
    public class OpenLandMapService
    {
        private readonly HttpClient _http = new HttpClient();

        public async Task<SoilSample?> GetSampleAsync(double lat, double lon)
        {
            try
            {
                string url =
                    $"https://api.openlandmap.org/query?lat={lat}&lon={lon}&layers=PHIHOX_M_sl3_250m,ORCDRC_M_sl3_250m,SLTPPT_M_sl3_250m,CLYPPT_M_sl3_250m";

                var json = await _http.GetStringAsync(url);
                var doc = JsonDocument.Parse(json);

                var root = doc.RootElement;

                return new SoilSample
                {
                    Latitude = lat,
                    Longitude = lon,
                    pH = root.GetProperty("PHIHOX_M_sl3_250m").GetDouble(),
                    OrganicCarbon = root.GetProperty("ORCDRC_M_sl3_250m").GetDouble(),
                    Clay = root.GetProperty("CLYPPT_M_sl3_250m").GetDouble(),
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
