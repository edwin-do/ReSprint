using ReSprint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace FinalSprint.ViewModel
{
    public class DataGenerator
    {
        private Random randomNumber;
        private ObservableCollection<Data> data = new ObservableCollection<Data>();
        public int dataCount = 50000;
        private int rate = 1; // Use this to change rate/speed
        int index = 0;
        DispatcherTimer timer;
        public ObservableCollection<Data> DynamicData { get; set; }


        public DataGenerator()
        {
            DynamicData = new ObservableCollection<Data>();
            //data = GenerateData();
            LoadData();

            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();
        }

        public void AddData()
        {
            for (int i = 0; i < rate; i++)
            {
                index++;
                if (index < 100)
                {
                    DynamicData.Add(this.data[index]);
                }
                else if (index > 100)
                {
                    DynamicData.RemoveAt(0);//Remove data not visible
                    DynamicData.Add(this.data[(index % (this.data.Count - 1))]);
                }
            }
        }

        public void LoadData()
        {
            for (int i = 0; i < 10; i++)
            {
                index++;
                if (index < data.Count)
                {
                    DynamicData.Add(this.data[index]);
                }
            }
        }

        public static ObservableCollection<Data> GenerateData(HardwareInput hardwareInput)
        {
            ObservableCollection<Data> generatedData = new ObservableCollection<Data>();

            for (int i = 0; i < 1; i++)
            {
                generatedData.Add(new Data(hardwareInput.Time, hardwareInput.Resistance, hardwareInput.Resistivity, hardwareInput.Temperature, hardwareInput.Voltage));
            }

            return generatedData;
        }

        private void timer_Tick(object sender, object e)
        {
            AddData();
        }
    }

    public class Data
    {
        public Data(DateTime date, double resistance, double resistivity, double temp, double voltage)
        {
            Date = date;
            Resistance = resistance;
            Resistivity = resistivity;
            Temp = temp;
            Voltage = voltage;
        }

        public DateTime Date { get; set; }

        public double Resistance { get; set; }

        public double Resistivity { get; set; }

        public double Temp { get; set; }
        public double Voltage { get; set; }
    }
}


//}
