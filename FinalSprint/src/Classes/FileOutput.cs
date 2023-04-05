using NationalInstruments.Restricted;
using Syncfusion.Windows.Shared;
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
            if (filePath.IsEmpty() || filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("File Path is not given or empty", "filePath");
            }
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
            
            if(userInput.UserName == null ) {
                throw new ArgumentException("The Username was not found when creating file", "userInput.UserName");
            }
            if (userInput.UserSampleName == null)
            {
                throw new ArgumentException("The Sample Name was not found when creating file", "userInput.UserSampleName");
            }
            if (userInput.UserSampleLength == 0)
            {
                throw new ArgumentException("The UserSampleLength was not found when creating file", "userInput.UserSampleLength");
            }
            if (userInput.UserSampleWidth == 0)
            {
                throw new ArgumentException("The UserSampleWidth was not found when creating file", "userInput.UserSampleWidth");
            }
            if (userInput.UserSampleThickness == 0)
            {
                throw new ArgumentException("The UserSampleThickness was not found when creating file", "userInput.UserSampleThickness");
            }

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(userInputHeader);
                writer.WriteLine($"{userInput.UserName},{userInput.UserSampleName},{DateTime.Now},{userInput.UserSampleLength},{userInput.UserSampleWidth},{userInput.UserSampleThickness}\n\n");
                writer.WriteLine(hardwareInputHeader);
            }
        }

        public void WriteSampleOutput(HardwareInput hardwareInput)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine($"{hardwareInput.Time.ToString("hh:mm:ss:fff")}, ,{hardwareInput.Voltage},{hardwareInput.Current},{hardwareInput.Resistance},{hardwareInput.Resistivity},{hardwareInput.Temperature}");
            }
        }
    }
}
