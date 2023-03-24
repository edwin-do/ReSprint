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
using System.ComponentModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using FinalSprint.Hubs;
using FinalSprint.Display;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Cors;
using System.IO;
using Syncfusion.UI.Xaml.ScrollAxis;

namespace FinalSprint
{
    public partial class MainWindow : Window
    {
        private Device currentSource;
        private Device nanoVoltmeter;
        private Device multimeter;

        private ChatHub _chatHub;
        private Calculation Calc;
        private FileOutput File;

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

        private string currentVisualStyle;
        private string currentSizeMode;

        private HardwareInput hardwareInput = new HardwareInput();
        private UserInput userInput;

        ObservableCollection<Data> HardwareData = new ObservableCollection<Data>();

        private HubConnection _connection;
        public MainWindow()
        {
            _chatHub = new ChatHub(this);

            InitializeComponent();

            Calc = new Calculation();
            OutputTable.ItemsSource = HardwareData;
            Chart.Series[0].ItemsSource = HardwareData;
            Chart.Series[1].ItemsSource = HardwareData;
            Chart.Series[2].ItemsSource = HardwareData;
            Chart.Series[3].ItemsSource = HardwareData;
            Chart.Series[4].ItemsSource = HardwareData;
            Chart_vs.Series[0].ItemsSource = HardwareData;

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
                LiveRate.Text = y.ToString("5");
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
        }

        private bool check()
        {
            if (string.IsNullOrEmpty(OperatorName.Text))
            {
                MessageBox.Show("Please Operator Name in the textbox.");
                return false;

            }
            else if (string.IsNullOrEmpty(SampleName.Text))
            {
                MessageBox.Show("Please Sample Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SampleLength.Text))
            {
                MessageBox.Show("Please SampleWidth Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SampleWidth.Text))
            {
                MessageBox.Show("Please SampleLength Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SampleThickness.Text))
            {
                MessageBox.Show("Please SamplingRate Name in the textbox.");
                return false;
            }

            userInput = new UserInput
            {
                UserName = OperatorName.Text.ToString(),
                UserSampleName = SampleName.Text.ToString(),
                UserSampleLength = double.Parse(SampleLength.Text),
                UserSampleWidth = double.Parse(SampleWidth.Text),
                UserSampleThickness = double.Parse(SampleThickness.Text)
            };
            string SampleDate = DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.ToShortTimeString();
            File = new FileOutput(@$"{userInput.UserName}_{userInput.UserSampleName}_{SampleDate}.csv");
            return true;
        }

        public void TurnCurrentOn()
        {
            Dispatcher.Invoke(() =>
            {
                // call SCPI connect to 6220
                if (currentSource != null)
                {
                    currentSource.Dispose();
                }

                int currentSecondaryAddress = 0;

                currentSource = new Device(0, 12, (byte)currentSecondaryAddress);

                currentSource.Write("OUTP ON");
            });
        }

        public void TurnCurrentOff()
        {
            Dispatcher.Invoke(() =>
            {
                // call SCPI connect to 6220
                if (currentSource != null)
                {
                    currentSource.Dispose();
                }

                int currentSecondaryAddress = 0;

                currentSource = new Device(0, 12, (byte)currentSecondaryAddress);

                currentSource.Write("OUTP OFF");
            });
        }

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
        }

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
            if (Double.TryParse(CurrentLevel.Text, out n1)) { currLevel = CurrentLevel.Text; }
            else
            {
                MessageBox.Show("Invalid current, please enter a valid decimal number in mA.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double n2;
            if (Double.TryParse(CurrentLevel.Text, out n2)) { compliance = CurrentLevel.Text; }
            else
            {
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
        }

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
        }

        private void CurrentPower(object sender, RoutedEventArgs e)
        {
            try
            {
                OnButton.IsEnabled = !OnButton.IsEnabled;
                OffButton.IsEnabled = !OffButton.IsEnabled;
                
                if (OnButton.IsEnabled) { currentSource.Write("OUTP ON"); }
                if (OffButton.IsEnabled) { currentSource.Write("OUTP OFF"); }
                currentSource.Write("OUTP ON");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }

        private void SetVolt(object sender, RoutedEventArgs e)
        {
            double r;
            if (Double.TryParse(SampleRate.Text, out r)) { rate = r; }
            else
            {
                MessageBox.Show("Invalid acquisition rate, please enter a valid decimal number in Hz.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                aperture = Calc.CalcAperture(rate);
                nanoVoltmeter.Write("SENS:VOLT:APER " + aperture);

                nanoVoltmeter.Write("SENS:VOLT:APER?");
                double x = 1 / (Double.Parse(nanoVoltmeter.ReadString())); // 1000/xx
                LiveRate.Text = x.ToString("G5");       // F5
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }

        private void StartCap(object sender, RoutedEventArgs e)
        {
            if (check())
            {
                area = width * thickness;

                try
                {
                    StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                    StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                    startCap_volt();
                    startCap_temp();                   
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                    return;
                }
            }

        }
        private async void startCap_volt()
        {
            capture_volt = true;
            _canceller_volt = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    Capture();                 
                    if (_canceller_volt.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller_volt.Dispose();
        }

        private async void startCap_temp()
        {
            capture_temp = true;
            _canceller_temp = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    CaptureTemp();
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        HardwareData.Add(new Data(hardwareInput.Time, hardwareInput.Voltage, hardwareInput.Current, hardwareInput.Resistance, hardwareInput.Resistivity, hardwareInput.Temperature));
                        OutputTable.ScrollInView(new RowColumnIndex(HardwareData.Count, 0));
                        File.WriteSampleOutput(hardwareInput);
                        if (HardwareData.Count > 750)
                        {
                            HardwareData.RemoveAt(0);
                        }
                    }));
                    if (_canceller_temp.Token.IsCancellationRequested)
                        break;
                } while (true);
            });
            _canceller_temp.Dispose();
        }

        private void StopCap(object sender, RoutedEventArgs e)
        {
            try
            {
                _canceller_volt.Cancel();
                capture_volt = false;
                _canceller_temp.Cancel();
                capture_temp = false;

                StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no experiment in progress. Please restart the application if needed.\n\n" + ex.Message);
                return;
            }
        }

        private void Capture()
        {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
            }
            hardwareInput.Voltage = Convert.ToDouble(voltage_output);
        }

        private void CaptureTemp()
        {
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
            }

            t_volt = Convert.ToDouble(temp_output);

            hardwareInput.Current = Convert.ToDouble(CurrentLevel.Text) / 1000;          ///////// CHECK THIS (test?)

            hardwareInput.Resistance = Calc.CalcResistance(voltage, current);
            hardwareInput.Resistivity = Calc.CalcResistivity(resistance, area, length);
            hardwareInput.Temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, temperature);
            hardwareInput.Time = DateTime.Now;
        }

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
        }

        private void Set_jTemp(object sender, RoutedEventArgs e)
        {
            double m;
            if (Double.TryParse(jTempTextBox.Text, out m)) { j_temp = m; }
            else { MessageBox.Show("Invalid temperature, please enter a valid decimal number in °C.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

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
        }

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
        }

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
        }

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
        }

        public void xR_Click(object sender, RoutedEventArgs e)
        {
            component.XBindingPath = "Resistance";
            Chart_vs.PrimaryAxis.Header = "Resistance";
        }
        public void xRy_Click(object sender, RoutedEventArgs e)
        {
            component.XBindingPath = "Resistivity";
            Chart_vs.PrimaryAxis.Header = "Resistivity";
        }
        public void xV_Click(object sender, RoutedEventArgs e)
        {
            component.XBindingPath = "Voltage";
            Chart_vs.PrimaryAxis.Header = "Voltage";
        }
        public void xC_Click(object sender, RoutedEventArgs e)
        {
            component.XBindingPath = "Current";
            Chart_vs.PrimaryAxis.Header = "Current";
        }
        public void xT_Click(object sender, RoutedEventArgs e)
        {
            component.XBindingPath = "Temperature";
            Chart_vs.PrimaryAxis.Header = "Temperature";
        }

        public void yR_Click(object sender, RoutedEventArgs e)
        {
            component.YBindingPath = "Resistance";
            Chart_vs.SecondaryAxis.Header = "Resistance";
        }
        public void yRy_Click(object sender, RoutedEventArgs e)
        {
            component.YBindingPath = "Resistivity";
            Chart_vs.SecondaryAxis.Header = "Resistivity";
        }
        public void yV_Click(object sender, RoutedEventArgs e)
        {
            component.YBindingPath = "Voltage";
            Chart_vs.SecondaryAxis.Header = "Voltage";
        }
        public void yC_Click(object sender, RoutedEventArgs e)
        {
            component.YBindingPath = "Current";
            Chart_vs.SecondaryAxis.Header = "Current";
        }
        public void yT_Click(object sender, RoutedEventArgs e)
        {
            component.YBindingPath = "Temperature";
            Chart_vs.SecondaryAxis.Header = "Temperature";
        }
    }
} 
                      
