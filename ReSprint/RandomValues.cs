using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReSprint
{
    class RandomValues
    {
        public RandomValues()
        {
            public void generate(string fileName)
            {
                Random rnd = new Random();

                int currentValue = rnd.Next(1,11);
                int voltageValue = rnd.Next(1,11);
                while (true)
                {
                    if (!File.Exists(fileName))
                    {
                        string line = currentValue + "," + voltageValue;

                        File.WriteAllText(fileName, line);
                    }
                    Thread.Sleep(3000);
                }            
            }             
        }
    }

}


