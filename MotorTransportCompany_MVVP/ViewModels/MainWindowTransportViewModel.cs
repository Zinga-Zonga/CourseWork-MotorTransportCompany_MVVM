using AutoMapper;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model.Domain;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowTransportViewModel : INotifyPropertyChanged
    {
        public MainWindowTransportViewModel()
        {
            _mapper = InitMappings();
            FillTransportDataGrid();

            AddCommand = new RelayCommand(AddTransport, () => true);
            EditCommand = new RelayCommand(EditTransport, () => true);
            DeleteCommand = new RelayCommand(DeleteTransport, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<TransportViewModel> Transport { get; set; }

        private readonly IMapper _mapper;

        
        private readonly DepartmentService _departmentService = new DepartmentService();
        //private readonly SexService _sexService = new SexService();
        private readonly TechnicalConditionService _technicalConditionService = new TechnicalConditionService();
        private readonly TransportService _transportService = new TransportService();
        private readonly TransportSpecificationService _transportSpecificationService = new TransportSpecificationService();

        private readonly IDialogService _dialogService = new DialogService();


        //private DriverViewModel _selectedEntity;

        public event PropertyChangedEventHandler PropertyChanged;

        public TransportViewModel SelectedEntity { get; set; }
        
        

        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

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

            });



            return config.CreateMapper();

        }
        private void FillTransportDataGrid()
        {
            //Transport = _transportService.GetAll();
            Transport = _mapper.Map<ObservableCollection<TransportViewModel>>(_transportService.GetAll());
        }
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

    }
}
