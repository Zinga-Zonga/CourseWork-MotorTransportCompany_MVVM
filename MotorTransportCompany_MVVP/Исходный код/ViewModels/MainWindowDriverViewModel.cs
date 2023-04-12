using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System.Reflection;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowDriverViewModel : INotifyPropertyChanged
    {
        public MainWindowDriverViewModel()
        {
            _mapper = InitMappings();
            FillDriversDataGrid();

            AddCommand = new RelayCommand(AddDriver, () => true);
            EditCommand = new RelayCommand(EditDriver, () => true);
            DeleteCommand = new RelayCommand(DeleteDriver, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }

        private readonly IMapper _mapper;

        private readonly DriverService _driverService = new DriverService();
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();

        private readonly IDialogService _dialogService = new DialogService();


        private DriverViewModel _selectedEntity;

        public event PropertyChangedEventHandler PropertyChanged;

        public DriverViewModel SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
            }
        }

        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

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

            });



            return config.CreateMapper();

        }
        private void FillDriversDataGrid()
        {
            Drivers = _mapper.Map<ObservableCollection<DriverViewModel>>(_driverService.GetAll());
        }
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
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<DriverWindowViewModel>(SelectedEntity);

                var mechanic = _mapper.Map<Driver>(viewModel);

                _driverService.Delete(mechanic.Id);

                FillDriversDataGrid();
            }
            
        }
        #endregion
        

    }
}
