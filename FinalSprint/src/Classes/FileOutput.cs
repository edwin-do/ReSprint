using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinalSprint.src.Classes
{
    public class FileOutput
    {
        private readonly string _filePath;
        private readonly string userInputHeader = "Operator Name, Sample Name, Date, Sample Length, Sample Width, Sample Thickness\n";
        private readonly string hardwareInputHeader = "Time, , Voltage, Current, Resistance, Resistivity, Temperture\n";

        public FileOutput(string filePath)
        {
            if (!filePath.EndsWith(".csv")) {
                throw new ArgumentException("File Path does not have a .csv extnesion", "filePath");
            }
            _filePath = filePath;
        }

        public string GetFilePath()
        {
            return _filePath;
        }

        public void WriteUserInput(UserInput userInput)
        {
            
            if(userInput.UserName == null) {
                throw new ArgumentException("The Username was not found when creating file", "userInput.UserName");
            }

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(userInputHeader);
                writer.WriteLine($"{userInput.UserName}, {userInput.UserSampleName}, {DateTime.Now}, {userInput.UserSampleLength}, {userInput.UserSampleWidth}, {userInput.UserSampleThickness}\n\n");
                writer.WriteLine(hardwareInputHeader);
            }
        }

        public void WriteSampleOutput(HardwareInput hardwareInput)
        {

            if (Math.Abs(hardwareInput.Voltage) < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (Math.Abs(hardwareInput.Current) < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");
            if (Math.Abs(hardwareInput.Resistance) < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            if (Math.Abs(hardwareInput.Resistivity) < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");

            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine($"{hardwareInput.Time.ToString("hh:mm:ss:fff")}, ,{hardwareInput.Voltage}, {hardwareInput.Current}, {hardwareInput.Resistance}, {hardwareInput.Resistivity}, {hardwareInput.Temperature}");
            }
        }
    }
}
