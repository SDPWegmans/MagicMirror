using System.Runtime.Serialization;

namespace MagicMirror.DTO.Weather
{
    [DataContract]
    public class DailyData : WeatherDataBase
    {
        [DataMember]
        public int sunriseTime { get; set; }
        [DataMember]
        public int sunsetTime { get; set; }
        [DataMember]
        public double moonPhase { get; set; }
        [DataMember]
        public double precipIntensityMax { get; set; }
        [DataMember]
        public int precipIntensityMaxTime { get; set; }
        [DataMember]
        public double temperatureMin { get; set; }
        [DataMember]
        public int temperatureMinTime { get; set; }
        [DataMember]
        public double temperatureMax { get; set; }
        [DataMember]
        public int temperatureMaxTime { get; set; }
        [DataMember]
        public double apparentTemperatureMin { get; set; }
        [DataMember]
        public int apparentTemperatureMinTime { get; set; }
        [DataMember]
        public double apparentTemperatureMax { get; set; }
        [DataMember]
        public int apparentTemperatureMaxTime { get; set; }
    }
}