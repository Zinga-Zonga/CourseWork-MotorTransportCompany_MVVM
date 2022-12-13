using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using MotorTransportCompany_MVVP.Util.Dialogs;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class GarageManagerWindowViewModel : IDialogViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int PassportNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OkCommand { get; }


        public bool? DialogResult { get; set; }
        public List<string> DepartmentsNames { get; set; } = new List<string>();
        public List<string> SexTypes { get; set; } = new List<string>();
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
        private void FillSexTypes(List<SexType> sexTypes)
        {
            if (sexTypes.Count != 0)
            {
                foreach (SexType sex in sexTypes)
                {
                    SexTypes.Add(sex.Name);
                }
            }
        }
        static DepartmentService _departmentService = new DepartmentService();
        static SexService _sexService = new SexService();
        public GarageManagerWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillDepartmentsNames(_departmentService.GetAll());
            FillSexTypes(_sexService.GetAll());
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
