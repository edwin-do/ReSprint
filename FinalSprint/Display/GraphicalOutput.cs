namespace ReSprint
{
    using Syncfusion.UI.Xaml.Charts;
    using System;

    public class GraphicalOutput
    {
        private readonly string hardwareInputHeader = "Time, Voltage, Current, Resistance, Resistivity, Temperture\n";
        public void GraphInit()
        {
            SfChart graph = new SfChart();
            CategoryAxis primaryAxis = new CategoryAxis();
            graph.PrimaryAxis = primaryAxis;
            NumericalAxis secondaryAxis = new NumericalAxis();
            graph.SecondaryAxis = secondaryAxis;
        }
        
        public void GraphTimeVResistivity()
        {
            /*
            if (string.IsNullOrEmpty(hardwareInput.Time))
                throw new ArgumentOutOfRangeException("hardwareInput.Time", "Value cannot be empty.");
            if (hardwareInput.Resistivity < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");
            graph.Header = "Time V Resistivity";
            */
        }

        public void GraphTimeVResistance()
        {   /*
            if (string.IsNullOrEmpty(hardwareInput.Time))
                throw new ArgumentOutOfRangeException("hardwareInput.Time", "Value cannot be empty.");
            if (hardwareInput.Resistance < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            graph.Header = "Time V Resistance";
            */
        }

        public void GraphVoltageVResistance()
        {
            /*
            if (hardwareInput.Voltage < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Voltage", "Value cannot be negative.");
            if (hardwareInput.Resistance < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            graph.Header = "Voltage V Resistance";
            */
        }
        public void GraphVoltageVResistivity()
        {
            /*
            if (hardwareInput.Voltage < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Voltage", "Value cannot be negative.");
            if (hardwareInput.Resistivity < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");
            graph.Header = "Voltage V Resistivity";
            */
        }
        public void GraphTemperatureVResistance()
        {
            /*
            if (string.IsNullOrEmpty(hardwareInput.Temperature))
                throw new ArgumentOutOfRangeException("hardwareInput.Temperature", "Value cannot be empty.");
            if (hardwareInput.Resistance < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistance", "Value cannot be negative.");
            graph.Header = "Temperature V Resistance";
            */
        }
        public void GraphTemperatureVResistivity()
        {
            /*
            if (string.IsNullOrEmpty(hardwareInput.Temperature))
                throw new ArgumentOutOfRangeException("hardwareInput.Temperature", "Value cannot be empty.");
            if (hardwareInput.Resistivity < 0)
                throw new ArgumentOutOfRangeException("hardwareInput.Resistivity", "Value cannot be negative.");
            graph.Header = "Temperature V Resistivity";
            */
        }
    }

}
