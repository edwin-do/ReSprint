using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.Display
{
    public class FileOutput
    {
        private readonly string _filePath;
        private readonly string userInputHeader = "Name, SampleName, Date, SamplingRate, SampleLength, SampleWidth\n";
        private readonly string hardwareInputHeader = "Time, ,Critical Resistence Change, Critical Slope Change, Voltage, Current, Resistance, Resistivity, Temperture\n";

        public FileOutput(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteUserInput(UserInput userInput)
        {

            if (userInput.UserSampleWidth < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (userInput.UserSampleLength < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(userInputHeader);
                writer.WriteLine($"{userInput.UserName}, {userInput.UserSampleName}, {DateTime.Now}, {userInput.UserSampleLength}, {userInput.UserSampleWidth}, {userInput.UserSampleThickness}\n\n");
                writer.WriteLine(hardwareInputHeader);
            }
        }

        public void WriteSampleOutput(HardwareInput hardwareInput)
        {

            if (hardwareInput.Voltage < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (hardwareInput.Current < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");
            if (hardwareInput.Resistance < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            if (hardwareInput.Resistivity < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");

            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine($"{hardwareInput.Time.ToString("hh:mm:ss:fff")}, , , ,{hardwareInput.Voltage}, {hardwareInput.Current}, {hardwareInput.Resistance}, {hardwareInput.Resistivity}, {hardwareInput.Temperature}");
            }
        }
    }
}
