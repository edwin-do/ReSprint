using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReSprint
{
    class calculation
    {
        public calculation(){}

        public double calcResistence(double voltage, double current)
        {
            return Math.Round(voltage/current, 3);
        }

        public double calcResistivity(double resisitance, double area, double length)
        {
            return Math.Round((resisitance*area)/length,3);
        }
    }
}
