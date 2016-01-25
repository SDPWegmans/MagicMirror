using System.Runtime.Serialization;

namespace MirrorCompanion.Services.DTO
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string NoteText { get; set; }
    }
}