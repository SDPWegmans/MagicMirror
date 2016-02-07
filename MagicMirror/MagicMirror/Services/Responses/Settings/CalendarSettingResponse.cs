using MagicMirror.DTO.Settings;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MagicMirror.Services.Responses.Settings
{
    [DataContract]
    public class CalendarSettingResponse
    {
        [DataMember]
        public CalendarSetting[] CalendarSettings { get; set; }
    }
}