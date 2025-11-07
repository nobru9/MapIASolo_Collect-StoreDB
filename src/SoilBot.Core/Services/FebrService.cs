using System.Net.Http;
using SoilBot.Core.Models;

namespace SoilBot.Core.Services
{
    public class FebrService
    {
        private readonly HttpClient _http = new HttpClient();
        private const string CsvUrl = "https://raw.githubusercontent.com/lisandrop/soilbrdata/main/data/soil_samples_br.csv";
        public async Task<List<SoilSample>> GetSampleAsync(int limit = 30)
        {
            var list = new List<SoilSample>();

            Console.WriteLine("Baixando dados FEBR...");

            var csvStream = await _http.GetStreamAsync(CsvUrl);
            using var reader = new StreamReader(csvStream);

            var header = reader.ReadLine();
            if (string.IsNullOrEmpty(header)) return list;

            int count = 0;
            while (!reader.EndOfStream && count < limit)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var cols = line.Split(',');

                try
                {
                    double lat = double.Parse(cols[0], System.Globalization.CultureInfo.InvariantCulture);
                    double lon = double.Parse(cols[1], System.Globalization.CultureInfo.InvariantCulture);
                    float ph = float.Parse(cols[2], System.Globalization.CultureInfo.InvariantCulture);
                    float clay = float.Parse(cols[3], System.Globalization.CultureInfo.InvariantCulture);
                    float sand = float.Parse(cols[4], System.Globalization.CultureInfo.InvariantCulture);
                    float oc = float.Parse(cols[5], System.Globalization.CultureInfo.InvariantCulture);

                    list.Add(new SoilSample
                    {
                        Latitude = lat,
                        Longiitude = lon,
                        pH = ph,
                        Clay = clay,
                        Sand = sand,
                        OrganicCarbon = oc,
                        Source = "FEBR CVS"
                    });

                    count++;
                }

                catch
                {
                    continue;
                }

            }

            Console.WriteLine($"{list.Count} amostras FEBR coletadas.");
            return list;
        }
    }
    
}