using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.Services.Responses
{
    [DataContract]
    public class MirrorDataResponse
    {
        [DataMember]
        public List<DTO.Calendar.Event> CalendarEvents { get; set; }

        [DataMember]
        public DTO.Note.Note Note { get; set; }

        [DataMember]
        public DTO.QOTD.Quote QuoteOfTheDay { get; set; }

        [DataMember]
        public DTO.Weather.WeatherResponse WeatherForecast { get; set; }

        //[DataMember]
        //public DateTime NoteRefreshDate { get; set; }
    }
}