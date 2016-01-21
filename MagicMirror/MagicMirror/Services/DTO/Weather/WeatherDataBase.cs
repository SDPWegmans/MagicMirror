using System.Runtime.Serialization;

namespace MagicMirror.Services.DTO.Weather
{
    [DataContract]
    public class WeatherDataBase
    {
        [DataMember]
        public int time { get; set; }

        [DataMember]
        public string summary { get; set; }

        [DataMember]
        public string icon { get; set; }

        [DataMember]
        public double precipIntensity { get; set; }

        [DataMember]
        public double precipProbability { get; set; }

        [DataMember]
        public string precipType { get; set; }
        
        [DataMember]
        public double dewPoint { get; set; }

        [DataMember]
        public double humidity { get; set; }

        [DataMember]
        public double windSpeed { get; set; }

        [DataMember]
        public int windBearing { get; set; }

        [DataMember]
        public double cloudCover { get; set; }

        [DataMember]
        public double pressure { get; set; }

        [DataMember]
        public double ozone { get; set; }
    }
}