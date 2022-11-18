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
        public string[] GetData(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            //foreach (string line in lines) {System.Diagnostics.Debug.WriteLine(line); }

            return lines;
        }

    }
    
}


