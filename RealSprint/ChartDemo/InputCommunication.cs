using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealSprint
{
    class InputCommunication
    {
        public InputCommunication() 
        { 
            voltage = 0.0;
            current = 0.0;

        }

        public double GetVoltage()
        {
            //Returns voltage reading (in mV) from nanovoltmeter
            voltage = 0.00356;
            return voltage;
        }

        public double GetCurrent()
        {
            //Returns output value (in mA) from current source
            current = 1.0;
            return current;
        }

        public void EnableCurrent()
        {
            //enable current output on current source
            //TODO: implement with SCPI commands
            return;
        }

        public void DisableCurrent()
        {
            //disable current output on current source
            //TODO: implement with SCPI commands
            return;
        }

        //private member variables
        private double voltage;
        private double current;
    }
}
