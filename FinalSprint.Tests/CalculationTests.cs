using FinalSprint.src.Classes;
using System.Diagnostics;

namespace FinalSprint.Tests
{
    public class CalcReistanceTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void NormalTest()
        {
            Assert.IsTrue(Calc.CalcResistance(5, 1) == 5);
        }

        [Test]
        public void NegativeTest()
        {
            Assert.IsTrue(Calc.CalcResistance(5, -1) == 5);
            Assert.IsTrue(Calc.CalcResistance(-5, 1) == 5);
        }

        [Test]
        public void ZeroTest()
        {
            Assert.IsTrue(Calc.CalcResistance(5, 0) == -1);
            Assert.IsTrue(Calc.CalcResistance(0, 5) == 0);
        }

    }

    public class CalcResistivityTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void NormalTest()
        {
            Assert.IsTrue(Calc.CalcResistivity(5, 4 * 1, 4) == 5);
        }

        [Test]
        public void NegativeTest()
        {
            Assert.IsTrue(Calc.CalcResistivity(-5, 4 * 1, 4) == 5);
            Assert.IsTrue(Calc.CalcResistivity(-5, -4 * 1, 4) == 5);
            Assert.IsTrue(Calc.CalcResistivity(-5, 4 * -1, 4) == 5);
            Assert.IsTrue(Calc.CalcResistivity(-5, 4 * 1, -4) == 5);
        }

        [Test]
        public void ZeroTest()
        {
            Assert.IsTrue(Calc.CalcResistivity(5, 4 * 1, 0) == -1);
            Assert.IsTrue(Calc.CalcResistivity(0, 4 * 1, 4) == 0);
            Assert.IsTrue(Calc.CalcResistivity(5, 0 * 1, 4) == 0);
            Assert.IsTrue(Calc.CalcResistivity(5, 4 * 0, 4) == 0);
        }
    }

    public class CalcTemperatureTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void KTempRangeNormalTest()
        {
            Assert.That(Calc.CalcTemperature(5, 5, 0, -10), Is.EqualTo(5.125838016822272));
            Debug.WriteLine(Calc.CalcTemperature(5, 5, 0, -10));
            Debug.WriteLine(Calc.CalcTemperature(5, 5, 0, 10));
            Debug.WriteLine(Calc.CalcTemperature(5, 5, 0, 500));
            Debug.WriteLine(Calc.CalcTemperature(5, 5, 0, 1400));
            Debug.WriteLine(Calc.CalcTemperature(5, 5, 0, -1400));

            Debug.WriteLine(Calc.CalcTemperature(0, 5, 0, 10));
            Debug.WriteLine(Calc.CalcTemperature(5, 0, 0, 10));
        }
    }
}