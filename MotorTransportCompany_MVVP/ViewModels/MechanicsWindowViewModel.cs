using AutoMapper;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MechanicsWindowViewModel : INotifyPropertyChanged, IDialogViewModel
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

        static DepartmentService _departmentService = new DepartmentService();
        static SexService _sexService = new SexService();
        
        public List<string> DepartmentsNames { get; set; } = new List<string>();
        public List<string> SexTypes { get; set; } = new List<string>();
        private void FillDepartmentsNames(List<Department> departments)
        {
            if(departments.Count != 0)
            {
                foreach(Department dep in departments)
                {
                    DepartmentsNames.Add(dep.Name);
                }
            }
        }
        private void FillSexTypes(List<SexType> sexTypes)
        {
            if(sexTypes.Count != 0)
            {
                foreach(SexType sex in sexTypes)
                {
                    SexTypes.Add(sex.Name);
                }
            }
        }
        public MechanicsWindowViewModel()
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

        //#region INotifyDataErrorInfo implementation

        //public IEnumerable GetErrors(string propertyName)
        //{
        //    var validationResults = new List<ValidationResult>();

        //    var property = GetType().GetProperty(propertyName);

        //    var validationContext = new ValidationContext(this)
        //    {
        //        MemberName = propertyName
        //    };

        //    var isValid = Validator.TryValidateProperty(property.GetValue(this), validationContext, validationResults);
        //    if (isValid)
        //    {
        //        return null;
        //    }

        //    return validationResults;
        //}

        //public bool HasErrors
        //{
        //    get
        //    {
        //        // получаем список всех свойств автоматически через рефлексию:

        //        var properties = GetType().GetProperties().Where(p => p.CustomAttributes.Count() > 0);

        //        // пробегаемся по всем свойствам
        //        // и если какое-то невалидно, то текст общей ошибки валидации = текст ошибки по этому свойству

        //        foreach (var property in properties)
        //        {
        //            var error = GetErrors(property.Name);

        //            if (error != null)
        //            {
        //                return true;
        //            }
        //        }

        //        return false;
        //    }
        //}

        //#endregion
    }
}
