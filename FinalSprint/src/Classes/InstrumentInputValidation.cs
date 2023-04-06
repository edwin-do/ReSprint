using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinalSprint.src.Classes
{
    public class InstrumentInputValidation
    {

        public void HardwareInput() { }

        public bool CheckSampleRate(double r)
        {
            if (r < 1 || r > 600)
            {
                throw new ArgumentException("Invalid acquisition rate, please enter a valid decimal number in Hz.");
            }
            return true;
        }

        public bool CheckCurrentLevel(double c)
        {
            if (c < -105 || c > 105 || c == 0)
            {
                throw new ArgumentException("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA.");
            }

            return true;
        }

        public bool CheckCompliance(double c)
        {
            if (c <0.1 || c > 105)
            {
                throw new ArgumentException("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V.");
            }
            return true;
        }

        public bool checkJuncTemperature(double t)
        {
            if (t > 9999 || t < -9999)
            {
                throw new ArgumentException("Invalid temperature, please enter a valid decimal number in °C.");
            }
            return true;
        }
    }
}
