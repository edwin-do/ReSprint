using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ReSprint
{
    class ReadData
    {
        public ReadData() //Class constructor
        {
            current = new List<double>();
            voltage = new List<double>();
            temp = new List<double>();
            time = new List<string>();
            rate = 3000;
            next = true;
        }
        public void GetData(string fileName)
        {
            string line = File.ReadLines(fileName).Last(); //read the latest (last) line of data
            var values = line.Split(',');
            time.Insert(0, values[0]);
            temp.Insert(0, Convert.ToDouble(values[1]));
            current.Insert(0, Convert.ToDouble(values[2]));
            voltage.Insert(0, Convert.ToDouble(values[3]));
        }
        public string GetTime() { return time[0]; }
        public int GetTimeCount() { return time.Count; }
        public double GetTemp() { return temp[0]; }
        public int GetTempCount() { return temp.Count; }
        public double GetCurrent() { return current[0]; }
        public int GetCurrentCount() { return current.Count; }
        public double GetVoltage() { return voltage[0]; }
        public int GetVoltageCount() { return voltage.Count; }

        // Private variables
        private List<double> current;
        private List<double> voltage;
        private List<string> time;
        private List<double> temp;
        private Boolean next;

        private int rate;       //Acquisition rate (ms)

    }
    
}


