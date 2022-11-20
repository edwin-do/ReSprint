using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Ailon.QuickCharts;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReSprint
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double current;
        private double voltage;
        private double resistance;
        private double resistivity;
        private string time;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void FileInputBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.FileInputTxtBox.Text = file.Path;
                this.SelectedFileTxt.Text = file.Name;
            }
            else
            {
                this.SelectedFileTxt.Text = "No File Selected";
            }
        }

        private void Start_Output(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            calculation cal= new calculation();
            current = r.Next(1, 10);
            voltage = r.Next(1, 10);
            resistance = cal.calcResistence(voltage, current);
            resistivity = cal.calcResistivity(resistance, 10, 5);
            time = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
            OutputData.Items.Add(String.Format("{0, -20}{1, -10:N1}{2, -20:N3}{3, -20:N3}{4, -20:N3}{5, -20:N3}", time, (25 + r.Next(2)), resistivity, resistance, voltage, current));
        }
    }
}
