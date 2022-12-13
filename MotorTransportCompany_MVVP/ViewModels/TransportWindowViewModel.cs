using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportWindowViewModel : IDialogViewModel
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
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }
        static DepartmentService _departmentService = new DepartmentService();
        static TechnicalConditionService _technicalConditionService = new TechnicalConditionService();
        static FuelTypeService _fuelTypeService = new FuelTypeService();
        public List<string> DepartmentsNames { get; set; } = new List<string>();
        public List<string> FuelTypes { get; set; } = new List<string>();
        public List<string> TechnicalConditions { get; set; } = new List<string>();
        private void FillDepartmentsNames(List<Department> departments)
        {
            if (departments.Count != 0)
            {
                foreach (Department dep in departments)
                {
                    DepartmentsNames.Add(dep.Name);
                }
            }
        }
        private void FillFuelTypes(List<FuelType> fuelTypes)
        {
            if (fuelTypes.Count != 0)
            {
                foreach (FuelType dep in fuelTypes)
                {
                    FuelTypes.Add(dep.Name);
                }
            }
        }
        private void FillTechnicalConditions(List<TechnicalCondition> technicalConditions)
        {
            if(technicalConditions.Count != 0)
            {
                foreach (TechnicalCondition ft in technicalConditions)
                {
                    TechnicalConditions.Add(ft.Condition);
                }
            }
        }
        public TransportWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillDepartmentsNames(_departmentService.GetAll());
            FillFuelTypes(_fuelTypeService.GetAll());
            FillTechnicalConditions(_technicalConditionService.GetAll());
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
