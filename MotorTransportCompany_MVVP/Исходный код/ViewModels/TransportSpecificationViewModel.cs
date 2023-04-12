using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportSpecificationViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FuelType { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
