using System.Runtime.Serialization;

namespace MagicMirror.DTO.QOTD
{
    [DataContract]   
    public class QOTDResponse
    {
        [DataMember]
        public Contents contents { get; set; }
    }
}