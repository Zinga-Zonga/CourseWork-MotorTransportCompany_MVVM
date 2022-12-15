using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MotorTransportCompany_MVVP.Model.Services;
using System.Collections.Specialized;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriverWindowViewModel : INotifyCollectionChanged
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
        public int LicenseNumber { get; set; }
        public string Categories { get; set; }

        public ObservableCollection<CheckComboCategories> CheckComboCategoriesList { get; set; }
        public class CheckComboCategories
        {
            public string Categs { get; set; }
            public bool Check_Status { get; set; }
        }
        public LicenseCategoriesService _licenseCategoriesService = new LicenseCategoriesService();
        static DepartmentService _departmentService = new DepartmentService();
        static SexService _sexService = new SexService();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        DriverWindowViewModel()
        {
            FillDepartmentsNames(_departmentService.GetAll());
            FillSexTypes(_sexService.GetAll());
            FillCheckComboCategories();
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
        private void FillCheckComboCategories()
        {
            foreach(var category in _licenseCategoriesService.GetAll())
            {
                bool check = false;
                if (Categories.Split(',').Contains(category.Category))
                {
                    check = true;
                }
                new CheckComboCategories()
                {
                    Categs = category.Category,
                    Check_Status = check
                };
            }
        }
    }
}
