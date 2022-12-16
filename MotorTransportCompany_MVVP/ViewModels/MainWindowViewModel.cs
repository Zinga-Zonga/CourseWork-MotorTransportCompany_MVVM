using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        // Если обьект к которому мы при привязываемся имеет Такую-то вью модель, то мы даём такой-то функционал
        public MainWindowViewModel()
        {
            _mapper = InitMappings();
            FillTransportDataGrid();
            FillMechanicsDataGrid();
            FillGarageManagersDataGrid();
            FillTransportDistributionDataGrid();
            FillDriversDataGrid();
            FillTransportSpecificationDataGrid();

            AddMechanicCommand = new RelayCommand(AddTransportSpecification, () => true);
            EditMechanicCommand = new RelayCommand(EditTransportSpecification, () => true);
            DeleteMechanicCommand = new RelayCommand(DeleteTransportSpecification, () => true);
        }

        public RelayCommand AddMechanicCommand { get; }
        public RelayCommand EditMechanicCommand { get; }
        public RelayCommand DeleteMechanicCommand { get; }
        #region initiationOfServices and entities
        private readonly MechanicService _mechanicService = new MechanicService();
        private readonly GarageManagerService _garageManagerService = new GarageManagerService();
        private readonly TransportService _transportService = new TransportService();
        private readonly TransportDistributionService _transportDistributionService = new TransportDistributionService();
        private readonly DriverService _driverService = new DriverService();
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();
        private readonly TransportSpecificationService _transportSpecificationService = new TransportSpecificationService();
        private readonly TechnicalConditionService _technicalConditionService = new TechnicalConditionService();
        private readonly FuelTypeService _fuelTypeService = new FuelTypeService();
        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }
        public ObservableCollection<GarageManagerViewModel> GarageManagers { get; set; }
        public ObservableCollection<TransportViewModel> Transport { get; set; }
        public ObservableCollection<TransportDistributionViewModel> TransportDistribution { get; set; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }
        public ObservableCollection<TransportSpecificationViewModel> TransportSpecifications { get; set; }
        #endregion
        private readonly IDialogService _dialogService = new DialogService();

        private TransportSpecificationViewModel _selectedEntity;
        public TransportSpecificationViewModel SelectedEntity
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
            // Таблица из SQL водителей и автом в таблицу водителей и авто
            cfg.CreateMap<TransportSqlView, TransportViewModel>()
               .ReverseMap();

            cfg.CreateMap<TransportViewModel, TransportWindowViewModel>()
                .ReverseMap();

            cfg.CreateMap<TransportWindowViewModel, Transport>()
                .ForMember(m => m.Department_ID, opt => opt.MapFrom
                    (f => _departmentService.GetAll().Find(c => c.Name == f.Department).Id))
                .ForMember(m => m.TransportSpecification_ID, opt => opt.MapFrom
                    (f => _transportSpecificationService.GetAll()
                        .Find(c => c.TrunkVolume == f.TrunkVolume && c.FuelConsumption == f.FuelConsumption && c.Model == f.Model)))
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
        //Drivers

        //Transport

        //static public ObservableCollection<object> DataGridEntities { get; set; }





        public event PropertyChangedEventHandler PropertyChanged;

    }

}
