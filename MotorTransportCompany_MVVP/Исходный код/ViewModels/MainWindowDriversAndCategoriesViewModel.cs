using AutoMapper;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MotorTransportCompany_MVVP.Model.Domain;
using AutoMapper.EquivalencyExpression;
using System.Reflection;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowDriversAndCategoriesViewModel : INotifyPropertyChanged
    {
        public MainWindowDriversAndCategoriesViewModel()
        {
            _mapper = InitMappings();
            FillDriversAndCategoriesDataGrid();

            AddCommand = new RelayCommand(AddDriverAndCategory, () => true);
            EditCommand = new RelayCommand(EditDriverAndCategory, () => true);
            DeleteCommand = new RelayCommand(DeleteDriverAndCategory, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<DriversAndCategoriesViewModel> DriversAndCategories { get; set; }

        private readonly IMapper _mapper;

        
        private readonly DriverService _driverService = new DriverService();

        private readonly LicenseCategoriesService _licenseCategoriesService = new LicenseCategoriesService();
        private readonly DriversAndCategoriesService _driversAndCategoriesService = new DriversAndCategoriesService();

        private readonly IDialogService _dialogService = new DialogService();

        public event PropertyChangedEventHandler PropertyChanged;

        public DriversAndCategoriesViewModel SelectedEntity { get; set; }



        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                #region DriversAndCategories

                cfg.CreateMap<DriversAndCategoriesSqlView, DriversAndCategoriesViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();


                cfg.CreateMap<DriversAndCategoriesViewModel, DriversAndCategoriesWindowViewModel>()
                    .ReverseMap();


                cfg.CreateMap<DriversAndCategoriesWindowViewModel, DriversAndCategories>()
                    .ForMember(m => m.CategoryID, opt => opt.MapFrom(f => _licenseCategoriesService.GetAll().Find(c => c.Category == f.Categories).Id))
                    .ForMember(m => m.DriverID, opt => opt.MapFrom(f => _driverService.GetAll().Find(c => c.Surname == f.FCS.Split(' ')[0] && c.Name == f.FCS.Split(' ')[1] && c.Patronymic == f.FCS.Split(' ')[2]).Id))
                    .ReverseMap();

                cfg.CreateMap<DriversAndCategoriesViewModel, DriversAndCategories>()
                    .ForMember(m => m.CategoryID, opt => opt.MapFrom(f => _licenseCategoriesService.GetAll().Find(c => c.Category == f.Categories).Id))
                    .ForMember(m => m.DriverID, opt => opt.MapFrom(f => _driverService.GetAll().Find(c => c.Surname == f.FCS.Split(' ')[0] && c.Name == f.FCS.Split(' ')[1] && c.Patronymic == f.FCS.Split(' ')[2]).Id))
                    .ReverseMap();
                #endregion

            });



            return config.CreateMapper();

        }
        private void FillDriversAndCategoriesDataGrid()
        {
            DriversAndCategories = _mapper.Map<ObservableCollection<DriversAndCategoriesViewModel>>(_driversAndCategoriesService.GetAll());
        }
        #region DriverAndCategoriesCommands
        private void AddDriverAndCategory()
        {
            var viewModel = _mapper.Map<DriversAndCategoriesWindowViewModel>(new DriversAndCategoriesViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;
            
            
                ChekOnAlreadyContainsCategories(viewModel);

                FillDriversAndCategoriesDataGrid();
            
            
        }
        private void EditDriverAndCategory()
        {
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<DriversAndCategoriesWindowViewModel>(SelectedEntity);

                var res = _dialogService.OpenDialog(viewModel);

                if (res != true) return;
                
                    ChekOnAlreadyContainsCategories(viewModel);

                    FillDriversAndCategoriesDataGrid();
                
            }
            
            
        }
        private void DeleteDriverAndCategory()
        {
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<DriversAndCategoriesWindowViewModel>(SelectedEntity);

                DeleteAllCategories(viewModel);

                FillDriversAndCategoriesDataGrid();

            }


        }
        private void ChekOnAlreadyContainsCategories(DriversAndCategoriesWindowViewModel filledDriversAndCategoriesWindowViewModel)
        {
            string surname = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[0];
            string name = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[1];
            string patronymic = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[2];

            foreach (var driversCategoriy in filledDriversAndCategoriesWindowViewModel.CheckComboCategoriesList)
            {
                if (driversCategoriy.Check_Status == true)
                {

                    if (_driversAndCategoriesService.GetAll().Find(f => f.Categories.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).Contains(driversCategoriy.Categs) && f.Name == name && f.Surname == surname && f.Patronymic == patronymic) != null)
                    {
                        continue;
                    }
                    else
                    {
                        var filledDriverAndCategory = new DriversAndCategoriesViewModel
                        {
                            Id = filledDriversAndCategoriesWindowViewModel.Id,
                            Department = filledDriversAndCategoriesWindowViewModel.Department,
                            FCS = filledDriversAndCategoriesWindowViewModel.FCS,
                            LicenseNumber = filledDriversAndCategoriesWindowViewModel.LicenseNumber,
                            Categories = driversCategoriy.Categs
                        };
                        var addingDriverAndCategory = _mapper.Map<DriversAndCategories>(filledDriverAndCategory);
                        _driversAndCategoriesService.Add(addingDriverAndCategory);
                    }

                }
                else
                {
                    var smthing = _driversAndCategoriesService.GetAll().Find(f => f.Categories.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries).Contains(driversCategoriy.Categs) && f.Name == name && f.Surname == surname && f.Patronymic == patronymic);
                    if (smthing != null)
                    {
                        var driver = _driverService.GetAll().Find(f => f.Name == smthing.Name && f.Patronymic == smthing.Patronymic && f.Surname == smthing.Surname && f.LicenseNumber == smthing.LicenseNumber);
                        var category = _licenseCategoriesService.GetAll().Find(f => f.Category == driversCategoriy.Categs).Id;
                        int deletingDriverAndCategoryId = _driversAndCategoriesService.GetTable().Find(f => f.DriverID == driver.Id && f.CategoryID == category).Id;
                        _driversAndCategoriesService.Delete(deletingDriverAndCategoryId);
                    }
                    else
                        continue;
                }
            }
        }
        private void DeleteAllCategories(DriversAndCategoriesWindowViewModel filledDriversAndCategoriesWindowViewModel)
        {

            string surname = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[0];
            string name = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[1];
            string patronymic = filledDriversAndCategoriesWindowViewModel.FCS.Split(' ')[2];
            foreach (var cat in filledDriversAndCategoriesWindowViewModel.Categories.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                var smthing = _driversAndCategoriesService.GetAll().Find(f => f.Name == name && f.Surname == surname && f.Patronymic == patronymic);
                if (smthing != null)
                {
                    var driver = _driverService.GetAll().Find(f => f.Name == smthing.Name && f.Patronymic == smthing.Patronymic && f.Surname == smthing.Surname && f.LicenseNumber == smthing.LicenseNumber);
                    int deletingDriverAndCategoryId = _driversAndCategoriesService.GetTable().Find(f => f.DriverID == driver.Id).Id;
                    _driversAndCategoriesService.Delete(deletingDriverAndCategoryId);
                }
            }
        }
        #endregion
        
    }
}
