using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.Services.DTO.QOTD
{
    [DataContract]
    public class Contents
    {
        [DataMember]
        public List<Quote> quotes { get; set; }
    }
}