using System.Runtime.Serialization;

namespace MagicMirror.DTO.Note
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