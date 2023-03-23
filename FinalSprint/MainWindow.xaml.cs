using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.SfSkinManager;
using NationalInstruments.NI4882;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using Syncfusion.Windows.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalSprint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Device currentSource;
        private Device nanoVoltmeter;
        private Device multimeter;

        private int range;
        private int scpiDevice;
        private int th_type;
        private double rate;
        private string aperture;
        private string compliance;
        private string currLevel;
        private string voltage_output;
        private string temp_output;

        private string name;
        private string sample;

        //Class objects
        private InputCommunication InputComm;
        private Calculation Calc;
        //private DataGenerator DatGen;

        //Member variables
        private bool capture_volt;
        private bool capture_temp;
        private double voltage;
        private double current;
        private double resistance;
        private double resistivity;
        private double area;
        private double width;
        private double thickness;
        private double length;
        private double t_volt;
        private double temperature;
        private double j_temp;

        private CancellationTokenSource _canceller_volt;
        private CancellationTokenSource _canceller_temp;

        //Timer
        DispatcherTimer main_timer;
        //DateTime Date;

        #region Fields
        private string currentVisualStyle;
        private string currentSizeMode;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current visual style.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CurrentVisualStyle
        {
            get
            {
                return currentVisualStyle;
            }
            set
            {
                currentVisualStyle = value;
                OnVisualStyleChanged();
            }
        }

        /// <summary>
        /// Gets or sets the current Size mode.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string CurrentSizeMode
        {
            get
            {
                return currentSizeMode;
            }
            set
            {
                currentSizeMode = value;
                OnSizeModeChanged();
            }
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;

            //Initialise Class objects
            Calc = new Calculation();
            InputComm = new InputCommunication();
            //DatGen = (DataGenerator)this.DataContext;

            //Initialise variables
            capture_volt = false;
            capture_temp = false;
            voltage = 0.0;
            current = 0.0;
            resistance = 0.0;
            resistivity = 0.0;
            area = 0.0;                 // 0.000003
            width = 0.0;                // 0.01
            thickness = 0.0;            // 0.0003
            length = 0.0;               // 0.04
            scpiDevice = 0;
            t_volt = 0.0;
            temperature = 0.0;
            j_temp = 0.0;
            th_type = 1;


            //Initialise timer for graph update
            main_timer = new DispatcherTimer();
            main_timer.Tick += main_timer_Tick;
            main_timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            main_timer.Start();

            int currentSecondaryAddress = 0;

            try
            {
                currentSource = new Device(0, 12, (byte)currentSecondaryAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }

            try
            {
                nanoVoltmeter = new Device(0, 7, (byte)currentSecondaryAddress);
                nanoVoltmeter.Write("*RST");
                nanoVoltmeter.Write("SENS:CHAN 1");
                nanoVoltmeter.Write("SENS:FUNC 'VOLT'");

                nanoVoltmeter.Write("SENS:VOLT:APER?");
                double y = 1000 / (Double.Parse(nanoVoltmeter.ReadString()));
                liveRate.Text = y.ToString("5");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }

            try
            {
                multimeter = new Device(0, 1, (byte)currentSecondaryAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }


        /// <summary>
        /// Windows Application Related Methods:
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExit(System.Windows.ExitEventArgs e)
        {
            try
            {
                currentSource.Write("OUTP OFF");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }

            try
            {
                nanoVoltmeter.Write("*RST");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                 //   YES

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CurrentVisualStyle = "Windows11Light";
            CurrentSizeMode = "Default";
        }                                   //   N/A

        /// <summary>
        /// On Visual Style Changed.
        /// </summary>
        /// <remarks></remarks>
        private void OnVisualStyleChanged()
        {
            VisualStyles visualStyle = VisualStyles.Default;
            Enum.TryParse(CurrentVisualStyle, out visualStyle);
            if (visualStyle != VisualStyles.Default)
            {
                SfSkinManager.ApplyStylesOnApplication = true;
                SfSkinManager.SetVisualStyle(this, visualStyle);
                SfSkinManager.ApplyStylesOnApplication = false;
            }
        }                                                       //   N/A

        /// <summary>
        /// On Size Mode Changed event.
        /// </summary>
        /// <remarks></remarks>
        private void OnSizeModeChanged()
        {
            SizeMode sizeMode = SizeMode.Default;
            Enum.TryParse(CurrentSizeMode, out sizeMode);
            if (sizeMode != SizeMode.Default)
            {
                SfSkinManager.ApplyStylesOnApplication = true;
                SfSkinManager.SetSizeMode(this, sizeMode);
                SfSkinManager.ApplyStylesOnApplication = false;
            }
        }                                                          //   N/A





        /// <summary>
        /// Interface Element Related Methods:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RangeDrop(object sender, SelectionChangedEventArgs e)
        {
            if (Range.SelectedIndex == 0)
            {
                range = 0;
            }

            if (Range.SelectedIndex == 1)
            {
                range = 1;
            }

            if (Range.SelectedIndex == 2)
            {
                range = 2;
            }

            if (Range.SelectedIndex == 3)
            {
                range = 3;
            }
        }                        //   N/A

        private void setCurrent(object sender, RoutedEventArgs e)
        {
            if (range == 0)
            {
                /// SCPI COMMAND CURR:RANG:AUTO ON
                try
                {
                    currentSource.Write("CURR:RANG:AUTO ON");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (range == 1)
            {
                /// SCPI COMMAND CURR:RANG:1e-3
                try
                {
                    currentSource.Write("CURR:RANG:1e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (range == 2)
            {
                /// SCPI COMMAND CURR:RANG:10e-3
                try
                {
                    currentSource.Write("CURR:RANG:10e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (range == 3)
            {
                /// SCPI COMMAND CURR:RANG:100e-3
                try
                {
                    currentSource.Write("CURR:RANG:100e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            double n1;
            if (Double.TryParse(currTextBox.Text, out n1)) { currLevel = currTextBox.Text; }
            else 
            { 
                MessageBox.Show("Invalid current, please enter a valid decimal number in mA.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double n2;
            if (Double.TryParse(compTextBox.Text, out n2)) { compliance = compTextBox.Text; }
            else { 
                MessageBox.Show("Invalid compliance voltage, please enter a valid decimal number in Volts.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                currentSource.Write("CURR:COMP " + compliance);
                currentSource.Write("CURR " + currLevel + "e-3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                     //   YES

        private void resetCurrent(object sender, RoutedEventArgs e)
        {
            try
            {
                currentSource.Write("*RST");
                currentSource.Write("CLE");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                   //   YES

        private void currTurnON(object sender, RoutedEventArgs e)
        {
            try
            {
                currentSource.Write("OUTP ON");
                if ((off_btn.IsEnabled = false) && (on_btn.IsEnabled = true))
                {
                    off_btn.IsEnabled = true;
                    on_btn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                     //   YES

        private void currTurnOFF(object sender, RoutedEventArgs e)
        {
            try
            {
                currentSource.Write("OUTP OFF");
                if ((off_btn.IsEnabled = true) && (on_btn.IsEnabled = false))
                {
                    on_btn.IsEnabled = true;
                    off_btn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                    //   YES

        /*
                private void Integration_Rate_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    if (Integration_Rate.SelectedIndex == 0)   
                    {
                        rate = "0.01";
                    }

                    if (Integration_Rate.SelectedIndex == 1)                        // CHANGE THIS
                    {
                        rate = "0.1";
                    }

                    if (Integration_Rate.SelectedIndex == 2)
                    {
                        rate = "1";
                    }

                    if (Integration_Rate.SelectedIndex == 3)
                    {
                        rate = "5";
                    }
                }
        */

        private void setVolt(object sender, RoutedEventArgs e)
        {
            double r;
            if (Double.TryParse(rateTextBox.Text, out r)) { rate = r; }
            else { 
                MessageBox.Show("Invalid acquisition rate, please enter a valid decimal number in Hz.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                aperture = Calc.CalcAperture(rate);
                nanoVoltmeter.Write("SENS:VOLT:APER " + aperture);

                nanoVoltmeter.Write("SENS:VOLT:APER?");
                double x = 1 / (Double.Parse(nanoVoltmeter.ReadString())); // 1000/xx
                liveRate.Text = x.ToString("G5");       // F5
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                        //   YES

        private void startCap(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nameTextBox.Text)) 
            { 
                name = nameTextBox.Text; 
            }

            else 
            { 
                MessageBox.Show("Please enter your name to continue.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (sampleTextBox.Text != null) { sample = sampleTextBox.Text; }
            else { MessageBox.Show("Please enter the sample name to continue.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double num1;
            if (Double.TryParse(lengthTextBox.Text, out num1)) { length = num1 / 1000; }
            else { MessageBox.Show("Invalid length, please enter a valid decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double num2;
            if (Double.TryParse(widthTextBox.Text, out num2)) { width = num2 / 1000; }
            else { MessageBox.Show("Invalid width, please enter a valid decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double num3;
            if (Double.TryParse(thicknessTextBox.Text, out num3)) { thickness = num3 / 1000; }
            else { MessageBox.Show("Invalid thickness, please enter a valid decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            area = width * thickness;

            try
            {
                startCap_volt();
                startCap_temp();

                if ((startCapBtn.IsEnabled = true) && (stopCapBtn.IsEnabled = false))
                {
                    startCapBtn.IsEnabled = false;
                    stopCapBtn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }                       //   YES

        private async void startCap_volt()
        {
            //startCapBtn.Enabled = false;
            //stopCapBtn.Enabled = true;
            capture_volt = true;
            //Date = DateTime.Now;
            _canceller_volt = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    //device.Write("FETC?");
                    //voltage_output = device.ReadString();
                    //voltVals.Add(voltage_output);

                    //Thread.Sleep(50);  (DISABLING PROVED TO BE THE FASTEST)
                    Capture();
                    if (_canceller_volt.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller_volt.Dispose();
            //startCapBtn.Enabled = true;
            //stopCapBtn.Enabled = false;
        }                                                        //   N/A

        private async void startCap_temp()
        {
            //startCapBtn.Enabled = false;
            //stopCapBtn.Enabled = true;
            capture_temp = true;
            //Date = DateTime.Now;
            _canceller_temp = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    //device.Write("FETC?");
                    //voltage_output = device.ReadString();
                    //voltVals.Add(voltage_output);

                    //Thread.Sleep(50);  (DISABLING PROVED TO BE THE FASTEST)
                    CaptureTemp();
                    if (_canceller_temp.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller_temp.Dispose();
            //startCapBtn.Enabled = true;
            //stopCapBtn.Enabled = false;
        }                                                        //   N/A

        private void stopCap(object sender, RoutedEventArgs e)
        {
            try
            {
                _canceller_volt.Cancel();
                capture_volt = false;
                _canceller_temp.Cancel();
                capture_temp = false;

                if ((startCapBtn.IsEnabled = false) && (stopCapBtn.IsEnabled = true))
                {
                    startCapBtn.IsEnabled = true;
                    stopCapBtn.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no experiment in progress. Please restart the application if needed.\n\n" + ex.Message);
                return;
            }
        }                        //   YES

        private void Capture()
        {
            //Get voltage and current values
            //voltage = InputComm.GetVoltage();
            //nanoVoltmeter.Write("SENS:func 'volt'");
            //nanoVoltmeter.Write("SENS:chan 1; :read?");

            try
            {
                if (nanoVoltmeter != null)
                {
                    nanoVoltmeter.Write("read?");
                    voltage_output = nanoVoltmeter.ReadString();
                }
                else
                {
                    MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }


            //nanoVoltmeter.Write("SENS:CH");
            voltage = Convert.ToDouble(voltage_output);
            //Debug.WriteLine(voltage);

            /*   nanoVoltmeter.Write("SENS:CHAN 2");
               nanoVoltmeter.Write("SENS:FUNC 'TEMP'");
               nanoVoltmeter.Write("read?");

               temp_output = nanoVoltmeter.ReadString();
               t_volt = (-1) * Convert.ToDouble(temp_output);
               Debug.WriteLine(t_volt);*/


            /*    multimeter.Write("*IDN?");
                temp_output = multimeter.ReadString();
                t_volt = (-1) * Convert.ToDouble(temp_output);
                Debug.WriteLine(t_volt);

                //current = InputComm.GetCurrent();
                current = Convert.ToDouble(currLevel)/1000;

                //Calculate resistance and resistivity values
                resistance = Calc.CalcResistance(voltage, current);
                resistivity = Calc.CalcResistivity(resistance, area, length);*/

        }                                                        //   YES

        private void CaptureTemp()
        {
            //Get voltage and current values
            //voltage = InputComm.GetVoltage();
            //nanoVoltmeter.Write("SENS:func 'volt'");
            //nanoVoltmeter.Write("SENS:chan 1; :read?");

            /*   nanoVoltmeter.Write("read?");


               //nanoVoltmeter.Write("SENS:CH");
               voltage_output = nanoVoltmeter.ReadString();
               voltage = (-1) * Convert.ToDouble(voltage_output);
               Debug.WriteLine(voltage);*/

            /*   nanoVoltmeter.Write("SENS:CHAN 2");
               nanoVoltmeter.Write("SENS:FUNC 'TEMP'");
               nanoVoltmeter.Write("read?");

               temp_output = nanoVoltmeter.ReadString();
               t_volt = (-1) * Convert.ToDouble(temp_output);
               Debug.WriteLine(t_volt);*/

            try
            {
                if (multimeter != null)
                {
                    multimeter.Write("*IDN?");
                    temp_output = multimeter.ReadString();
                }
                else
                {
                    MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }

            t_volt = Convert.ToDouble(temp_output);
            //Debug.WriteLine(t_volt);

            //current = InputComm.GetCurrent();
            current = Convert.ToDouble(currLevel) / 1000;          ///////// CHECK THIS (test?)

            //Calculate resistance, resistivity, and temperature values
            resistance = Calc.CalcResistance(voltage, current);
            resistivity = Calc.CalcResistivity(resistance, area, length);
            temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, temperature);
            Debug.WriteLine(temperature);
        }                                                    //   YES

        private void ThTypeDrop(object sender, SelectionChangedEventArgs e)
        {
            if (Range.SelectedIndex == 0)
            {
                th_type = 1;
            }

            else if (Range.SelectedIndex == 1)
            {
                th_type = 2;
            }
        }                       //   N/A

        private void set_jTemp(object sender, RoutedEventArgs e)
        {
            double m;
            if (Double.TryParse(jTempTextBox.Text, out m)) { j_temp = m; }
            else { MessageBox.Show("Invalid temperature, please enter a valid decimal number in °C.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }                      //   YES

        //private void timer_Tick(object? sender, object e)
        //{
        //    //Pass values to DataGenerator
        //    if (capture)
        //    {
        //        DatGen.AddData(new Data(Date, current, voltage, resistivity));
        //        Date = Date.Add(TimeSpan.FromMilliseconds(50));
        //    }
        //}

        private void main_timer_Tick(object sender, object e)
        {
            voltage_out.Text = voltage.ToString();
            temp_out.Text = t_volt.ToString();
            current_out.Text = currTextBox.Text;
            ohm_out.Text = resistance.ToString();
            rho_out.Text = resistivity.ToString();

            //if (capture)
            //{
            //    DatGen.AddData();
            //}
        }                                                   //   NO TESTS (?)

        private void SCPI_current(object sender, RoutedEventArgs e)
        {
            try
            {
                scpiDevice = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to device. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
            }
        }                    //   YES

        private void SCPI_voltage(object sender, RoutedEventArgs e)
        {
            try
            {
                scpiDevice = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to device. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
            }
        }                    //   YES

        private void SCPI_temp(object sender, RoutedEventArgs e)
        {
            try
            {
                scpiDevice = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to device. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
            }
        }                       //   YES

        private void SCPI_write(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SCPI_output.Text != null)
                {
                    if (scpiDevice == 1)
                    {
                        currentSource.Write(SCPI_input.Text);
                    }
                    else if (scpiDevice == 2)
                    {
                        nanoVoltmeter.Write(SCPI_input.Text);
                    }
                    else if (scpiDevice == 3)
                    {
                        multimeter.Write(SCPI_input.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a command.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid command.\n\n" + ex.Message);
            }
        }                      //   YES

        private void SCPI_read(object sender, RoutedEventArgs e)
        {
            try
            {
                if (scpiDevice == 1)
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + currentSource.ReadString();
                }
                else if (scpiDevice == 2)
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + nanoVoltmeter.ReadString();
                }
                else if (scpiDevice == 3)
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + multimeter.ReadString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nothing to read. Possibly an invalid command or an instruction command.\n\n" + ex.Message);
            }
        }                       //   YES

    }

}