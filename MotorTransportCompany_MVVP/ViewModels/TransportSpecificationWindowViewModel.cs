using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportSpecificationWindowViewModel : INotifyPropertyChanged, IDialogViewModel
    {
        public int Id { get; set; }
        public string FuelType { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private FuelTypeService _fuelTypeService = new FuelTypeService();
        public ObservableCollection<string> FuelTypes { get; set; } = new ObservableCollection<string>();

        private void FillFuelTypes()
        {
            foreach(var ft in _fuelTypeService.GetAll())
            {
                FuelTypes.Add(ft.Name);
            }
        }
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }
        TransportSpecificationWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillFuelTypes();
        }
        public void Ok()
        {
            DialogResult = true;
        }
        public bool CanOk()
        {
            return true;
        }
    }
}
