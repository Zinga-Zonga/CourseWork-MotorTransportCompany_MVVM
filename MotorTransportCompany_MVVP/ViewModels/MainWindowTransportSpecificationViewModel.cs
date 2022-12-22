using AutoMapper;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System.Collections.ObjectModel;
using MotorTransportCompany_MVVP.Model.Domain;
using AutoMapper.EquivalencyExpression;
using System.ComponentModel;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowTransportSpecificationViewModel : INotifyPropertyChanged
    {
        public MainWindowTransportSpecificationViewModel()
        {
            _mapper = InitMappings();
            FillTransportSpecificationDataGrid();

            AddCommand = new RelayCommand(AddTransportSpecification, () => true);
            EditCommand = new RelayCommand(EditTransportSpecification, () => true);
            DeleteCommand = new RelayCommand(DeleteTransportSpecification, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<TransportSpecificationViewModel> TransportSpecifications { get; set; }

        private readonly IMapper _mapper;
        
        private readonly TransportSpecificationService _transportSpecificationService = new TransportSpecificationService();
        private readonly FuelTypeService _fuelTypeService = new FuelTypeService();

        private readonly IDialogService _dialogService = new DialogService();


        

        public event PropertyChangedEventHandler PropertyChanged;

        public TransportSpecificationViewModel SelectedEntity { get; set; }



        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

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
        private void FillTransportSpecificationDataGrid()
        {
            TransportSpecifications = _mapper.Map<ObservableCollection<TransportSpecificationViewModel>>(_transportSpecificationService.GetAll());
        }
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
    }
}
