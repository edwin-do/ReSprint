using ReSprint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.UI.Xaml.Charts;
using System.Security.RightsManagement;

namespace FinalSprint
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        private readonly string hardwareInputHeader = "Time, Voltage, Current, Resistance, Resistivity, Temperture\n";
        private readonly HardwareInput hardwareInput;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private ObservableCollection<double> _resistance;
        public ObservableCollection<double> Resistance
        {
            set
            {
                _resistance = value;
                OnPropertyChanged("Resistance");
            }
            get
            {
                return _resistance;
            }
        }

        private ObservableCollection<double> _resistivity;
        public ObservableCollection<double> Resistivity
        {
            set
            {
                _resistivity = value;
                OnPropertyChanged("Resistivity");
            }
            get
            {
                return _resistivity;
            }
        }

        private ObservableCollection<double> _voltage;
        public ObservableCollection<double> Voltage
        {
            set
            {
                _voltage = value;
                OnPropertyChanged("Voltage");
            }
            get
            {
                return _voltage;
            }
        }
        
        public GraphViewModel()
        {
            Resistance = new ObservableCollection<double> {hardwareInput.Resistance};
            Voltage = new ObservableCollection<double> { hardwareInput.Voltage};
            Resistivity = new ObservableCollection<double> { hardwareInput.Resistivity };
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
