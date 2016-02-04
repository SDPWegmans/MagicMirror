using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.DTO.Weather
{
    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<DailyData> data { get; set; }
    }
}
