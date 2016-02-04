using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MagicMirror.DTO.Weather
{
    public class HourlyData : WeatherDataBase
    {
        [DataMember]
        public double temperature { get; set; }

        [DataMember]
        public double apparentTemperature { get; set; }
    }
}
