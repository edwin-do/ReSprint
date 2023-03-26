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
using System.Text.RegularExpressions;

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
        public bool captureStatus = false;
        Semaphore volt;
        Semaphore temp;

        private CancellationTokenSource _canceller;

        private HardwareInput hardwareInput = new HardwareInput();
        private UserInput userInput;

        ObservableCollection<Data> HardwareData = new ObservableCollection<Data>();

        private HubConnection _connection;
        public MainWindow()
        {
            _chatHub = new ChatHub(this);
            

            InitializeComponent();
            Start_Server();

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

            volt = new Semaphore(0, 1);
            temp = new Semaphore(0, 1);

            hardwareInput.Temperature = 0.0;


            int currentSecondaryAddress = 0;

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
                MessageBox.Show("1 Unable to connect to the Current Source. The device is either powered off or is not connected to the computer.\n\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                currentSource = null;
                Debug.WriteLine(currentSource);
            }

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

        private IHost _host;/*        var chatHub = new ChatHub(this);*/

        private async void Start_Server()
        {
            _host?.Dispose();
            _host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                /*webBuilder.UseUrls("http://localhost:5100");*/
                webBuilder.UseUrls("http://*:5100");
                webBuilder.ConfigureServices(services =>
                {
                    services.AddSingleton<ChatHub>(new ChatHub(this)); // Instantiate ChatHub and pass in a reference to MainWindow
                    services.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy",
                            builder =>
                            {
                                builder.WithOrigins("https://resprint.netlify.app", "http://192.168.0.119:45455", "https://6415e03808316473061d47f8--resprint.netlify.app", "http://localhost:3000", "null")
                                       .AllowAnyMethod()
                                       .AllowAnyHeader()
                                       .WithExposedHeaders("Content-Disposition")
                                       .WithHeaders("x-requested-with", "X-SignalR-User-Agent")
                                       .SetIsOriginAllowed((x) => true)
                                       .AllowCredentials();
                            });
                    });
                    services.AddSignalR();
                });
                webBuilder.Configure(app =>
                {
                    app.UseWebSockets();

                    app.Use(async (context, next) =>
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "https://resprint.netlify.app", "http://localhost:3000", "https://6415e03808316473061d47f8--resprint.netlify.app", "null" });
                        context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                        await next();
                    });
                    app.UseCors("CorsPolicy");
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/Hubs/chatHub");
                    });
                });
            })
            .Build();


            await _host.StartAsync();
        }

        //Hub Methods 
        /*        public void UpdateLabel(string message)
                {
                    Dispatcher.Invoke(() =>
                    {
                        TestLabel.Content = message;
                    });
                }*/

        public bool GetCaptureStatus()
        {
            return captureStatus;
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
            else if ((!double.TryParse(SampleLength.Text, out double checkLength)) || checkLength <= 0)
            {
                MessageBox.Show("Invalid Sample Length, please enter a positive non-zero decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleWidth.Text))
            {
                MessageBox.Show("Please enter Sample Width in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((!double.TryParse(SampleWidth.Text, out double checkWidth)) || checkWidth <= 0)
            {
                MessageBox.Show("Invalid Sample Width, please enter a positive non-zero decimal number in mm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(SampleThickness.Text))
            {
                MessageBox.Show("Please enter Sample Thickness in the textbox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((!double.TryParse(SampleThickness.Text, out double checkThickness)) || checkThickness <= 0)
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

            File = new FileOutput(@$"{userInput.UserName}_{userInput.UserSampleName}_{DateTime.Now.ToString("yyyy-MM-dd-hh-mm")}.csv");
            return true;
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
                if (check())
                {
                    captureStatus = true;
                    area = width * thickness;

                    StartCapBtn.IsEnabled = !StartCapBtn.IsEnabled;
                    StopCapBtn.IsEnabled = !StartCapBtn.IsEnabled;

                    File.WriteUserInput(userInput);

                    startCap_loop();
                }
            }
            else
            {
                MessageBox.Show("Unable to connect to instrument(s). A device is either powered off or is not connected to the computer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

    
        private async void startCap_loop()
        {
            capture_volt = true;
            _canceller = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    Capture();

                    /*volt.Release();
                    temp.WaitOne();*/
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
            Chart.Save($@"{userInput.UserName}_{userInput.UserSampleName}_{hardwareInput.Time.ToString("yyyy-MM-dd-hh-mm")}");
  /*          Chart_vs.Save($@"{userInput.UserName}_{userInput.UserSampleName}_{hardwareInput.Time.ToString("yyyy-MM-dd-hh-mm")}");*/
        }
        
        private void Capture()
        {
            nanoVoltmeter.Write("read?");
            voltage_output = nanoVoltmeter.ReadString();
            hardwareInput.Voltage = double.Parse(voltage_output);

            multimeter.Write("*IDN?");                  /////// Try only READ (try writing ONCE then reading continuously, both Volts and Temp)
            temp_output = multimeter.ReadString();

            t_volt = double.Parse(temp_output) * 1000;      // mV to uV

            hardwareInput.Resistance = Calc.CalcResistance(hardwareInput.Voltage, hardwareInput.Current);
            hardwareInput.Resistivity = Calc.CalcResistivity(hardwareInput.Resistance, userInput.UserSampleThickness*userInput.UserSampleWidth, userInput.UserSampleLength);
            hardwareInput.Temperature = Calc.CalcTemperature(t_volt, j_temp, th_type, hardwareInput.Temperature);
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
        }                     //   YES

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

        public class Converter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                try
                {
                    var formattedString = string.Format("{0:E}", value);
                    return formattedString;
                }
                catch (Exception ex)
                {
                }
                return value;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
} 
                      
