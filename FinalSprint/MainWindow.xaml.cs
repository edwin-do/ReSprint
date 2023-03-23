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
//using System.Windows.Forms;

namespace FinalSprint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class UserInput
    {
        public string? Name { get; set; }
        public string? SampleName { get; set; }
        public string? Date { get; set; }
        public double SamplingRate { get; set; }
        public double SampleLength { get; set; }
        public double SampleWidth { get; set; }
    }

    public class HardwareInput
    { 
        public string? Time { get; set; }
        public double Voltage { get; set; }

        public double Current { get; set; }

        public double Resistance { get; set; }

        public double Resistivity { get; set; }
        public double Temperature { get; set; }
    }
    public partial class MainWindow : Window
    {
        private Device currentSource;
        private Device nanoVoltmeter;
        private Device multimeter;
        private ChatHub _chatHub;

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
        private FileOutput File;
        private DataGenerator Graph;

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
        private HardwareInput hardwareInput = new HardwareInput();
        private UserInput userInput;

        ObservableCollection<HardwareInput> myDataCollection = new ObservableCollection<HardwareInput>();
        ObservableCollection<Data> graphData = new ObservableCollection<Data>();
        #endregion
        private HubConnection _connection;

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
            _chatHub = new ChatHub(this);
			Graph = (DataGenerator)this.DataContext;

            this.Loaded += OnLoaded;
            Start_Server();

            //Initialise Class objects
            Calc = new Calculation();
            InputComm = new InputCommunication();
            SampleTable.ItemsSource = myDataCollection;
            Chart.Series[0].ItemsSource = graphData;
            Chart.Series[1].ItemsSource = graphData;
            Chart.Series[2].ItemsSource = graphData;
            Chart2.Series[0].ItemsSource = graphData;
            

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
/*            main_timer = new DispatcherTimer();
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
        /// 

		private bool check()
        {
            if (string.IsNullOrEmpty(OperatorLabel.Text)){
                MessageBox.Show("Please Operator Name in the textbox.");
                return false;

            }
            else if (string.IsNullOrEmpty(SampleNameLabel.Text)){
                MessageBox.Show("Please Sample Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SampleWidthLabel.Text)){
                MessageBox.Show("Please SampleWidth Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SampleLengthLabel.Text)){
                MessageBox.Show("Please SampleLength Name in the textbox.");
                return false;
            }
            else if (string.IsNullOrEmpty(SamplingRateLabel.Text)){
                MessageBox.Show("Please SamplingRate Name in the textbox.");
                return false;
            }

            userInput = new UserInput
            {
                Name = OperatorLabel.Text.ToString(),
                SampleName = SampleNameLabel.Text.ToString(),
                Date = DateTime.Now.ToString("yyyy-MM-dd")+"-"+DateTime.Now.ToShortTimeString(),
                SamplingRate = double.Parse(SampleWidthLabel.Text),
                SampleLength = double.Parse(SampleLengthLabel.Text),
                SampleWidth = double.Parse(SampleWidthLabel.Text)
            };
            File = new FileOutput(@$"{userInput.Name}_{userInput.SampleName}_{userInput.Date}.csv");
            return true;
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

/*        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            _host?.Dispose();
            _host = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                *//*webBuilder.UseUrls("http://localhost:5100");*//*
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
                        context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "https://resprint.netlify.app", "http://localhost:3000", "https://6415e03808316473061d47f8--resprint.netlify.app", "null"});
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
        }*/

        private async void Stop_Click(object sender, RoutedEventArgs e)
        {
            /*            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", "/c mkdir test");
                        startInfo.CreateNoWindow = true;
                        startInfo.UseShellExecute = false;

                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.Start();*/
/*            MemoryStream memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            Console.SetOut(textWriter);
           
            string output = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());*/

           /* StartProcess("C:\\conveyorcli\\conveyorcli.exe", "-p 5000", ".\\test.txt");*/
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
        }
        private static StringBuilder output = new StringBuilder();
        private void StartProcess(string command, string args, string outputFilePath)
        {
            /*            ProcessStartInfo processStartInfo = new ProcessStartInfo("C:\\conveyorcli\\conveyorcli.exe");
                        processStartInfo.ArgumentList.Add("-p");
                        Process process = new Process();
                        process.Start(processStartInfo);*/
/*
            Process process = new Process();*/
            /*Process p = Process.Start("C:\\conveyorcli\\conveyorcli.exe", "-p 5100i");*/
            
/*            Debug.WriteLine(p.BeginOutputReadLine());*/
            /*            {
                            FileName = command,
                            Arguments = args,
                            WorkingDirectory="C:\\conveyorcli\\conveyorcli.exe",
                            UserName ="e.window@outlook.com",
                            PasswordInClearText = "Dragon_boy789",
                            UseShellExecute = true,
                            CreateNoWindow = true,
                            Verb = "RunAs" // Run as administrator
                        };*/
            /*            Process process = new Process();
                        process.StartInfo = processStartInfo;
                        process.Start();
                        process.BeginOutputReadLine();
                        process.WaitForExit();

                        Debug.WriteLine(output);

                        process.WaitForExit();
                        process.Close();

                        Debug.WriteLine("\n\nPress any key to exit.");*/
        }



        protected override void OnClosing(CancelEventArgs e)
        {
            _host?.Dispose();
            base.OnClosing(e);
        }

        /*        public void UpdateLabel(string message)
                {
                    Debug.WriteLine(message);
                    TestLabel.Content = message;
                }*/

        //Hub Methods 
        public void UpdateLabel(string message)
        {
            Dispatcher.Invoke(() =>
            {
                TestLabel.Content = message;
            });
        }

        public void TurnCurrentOn()
        {
            Dispatcher.Invoke(() =>
            {
                // call SCPI connect to 6220
                if (device != null)
                {
                    device.Dispose();
                }

                int currentSecondaryAddress = 0;

                device = new Device(0, 12, (byte)currentSecondaryAddress);

                device.Write("OUTP ON");
            });
        }

        public void TurnCurrentOff()
        {
            Dispatcher.Invoke(() =>
            {
                // call SCPI connect to 6220
                if (device != null)
                {
                    device.Dispose();
                }

                int currentSecondaryAddress = 0;

                device = new Device(0, 12, (byte)currentSecondaryAddress);

                device.Write("OUTP OFF");
            });
        }





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
        }   
        
            // --------------- FOR TIM ----------------                                                     //   N/A
            // if (check())
            // {

            //     File.WriteUserInput(userInput);

            //     //startCapBtn.Enabled = false;
            //     //stopCapBtn.Enabled = true;
            //     capture = true;
            //     //Date = DateTime.Now;
            //     _canceller = new CancellationTokenSource();
            //     await Task.Run(() =>
            //     {
            //         do
            //         {
            //             //device.Write("FETC?");
            //             //out_put = device.ReadString();
            //             //voltVals.Add(out_put);
            //             Thread.Sleep(50);
            //             Capture();
            //             Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //             {
            //                 myDataCollection.Add(hardwareInput);
            //                 graphData.Add(new Data(CaptureTime, hardwareInput.Voltage, hardwareInput.Current, hardwareInput.Resistance, hardwareInput.Temperature));
            //                 int latestRow = SampleTable.Items.Count - 1;
            //                 SampleTable.ScrollIntoView(SampleTable.Items[latestRow]);                            
            //             }));
                        
            //             File.WriteSampleOutput(hardwareInput);
            //             if (_canceller.Token.IsCancellationRequested)
            //                 break;
            //         } while (true);
            //     });

            //     _canceller.Dispose();
            //     //startCapBtn.Enabled = true;
            //     //stopCapBtn.Enabled = false;
            // }
        }

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
            Random rand = new Random();
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

   /*     private void main_timer_Tick(object? sender, object e)
        {
            if (capture){
                Data d = new Data(CaptureTime, hardwareInput.Voltage);
                Graph.AddData(d);
            }
        }*/

        /*private void main_timer_Tick(object sender, object e)
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