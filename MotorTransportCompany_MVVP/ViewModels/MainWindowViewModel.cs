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

            AddMechanicCommand = new RelayCommand(AddTransportDistribution, () => true);
            EditMechanicCommand = new RelayCommand(EditMechanic, () => true);
            DeleteMechanicCommand = new RelayCommand(DeleteMechanics, () => true);
        }

        public RelayCommand AddMechanicCommand { get; }
        public RelayCommand EditMechanicCommand { get; }
        public RelayCommand DeleteMechanicCommand { get; }
        #region initiationOfServices and entities
        private readonly MechanicService _mechanicService = new MechanicService();
        private readonly GarageManagerService _gmService = new GarageManagerService();
        private readonly TransportService _transportService = new TransportService();
        private readonly TransportDistributionService _transportDistributionService = new TransportDistributionService();
        private readonly DriverService _driverService = new DriverService();
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();
        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }
        public ObservableCollection<GarageManagerViewModel> GarageManagers { get; set; }
        public ObservableCollection<TransportViewModel> Transport { get; set; }
        public ObservableCollection<TransportDistributionViewModel> TransportDistribution { get; set; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }
        #endregion
        private readonly IDialogService _dialogService = new DialogService();

        private MechanicsViewModel _selectedEntity;
        public MechanicsViewModel SelectedEntity
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


                // Транспорт из SQL в Табличный транспорт
                cfg.CreateMap<TransportSqlView, TransportViewModel>().ReverseMap();

                //Водители и категории SQL
                cfg.CreateMap<DriverSqlView, DriverViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();

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
            GarageManagers = _mapper.Map<ObservableCollection<GarageManagerViewModel>>(_gmService.GetAll());
        }
        private void FillTransportDataGrid()
        {
            Transport = _mapper.Map<ObservableCollection<TransportViewModel>>(_transportService.GetEntityByID(2));
        }
        private void FillTransportDistributionDataGrid()
        {
            TransportDistribution = _mapper.Map<ObservableCollection<TransportDistributionViewModel>>(_transportDistributionService.GetAll());
        }
        private void FillDriversDataGrid()
        {
            Drivers = _mapper.Map<ObservableCollection<DriverViewModel>>(_driverService.GetAll());
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

            var mechanic = _mapper.Map<Mechanic>(viewModel);

            _mechanicService.Add(mechanic);

            FillMechanicsDataGrid();
        }



        #endregion
        private void AddTransportDistribution()
        {
            var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(new TransportDistributionViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var transportDistribution = _mapper.Map<TransportDistribution>(viewModel);

            _transportDistributionService.Add(transportDistribution);

            FillTransportDistributionDataGrid();
        }


        //static public ObservableCollection<object> DataGridEntities { get; set; }





        public event PropertyChangedEventHandler PropertyChanged;

    }

}
