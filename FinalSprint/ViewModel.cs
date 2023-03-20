using ReSprint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace FinalSprint
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
            randomNumber = new Random();
            DynamicData = new ObservableCollection<Data>();
            data = GenerateData();
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

        public ObservableCollection<Data> GenerateData()
        {
            ObservableCollection<Data> generatedData = new ObservableCollection<Data>();

            DateTime date = new DateTime(2009, 1, 1);
            double resistance = 1000;
            double resistivity = 1001;
            double temp = 1002;
            double voltage = 1003;

            for (int i = 0; i < this.dataCount; i++)
            {
                generatedData.Add(new Data(date, resistance, resistivity, temp, voltage));
                date = date.Add(TimeSpan.FromSeconds(5));

                resistance = resistance;
                resistivity = resistivity;
                temp = temp;
                voltage = voltage;

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
