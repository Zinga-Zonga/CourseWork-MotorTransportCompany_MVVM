using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriversAndCategoriesWindowViewModel : IDialogViewModel, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Department { get; set; }
        private string _fcs;
        public string FCS 
        { 
            get { return _fcs; }
            set
            {
                _fcs = value;
                CheckComboCategoriesList.Clear();
                SelectedCategories.Clear();
                FillCheckComboCategories();
                
                LicenseNumber = _driverService.GetAll().Find(f => f.Surname == FCS.Split(' ')[0] && f.Name == FCS.Split(' ')[1] && f.Patronymic == FCS.Split(' ')[2]).LicenseNumber;
                Department = _driverService.GetAll().Find(f => f.Surname == FCS.Split(' ')[0] && f.Name == FCS.Split(' ')[1] && f.Patronymic == FCS.Split(' ')[2]).DepartmentName;
            }
        }
        public int LicenseNumber { get; set; }
        public string Categories { get; set; }
        public ObservableCollection<CheckComboCategories> SelectedCategories { get; set; } = new ObservableCollection<CheckComboCategories>();

        static DriverService _driverService = new DriverService();
        public ObservableCollection<string> FCSs { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<CheckComboCategories> CheckComboCategoriesList { get; set; } = new ObservableCollection<CheckComboCategories>();
        
        public LicenseCategoriesService _licenseCategoriesService = new LicenseCategoriesService();
        public DriversAndCategoriesService _driversAndCategoriesService = new DriversAndCategoriesService();

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }
        public class CheckComboCategories : INotifyPropertyChanged
        {
            public string Categs { get; set; }
            public bool Check_Status { get; set; } = false;

            public event PropertyChangedEventHandler PropertyChanged;
        }
        private void FillFCSs()
        {
            foreach (var fc in _driverService.GetAll())
            {
                FCSs.Add($"{fc.Surname} {fc.Name} {fc.Patronymic}");
            }
        }
        private void FillCheckComboCategories()
        {
            if(FCS != null)
            {
                string Surname = FCS.Split(' ')[0];
                string Name = FCS.Split(' ')[1];
                string Patronymic = FCS.Split(' ')[2];
                var dac = _driversAndCategoriesService.GetAll().Find(f => f.Name == Name && f.Surname == Surname && f.Patronymic == Patronymic);
                if (dac != null)
                {
                    foreach(var cat in _licenseCategoriesService.GetAll())
                    {
                        string[] categories = dac.Categories.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (categories.Contains(cat.Category))
                        {
                            CheckComboCategoriesList.Add(new CheckComboCategories
                            {
                                Categs = cat.Category,
                                Check_Status = true
                            });
                        }
                        else
                        {
                            CheckComboCategoriesList.Add(new CheckComboCategories
                            {
                                Categs = cat.Category,
                                Check_Status = false
                            });
                        }
                    }                
                }
                else
                {
                    foreach (var cat in _licenseCategoriesService.GetAll())
                    {
                        CheckComboCategoriesList.Add(new CheckComboCategories
                        {
                            Categs = cat.Category,
                            Check_Status = false
                        });
                    }
                }
            }
            
            



        }
        DriversAndCategoriesWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillFCSs();
            FillCheckComboCategories();
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
