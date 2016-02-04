using System.Runtime.Serialization;

namespace MagicMirror.DTO.Weather
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