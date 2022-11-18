using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReSprint
{
    class ReadData
    {
        public ReadData()
        {
            current = new List<string>();
            voltage = new List<string>();
        }
        public void GetData(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            
            for (int i = 0; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                current.Add(values[0]);
                voltage.Add(values[1]);
            }

        }
        public List<string> GetCurrent() { return current; }
        public List<string> GetVoltage() { return voltage; }

        // Private variables
        private List<string> current;
        private List<string> voltage;

    }
    
}


