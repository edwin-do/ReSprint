using FinalSprint.src.Classes;
using System.IO;

namespace FinalSprint.Tests
{
    public class WriteUserInput
    {
        private UserInput userInput = new UserInput();
        private FileOutput? file;

        [Test]
        public void VaildFileName()
        {
            file = new FileOutput(@$"UnitTestSample.csv");
            Assert.That(file.GetFilePath, Is.EqualTo("UnitTestSample.csv"));

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }
        }

        [Test]
        public void InvaildFileName()
        {
            var ex = Assert.Throws<ArgumentException>(() => file = new FileOutput(@$"UnitTestSample"));
            Assert.That(ex.Message, Is.EqualTo("File Path does not have a .csv extnesion (Parameter 'filePath')"));

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }

        }

        [Test]
        public void EmptyFileName()
        {
            var ex = Assert.Throws<ArgumentException>(() => file = new FileOutput(""));
            Assert.That(ex.Message, Is.EqualTo("File Path is not given or empty (Parameter 'filePath')"));

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }
        }

        [Test]
        public void MissingArgumentTest()
        {
            userInput = new UserInput();
            file = new FileOutput(@$"UnitTestSample.csv");

            var ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));
            Assert.That(ex.Message, Is.EqualTo("The Username was not found when creating file (Parameter 'userInput.UserName')"));

            userInput.UserName = "Bob";

            ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));
            Assert.That(ex.Message, Is.EqualTo("The Sample Name was not found when creating file (Parameter 'userInput.UserSampleName')"));

            userInput.UserSampleName = "Test";

            ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));
            Assert.That(ex.Message, Is.EqualTo("The UserSampleLength was not found when creating file (Parameter 'userInput.UserSampleLength')"));

            userInput.UserSampleLength = 10;

            ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));
            Assert.That(ex.Message, Is.EqualTo("The UserSampleWidth was not found when creating file (Parameter 'userInput.UserSampleWidth')"));

            userInput.UserSampleWidth = 10;

            ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));
            Assert.That(ex.Message, Is.EqualTo("The UserSampleThickness was not found when creating file (Parameter 'userInput.UserSampleThickness')"));

            userInput.UserSampleThickness = 10;

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }

        }

        [Test]
        public void CreateFileTest()
        {
            List<string[]> result = new List<string[]>();
            file = new FileOutput(@$"UnitTestSample.csv");
            userInput = new UserInput
            {
                UserName ="Bob",
                UserSampleName = "Test",
                UserSampleLength = 10,
                UserSampleWidth = 10,
                UserSampleThickness = 10
            };

            file.WriteUserInput(userInput);

            using(StreamReader sr = new StreamReader(@$"UnitTestSample.csv"))
{
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (!(currentLine == "")) {
                       result.Add(currentLine.Split(','));
                    }
                }
            }
            Assert.That(result[1][0], Is.EqualTo("Bob"));
            Assert.That(result[1][1], Is.EqualTo("Test"));
            Assert.That(result[1][3], Is.EqualTo("10"));
            Assert.That(result[1][4], Is.EqualTo("10"));
            Assert.That(result[1][5], Is.EqualTo("10"));

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }
        }

        [Test]
        public void HardwareInputTest()
        {
            List<string[]> result = new List<string[]>();
            file = new FileOutput(@$"UnitTestSample.csv");
            HardwareInput hardwareInput = new HardwareInput
            {
                Voltage = 10,
                Current = 9,
                Resistance = 8,
                Resistivity = 7,
                Temperature = 6,
                Time = DateTime.Now,
            };

            userInput = new UserInput
            {
                UserName = "Bob",
                UserSampleName = "Test",
                UserSampleLength = 10,
                UserSampleWidth = 10,
                UserSampleThickness = 10
            };

            file.WriteUserInput(userInput);
            file.WriteSampleOutput(hardwareInput);

            using (StreamReader sr = new StreamReader(@$"UnitTestSample.csv"))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (!(currentLine == ""))
                    {
                        result.Add(currentLine.Split(','));
                    }
                }
            }
            Assert.That(result[3][2], Is.EqualTo("10"));
            Assert.That(result[3][3], Is.EqualTo("9"));
            Assert.That(result[3][4], Is.EqualTo("8"));
            Assert.That(result[3][5], Is.EqualTo("7"));
            Assert.That(result[3][6], Is.EqualTo("6"));

            if (File.Exists(@$"UnitTestSample.csv"))
            {
                File.Delete(@$"UnitTestSample.csv");
            }
        }
    }
}
