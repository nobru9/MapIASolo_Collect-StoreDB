namespace SoilBot.Core.Models
{
    public class SoilSample
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double pH { get; set; }
        public double Clay { get; set; }
        public double Sand { get; set; }
        public double OrganicCarbon { get; set; }
        public string Source { get; set; } = "Unknown";
    }
}
