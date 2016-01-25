using MirrorCompanion.Services.DTO;
using System.Runtime.Serialization;

namespace MirrorCompanion.Services.Requests
{
    [DataContract]
    public class NoteRequest
    {
        //[DataMember]
        //public Note Note { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string NoteText { get; set; }
    }
}