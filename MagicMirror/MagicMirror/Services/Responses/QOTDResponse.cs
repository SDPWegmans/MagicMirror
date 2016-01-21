using MagicMirror.Services.DTO.QOTD;
using System.Runtime.Serialization;

namespace MagicMirror.Services.Responses
{
    [DataContract]
    public class QOTDResponse
    {
        [DataMember]
        public Contents contents { get; set; }
    }
}
