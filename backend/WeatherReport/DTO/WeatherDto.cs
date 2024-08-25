namespace WeatherReport.DTO
{
    public class WeatherDto
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}
