using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportSpecificationWindowViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int FuelType { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private FuelTypeService _fuelTypeService = new FuelTypeService();
        private ObservableCollection<string> FuelTypes { get; set; } = new ObservableCollection<string>();

        private void FillFuelTypes()
        {
            foreach(var ft in _fuelTypeService.GetAll())
            {
                FuelTypes.Add(ft.Name);
            }
        }
        TransportSpecificationWindowViewModel()
        {
            FillFuelTypes();
        }
    }
}
