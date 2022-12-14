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
    internal class TransportDistributionWindowViewModel : IDialogViewModel, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _department;
        public string Department 
        {
            get { return _department; }
            set
            {
                _department = value;
                FillNumbersAndModels();
                FillFCSs();
                NumberAndModel = null;
                FCS = null;
            }
        }
        public string FCS { get; set; }
        public string NumberAndModel { get; set; }
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }

        static DriverService _driverService = new DriverService();
        static TransportService _transportService = new TransportService();
        static DepartmentService _departmentService = new DepartmentService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> NumbersAndModels { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FCSs { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Departments{ get; set; } = new ObservableCollection<string>();



        TransportDistributionWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillDepartmentsNames();
            FillNumbersAndModels();
            FillFCSs();
            
        }
        private void FillDepartmentsNames()
        {
            foreach(Department dep in _departmentService.GetAll())
            {
                Departments.Add(dep.Name);
            }
        }
        private void FillNumbersAndModels()
        {
            foreach (var ts in _transportService.GetAll())
            {
                if(ts.Department == Department)
                    NumbersAndModels.Add($"{ts.Number}|{ts.Model}");
            }
        }
        
        

       
        
    
        private void FillFCSs()
        {
            foreach(var fc in _driverService.GetAll())
            {
                if(fc.DepartmentName == Department)
                    FCSs.Add($"{fc.Surname} {fc.Name} {fc.Patronymic}");
            }
        }
        public void Ok()
        {
            DialogResult = true;
        }
        public bool CanOk()
        {
            return true;
        }


        // Работа с комбобоксом Transport
        public class TransportBoxItems : INotifyPropertyChanged
        {
            public string NumberAndModel { get; set; }
            public bool Check_Status { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }
        public ObservableCollection<TransportBoxItems> TransportToCombobox { get; set; } = new ObservableCollection<TransportBoxItems>();

        private void FillTransportToCombobox()
        {
            foreach(var nm in NumbersAndModels)
            {
                TransportToCombobox.Add(new TransportBoxItems
                {
                    NumberAndModel = nm,
                    Check_Status = true
                });
            }
        }
        // Работа с комбобоксом Drivers
        public class FCSsComboboxBoxItems : INotifyPropertyChanged
        {
            public string FCS { get; set; }
            public bool Check_Status { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }
        public ObservableCollection<FCSsComboboxBoxItems> DriversToCombobox { get; set; } = new ObservableCollection<FCSsComboboxBoxItems>();

        private void FillDriversToCombobox()
        {
            foreach (var nm in FCSs)
            {
                DriversToCombobox.Add(new FCSsComboboxBoxItems
                {
                    FCS = nm,
                    Check_Status = true
                });
            }
        }


    }
}
