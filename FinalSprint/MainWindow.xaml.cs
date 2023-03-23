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
        public Device device;
        private ChatHub _chatHub;

        private int range;
        private string rate;
        private string compLevel;
        private string currLevel;
        private string out_put;

        //Class objects
        private InputCommunication InputComm;
        private Calculation Calc;
        private FileOutput File;
        private DataGenerator Graph;

        //private DataGenerator DatGen;

        //Member variables
        private bool capture;
        private double voltage;
        private double current;
        private double resistance;
        private double resistivity;
        private double area;
        private double length;
        private DateTime CaptureTime;

        private CancellationTokenSource _canceller;

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
            capture = false;
            voltage = 0.0;
            current = 0.0;
            resistance = 0.0;
            resistivity = 0.0;
            area = 0.000003;
            length = 0.04;


            //Initialise timer for graph update
/*            main_timer = new DispatcherTimer();
            main_timer.Tick += main_timer_Tick;
            main_timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            main_timer.Start();
*/
        }
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

        private void currRadio(object sender, RoutedEventArgs e)
        {
            // call SCPI connect to 6220
            if (device != null)
            {
                device.Dispose();
            }

            int currentSecondaryAddress = 0;

            device = new Device(0, 12, (byte)currentSecondaryAddress);

            device.Write("*RST");
            device.Write("CLE");
        }

        private void voltRadio(object sender, RoutedEventArgs e)
        {
            // call SCPI connect to 2182A
            if (device != null)
            {
                device.Dispose();
            }

            int currentSecondaryAddress = 0;

            device = new Device(0, 7, (byte)currentSecondaryAddress);

            device.Write("*RST");
            device.Write("INIT:CONT ON");
        }

        private void currText(object sender, TextChangedEventArgs e)
        {
            currLevel = currTextBox.Text;
        }

        private void compText(object sender, TextChangedEventArgs e)
        {
            compLevel = compTextBox.Text;
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

        private void setCurr(object sender, RoutedEventArgs e)
        {
            if (range == 0)
            {
                /// SCPI COMMAND CURR:RANG:AUTO ON
                try
                {
                    device.Write("CURR:RANG:AUTO ON");
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
                    device.Write("CURR:RANG:1e-3");
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
                    device.Write("CURR:RANG:10e-3");
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
                    device.Write("CURR:RANG:100e-3");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            device.Write("CURR:COMP " + compLevel);
            device.Write("CURR " + currLevel + "e-3");
        }

        private void currTurnON(object sender, RoutedEventArgs e)
        {
            device.Write("OUTP ON");
        }

        private void currTurnOFF(object sender, RoutedEventArgs e)
        {
            device.Write("OUTP OFF");
        }

        private void Integration_Rate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Integration_Rate.SelectedIndex == 0)
            {
                rate = "0.01";
            }

            if (Integration_Rate.SelectedIndex == 1)
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
            device.Write("SENS:VOLT:NPLC " + rate);
        }

        private async void startCap(object sender, RoutedEventArgs e)
        {
            if (check())
            {

                File.WriteUserInput(userInput);

                //startCapBtn.Enabled = false;
                //stopCapBtn.Enabled = true;
                capture = true;
                //Date = DateTime.Now;
                _canceller = new CancellationTokenSource();
                await Task.Run(() =>
                {
                    do
                    {
                        //device.Write("FETC?");
                        //out_put = device.ReadString();
                        //voltVals.Add(out_put);
                        Thread.Sleep(50);
                        Capture();
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            myDataCollection.Add(hardwareInput);
                            graphData.Add(new Data(CaptureTime, hardwareInput.Voltage, hardwareInput.Current, hardwareInput.Resistance, hardwareInput.Temperature));
                            int latestRow = SampleTable.Items.Count - 1;
                            SampleTable.ScrollIntoView(SampleTable.Items[latestRow]);                            
                        }));
                        
                        File.WriteSampleOutput(hardwareInput);
                        if (_canceller.Token.IsCancellationRequested)
                            break;
                    } while (true);
                });

                _canceller.Dispose();
                //startCapBtn.Enabled = true;
                //stopCapBtn.Enabled = false;
            }
        }

        private void stopCap(object sender, RoutedEventArgs e)
        {
            _canceller.Cancel();
            capture = false;
        }

        private void Capture()
        {
            Random rand = new Random();
            //Get voltage and current values
            //voltage = InputComm.GetVoltage();
            CaptureTime = DateTime.Now;
            hardwareInput.Voltage = rand.NextDouble() * 4 + 1;
            hardwareInput.Current = rand.NextDouble() * 4 + 1;
            hardwareInput.Temperature = rand.NextDouble() * 60 - 10;
            hardwareInput.Time = $"{CaptureTime.Hour:00}:{CaptureTime.Minute:00}:{CaptureTime.Second:00}.{CaptureTime.Millisecond:000}.{CaptureTime.Microsecond:000}";
            hardwareInput.Resistance = Calc.CalcResistance(hardwareInput.Voltage, hardwareInput.Current);
            hardwareInput.Resistivity = Calc.CalcResistivity(hardwareInput.Resistance, userInput.SampleLength * userInput.SampleWidth, userInput.SampleLength);



/*
            device.Write("FETC?");
            //device.Write("SENS:CH");
            out_put = device.ReadString();
            hardwareInput.Voltage = Math.Abs(Convert.ToDouble(out_put));

            //current = InputComm.GetCurrent();
            hardwareInput.Current = Convert.ToDouble(currLevel) / 1000;

            //Calculate resistance and resistivity values
            hardwareInput.Resistance = Calc.CalcResistance(hardwareInput.Voltage, hardwareInput.Current);
            hardwareInput.Resistivity = Calc.CalcResistivity(hardwareInput.Resistance, area, length);

            hardwareInput.Temperature = rand.NextDouble() * 60 - 10;

            hardwareInput.Time = $"{System.DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}:{DateTime.Now.Second:00}.{DateTime.Now.Millisecond:000}.{DateTime.Now.Microsecond:000}";*/

        }


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
            current_out.Text = currLevel;
            ohm_out.Text = resistance.ToString();
            rho_out.Text = resistivity.ToString();

            //if (capture)
            //{
            //    DatGen.AddData();
            //}
        }*/
    }

}
