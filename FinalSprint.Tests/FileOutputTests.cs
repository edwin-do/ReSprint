using FinalSprint.src.Classes;
using System.Diagnostics;

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
        }

        [Test]
        public void InvaildFileName()
        {
            var ex = Assert.Throws<ArgumentException>(() => file = new FileOutput(@$"UnitTestSample"));
            Assert.That(ex.Message, Is.EqualTo("File Path does not have a .csv extnesion (Parameter 'filePath')"));

        }

        [Test]
        public void MissingUsernameTest()
        {
            
            file = new FileOutput(@$"{userInput.UserName}_{userInput.UserSampleName}_{DateTime.Now.ToString("yyyy-MM-dd-hh-mm")}.csv");

            var ex = Assert.Throws<ArgumentException>(() => file.WriteUserInput(userInput));

            Assert.That(ex.Message, Is.EqualTo("The Username was not found when creating file (Parameter 'userInput.UserName')"));
        }
    }
}
