using FinalSprint.src.Classes;
using FinalSprint;

namespace FinalSprint.Tests
{
    public class CheckSampleRateTests
    {
        private InstrumentInputValidation Valid = new InstrumentInputValidation();
        [Test]
        public void CheckSampleRateNormalTest()
        {
            Assert.IsTrue(Valid.CheckSampleRate(100));
        }

        [Test]
        public void CheckSampleRateNegativeTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckSampleRate(-100));
            Assert.That(ex.Message, Is.EqualTo("Invalid acquisition rate, please enter a valid decimal number in Hz."));
        }

        [Test]
        public void CheckSampleRateZeroTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckSampleRate(0));
            Assert.That(ex.Message, Is.EqualTo("Invalid acquisition rate, please enter a valid decimal number in Hz."));
        }

        [Test]
        public void CheckSampleRateOutOfRangeTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckSampleRate(601));
            Assert.That(ex.Message, Is.EqualTo("Invalid acquisition rate, please enter a valid decimal number in Hz."));

            ex = Assert.Throws<ArgumentException>(() => Valid.CheckSampleRate(0.1));
            Assert.That(ex.Message, Is.EqualTo("Invalid acquisition rate, please enter a valid decimal number in Hz."));
        }
    }

    public class CheckCurrentLevelTests
    {
        private InstrumentInputValidation Valid = new InstrumentInputValidation();
        [Test]
        public void CheckCurrentLevelNormalTest()
        {
            Assert.IsTrue(Valid.CheckCurrentLevel(10));
        }

        [Test]
        public void CheckCurrentLevelNegTest()
        {
            Assert.IsTrue(Valid.CheckCurrentLevel(-10));
        }

        [Test]
        public void CheckCurrentLevelZeroTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckCurrentLevel(0));
            Assert.That(ex.Message, Is.EqualTo("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA."));
        }

        [Test]
        public void CheckCurrentLevelOutOfRangeTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckCurrentLevel(106));
            Assert.That(ex.Message, Is.EqualTo("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA."));

            ex = Assert.Throws<ArgumentException>(() => Valid.CheckCurrentLevel(-106));
            Assert.That(ex.Message, Is.EqualTo("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA."));
        }
    }

    public class CheckComplianceTests
    {
        private InstrumentInputValidation Valid = new InstrumentInputValidation();
        [Test]
        public void CheckComplianceNormalTest()
        {
            Assert.IsTrue(Valid.CheckCompliance(10));
        }

        [Test]
        public void CheckComplianceNegTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckCompliance(-1));
            Assert.That(ex.Message, Is.EqualTo("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V."));
        }

        [Test]
        public void CheckComplianceZeroTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckCompliance(0));
            Assert.That(ex.Message, Is.EqualTo("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V."));
        }

        [Test]
        public void CheckComplianceOutOfRangeTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.CheckCompliance(0.01));
            Assert.That(ex.Message, Is.EqualTo("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V."));

            ex = Assert.Throws<ArgumentException>(() => Valid.CheckCompliance(106));
            Assert.That(ex.Message, Is.EqualTo("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V."));
        }
    }

    public class CheckJuncTemperatureTests
    {
        private InstrumentInputValidation Valid = new InstrumentInputValidation();
        [Test]
        public void CheckJuncTemperatureNormalTest()
        {
            Assert.IsTrue(Valid.checkJuncTemperature(10));
        }

        [Test]
        public void CheckJuncTemperatureNegTest()
        {
            Assert.IsTrue(Valid.checkJuncTemperature(-10)); 
        }

        [Test]
        public void CheckJuncTemperatureZeroTest()
        {
            Assert.IsTrue(Valid.checkJuncTemperature(0));
        }

        [Test]
        public void CheckJuncTemperatureOutOfRangeTest()
        {
            var ex = Assert.Throws<ArgumentException>(() => Valid.checkJuncTemperature(10000));
            Assert.That(ex.Message, Is.EqualTo("Invalid temperature, please enter a valid decimal number in °C."));

            ex = Assert.Throws<ArgumentException>(() => Valid.checkJuncTemperature(-10000));
            Assert.That(ex.Message, Is.EqualTo("Invalid temperature, please enter a valid decimal number in °C."));
        }
    }
}
