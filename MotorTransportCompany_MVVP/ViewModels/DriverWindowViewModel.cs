using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System.ComponentModel;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriverWindowViewModel : INotifyPropertyChanged, IDialogViewModel
    {
        // Что касается Dirvera
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int PassportNumber { get; set; }
        public int LicenseNumber { get; set; }
        // что касается DriverCategories
        

        public List<CheckComboCategories> CheckComboCategoriesList { get; set; } = new List<CheckComboCategories>();
        public class CheckComboCategories
        {
            public string Categs { get; set; }
            public bool Check_Status { get; set; }
        }
        public LicenseCategoriesService _licenseCategoriesService = new LicenseCategoriesService();
        static DepartmentService _departmentService = new DepartmentService();
        static SexService _sexService = new SexService();
        public DriversAndCategoriesService _driversAndCategoriesService = new DriversAndCategoriesService();

        public event PropertyChangedEventHandler PropertyChanged;

        DriverWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillDepartmentsNames(_departmentService.GetAll());
            FillSexTypes(_sexService.GetAll());
            
        }
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }
        public ObservableCollection<string> DepartmentsNames { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> SexTypes { get; set; } = new ObservableCollection<string>();
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
