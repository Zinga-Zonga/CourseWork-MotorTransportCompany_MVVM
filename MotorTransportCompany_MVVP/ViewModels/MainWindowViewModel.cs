using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Security;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        // Если обьект к которому мы при привязываемся имеет Такую-то вью модель, то мы даём такой-то функционал
        public MainWindowDriverViewModel MainWindowDriverVM { get; set; }
        public MainWindowViewModel()
        {
            MainWindowDriverVM = new MainWindowDriverViewModel();
            SelectedViewModel = MainWindowDriverVM;
        }
        public object SelectedViewModel { get; set; }
        public RelayCommand AddMechanicCommand { get; }
        public RelayCommand EditMechanicCommand { get; }
        public RelayCommand DeleteMechanicCommand { get; }
        #region initiationOfServices and entities
        private readonly MechanicService _mechanicService = new MechanicService();
        private readonly GarageManagerService _garageManagerService = new GarageManagerService();
        private readonly TransportService _transportService = new TransportService();
        private readonly TransportDistributionService _transportDistributionService = new TransportDistributionService();
        private readonly DriverService _driverService = new DriverService();
        private readonly DriversAndCategoriesService _driversAndCategoriesService = new DriversAndCategoriesService();
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();
        private readonly TransportSpecificationService _transportSpecificationService = new TransportSpecificationService();
        private readonly TechnicalConditionService _technicalConditionService = new TechnicalConditionService();
        private readonly FuelTypeService _fuelTypeService = new FuelTypeService();
        private readonly LicenseCategoriesService _licenseCategoriesService = new LicenseCategoriesService();
        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }
        public ObservableCollection<GarageManagerViewModel> GarageManagers { get; set; }
        public ObservableCollection<TransportViewModel> Transport { get; set; }
        public ObservableCollection<TransportDistributionViewModel> TransportDistribution { get; set; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }
        public ObservableCollection<TransportSpecificationViewModel> TransportSpecifications { get; set; }
        public ObservableCollection<DriversAndCategoriesViewModel> DriversAndCategories { get; set; }
        #endregion
        private readonly IDialogService _dialogService = new DialogService();

        private TransportViewModel _selectedEntity;
        public TransportViewModel SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
            }
        }

        private readonly IMapper _mapper;

        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
            cfg.AddCollectionMappers();

                #region GarageManager
                // Завгар из SQL в Табличного завгара
                cfg.CreateMap<GarageManagerSqlView, GarageManagerViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();

                // Из таблицы  в окошко редактирования/добавления
                cfg.CreateMap<GarageManagerViewModel, GarageManagerWindowViewModel>()
                .ForMember(m => m.Surname, opt => opt.MapFrom(f => f.FCS.Split(' ')[0]))
                .ForMember(m => m.Name, opt => opt.MapFrom(f => f.FCS.Split(' ')[1]))
                .ForMember(m => m.Patronymic, opt => opt.MapFrom(f => f.FCS.Split(' ')[2]))
                .ReverseMap();

                // Из ВьюМодели в базовую таблицу механиков
                cfg.CreateMap<GarageManagerWindowViewModel, GarageManager>()
                .ForMember(m => m.Department_id, opt => opt.MapFrom
                    (f => _departmentService.GetAll().Find(c => c.Name == f.DepartmentName).Id))
                .ForMember(m => m.IdSex, opt => opt.MapFrom
                    (f => _sexService.GetAll().Find(c => c.Name == f.Sex).Id))
                .ReverseMap();
                #endregion

                #region TransportDistribution
                // Таблица из SQL водителей и автом в таблицу водителей и авто
                cfg.CreateMap<TransportDistributionSqlView, TransportDistributionViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();

                cfg.CreateMap<TransportDistributionViewModel, TransportDistributionWindowViewModel>()
                    .ForMember(m => m.NumberAndModel, opt => opt.MapFrom(f => string.Format("{0}|{1}", f.Number, f.Model)))
                    .ReverseMap();

                cfg.CreateMap<TransportDistributionWindowViewModel, TransportDistribution>()
                    .ForMember(m => m.Transport_ID, opt => opt.MapFrom
                        (f => _transportService.GetAll().Find(c => c.Model == f.NumberAndModel.Split('|')[1] && c.Number == f.NumberAndModel.Split('|')[0]).Id))
                    .ForMember(m => m.Driver_ID, opt => opt.MapFrom
                        (f => _driverService.GetAll().Find(c => c.Surname == f.FCS.Split(' ')[0] && c.Name == f.FCS.Split(' ')[1] && c.Patronymic == f.FCS.Split(' ')[2]).Id))
                    .ReverseMap();

                #endregion

                #region Mechanics
                // Механик из SQL в Табличного механика
                cfg.CreateMap<MechanicSqlView, MechanicsViewModel>()
                    .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                    .ReverseMap();

                // Из таблицы  в окошко редактирования/добавления
                cfg.CreateMap<MechanicsViewModel, MechanicsWindowViewModel>()
                .ForMember(m => m.Surname, opt => opt.MapFrom(f => f.FCS.Split(' ')[0]))
                .ForMember(m => m.Name, opt => opt.MapFrom(f => f.FCS.Split(' ')[1]))
                .ForMember(m => m.Patronymic, opt => opt.MapFrom(f => f.FCS.Split(' ')[2]))
                .ReverseMap();

                // Из ВьюМодели в базовую таблицу механиков
                cfg.CreateMap<MechanicsWindowViewModel, Mechanic>()
                .ForMember(m => m.Department_id, opt => opt.MapFrom
                    (f => _departmentService.GetAll().Find(c => c.Name == f.DepartmentName).Id))
                .ForMember(m => m.IdSex, opt => opt.MapFrom
                    (f => _sexService.GetAll().Find(c => c.Name == f.Sex).Id))
                .ReverseMap();
                #endregion

                #region Transport
                
                cfg.CreateMap<TransportSqlView, TransportViewModel>()
                   .ReverseMap();

                cfg.CreateMap<TransportViewModel, TransportWindowViewModel>()
                    .ReverseMap();

                cfg.CreateMap<TransportWindowViewModel, Transport>()
                    .ForMember(m => m.Department_ID, opt => opt.MapFrom
                        (f => _departmentService.GetAll().Find(c => c.Name == f.Department).Id))
                    .ForMember(m => m.TransportSpecification_ID, opt => opt.MapFrom(f => _transportSpecificationService.GetAll().Find(c => c.Model == f.Model && c.FuelType == f.Fuel).Id))
                    .ForMember(m => m.TechnicalCondition_ID, opt => opt.MapFrom(f => _technicalConditionService.GetAll().Find(c => c.Condition == f.TechnicalCondition).Id))
                    .ForMember(m => m.TransportNumber, opt => opt.MapFrom(f => f.Number))
                    .ReverseMap();

                    #endregion

                #region Drivers
                cfg.CreateMap<DriverSqlView, DriverViewModel>()
                    .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                    .ReverseMap();

                // Из таблицы  в окошко редактирования/добавления
                cfg.CreateMap<DriverViewModel, DriverWindowViewModel>()
                .ForMember(m => m.Surname, opt => opt.MapFrom(f => f.FCS.Split(' ')[0]))
                .ForMember(m => m.Name, opt => opt.MapFrom(f => f.FCS.Split(' ')[1]))
                .ForMember(m => m.Patronymic, opt => opt.MapFrom(f => f.FCS.Split(' ')[2]))
                .ReverseMap();

                // Из ВьюМодели в базовую таблицу механиков
                cfg.CreateMap<DriverWindowViewModel, Driver>()
                .ForMember(m => m.Department_id, opt => opt.MapFrom
                    (f => _departmentService.GetAll().Find(c => c.Name == f.DepartmentName).Id))
                .ForMember(m => m.IdSex, opt => opt.MapFrom
                    (f => _sexService.GetAll().Find(c => c.Name == f.Sex).Id))
                .ReverseMap();
                #endregion

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
                #region TransportSpecification
                cfg.CreateMap<TransportSpecificationSqlView, TransportSpecificationViewModel>()
                .ReverseMap();

                cfg.CreateMap<TransportSpecificationViewModel, TransportSpecificationWindowViewModel>()
                    .ReverseMap();

                cfg.CreateMap<TransportSpecificationWindowViewModel, TransportSpecification>()
                    .ForMember(m => m.FuelType_ID, opt => opt.MapFrom(f => _fuelTypeService.GetAll().Find(c => c.Name == f.FuelType).Id))
                    .ReverseMap();
                #endregion
            });

            return config.CreateMapper();

        }
        #region FillGridFuncs
        private void FillMechanicsDataGrid()
        {
            Mechanics = _mapper.Map<ObservableCollection<MechanicsViewModel>>(_mechanicService.GetAll());
        }
        private void FillGarageManagersDataGrid()
        {
            GarageManagers = _mapper.Map<ObservableCollection<GarageManagerViewModel>>(_garageManagerService.GetAll());
        }
        private void FillTransportDataGrid()
        {
            //Transport = _transportService.GetAll();
            Transport = _mapper.Map<ObservableCollection<TransportViewModel>>(_transportService.GetAll());
        }
        private void FillTransportDistributionDataGrid()
        {
            TransportDistribution = _mapper.Map<ObservableCollection<TransportDistributionViewModel>>(_transportDistributionService.GetAll());
        }
        private void FillDriversDataGrid()
        {
            Drivers = _mapper.Map<ObservableCollection<DriverViewModel>>(_driverService.GetAll());
        }
        private void FillTransportSpecificationDataGrid()
        {
            TransportSpecifications = _mapper.Map<ObservableCollection<TransportSpecificationViewModel>>(_transportSpecificationService.GetAll());
        }
        private void FillDriversAndCategoriesDataGrid()
        {
            DriversAndCategories = _mapper.Map<ObservableCollection<DriversAndCategoriesViewModel>>(_driversAndCategoriesService.GetAll());
        }
        #endregion

        #region MechanicsCommands
        private void AddMechanics()


        {   
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(new MechanicsViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Mechanic>(viewModel);

            _mechanicService.Add(mechanic);

            FillMechanicsDataGrid();
        }
        private void EditMechanic()
        {
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(SelectedEntity);
            
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Mechanic>(viewModel);

            _mechanicService.Update(mechanic);

            FillMechanicsDataGrid();
        }
        private void DeleteMechanics()
        {
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<Mechanic>(viewModel);

            _mechanicService.Delete(mechanic.Id);

            FillMechanicsDataGrid();
        }
        #endregion

        #region GarageManagersCommands
        private void AddGarageManager()
        {
            var viewModel = _mapper.Map<GarageManagerWindowViewModel>(new GarageManagerViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<GarageManager>(viewModel);

            _garageManagerService.Add(mechanic);

            FillGarageManagersDataGrid();
        }

        private void EditGarageManager()
        {
            var viewModel = _mapper.Map<GarageManagerWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<GarageManager>(viewModel);

            _garageManagerService.Update(mechanic);

            FillGarageManagersDataGrid();
        }
        private void DeleteGarageManager()
        {
            var viewModel = _mapper.Map<GarageManagerWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<GarageManager>(viewModel);

            _mechanicService.Delete(mechanic.Id);

            FillGarageManagersDataGrid();
        }

        #endregion

        #region TransportDistributionCommands
        private void AddTransportDistribution()
        {
            var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(new TransportDistributionViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var transportDistribution = _mapper.Map<TransportDistribution>(viewModel);

            _transportDistributionService.Add(transportDistribution);

            FillTransportDistributionDataGrid();
        }
        private void EditTransportDistribution()
        {
            var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<TransportDistribution>(viewModel);

            _transportDistributionService.Update(mechanic);

            FillTransportDistributionDataGrid();
        }

        private void DeleteTransportDistribution()
        {
            var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<TransportDistribution>(viewModel);

            _mechanicService.Delete(mechanic.Id);

            FillTransportDistributionDataGrid();
        }
        #endregion

        #region TransportSpecificationCommands
        private void AddTransportSpecification()
        {
            var viewModel = _mapper.Map<TransportSpecificationWindowViewModel>(new TransportSpecificationViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<TransportSpecification>(viewModel);

            _transportSpecificationService.Add(mechanic);

            FillTransportSpecificationDataGrid();
        }
        private void EditTransportSpecification()
        {
            var viewModel = _mapper.Map<TransportSpecificationWindowViewModel>(SelectedEntity);
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<TransportSpecification>(viewModel);

            _transportSpecificationService.Update(mechanic);

            FillTransportSpecificationDataGrid();
        }
        private void DeleteTransportSpecification()
        {
            var viewModel = _mapper.Map<TransportSpecificationWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<TransportSpecification>(viewModel);

            _transportSpecificationService.Delete(mechanic.Id);

            FillTransportSpecificationDataGrid();
        }
        #endregion

        #region DirverCommands
        private void AddDriver()
        {   
            var viewModel = _mapper.Map<DriverWindowViewModel>(new DriverViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Driver>(viewModel);

            _driverService.Add(mechanic);

            FillDriversDataGrid();
        }
        private void EditDriver()
        {
            var viewModel = _mapper.Map<DriverWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Driver>(viewModel);

            _driverService.Update(mechanic);

            FillDriversDataGrid();
        }
        private void DeleteDriver()
        {
            var viewModel = _mapper.Map<DriverWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<Driver>(viewModel);

            _driverService.Delete(mechanic.Id);

            FillDriversDataGrid();
        }
        #endregion

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
            var viewModel = _mapper.Map<DriversAndCategoriesWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            ChekOnAlreadyContainsCategories(viewModel);

            FillDriversAndCategoriesDataGrid();
        }
        private void DeleteDriverAndCategory()
        {
            var viewModel = _mapper.Map<DriversAndCategoriesWindowViewModel>(SelectedEntity);


            DeleteAllCategories(viewModel);

            FillDriversAndCategoriesDataGrid();
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
                    
                    if (_driversAndCategoriesService.GetAll().Find(f => f.Categories.Split(new char[] {',',' '}, System.StringSplitOptions.RemoveEmptyEntries).Contains(driversCategoriy.Categs) && f.Name == name && f.Surname == surname && f.Patronymic == patronymic) != null)
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
            foreach(var cat in filledDriversAndCategoriesWindowViewModel.Categories.Split(new char[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries))
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
        //Transport 
        #region TransportCommands
        private void AddTransport()
        {
            var viewModel = _mapper.Map<TransportWindowViewModel>(new TransportViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Transport>(viewModel);

            _transportService.Add(mechanic);

            FillTransportDataGrid();
        }
        private void EditTransport()
        {
            var viewModel = _mapper.Map<TransportWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Transport>(viewModel);

            _transportService.Update(mechanic);

            FillTransportDataGrid();
        }
        private void DeleteTransport()
        {
            var viewModel = _mapper.Map<TransportWindowViewModel>(SelectedEntity);

            var mechanic = _mapper.Map<Transport>(viewModel);

            _transportService.Delete(mechanic.Id);

            FillTransportDataGrid();
        }
        #endregion
        //static public ObservableCollection<object> DataGridEntities { get; set; }





        public event PropertyChangedEventHandler PropertyChanged;

    }

}
