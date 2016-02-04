using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.Weather
{
    [DataContract]
    public class Flags
    {
        [DataMember]
        public List<string> sources { get; set; }

        [DataMember(Name = "isd-stations")]
        public List<string> ISDStations { get; set; }

        [DataMember]
        public string units { get; set; }
    }
}