using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using NationalInstruments.NI4882;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using FinalSprint.Hubs;
using System.Collections.ObjectModel;
using System.IO;
using Syncfusion.UI.Xaml.ScrollAxis;
using FinalSprint.src.Classes;

namespace FinalSprint
{
    public partial class MainWindow : Window
    {
        private Device? currentSource;
        private Device nanoVoltmeter;
        private Device multimeter;

        private ChatHub _chatHub;
        private Calculation Calc;
        private FileOutput File;

        private int range;
        private int scpiDevice;
        private double rate;
        private string aperture;
        private string compliance;
        private string currLevel;
        private string voltage_output;
        private string temp_output;
        private bool capture_volt = false;
        private bool capture_temp = false;
        private double width = 0.0;
        private double thickness = 0.0;
        private double length = 0.0;
        private double t_volt = 0.0;
        private double j_temp = 25.0;
        private int th_type = 1;
        private int currentSecondaryAddress = 0;
        private int spciDevice = 0;
        public bool captureStatus = false;
        private IHost _host;

        private string directory = Directory.GetCurrentDirectory();

        private CancellationTokenSource _canceller;

        private HardwareInput hardwareInput = new HardwareInput();
        private UserInput userInput;

        ObservableCollection<Data> HardwareData = new ObservableCollection<Data>();
        public MainWindow()
        {
            InitializeComponent();
            _chatHub = new ChatHub(this);
            RemoteAccess server = new RemoteAccess(this);
            server.StartServer(_host);

            Calc = new Calculation();
            OutputTable.ItemsSource = HardwareData;
            Chart.Series[0].ItemsSource = HardwareData;
            Chart.Series[1].ItemsSource = HardwareData;
            Chart.Series[2].ItemsSource = HardwareData;
            Chart.Series[3].ItemsSource = HardwareData;
            Chart.Series[4].ItemsSource = HardwareData;
            Chart_vs.Series[0].ItemsSource = HardwareData;
            hardwareInput.Temperature = 0.0;

            initializeCurrentSource(currentSecondaryAddress);
            initializeNanoVoltmeter(currentSecondaryAddress);
            initializeMultimeter(currentSecondaryAddress);
        }

        private void initializeCurrentSource(int currentSecondaryAddress)
        {
            try
            {
                currentSource = new Device(0, 12, (byte)currentSecondaryAddress);

                currentSource.Write("OUTP?");
                int checkCurrent = int.Parse(currentSource.ReadString());

                if (checkCurrent == 0)
                {
                    OnButton.IsEnabled = true;
                    OffButton.IsEnabled = false;
                    supplyStatus.Foreground = Brushes.Red;
                }
                else
                {
                    OnButton.IsEnabled = false;
                    OffButton.IsEnabled = true;
                    supplyStatus.Foreground = Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                currentSource = null;
                Debug.WriteLine(currentSource);
            }
        }
        private void initializeNanoVoltmeter(int currentSecondaryAddress)
        {
            try
            {
                nanoVoltmeter = new Device(0, 7, (byte)currentSecondaryAddress);
                nanoVoltmeter.Write("*RST");
                nanoVoltmeter.Write("SENS:CHAN 1");
                nanoVoltmeter.Write("SENS:FUNC 'VOLT'");

                nanoVoltmeter.Write("SENS:VOLT:APER?");
                double y = 1000 / (Double.Parse(nanoVoltmeter.ReadString()));
                LiveRate.Text = y.ToString("G5");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to the Nano-voltmeter. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void initializeMultimeter(int currentSecondaryAddress)
        {
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

        public int getCurrentStatus()
        {
            if (currentSource != null)
            {
                currentSource.Write("OUTP?");
                return int.Parse(currentSource.ReadString());
            }
            return 0;
        }

        public bool getExperimentStatus()
        {
            return capture_volt && capture_temp;
        }

        public void TurnCurrentOn()     // Remote
        {
            Dispatcher.Invoke(() =>
            {
                if (currentSource != null)
                {
                    OnButton.IsEnabled = false;
                    OffButton.IsEnabled = true;
                    currentSource.Write("OUTP ON");

                    if (OffButton.IsEnabled)
                    {
                        supplyStatus.Foreground = Brushes.Green;
                    }
                    else
                    {
                        supplyStatus.Foreground = Brushes.Red;
                    }
                }

            });
        }

        public void TurnCurrentOff()     // Remote
        {
            Dispatcher.Invoke(() =>
            {
                if (currentSource != null)
                {
                    OnButton.IsEnabled = true;
                    OffButton.IsEnabled = false;
                    currentSource.Write("OUTP OFF");

                    if (OffButton.IsEnabled)
                    {
                        supplyStatus.Foreground = Brushes.Green;
                    }
                    else
                    {
                        supplyStatus.Foreground = Brushes.Red;
                    }
                }
            });
        }

        public bool GetCaptureStatus()
        {
            return captureStatus;
        }

        public void startCapture()
        {
            Dispatcher.Invoke(() =>
            {
                if ((currentSource != null) && (nanoVoltmeter != null) && (multimeter != null))        // Add check if (OUTP? = on)
                {

                    UserInputValidation userValidation = new UserInputValidation();
                    userValidation.checkInputBox(OperatorName);
                    userValidation.checkInputBox(SampleName);
                    userValidation.checkInputBox(SampleLength);
                    userValidation.checkInputBox(SampleWidth);
                    userValidation.checkInputBox(SampleThickness);
                    try
                    {
                        bool userDataIsValid = userValidation.validateUserData(OperatorName.Text, SampleName.Text, double.Parse(SampleLength.Text) / 1000, double.Parse(SampleWidth.Text) / 1000, double.Parse(SampleThickness.Text) / 1000);

                        if (userDataIsValid)
                        {
                            userInput = userValidation.checkUserInput(OperatorName.Text, SampleName.Text, double.Parse(SampleLength.Text) / 1000, double.Parse(SampleWidth.Text) / 1000, double.Parse(SampleThickness.Text) / 1000);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Missing input Parameters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    captureStatus = true;


                        StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                        StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                        File.WriteUserInput(userInput);

                        StartCap_loop();
                    
                }
                else
                {
                    MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });
        }

        public void StopCapture()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    _canceller.Cancel();
                    capture_volt = false;

                    StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                    StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                    captureStatus = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There is no experiment in progress. Please restart the application if needed.\n\n" + ex.Message);
                    return;
                }
                Chart.Save($@"C:\Users\hatem\Documents\ReSprint\FinalSprint\Result\Graph\{userInput.UserName}_{userInput.UserSampleName}_{hardwareInput.Time.ToString("yyyy-MM-dd-hh-mm")}");
            });
        }
        //---------------- REMOTE ENDS -----------//

        private void initializeFileOutput()
        {
            try
            {
                if (!Directory.Exists(@$"{directory}\Data\Table\"))
                {
                    Directory.CreateDirectory(@$"{directory}\Data\Table\");
                }

                string SampleDate = DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.ToShortTimeString();
                File = new FileOutput(@$"{directory}\Data\Table\{userInput.UserName}_{userInput.UserSampleName}_{DateTime.Now.ToString("yyyy-MM-dd-hh-mm")}.csv");
            }
            catch
            {
                MessageBox.Show("File Output could not be initialized");
            }
        }
        private void initializeGraphOutput()
        {
            if (!Directory.Exists(@$"{directory}\Data\Graph\"))
            {
                Directory.CreateDirectory(@$"{directory}\Data\Graph\");
            }
        }

        private void setCurrent(object sender, RoutedEventArgs e)
        {
            if (currentSource != null)
            {
                range = Range.SelectedIndex;
                if (range == 0) { currentSource.Write("CURR:RANG:AUTO ON"); }
                else if (range == 1) { currentSource.Write("CURR:RANG:1e-3"); }
                else if (range == 2) { currentSource.Write("CURR:RANG:10e-3"); }
                else if (range == 3) { currentSource.Write("CURR:RANG:100e-3"); }
                else { MessageBox.Show("No valid range for current selected"); }


                //Validation
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
                //

                try
                {
                    currentSource.Write("CURR:COMP " + compliance);
                    currentSource.Write("CURR " + currLevel + "e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("2 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("3 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("4 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                CurrentLevel.Text = "";
                Compliance.Text = "10.0";
            }
            else
            {
                MessageBox.Show("5 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void CurrentPowerOn(object sender, RoutedEventArgs e)
        {

            try
            {
                OnButton.IsEnabled = !OnButton.IsEnabled;
                OffButton.IsEnabled = !OffButton.IsEnabled;
                currentSource.Write("OUTP ON");

                if (OffButton.IsEnabled)
                {
                    supplyStatus.Foreground = Brushes.Green;
                }
                else
                {
                    supplyStatus.Foreground = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("6 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }

        private void CurrentPowerOff(object sender, RoutedEventArgs e)
        {
            try
            {
                OnButton.IsEnabled = !OnButton.IsEnabled;
                OffButton.IsEnabled = !OffButton.IsEnabled;
                currentSource.Write("OUTP OFF");

                if (OffButton.IsEnabled)
                {
                    supplyStatus.Foreground = Brushes.Green;
                }
                else
                {
                    supplyStatus.Foreground = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("7 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
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
                UserInputValidation userValidation = new UserInputValidation();
                userValidation.checkInputBox(OperatorName);
                userValidation.checkInputBox(SampleName);
                userValidation.checkInputBox(SampleLength);
                userValidation.checkInputBox(SampleWidth);
                userValidation.checkInputBox(SampleThickness);

                try
                {
                    bool userDataIsValid = userValidation.validateUserData(OperatorName.Text, SampleName.Text, double.Parse(SampleLength.Text) / 1000, double.Parse(SampleWidth.Text) / 1000, double.Parse(SampleThickness.Text) / 1000);
                    if (userDataIsValid)
                    {
                        userInput = userValidation.checkUserInput(OperatorName.Text, SampleName.Text, double.Parse(SampleLength.Text) / 1000, double.Parse(SampleWidth.Text) / 1000, double.Parse(SampleThickness.Text) / 1000);
                        initializeFileOutput();

                        StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                        StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                        File.WriteUserInput(userInput);
                        captureStatus = true;

                        StartCap_loop();
                    }
                }
                catch
                {
                    MessageBox.Show("Missing input Parameters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

    
        private async void StartCap_loop()
        {
            capture_volt = true;
            _canceller = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    nanoVoltmeter.Write("read?");
                    voltage_output = nanoVoltmeter.ReadString();
                    hardwareInput.Voltage = double.Parse(voltage_output);

                    multimeter.Write("*IDN?");// Try only READ (try writing ONCE then reading continuously, both Volts and Temp)
                    temp_output = multimeter.ReadString();

                    t_volt = double.Parse(temp_output) * 1000;// mV to uV
                    th_type = ThType.SelectedIndex;

                    hardwareInput.Resistance = Calc.CalcResistance(hardwareInput.Voltage, hardwareInput.Current);
                    hardwareInput.Resistivity = Calc.CalcResistivity(hardwareInput.Resistance, userInput.UserSampleThickness * userInput.UserSampleWidth, userInput.UserSampleLength);
                    hardwareInput.Temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, hardwareInput.Temperature);
                    hardwareInput.Time = DateTime.Now;

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (!double.IsInfinity(Math.Abs(hardwareInput.Resistance)) && !double.IsNaN(Math.Abs(hardwareInput.Resistance)) && !double.IsInfinity(Math.Abs(hardwareInput.Resistivity)) && !double.IsNaN(Math.Abs(hardwareInput.Resistivity)))
                        {
                            //Debug.WriteLine(DateTime.Now);
                            hardwareInput.Time = DateTime.Now;
                            HardwareData.Add(new Data(hardwareInput.Time, Math.Abs(hardwareInput.Voltage), Math.Abs(hardwareInput.Current), Math.Abs(hardwareInput.Resistance), Math.Abs(hardwareInput.Resistivity), hardwareInput.Temperature));
                            OutputTable.ScrollInView(new RowColumnIndex(HardwareData.Count, 0));
                        }
                        File.WriteSampleOutput(hardwareInput);
                        if (HardwareData.Count > 750)
                        {
                            HardwareData.RemoveAt(0);
                        }
                    }));
                    if (_canceller.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller.Dispose();
        }

        private void StopCap(object sender, RoutedEventArgs e)
        {
            try
            {
                _canceller.Cancel();
                capture_volt = false;

                StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                captureStatus = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is no experiment in progress. Please restart the application if needed.\n\n" + ex.Message);
                return;
            }

            initializeGraphOutput();
            Chart.Save($@"{directory}\Data\Graph\{userInput.UserName}_{userInput.UserSampleName}_{hardwareInput.Time.ToString("yyyy-MM-dd-hh-mm")}");
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
                        MessageBox.Show("8 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("9 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                      
