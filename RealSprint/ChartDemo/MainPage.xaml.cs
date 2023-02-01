using System;
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
