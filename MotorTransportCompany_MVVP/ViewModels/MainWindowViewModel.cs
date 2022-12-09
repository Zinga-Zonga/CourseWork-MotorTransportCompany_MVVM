using AutoMapper;
using AutoMapper.Collection;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Services;
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
        private readonly MechanicService _mechanicService = new MechanicService();
        private readonly GarageManagerService _gmService = new GarageManagerService();
        private readonly TransportService _transportService = new TransportService();
        private readonly TransportDistributionService _transportDistributionService = new TransportDistributionService();
        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }
        public ObservableCollection<GarageManagerViewModel> GarageManagers { get; set; }
        public ObservableCollection<TransportViewModel> Transport { get; set; }
        public ObservableCollection<TransportDistributionViewModel> TransportDistribution { get; set; }



        private readonly IMapper _mapper;
        
        public MainWindowViewModel()
        {
            _mapper = InitMappings();
            FillTransportDataGrid();
            FillMechanicsDataGrid();
            FillGarageManagersDataGrid();
            FillTransportDistributionDataGrid();
        }


        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                cfg.CreateMap<MechanicSqlView, MechanicsViewModel>()
                    .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)));
                cfg.CreateMap<GarageManagerSqlView, GarageManagerViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)));
                cfg.CreateMap<TransportSqlView, TransportViewModel>();
                cfg.CreateMap<TransportDistributionSqlView, TransportDistributionViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)));
            });

            return config.CreateMapper();

        }

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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
