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
        private string rate;
        private string compliance;
        private string currLevel;
        private string voltage_output;
        private string temp_output;

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
        private double length;
        private double temperature;
        private int scpiDevice;
        private string SCPI_command;

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
            temperature = 0.0;
            current = 0.0;
            resistance = 0.0;
            resistivity = 0.0;
            area = 0.000003;
            length = 0.04;
            scpiDevice = 0;


            //Initialise timer for graph update
            main_timer = new DispatcherTimer();
            main_timer.Tick += main_timer_Tick;
            main_timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            main_timer.Start();

            int currentSecondaryAddress = 0;

            currentSource = new Device(0, 12, (byte)currentSecondaryAddress);
            nanoVoltmeter = new Device(0, 7, (byte)currentSecondaryAddress);
            multimeter = new Device(0, 1, (byte)currentSecondaryAddress);

            // currentSource.Write("*RST");
            // currentSource.Write("CLE");

            //nanoVoltmeter.Write("*RST");

            nanoVoltmeter.Write("SENS:CHAN 1");
            nanoVoltmeter.Write("SENS:FUNC 'VOLT'");
        }
        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CurrentVisualStyle = "Windows11Light";
            CurrentSizeMode = "Default";
        }
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
        }

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
        }

        private void currText(object sender, TextChangedEventArgs e)
        {
            currLevel = currTextBox.Text;
        }

        private void compText(object sender, TextChangedEventArgs e)
        {
            compliance = compTextBox.Text;
        }

        private void RangeDrop(object sender, SelectionChangedEventArgs e)  // CHANGE METHOD
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
        }

        private void setCurr(object sender, RoutedEventArgs e)
        {
            if (range == 0)                                                 // RANGE CHANGE METHOD
            {
                /// SCPI COMMAND CURR:RANG:AUTO ON
                try
                {
                    currentSource.Write("CURR:RANG:AUTO ON");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                }
            }

            currentSource.Write("CURR:COMP " + compliance);
            currentSource.Write("CURR " + currLevel + "e-3");
        }

        private void currTurnON(object sender, RoutedEventArgs e)
        {
            currentSource.Write("OUTP ON");
        }

        private void currTurnOFF(object sender, RoutedEventArgs e)
        {
            currentSource.Write("OUTP OFF");
        }

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

        private void setVolt(object sender, RoutedEventArgs e)
        {
            nanoVoltmeter.Write("SENS:VOLT:NPLC " + rate);                  // CHANGE THIS
        }


        private void startCap(object sender, RoutedEventArgs e)
        {
            startCap_volt();
            startCap_temp();
        }


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
        }

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
        }

        private void stopCap(object sender, RoutedEventArgs e)
        {
            _canceller_volt.Cancel();
            capture_volt = false;
            _canceller_temp.Cancel();
            capture_temp = false;
        }

        private void Capture()
        {
            //Get voltage and current values
            //voltage = InputComm.GetVoltage();
            //nanoVoltmeter.Write("SENS:func 'volt'");
            //nanoVoltmeter.Write("SENS:chan 1; :read?");

            nanoVoltmeter.Write("read?");


            //nanoVoltmeter.Write("SENS:CH");
            voltage_output = nanoVoltmeter.ReadString();
            voltage = Convert.ToDouble(voltage_output);
            //Debug.WriteLine(voltage);

            /*   nanoVoltmeter.Write("SENS:CHAN 2");
               nanoVoltmeter.Write("SENS:FUNC 'TEMP'");
               nanoVoltmeter.Write("read?");

               temp_output = nanoVoltmeter.ReadString();
               temperature = (-1) * Convert.ToDouble(temp_output);
               Debug.WriteLine(temperature);*/


        /*    multimeter.Write("*IDN?");
            temp_output = multimeter.ReadString();
            temperature = (-1) * Convert.ToDouble(temp_output);
            Debug.WriteLine(temperature);

            //current = InputComm.GetCurrent();
            current = Convert.ToDouble(currLevel)/1000;

            //Calculate resistance and resistivity values
            resistance = Calc.CalcResistance(voltage, current);
            resistivity = Calc.CalcResistivity(resistance, area, length);*/

        }

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
               temperature = (-1) * Convert.ToDouble(temp_output);
               Debug.WriteLine(temperature);*/


            multimeter.Write("*IDN?");
            temp_output = multimeter.ReadString();
            temperature = Convert.ToDouble(temp_output);
            //Debug.WriteLine(temperature);

            //current = InputComm.GetCurrent();
            current = Convert.ToDouble(currLevel) / 1000;

            //Calculate resistance and resistivity values
            resistance = Calc.CalcResistance(voltage, current);
            resistivity = Calc.CalcResistivity(resistance, area, length);

        }


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
            temp_out.Text = temperature.ToString();
            current_out.Text = currLevel;
            ohm_out.Text = resistance.ToString();
            rho_out.Text = resistivity.ToString();

            //if (capture)
            //{
            //    DatGen.AddData();
            //}
        }

        private void SCPI_current(object sender, RoutedEventArgs e)
        {
            scpiDevice = 1;
        }

        private void SCPI_voltage(object sender, RoutedEventArgs e)
        {
            scpiDevice = 2;
        }

        private void SCPI_temp(object sender, RoutedEventArgs e)
        {
            scpiDevice = 3;
        }

        private void SCPI_write(object sender, RoutedEventArgs e)
        {
            if (scpiDevice == 1)
            {
                currentSource.Write(SCPI_command);
            }
            else if (scpiDevice == 2)
            {
                nanoVoltmeter.Write(SCPI_command);
            }
            else if (scpiDevice == 3)
            {
                multimeter.Write(SCPI_command);
            }
        }

        private void SCPI_read(object sender, RoutedEventArgs e)
        {
            if (scpiDevice == 1)
            {
                SCPI_output.Text = currentSource.ReadString();
            }
            else if (scpiDevice == 2)
            {
                SCPI_output.Text = nanoVoltmeter.ReadString();
            }
            else if (scpiDevice == 3)
            {
                SCPI_output.Text = multimeter.ReadString();
            }
        }

        private void SCPI_textInput(object sender, TextChangedEventArgs e)
        {
            SCPI_command = SCPI_input.Text;
        }
    }

}
