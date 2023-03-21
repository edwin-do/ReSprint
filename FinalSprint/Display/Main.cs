
using System;
  
namespace FinalSprint
{
    class FileOutputClass
    {
        public FileOutputClass() { }
        public void PrintOutput()
        {
    
            Console.WriteLine("Main Method");
            FileOutput fileOutput = new FileOutput(@"test.csv");
            UserInput userInput = new UserInput{
                Name = "Timothy", SampleName = "Sample1", Date = DateTime.Now.ToLongDateString(),
                SamplingRate = 60, SampleLength = 4, SampleWidth = 1
            };
            HardwareInput hardwareInput = new HardwareInput{
                Voltage = 5, Time = $"{System.DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}:{
                    DateTime.Now.Second:00}.{DateTime.Now.Millisecond:000}.{DateTime.Now.Microsecond:000}",
                Temperature = 50, Current = 1, Resistance = 2, Resistivity = 2
            };
            fileOutput.WriteUserInput(userInput);
            fileOutput.WriteSampleOutput(hardwareInput);
            fileOutput.WriteSampleOutput(hardwareInput);
            fileOutput.WriteSampleOutput(hardwareInput);
        }
    }

    public class UserInput
    {
        public string? Name { get; set; }
        public string? SampleName { get; set; }
        public string? Date { get; set; }
        public double SamplingRate { get; set; }
        public double SampleLength { get; set; }
        public double SampleWidth { get; set; }
    }

     public class HardwareInput
    {
        public double Voltage { get; set; }
        public string? Time { get; set; }
        public double Temperature { get; set; }
        public double Current { get; set; }
        public double Resistance { get; set; }
        public double Resistivity { get; set; }
    }
}
