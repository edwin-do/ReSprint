using System;
using System.Windows;
using System.Windows.Media;
using NationalInstruments.NI4882;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using FinalSprint.Hubs;
using System.Collections.ObjectModel;
using System.IO;
using Syncfusion.UI.Xaml.ScrollAxis;
using FinalSprint.src.Classes;

namespace FinalSprint
{
    public partial class MainWindow : Window
    {
        // Device Variables
        private int currentSecondaryAddress = 0;
        public Device? currentSource;
        private Device? nanoVoltmeter;
        private Device? multimeter;
        private Device? CurrentSPCIDevice;

        public bool capture_volt = false;
        public bool capture_temp = false;
        public bool captureStatus = false;
        public CancellationTokenSource? _canceller;

        private double t_volt = 0.0;
        private double j_temp = 0.0;
        private int th_type = 0;

        // Classes
        private Calculation Calc = new Calculation();
        private HardwareInput hardwareInput = new HardwareInput();
        private InstrumentInput instrumentInput = new InstrumentInput();
        private InstrumentInputValidation instrumentValidation = new InstrumentInputValidation();
        private UserInput userInput = new UserInput();

        //Output + Remote 
        private string directory = Directory.GetCurrentDirectory();
        private FileOutput? File;
        ObservableCollection<Data> HardwareData = new ObservableCollection<Data>();
        private IHost _host;

        public MainWindow()
        {
            InitializeComponent();
            ChatHub _chatHub = new ChatHub(this);
            RemoteAccess server = new RemoteAccess(this);

            server.StartServer(_host);
            initializeChart();
            initializeCurrentSource(currentSecondaryAddress);
            initializeNanoVoltmeter(currentSecondaryAddress);
            initializeMultimeter(currentSecondaryAddress);
        }

        // ---- Initialization Methods ------//
        private void initializeChart()
        {
            OutputTable.ItemsSource = HardwareData;
            Chart.Series[0].ItemsSource = HardwareData;
            Chart.Series[1].ItemsSource = HardwareData;
            Chart.Series[2].ItemsSource = HardwareData;
            Chart.Series[3].ItemsSource = HardwareData;
            Chart.Series[4].ItemsSource = HardwareData;
            Chart_vs.Series[0].ItemsSource = HardwareData;
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
            catch(Exception err) {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void initializeGraphOutput()
        {
            if (!Directory.Exists(@$"{directory}\Data\Graph\"))
            {
                Directory.CreateDirectory(@$"{directory}\Data\Graph\");
            }
        }
        protected virtual void OnExit(ExitEventArgs e)
        {
            if (currentSource != null)
            {
                currentSource.Write("OUTP OFF");
            }
            if (nanoVoltmeter != null)
            {
                nanoVoltmeter.Write("*RST");
            }
            return;

        }

        // ---- Instrument Related Methods ------//
        private void SetVolt(object sender, RoutedEventArgs e)
        {
            double.TryParse(SampleRate.Text, out double rate);
            bool SampleRateIsValid;
            try {SampleRateIsValid = instrumentValidation.CheckSampleRate(rate); }
            catch (Exception err){
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SampleRateIsValid = false;
            }

            try
            {
                if (nanoVoltmeter != null && SampleRateIsValid)
                {
                    string aperture = Calc.CalcAperture(rate);
                    nanoVoltmeter.Write("SENS:VOLT:APER " + aperture);
                    nanoVoltmeter.Write("SENS:VOLT:APER?");
                    double x = 1 / (double.Parse(nanoVoltmeter.ReadString()));
                    LiveRate.Text = x.ToString("G5");
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when communicatiing with the Nano-voltmeter. The device can be either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void SetJuncTemp(object sender, RoutedEventArgs e)
        {
            double.TryParse(jTempTextBox.Text, out double jTemp);
            bool JuncTemperatureIsValid;
            try { JuncTemperatureIsValid = instrumentValidation.checkJuncTemperature(jTemp); }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                JuncTemperatureIsValid = false;
            }
            if (JuncTemperatureIsValid) {
                j_temp = jTemp;
            }
            return;
        }
        private void SetCurrent(object sender, RoutedEventArgs e)
        {
            double.TryParse(CurrentLevel.Text, out double currentLevel);
            double.TryParse(Compliance.Text, out double compliance);

            if (currentSource == null)
            {
                MessageBox.Show("Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int range = Range.SelectedIndex;
            if (range == 0) { currentSource.Write("CURR:RANG:AUTO ON"); }
            else if (range == 1) { currentSource.Write("CURR:RANG:1e-3"); }
            else if (range == 2) { currentSource.Write("CURR:RANG:10e-3"); }
            else if (range == 3) { currentSource.Write("CURR:RANG:100e-3"); }
            else { MessageBox.Show("No valid range for current selected"); }

            try
            {
                bool CurrentLevelIsValid;
                try { CurrentLevelIsValid = instrumentValidation.CheckCurrentLevel(currentLevel); }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CurrentLevelIsValid = false;
                }

                bool CheckComplianceIsValid;
                try { CheckComplianceIsValid = instrumentValidation.CheckCompliance(compliance); }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CheckComplianceIsValid = false;
                }

                if (CurrentLevelIsValid && CheckComplianceIsValid)
                {
                    instrumentInput.Compliance = compliance;
                    instrumentInput.CurrentLevel = currentLevel / 1000;
                    hardwareInput.Current = currentLevel / 1000; //fix this - Edwin

                    currentSource.Write("CURR:COMP " + compliance);
                    currentSource.Write("CURR " + currentLevel + "e-3");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong when communicating with the current source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        private void ResetCurrent(object sender, RoutedEventArgs e)
        {
            if (currentSource == null)
            {
                MessageBox.Show("5 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
        public void ToggleCurrentPowerButton(object sender, RoutedEventArgs e)
        {
            OnButton.IsEnabled = !OnButton.IsEnabled;
            OffButton.IsEnabled = !OffButton.IsEnabled;

            try
            {
                if (OffButton.IsEnabled)
                {
                    currentSource.Write("OUTP ON");
                    supplyStatus.Foreground = Brushes.Green;
                }
                else
                {
                    currentSource.Write("OUTP OFF");
                    supplyStatus.Foreground = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("6 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message);
                return;
            }
        }

        // ------- SCPI Related Methods -------------//
        private void SelectSCPICurrent(object sender, RoutedEventArgs e)
        {
            CurrentSPCIDevice = currentSource;
        }
        private void SelectSCPIVoltage(object sender, RoutedEventArgs e)
        {
            CurrentSPCIDevice = nanoVoltmeter;
        }
        private void SelectSCPITemp(object sender, RoutedEventArgs e)
        {
            CurrentSPCIDevice = multimeter;
        }
        private void SCPIWrite(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SCPI_input.Text))
            {
                MessageBox.Show("Please enter a command, then click Write and Read.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CurrentSPCIDevice == null)
            {
                MessageBox.Show("Unable to connect to the selected device. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                CurrentSPCIDevice.Write(SCPI_input.Text);             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid command.\n\n" + ex.Message);
                return;
            }
        }
        private void SCPIRead(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(SCPI_input.Text))
            {
                MessageBox.Show("Please enter a command, then click Write and Read.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CurrentSPCIDevice == null)
            {
                MessageBox.Show("Unable to connect to the selected device. The device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                SCPI_output.Text = SCPI_output.Text + "\n" + CurrentSPCIDevice.ReadString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid command.\n\n" + ex.Message);
                return;
            }
        }

        // ---- Experiment Related Methods ------//
        private void StartCapture(object sender, RoutedEventArgs e)
        {
            if ((currentSource == null) || (nanoVoltmeter == null) || (multimeter == null))
            {
                MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

                    try { File.WriteUserInput(userInput); } catch(Exception err) {
                        MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    InitializeContinuousCapture();
                    captureStatus = true;
                    capture_volt = true;
                }
            }
            catch
            {
                MessageBox.Show("Missing input Parameters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void StopCapture(object sender, RoutedEventArgs e)
        {
            try
            {
                _canceller.Cancel();
                StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                capture_volt = false;
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
        private async void InitializeContinuousCapture()
        {
            _canceller = new CancellationTokenSource();
            th_type = ThType.SelectedIndex;

            await Task.Run(() =>
            {
                do
                {
                    nanoVoltmeter.Write("read?");
                    string voltage_output = nanoVoltmeter.ReadString();
                    hardwareInput.Voltage = double.Parse(voltage_output);

                    multimeter.Write("*IDN?");// Try only READ (try writing ONCE then reading continuously, both Volts and Temp)
                    string temp_output = multimeter.ReadString();

                    t_volt = double.Parse(temp_output) * 1000;// mV to uV


                    hardwareInput.Resistance = Calc.CalcResistance(hardwareInput.Voltage, hardwareInput.Current);
                    hardwareInput.Resistivity = Calc.CalcResistivity(hardwareInput.Resistance, userInput.UserSampleThickness * userInput.UserSampleWidth, userInput.UserSampleLength);
                    hardwareInput.Temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, hardwareInput.Temperature);
                    hardwareInput.Time = DateTime.Now;

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (!double.IsInfinity(Math.Abs(hardwareInput.Resistance)) && !double.IsNaN(Math.Abs(hardwareInput.Resistance)) && !double.IsInfinity(Math.Abs(hardwareInput.Resistivity)) && !double.IsNaN(Math.Abs(hardwareInput.Resistivity)))
                        {
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

        // ------- ADDITIONAL GRAPH METHODS --------//
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

        // ------- REMOTE METHODS --------//
        public int RemoteGetCurrentStatus()
        {
            if (currentSource != null)
            {
                currentSource.Write("OUTP?");
                return int.Parse(currentSource.ReadString());
            }
            return 0;
        }
        public bool RemoteGetExperimentStatus()
        {
            return capture_volt && capture_temp;
        }
        public void RemoteTurnCurrentOn()
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
        public void RemoteTurnCurrentOff()
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
        public bool RemoteGetCaptureStatus()
        {
            return captureStatus;
        }
        public void RemoteStartCapture()
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

                    InitializeContinuousCapture();

                }
                else
                {
                    MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });
        }
        public void RemoteStopCapture()
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
    }
} 