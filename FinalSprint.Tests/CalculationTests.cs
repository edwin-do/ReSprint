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
            Assert.That(Calc.CalcResistance(5, 1), Is.EqualTo(5.0));
        }

        [Test]
        public void NegativeTest()
        {
            Assert.That(Calc.CalcResistance(5, -1), Is.EqualTo(5.0));
            Assert.That(Calc.CalcResistance(-5, 1), Is.EqualTo(5.0));
        }

        [Test]
        public void ZeroTest()
        {
            Assert.That(Calc.CalcResistance(5, 0), Is.EqualTo(-1));
            Assert.That(Calc.CalcResistance(0, 1), Is.EqualTo(0));
        }

    }

    public class CalcResistivityTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void NormalTest()
        {
            Assert.That(Calc.CalcResistivity(5, 4 * 1, 4), Is.EqualTo(5));
        }

        [Test]
        public void NegativeTest()
        {
            Assert.That(Calc.CalcResistivity(-5, 4 * 1, 4), Is.EqualTo(5));
            Assert.That(Calc.CalcResistivity(5, -4 * 1, 4), Is.EqualTo(5));
            Assert.That(Calc.CalcResistivity(5, 4 * -1, 4), Is.EqualTo(5));
            Assert.That(Calc.CalcResistivity(5, 4 * 1, -4), Is.EqualTo(5));
        }

        [Test]
        public void ZeroTest()
        {
            Assert.That(Calc.CalcResistivity(5, 4 * 1, 0), Is.EqualTo(-1));
            Assert.That(Calc.CalcResistivity(5, 4 * 0, 4), Is.EqualTo(0));
            Assert.That(Calc.CalcResistivity(5, 0 * 1, 4), Is.EqualTo(0));
            Assert.That(Calc.CalcResistivity(0, 4 * 1, 4), Is.EqualTo(0));
        }
    }

    public class CalcTemperatureTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void KTempRangeNormalTest()
        {
            Assert.That(Calc.CalcTemperature(5, 5, 0, -10), Is.EqualTo(5.125838016822272));
            Assert.That(Calc.CalcTemperature(5, 5, 0, 10), Is.EqualTo(5.1254196837892954));
            Assert.That(Calc.CalcTemperature(5, 5, 0, 500), Is.EqualTo(-126.56433004394469));
        }

        [Test]
        public void KTempOutOfRangeTest() 
        {
            Assert.That(Calc.CalcTemperature(5, 5, 0, 1400), Is.EqualTo(99999));
            Assert.That(Calc.CalcTemperature(5, 5, 0, -1400), Is.EqualTo(99999));
        }

        [Test]
        public void KTempZeroTest()
        {
            Assert.That(Calc.CalcTemperature(5, 0, 0, -10), Is.EqualTo(0.12583801682227208));
            Assert.That(Calc.CalcTemperature(5, 0, 0, 10), Is.EqualTo(0.12541968378929455));
            Assert.That(Calc.CalcTemperature(5, 0, 0, 500), Is.EqualTo(-131.56433004394469));

            Assert.That(Calc.CalcTemperature(0, 5, 0, -10), Is.EqualTo(5.0));
            Assert.That(Calc.CalcTemperature(0, 5, 0, 10), Is.EqualTo(5.0));
            Assert.That(Calc.CalcTemperature(0, 5, 0, 500), Is.EqualTo(-126.8058));
        }
    }
}