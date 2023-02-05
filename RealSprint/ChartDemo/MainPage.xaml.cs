using ReSprint;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RealSprint
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Initialise class objects
            InputComm = new InputCommunication();
            Calc = new Calculation();
            DatGen = (DataGenerator)this.DataContext;

            //Initialise member variables
            capture = false;
            voltage = 0.0;
            current = 0.0;
            resistance = 0.0;
            resistivity = 0.0;
            area = 0.000003;
            length = 0.04;

            //Initialise buttons
            StartCaptureBtn.IsEnabled = true;
            StopCaptureBtn.IsEnabled = false;

            //Initialise timer for graph update
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();

        }

        private async void StartCaptureBtn_Click(object sender, RoutedEventArgs e)
        {
            StartCaptureBtn.IsEnabled = false;
            StopCaptureBtn.IsEnabled = true;
            capture = true;
            date = DateTime.Now;
            _canceller = new CancellationTokenSource();

            await Task.Run(() =>
            {
                do
                {
                    Capture();
                    if (_canceller.Token.IsCancellationRequested)
                        break;
                } while (true);
            });

            _canceller.Dispose();
            StartCaptureBtn.IsEnabled = true;
            StopCaptureBtn.IsEnabled = false;   

        }

        private void StopCaptureBtn_Click(object sender, RoutedEventArgs e)
        {
            _canceller.Cancel();
            capture = false;
        }

        private void Capture()
        {
            //Get voltage and current values
            voltage = InputComm.GetVoltage();
            current = InputComm.GetCurrent();

            //Calculate resistance and resistivity values
            resistance = Calc.calcResistence(voltage, current);
            resistivity = Calc.calcResistivity(resistance, area, length);

            //Send values to graph
            //TODO
            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //{
            //    DatGen.StartOutput();
            //});

            //Send values to CurrentState output
            //TODO

        }

        private void timer_Tick(object sender, object e)
        {
            //Pass values to DataGenerator
            if (capture)
            {
                DatGen.AddData(new Data(date, current, voltage, resistivity));
                date = date.Add(TimeSpan.FromMilliseconds(50));
            }
        }


        //Class objects
        private InputCommunication InputComm;
        private Calculation Calc;
        private DataGenerator DatGen;

        //Member variables
        private bool capture;
        private double voltage;
        private double current;
        private double resistance;
        private double resistivity;
        private double area;
        private double length;

        //For ending continuous capture 
        private CancellationTokenSource _canceller;

        //Timer
        DispatcherTimer timer;

        DateTime date;

        private void InstrumentVoltageValueLabel_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void InstrumentVoltageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }

    //private async void FileInputBtn_Click(object sender, RoutedEventArgs e)
    //{
    //    var picker = new Windows.Storage.Pickers.FileOpenPicker();
    //    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
    //    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
    //    picker.FileTypeFilter.Add(".csv");
    //    picker.FileTypeFilter.Add(".txt");

    //    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
    //    if (file != null)
    //    {
    //        // Application now has read/write access to the picked file
    //        this.FileInputTxtBox.Text = file.Path;
    //        this.SelectedFileTxt.Text = file.Name;
    //    }
    //    else
    //    {
    //        this.SelectedFileTxt.Text = "No File Selected";
    //    }
    //}




}
