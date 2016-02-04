using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.DTO.Weather
{
    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<HourlyData> data { get; set; }
    }
}
