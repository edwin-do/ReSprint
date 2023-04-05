using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinalSprint.src.Classes
{
    internal class InstrumentInputValidation
    {

        public void HardwareInput() { }

        public bool CheckSampleRate(double r)
        {
            if (r < 0 || r > 600)
            {
                MessageBox.Show("Invalid acquisition rate, please enter a valid decimal number in Hz.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
                return true;
        }

        public bool CheckCurrentLevel(double c)
        {
            if (c < -105 || c > 105)
            {
                MessageBox.Show("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public bool CheckCompliance(double c)
        {
            if (c <0.1 || c > 105)
            {
                MessageBox.Show("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public bool checkJuncTemperature(double t)
        {
            if (t > 9999 || t < -9999)
            {
                MessageBox.Show("Invalid temperature, please enter a valid decimal number in °C.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
