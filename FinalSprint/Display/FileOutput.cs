using System;
using System.Text;
using System.IO;

namespace FinalSprint
{
    public class FileOutput
    {
        private readonly string _filePath;
        private readonly string userInputHeader = "Name, SampleName, Date, SamplingRate, SampleLength, SampleWidth\n";
        private readonly string hardwareInputHeader = "Time, ,Voltage, Current, , Resistance, Resistivity, Temperture\n";

        public FileOutput(string filePath){
            _filePath = filePath;
        }

        public void WriteUserInput(UserInput userInput){

            if (userInput.SamplingRate < 0 )
                throw new ArgumentOutOfRangeException("userInput.SamplingRate", "Value cannot be negative.");
            if (userInput.SampleWidth < 0 )
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (userInput.SampleLength < 0 )
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");
            
            using (StreamWriter writer = new StreamWriter(_filePath)){
                writer.WriteLine(userInputHeader);
                writer.WriteLine($"{userInput.Name}, {userInput.SampleName}, {userInput.Date
                },{userInput.SamplingRate}, {userInput.SampleLength}, {userInput.SampleWidth}\n\n");
                writer.WriteLine(hardwareInputHeader);
            }
        }

        public void WriteSampleOutput(HardwareInput hardwareInput){

            if (string.IsNullOrEmpty(hardwareInput.Time))
                throw new ArgumentOutOfRangeException("hardwareInput.Time", "Value cannot be empty.");
            if (hardwareInput.Voltage < 0 )
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (hardwareInput.Current < 0 )
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");
            if (hardwareInput.Resistance < 0 )
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            if (hardwareInput.Resistivity < 0 )
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");

            using (StreamWriter writer = new StreamWriter(_filePath, true)){
                writer.WriteLine($"{hardwareInput.Time}, ,{hardwareInput.Voltage}, {hardwareInput.Current
                }, {hardwareInput.Resistance}, {hardwareInput.Resistivity}, {hardwareInput.Temperature}");
            }
        }
    }
}
