using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _mapper = InitMappings();
            FillTransportDataGrid();
            FillMechanicsDataGrid();
            FillGarageManagersDataGrid();
            FillTransportDistributionDataGrid();
            FillDriversDataGrid();

            AddMechanicCommand = new RelayCommand(AddMechanics, () => true);
        }

        public RelayCommand AddMechanicCommand { get; }

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

        private readonly IDialogService _dialogService = new DialogService();



        private readonly IMapper _mapper;

        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                // Механик из SQL в Табличного механика
                cfg.CreateMap<MechanicSqlView, MechanicsViewModel>()
                    .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                    .ReverseMap();
                // Завгар из SQL в Табличного завгара
                cfg.CreateMap<GarageManagerSqlView, GarageManagerViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();
                // Транспорт из SQL в Табличный транспорт
                cfg.CreateMap<TransportSqlView, TransportViewModel>().ReverseMap();
                // Таблица из SQL водителей и автом в таблицу водителей и авто
                cfg.CreateMap<TransportDistributionSqlView, TransportDistributionViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();
                //Водители и категории
                cfg.CreateMap<DriverSqlView, DriverViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)))
                   .ReverseMap();
                // Из таблицы  в окошко редактирования/добавления
                cfg.CreateMap<MechanicsViewModel, MechanicsWindowViewModel>().ReverseMap();
                // Из ВьюМодели в базовую таблицу механиков
                cfg.CreateMap<MechanicsWindowViewModel, Mechanic>()
                .ForMember(m => m.Department_id, opt => opt.MapFrom
                    (f => _departmentService.GetAll().Find(c => c.Name == f.DepartmentName).Id))
                .ForMember(m => m.IdSex, opt => opt.MapFrom
                    (f => _sexService.GetAll().Find(c => c.Name == f.Sex).Id
                    ))
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
        private void AddMechanics()
        {
            //MechanicsWindowViewModel mechanic1 = new MechanicsWindowViewModel()
            //{
            //    Id = 0,
            //    DepartmentName = "Межгородний",
            //    Sex = "Мужчина",
            //    Age = 20,
            //    BirthdayDate = "22-12-2013",
            //    Name = "Maks",
            //    Surname = "Bobik",
            //    Patronymic = "Dayb",
            //    PassportNumber = 22222

            //};
            //var viewModel = _mapper.Map<MechanicsWindowViewModel>(new MechanicsViewModel());
            //var res = _dialogService.OpenDialog(mechanic1);

            //if (res != true) return;

            //var mechanic = _mapper.Map<Mechanic>(mechanic1);

            //_mechanicService.Add(mechanic);

            //FillMechanicsDataGrid();
            
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(new MechanicsViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            var mechanic = _mapper.Map<Mechanic>(viewModel);

            _mechanicService.Add(mechanic);

            FillMechanicsDataGrid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

}
