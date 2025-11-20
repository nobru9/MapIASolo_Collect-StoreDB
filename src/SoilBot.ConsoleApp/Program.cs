using SoilBot.Core.Services;
using SoilBot.Core.Utils;

class Program 
{
    static async Task Main()
    {
        Console.WriteLine("Coletando dados de solo...");

        var soilGrids = new SoilGridsService();
        var openLand = new OpenLandMapService();
        var uploader = new S3Uploader();

        var samples = new List<SoilBot.Core.Models.SoilSample>();

        var coords = new (double lat, double lon)[]
        {
            (-15.8, -47.9),
            (-22.9, -43.2),
            (-23.5, -46.6),
            (-30.0, -51.2),
            (-3.1, -60.0),
            (-10.12, -48.18)
        };

        foreach (var (lat, lon) in coords)
        {
        
            var sg = await soilGrids.GetSampleAsync(lat, lon);
            if (sg != null)
            {
                sg.Source = "SoilGrids";
                samples.Add(sg);
            }

            var ol = await openLand.GetSampleAsync(lat, lon);
            if (ol != null)
            {
                ol.Source = "OpenLandMap"; 
                samples.Add(ol);
            }
        }

        string csv = CsvHelper.ToCsv(samples);
        string fileName = $"soil_data_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";

        await uploader.UploadAsync(
            bucketName: "solo-dataset-ml", 
            fileName: fileName, 
            csvContent: csv
        );

        Console.WriteLine($"Upload concluído: s3://solo-dataset-ml/{fileName}");
    }
}
