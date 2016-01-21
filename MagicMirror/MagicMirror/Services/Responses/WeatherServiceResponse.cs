using MagicMirror.Services.DTO.Weather;
using System.Runtime.Serialization;

namespace MagicMirror.Services.Responses
{
    [DataContract]    
    public class WeatherServiceResponse //: ServiceResponseBase
    {
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double longitude { get; set; }
        [DataMember]
        public string timezone { get; set; }
        [DataMember]
        public int offset { get; set; }
        [DataMember]
        public Currently currently { get; set; }
        [DataMember]
        public Hourly hourly { get; set; }
        [DataMember]
        public Daily daily { get; set; }
        [DataMember]
        public Flags flags { get; set; }
    }
}