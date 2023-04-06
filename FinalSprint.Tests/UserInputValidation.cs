using FinalSprint.src.Classes;
using FinalSprint;
using System.Windows.Controls;
using System.Windows;
using NuGet.Frameworks;

namespace FinalSprint.Tests
{
     public class UserInputTests
    {
        UserInputValidation userValidation = new UserInputValidation();
        UserInput userInput = new UserInput();

        string userName = "Bob";
        string sampleName = "Test";
        double length = 10;
        double width = 10;
        double thickness = 10;

        [Test]
        public void checkUserInputValidTest()
        {
            userInput = userValidation.checkUserInput(userName, sampleName, length, width, thickness);
            Assert.That(userInput.UserName, Is.EqualTo("Bob"));
            Assert.That(userInput.UserSampleName, Is.EqualTo("Test"));
        }

        [Test]
        public void checkUserInputInvalidTest()
        {
            userInput = userValidation.checkUserInput(null, sampleName, length, width, thickness);
            Assert.That(userInput.UserName, Is.EqualTo(null));
            Assert.That(userInput.UserSampleName, Is.EqualTo(null));
        }
    }


    public class ValidateUserDataTests
    {
        UserInputValidation userValidation = new UserInputValidation();

        string userName = "Bob";
        string sampleName = "Test";
        double length = 10;
        double width = 10;
        double thickness = 10;

        [Test]
        public void DataNormalTest() 
        {
            Assert.IsTrue(userValidation.validateUserData(userName, sampleName, length, width, thickness));
        }

        [Test]
        public void UserNameNullTest()
        {
            Assert.IsFalse(userValidation.validateUserData(null, sampleName, length, width, thickness));
        }

        [Test]
        public void SampleNameNullTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, null, length, width, thickness));
        }

        [Test]
        public void SampleLengthZeroTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, 0, width, thickness));
        }

        [Test]
        public void SampleLengthNegTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, -10, width, thickness));
        }

        [Test]
        public void SampleWidthZeroTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, length, 0, thickness));
        }

        [Test]
        public void SampleWidthNegTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, length, -10, thickness));
        }

        [Test]
        public void SampleThicknessZeroTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, length, width, 0));
        }

        [Test]
        public void SampleThicknessNegTest()
        {
            Assert.IsFalse(userValidation.validateUserData(userName, sampleName, length, width, -10));
        }
    }
}
