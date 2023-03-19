﻿using System;
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

using System.ComponentModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using FinalSprint.Hubs;
using Microsoft.AspNetCore.Cors;
using System.IO;
//using System.Windows.Forms;

namespace FinalSprint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        //private DataGenerator DatGen;

        //Member variables
        private bool capture;
        private double voltage;
        private double current;
        private double resistance;
        private double resistivity;
        private double area;
        private double length;

        private CancellationTokenSource _canceller;

        //Timer
        DispatcherTimer main_timer;
        //DateTime Date;

        #region Fields
        private string currentVisualStyle;
        private string currentSizeMode;
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

            this.Loaded += OnLoaded;
            Start_Server();

            //Initialise Class objects
            Calc = new Calculation();
            InputComm = new InputCommunication();
            //DatGen = (DataGenerator)this.DataContext;

            //Initialise variables
            capture = false;
            voltage = 0.0;
            current = 0.0;
            resistance = 0.0;
            resistivity = 0.0;
            area = 0.000003;
            length = 0.04;


            //Initialise timer for graph update
            main_timer = new DispatcherTimer();
            main_timer.Tick += main_timer_Tick;
            main_timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            main_timer.Start();
        }
        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// 



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
            .Build();*/


            await _host.StartAsync();
        }

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
                    if (_canceller.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller.Dispose();
            //startCapBtn.Enabled = true;
            //stopCapBtn.Enabled = false;
        }

        private void stopCap(object sender, RoutedEventArgs e)
        {
            _canceller.Cancel();
            capture = false;
        }

        private void Capture()
        {
            //Get voltage and current values
            //voltage = InputComm.GetVoltage();
            device.Write("FETC?");
            //device.Write("SENS:CH");
            out_put = device.ReadString();
            voltage = (-1)*Convert.ToDouble(out_put);
            Debug.WriteLine(voltage);

            //current = InputComm.GetCurrent();
            current = Convert.ToDouble(currLevel)/1000;

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
            current_out.Text = currLevel;
            ohm_out.Text = resistance.ToString();
            rho_out.Text = resistivity.ToString();

            //if (capture)
            //{
            //    DatGen.AddData();
            //}
        }
    }

}
