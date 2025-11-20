using System.Globalization;
using System.Text;
using SoilBot.Core.Models;

namespace SoilBot.Core.Utils
{
    public static class CsvHelper
    {
        public static string ToCsv(List<SoilSample> samples){

            var sb = new StringBuilder();
            sb.AppendLine("Latitude,Longitude,pH,Clay,Sand,OrganicCarbon,Source");
            foreach (var s in samples)
            sb.AppendLine($"{s.Latitude.ToString(CultureInfo.InvariantCulture)},{s.Longitude.ToString(CultureInfo.InvariantCulture)},{s.pH},{s.Clay},{s.Sand},{s.OrganicCarbon},{s.Source}");
            return sb.ToString();
        }

    }

}