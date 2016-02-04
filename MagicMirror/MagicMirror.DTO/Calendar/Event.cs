using System;

namespace MagicMirror.DTO.Calendar
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
            get; set;
            //get
            //{
            //    return (StartTime == DateTime.MinValue) ? true : false;
            //}
            //set
            //{
            //    try
            //    {
            //        IsAllDayEvent = value;
            //    }
            //    catch (Exception)
            //    {
            //        //TODO: This sucks...Make it better
            //        IsAllDayEvent = false;
            //    }
                
            //}
        }
    }
}