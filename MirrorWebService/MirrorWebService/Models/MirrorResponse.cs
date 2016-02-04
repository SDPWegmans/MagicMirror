using System;
using System.Collections.Generic;
using System.Linq;
using Cal = MagicMirror.DTO.Calendar;
using DTO = MagicMirror.DTO;

namespace MirrorWebService.Models
{
    public class MirrorResponse
    {
        public List<DTO.Calendar.Event> CalendarEvents { get; set; }
        public DTO.Note.Note Note { get; set; }
        public DTO.QOTD.Quote QuoteOfTheDay { get; set; }
        public DTO.Weather.WeatherResponse WeatherForecast { get; set; }
    }
}