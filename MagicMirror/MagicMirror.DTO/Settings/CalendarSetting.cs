using System;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.Settings
{
    [DataContract]
    public class CalendarSetting
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "CalendarOwnerAccount")]
        public string CalendarOwnerAccount { get; set; }

        [DataMember(Name = "ActiveCalendarAccount")]
        public string ActiveCalendarAccount { get; set; }

        [DataMember(Name = "RefreshInterval")]
        public string RefreshIntervalStr { get; set; }

        public TimeSpan RefreshInterval
        {
            get
            {
                return TimeSpan.Parse(RefreshIntervalStr);
            }
        }

    }
}
