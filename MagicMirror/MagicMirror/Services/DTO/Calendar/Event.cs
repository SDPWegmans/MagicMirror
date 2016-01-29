using System;

namespace MagicMirror.Services.DTO.Calendar
{
    public class Event
    {
        public string Summary { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration
        {
            get
            {
                return new TimeSpan((EndTime - StartTime).Ticks);
            }
        }
        public bool IsAllDayEvent
        {
            get
            {
                return (StartTime == DateTime.MinValue) ? true : false;
            }
        }
    }
}