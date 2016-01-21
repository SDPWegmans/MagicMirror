using System.Runtime.Serialization;

namespace MagicMirror.Services.DTO.Weather
{
    [DataContract]
    public class Currently : WeatherDataBase
    {
        [DataMember]
        public double temperature { get; set; }

        [DataMember]
        public double apparentTemperature { get; set; }
    }
}