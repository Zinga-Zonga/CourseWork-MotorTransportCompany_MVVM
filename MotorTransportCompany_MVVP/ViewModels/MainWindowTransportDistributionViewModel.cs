using AutoMapper;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using System.Reflection;
using System;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowTransportDistributionViewModel : INotifyPropertyChanged
    {
        public MainWindowTransportDistributionViewModel()
        {
            _mapper = InitMappings();
            FillTransportDistributionDataGrid();

            AddCommand = new RelayCommand(AddTransportDistribution, () => true);
            EditCommand = new RelayCommand(EditTransportDistribution, () => true);
            DeleteCommand = new RelayCommand(DeleteTransportDistribution, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<TransportDistributionViewModel> TransportDistribution { get; set; }

        private readonly IMapper _mapper;
        
        private readonly TransportDistributionService _transportDistributionService = new TransportDistributionService();
        private readonly TransportService _transportService = new TransportService();
        private readonly DriverService _driverService = new DriverService();

        private readonly IDialogService _dialogService = new DialogService();

        public event PropertyChangedEventHandler PropertyChanged;

        public TransportDistributionViewModel SelectedEntity { get; set; }



        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

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

            });



            return config.CreateMapper();

        }
        private void FillTransportDistributionDataGrid()
        {
            TransportDistribution = _mapper.Map<ObservableCollection<TransportDistributionViewModel>>(_transportDistributionService.GetAll());
        }
        #region TransportDistributionCommands
        private void AddTransportDistribution()
        {
            var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(new TransportDistributionViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            if(viewModel.FCS != null && viewModel.NumberAndModel != null)
            {
                var transportDistribution = _mapper.Map<TransportDistribution>(viewModel);
                _transportDistributionService.Add(transportDistribution);
            }
           
                

            FillTransportDistributionDataGrid();
        }
        private void EditTransportDistribution()
        {
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(SelectedEntity);

                var res = _dialogService.OpenDialog(viewModel);

                if (res != true) return;

                if (IsAnyNullOrEmpty(viewModel))
                {
                    var mechanic = _mapper.Map<TransportDistribution>(viewModel);

                    _transportDistributionService.Update(mechanic);

                    FillTransportDistributionDataGrid();
                }
                
            }
            
        }

        private void DeleteTransportDistribution()
        {
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<TransportDistributionWindowViewModel>(SelectedEntity);

                var mechanic = _mapper.Map<TransportDistribution>(viewModel);

                _transportDistributionService.Delete(mechanic.Id);

                FillTransportDistributionDataGrid();
            }
            
        }
        #endregion
        bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                string value = (string)pi.GetValue(myObject);
                if (String.IsNullOrEmpty(value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
