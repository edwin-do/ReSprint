using FinalSprint.src.Classes;

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

        [Test]
        public void RTempRangeNormalTest()
        {
            Assert.That(Calc.CalcTemperature(5, 5, 1, -10), Is.EqualTo(5.9445851949746746));
            Assert.That(Calc.CalcTemperature(5, 5, 1, 250), Is.EqualTo(19.082167840000828));
            Assert.That(Calc.CalcTemperature(5, 5, 1, 1200), Is.EqualTo(-76.219013085514518));
            Assert.That(Calc.CalcTemperature(5, 5, 1, 1700), Is.EqualTo(34031.589915645673));
        }

        [Test]
        public void RTempOutOfRangeTest()
        {
            Assert.That(Calc.CalcTemperature(5, 5, 1, 2000), Is.EqualTo(99999));
            Assert.That(Calc.CalcTemperature(5, 5, 1, -1400), Is.EqualTo(99999));
        }

        [Test]
        public void RTempZeroTest()
        {
            Assert.That(Calc.CalcTemperature(5, 0, 1, -10), Is.EqualTo(0.94458519497467563));
            Assert.That(Calc.CalcTemperature(5, 0, 1, 250), Is.EqualTo(14.082167840000828));
            Assert.That(Calc.CalcTemperature(5, 0, 1, 1200), Is.EqualTo(-81.219013085514518));
            Assert.That(Calc.CalcTemperature(5, 0, 1, 1700), Is.EqualTo(34026.589915645673));

            Assert.That(Calc.CalcTemperature(0, 5, 1, -10), Is.EqualTo(5.0));
            Assert.That(Calc.CalcTemperature(0, 5, 1, 250), Is.EqualTo(18.345845050000001));
            Assert.That(Calc.CalcTemperature(0, 5, 1, 1200), Is.EqualTo(-76.995994159999995));
            Assert.That(Calc.CalcTemperature(0, 5, 1, 1700), Is.EqualTo(34066.778359999997));
        }

        [Test]
        public void ThTypeOutofRangeTest()
        {
            Assert.That(Calc.CalcTemperature(5, 5, -1, 10), Is.EqualTo(99999));
            Assert.That(Calc.CalcTemperature(5, 5, 3, 10), Is.EqualTo(99999));
        }
    }

    public class CalcApertureTests
    {
        private Calculation Calc = new Calculation();

        [Test]
        public void CalcApertureNormalTest() 
        {
            Assert.That(Calc.CalcAperture(1), Is.EqualTo("1"));
            Assert.That(Calc.CalcAperture(100), Is.EqualTo("0.01"));
            Assert.That(Calc.CalcAperture(600), Is.EqualTo("0.00167"));
            Assert.That(Calc.CalcAperture(6000), Is.EqualTo("0.00017"));
        }

        [Test]
        public void CalcApertureGEMaxTest()
        {
            Assert.That(Calc.CalcAperture(6000), Is.EqualTo("0.00017"));
        }

        [Test]
        public void CalcApertureLenTest() 
        {
            Assert.That(Calc.CalcAperture(1).Length, Is.LessThanOrEqualTo(7));
            Assert.That(Calc.CalcAperture(100).Length, Is.LessThanOrEqualTo(7));
            Assert.That(Calc.CalcAperture(600).Length, Is.LessThanOrEqualTo(7));
            Assert.That(Calc.CalcAperture(6000).Length, Is.LessThanOrEqualTo(7));
        }

        [Test]
        public void CalcApertureEdgeRateTest()
        {
            Assert.That(Calc.CalcAperture(-1), Is.LessThanOrEqualTo("INVAILDRATE"));
            Assert.That(Calc.CalcAperture(0), Is.LessThanOrEqualTo("INVAILDRATE"));
        }
    }
}