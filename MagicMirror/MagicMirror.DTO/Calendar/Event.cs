using System;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.Calendar
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public string Summary { get; set; }
        //[DataMember]
        //public DateTime StartTime { get; set; }
        [DataMember]
        public string StartTimeStr { get; set; }
        //[DataMember]
        //public DateTime EndTime { get; set; }
        [DataMember]
        public string EndTimeStr { get; set; }
        //[DataMember]
        //public TimeSpan Duration
        //{
        //    get
        //    {
        //        return new TimeSpan((EndTime - StartTime).Ticks);
        //    }
        //}
        [DataMember]
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