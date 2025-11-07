namespace SoilBot.Core.Models
{
    public class SoilSample
    {
        public double Latitude { get; set; }
        public double Longiitude { get; set; }
        public float pH { get; set; }
        public float Clay { get; set; }
        public float Sand { get; set; }
        public float OrganicCarbon { get; set; }
        public string Source { get; set; } = "Unknown";
    }
}
