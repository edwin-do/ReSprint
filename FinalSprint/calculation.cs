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

        public double CalcResistance(double voltage, double current)
        {
            return voltage / current;
        }

        public double CalcResistivity(double resistance, double area, double length)
        {
            return (resistance * area) / length;
        }
        /*public double CalcTemperature(double temperature, double area, double length)
        {
            return (resistance * area) / length;
        }*/
    }
}