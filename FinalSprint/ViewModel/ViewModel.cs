﻿using ReSprint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
//using Windows.UI.Xaml;

namespace FinalSprint.ViewModel
{
    public class DataGenerator
    {
        //private Random randomNumber;
        private ObservableCollection<Data> data = new ObservableCollection<Data>();
        public int dataCount = 50000;
        private int rate = 1; // Use this to change rate/speed
        int index = 0;
        DispatcherTimer timer;
        public ObservableCollection<Data> DynamicData { get; set; }

        public DataGenerator() { }
        public DataGenerator(HardwareInput hardwareInput)
        {
            DynamicData = new ObservableCollection<Data>();
            data = GenerateData(hardwareInput);
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

        public ObservableCollection<Data> GenerateData(HardwareInput hardwareInput)
        {
            ObservableCollection<Data> generatedData = new ObservableCollection<Data>();

            for (int i = 0; i < this.dataCount; i++)
            {
                generatedData.Add(new Data(DateTime.Now, hardwareInput.Resistance, hardwareInput.Resistivity, hardwareInput.Temperature, hardwareInput.Voltage));
            }

            return generatedData;
        }

        private void timer_Tick(object sender, object e)
        {
            //if(MainWindow.capture)
                AddData();
        }
    }

    public class Data
    {
        public Data(DateTime time, double resistance, double resistivity, double temperature, double voltage)
        {
            Time = time;
            Resistance = resistance;
            Resistivity = resistivity;
            Temperature = temperature;
            Voltage = voltage;
        }

        public DateTime Time { get; set; }

        public double Resistance { get; set; }

        public double Resistivity { get; set; }

        public double Temperature { get; set; }
        public double Voltage { get; set; }
    }
}


//}