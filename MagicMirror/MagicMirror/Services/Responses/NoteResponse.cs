using MagicMirror.Services.DTO.Note;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.Services.Responses
{
    [DataContract]
    public class NoteResponse
    {
        [DataMember]
        public Note[] Notes { get; set; }
    }
}