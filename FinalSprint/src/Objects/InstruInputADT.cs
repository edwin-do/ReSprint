using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    class InstruInput
    {
        public double CurrentLevel { get; set; }
        public double Compliance { get; set; }

        public string? Range { get; set; }

        public double SampleRate { get; set; }

        public string? ThType { get; set; }
        public double JuncTemperature { get; set; }
    }
}
