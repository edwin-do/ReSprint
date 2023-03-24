using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
/*            data = GenerateData();

            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();*/
        }

        public void AddData(Data values)
        {
            for (int i = 0; i < rate; i++)
            {
                index++;
                if (index < 100)
                {
                    DynamicData.Add(values);
                }
                else if (index > 100)
                {
                    DynamicData.RemoveAt(0);//Remove data not visible
                    DynamicData.Add(values);
                }
            }
        }

 /*       public void AddData()
        {
            Data sample = new Data(DateTime.Now, 1003, 1003, 1003);
            index++;
            Debug.WriteLine(sample.Value1);
                if (index < 100)
                {
                    DynamicData.Add(new Data(DateTime.Now, 1003, 1003, 1003));
                }
                else if (index > 100)
                {
                    DynamicData.RemoveAt(0);//Remove data not visible
                    DynamicData.Add(this.data[(index % (this.data.Count - 1))]);
                }
        }*/

/*        public void LoadData()
        {
            for (int i = 0; i < 10; i++)
            {
                index++;
                if (index < data.Count)
                {
                    DynamicData.Add(this.data[index]);
                }
            }
        }*/

/*        public ObservableCollection<Data> GenerateData()
        {
            ObservableCollection<Data> generatedData = new ObservableCollection<Data>();

            DateTime date = new DateTime(2009, 1, 1);
            double value = 1000;
            double value1 = 1001;
            double value2 = 1002;
            for (int i = 0; i < this.dataCount; i++)
            {
                generatedData.Add(new Data(date, value, value1, value2));
                date = date.Add(TimeSpan.FromSeconds(5));

                if ((randomNumber.NextDouble() + value2) < 1004.85)
                {
                    double random = randomNumber.NextDouble();
                    value += random;
                    value1 += random;
                    value2 += random;
                }
                else
                {
                    double random = randomNumber.NextDouble();
                    value -= random;
                    value1 -= random;
                    value2 -= random;
                }
            }

            return generatedData;
        }

        private void timer_Tick(object sender, object e)
        {
            AddData();
        }*/
    }
}


//}
