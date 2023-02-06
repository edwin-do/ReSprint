using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    class Calculation
    {
        public Calculation() { }

        public double calcResistence(double voltage, double current)
        {
            return voltage / current;
        }

        public double calcResistivity(double resisitance, double area, double length)
        {
            return (resisitance * area) / length;
        }
    }
}