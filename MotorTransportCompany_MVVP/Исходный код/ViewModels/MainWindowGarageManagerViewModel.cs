using AutoMapper;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Util.Dialogs;
using MotorTransportCompany_MVVP.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorTransportCompany_MVVP.Model.Domain;
using AutoMapper.EquivalencyExpression;
using System.ComponentModel;
using System.Reflection;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowGarageManagerViewModel : INotifyPropertyChanged
    {
        public MainWindowGarageManagerViewModel()
        {
            _mapper = InitMappings();
            FillGarageManagersDataGrid();

            AddCommand = new RelayCommand(AddGarageManager, () => true);
            EditCommand = new RelayCommand(EditGarageManager, () => true);
            DeleteCommand = new RelayCommand(DeleteGarageManager, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }

        private readonly IMapper _mapper;

        private readonly GarageManagerService _garageManagerService = new GarageManagerService();
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();
        private readonly MechanicService _mechanicService = new MechanicService();

        public ObservableCollection<GarageManagerViewModel> GarageManagers { get; set; }

        private readonly IDialogService _dialogService = new DialogService();


        private GarageManagerViewModel _selectedEntity;

        public GarageManagerViewModel SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        

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

            });



            return config.CreateMapper();

        }
        private void FillGarageManagersDataGrid()
        {
            GarageManagers = _mapper.Map<ObservableCollection<GarageManagerViewModel>>(_garageManagerService.GetAll());
        }
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
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<GarageManagerWindowViewModel>(SelectedEntity);

                var mechanic = _mapper.Map<GarageManager>(viewModel);

                _garageManagerService.Delete(mechanic.Id);

                FillGarageManagersDataGrid();
            }
            
        }

        #endregion
        
    }
}
