using System;
using System.Runtime.Serialization;

namespace MirrorWebService.Models.Settings
{
    [DataContract]
    public class NoteSettings
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string NoteRefreshInterval { get; set; }
        [DataMember]
        public int NumberOfActiveNotes { get; set; }
    }
}