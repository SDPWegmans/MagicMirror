using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.QOTD
{
    [DataContract]
    public class Quote
    {
        [DataMember]
        public string quote { get; set; }
        [DataMember]
        public string length { get; set; }
        [DataMember]
        public string author { get; set; }
        [DataMember]
        public List<string> tags { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string background { get; set; }
        [DataMember]
        public string id { get; set; }
    }
}