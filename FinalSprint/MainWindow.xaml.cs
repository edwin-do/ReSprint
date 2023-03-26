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
        private bool isSupplying;
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
        private double slope;

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
            isSupplying = false;
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
            j_temp = 25.0;
            th_type = 1;
            slope = 0.0;


            int currentSecondaryAddress = 0;

            try
            {
                currentSource = new Device(0, 12, (byte)currentSecondaryAddress);
                currentSource.Write("OUTP?");
                if (currentSource.ReadString() == "TRUE") { isSupplying = true; }
                if (currentSource.ReadString() == "FALSE") { isSupplying = false; }

                OnButton.IsEnabled = isSupplying;
                OffButton.IsEnabled = !isSupplying;

                if (isSupplying)
                {
                    supplyStatus.Foreground = Brushes.Green;
                }
                if (!isSupplying)
                {
                    supplyStatus.Foreground = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                nanoVoltmeter = new Device(0, 7, (byte)currentSecondaryAddress);
                nanoVoltmeter.Write("*RST");
                nanoVoltmeter.Write("SENS:CHAN 1");
                nanoVoltmeter.Write("SENS:FUNC 'VOLT'");

                nanoVoltmeter.Write("SENS:VOLT:APER?");
                double y = 1000 / (double.Parse(nanoVoltmeter.ReadString()));
                LiveRate.Text = y.ToString("G5");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                multimeter = new Device(0, 1, (byte)currentSecondaryAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected virtual void OnExit(System.Windows.ExitEventArgs e)
        {
            currentSource.Write("OUTP OFF");
            nanoVoltmeter.Write("*RST");
        }

        private bool check()
        {
            if (string.IsNullOrEmpty(OperatorName.Text))
            {
                MessageBox.Show("Please enter Operator Name in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleName.Text))
            {
                MessageBox.Show("Please enter Sample Name in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleLength.Text))
            {
                MessageBox.Show("Please enter Sample Length in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((!double.TryParse(SampleLength.Text, out double checkLength)) && checkLength <= 0)
            {
                MessageBox.Show("Invalid Sample Length, please enter a positive non-zero decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleWidth.Text))
            {
                MessageBox.Show("Please enter Sample Width in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((!double.TryParse(SampleWidth.Text, out double checkWidth)) && checkWidth <= 0)
            {
                MessageBox.Show("Invalid Sample Width, please enter a positive non-zero decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleThickness.Text))
            {
                MessageBox.Show("Please enter Sample Thickness in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((!double.TryParse(SampleThickness.Text, out double checkThickness)) && checkThickness <= 0)
            {
                MessageBox.Show("Invalid Sample Thickness, please enter a positive non-zero decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            userInput = new UserInput
            {
                UserName = OperatorName.Text.ToString(),
                UserSampleName = SampleName.Text.ToString(),
                UserSampleLength = double.Parse(SampleLength.Text) / 1000,
                UserSampleWidth = double.Parse(SampleWidth.Text) / 1000,
                UserSampleThickness = double.Parse(SampleThickness.Text) / 1000
            };
            string SampleDate = DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.ToShortTimeString();
            File = new FileOutput(@$"{userInput.UserName}_{userInput.UserSampleName}_{SampleDate}.csv");
            return true;
        }

        public void TurnCurrentOn()     // Remote
        {
            Dispatcher.Invoke(() =>
            {
                currentSource.Write("OUTP ON");
            });
        }

        public void TurnCurrentOff()     // Remote
        {
            Dispatcher.Invoke(() =>
            {
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
            if (currentSource != null)
            {
                if (range == 0) { currentSource.Write("CURR:RANG:AUTO ON"); }
                if (range == 1) { currentSource.Write("CURR:RANG:1e-3"); }
                if (range == 2) { currentSource.Write("CURR:RANG:10e-3"); }
                if (range == 3) { currentSource.Write("CURR:RANG:100e-3"); }

                if (double.TryParse(CurrentLevel.Text, out double n1) && (-105 <= n1 && n1 <= 105))
                { 
                    currLevel = CurrentLevel.Text;
                    hardwareInput.Current = double.Parse(currLevel) / 1000;          ///////// CHECK THIS (test?)
                }
                else
                {
                    MessageBox.Show("Invalid current, please enter a valid decimal number in mA.\n\nThe current supply has a range of -105 to 105 mA.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (double.TryParse(Compliance.Text, out double n2) && (0.1 <= n2 && n2 <= 105)) 
                { 
                    compliance = Compliance.Text; 
                }
                else
                {
                    MessageBox.Show("Invalid compliance voltage, please enter a positive decimal number in Volts.\n\nThe voltage compliance range is 0.1 to 105 V.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    currentSource.Write("CURR:COMP " + compliance);
                    currentSource.Write("CURR " + currLevel + "e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void resetCurrent(object sender, RoutedEventArgs e)
        {
            if (currentSource != null)
            {
                try
                {
                    currentSource.Write("*RST");
                    currentSource.Write("CLE");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                CurrentLevel.Text = "";
                Compliance.Text = "10.0";
            }
            else
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void CurrentPower(object sender, RoutedEventArgs e)
        {
            if (currentSource != null)
            {
                try
                {
                    currentSource.Write("OUTP?");
                    if (currentSource.ReadString() == "TRUE") { isSupplying = true; }
                    if (currentSource.ReadString() == "FALSE") { isSupplying = false; }

                    OnButton.IsEnabled = isSupplying;
                    OffButton.IsEnabled = !isSupplying;

                    if (!isSupplying)
                    {
                        currentSource.Write("OUTP ON");
                        supplyStatus.Foreground = Brushes.Green;
                    }

                    if (isSupplying)
                    {
                        currentSource.Write("OUTP OFF");
                        supplyStatus.Foreground = Brushes.Red;
                    }

                    //OnButton.IsEnabled = !OnButton.IsEnabled;
                    //OffButton.IsEnabled = !OffButton.IsEnabled;

                    //if (OnButton.IsEnabled) { currentSource.Write("OUTP ON"); supplyStatus.Foreground = Brushes.Green; }
                    //if (OffButton.IsEnabled) { currentSource.Write("OUTP OFF"); supplyStatus.Foreground = Brushes.Red; }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void SetVolt(object sender, RoutedEventArgs e)
        {
            if (nanoVoltmeter != null)
            {
                if (double.TryParse(SampleRate.Text, out double r)) { rate = r; }
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
                    double x = 1 / (double.Parse(nanoVoltmeter.ReadString()));
                    LiveRate.Text = x.ToString("G5");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void StartCap(object sender, RoutedEventArgs e)
        {
            if ((currentSource != null) && (nanoVoltmeter != null) && (multimeter != null))        // Add check if (OUTP? = on)
            {
                if (check())
                {
                    area = width * thickness;

                    StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                    StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                    startCap_volt();
                    startCap_temp();
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
            nanoVoltmeter.Write("read?");
            voltage_output = nanoVoltmeter.ReadString();
            hardwareInput.Voltage = double.Parse(voltage_output);  //replaced "Convert.ToDouble()"
        }

        private void CaptureTemp()
        {
            multimeter.Write("*IDN?");                  /////// Try only READ (try writing ONCE then reading continuously, both Volts and Temp)
            temp_output = multimeter.ReadString();

            t_volt = double.Parse(temp_output) * 1000;      // mV to uV

            // hardwareInput.Current = double.Parse(currLevel) / 1000;          ///////// CHECK THIS (test?)
            hardwareInput.Resistance = Calc.CalcResistance(voltage, current);
            hardwareInput.Resistivity = Calc.CalcResistivity(resistance, area, length);
            hardwareInput.Temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, temperature);
            hardwareInput.Time = DateTime.Now;

          /*  slope = Calc.CalcSlope(resistivity, temperature);
            bool change = Calc.CalcChange(slope, temperature);*/
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
            if (double.TryParse(jTempTextBox.Text, out double m)) 
            { 
                j_temp = m; 
            }
            else
            { 
                MessageBox.Show("Invalid temperature, please enter a valid decimal number in °C.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
            try
            {
                if (!string.IsNullOrEmpty(SCPI_output.Text))
                {
                    if ((scpiDevice == 1) && (currentSource != null))
                    {
                        currentSource.Write(SCPI_input.Text);
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if ((scpiDevice == 2) && (nanoVoltmeter != null))
                    {
                        nanoVoltmeter.Write(SCPI_input.Text);
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if ((scpiDevice == 3) && (multimeter != null))
                    {
                        multimeter.Write(SCPI_input.Text);
                    }
                    else
                    {
                        MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a command, then click Write and Read.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid command.\n\n" + ex.Message);
                return;
            }
        }

        private void SCPI_read(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((scpiDevice == 1) && (currentSource != null))
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + currentSource.ReadString();
                }
                else
                {
                    MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if ((scpiDevice == 2) && (nanoVoltmeter != null))
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + nanoVoltmeter.ReadString();
                }
                else
                {
                    MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if ((scpiDevice == 3) && (multimeter != null))
                {
                    SCPI_output.Text = SCPI_output.Text + "\n" + multimeter.ReadString();
                }
                else
                {
                    MessageBox.Show("Unable to connect to the Multimeter. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nothing to read. Possibly an invalid command or an instruction command.\n\n" + ex.Message);
                return;
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
