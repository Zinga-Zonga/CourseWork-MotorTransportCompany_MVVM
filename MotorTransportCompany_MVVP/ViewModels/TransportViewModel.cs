using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportViewModel : INotifyPropertyChanged
    { 
        public int Id { get; set; }
        public string Department { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
        public string Fuel { get; set; }
        public double TrunkVolume { get; set; }
        public double FuelConsumption { get; set; }
        public string TechnicalCondition { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
