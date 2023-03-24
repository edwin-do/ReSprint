using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    public class Data
    {
        public Data(DateTime time, double voltage, double current, double resistance, double resistivity, double temperature)
        {
            Time = time;
            Voltage = voltage;
            Current = current;
            Resistance = resistance;
            Resistivity = resistivity;
            Temperature = temperature;
        }

        public DateTime Time { get; set; }
        public double Voltage { get; set; }

        public double Current { get; set; }

        public double Resistance { get; set; }

        public double Resistivity { get; set; }
        public double Temperature { get; set; }
    }
}
