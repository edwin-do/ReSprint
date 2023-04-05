﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.src.Classes
{
    public class FileOutput
    {
        private readonly string _filePath;
        private readonly string userInputHeader = "Operator Name, Sample Name, Date, Sample Length, Sample Width, Sample Thickness\n";
        private readonly string hardwareInputHeader = "Time, , Voltage, Current, Resistance, Resistivity, Temperture\n";

        public FileOutput(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteUserInput(UserInput userInput)
        {
            
            Debug.WriteLine("file user in");
            if (userInput.UserSampleWidth < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleWidth", "Value cannot be negative.");
            if (userInput.UserSampleLength < 0)
                throw new ArgumentOutOfRangeException("userInput.SampleLength", "Value cannot be negative.");

            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                Debug.WriteLine("file user write");
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