using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.Weather
{
    [DataContract]
    public class WeatherResponse
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
