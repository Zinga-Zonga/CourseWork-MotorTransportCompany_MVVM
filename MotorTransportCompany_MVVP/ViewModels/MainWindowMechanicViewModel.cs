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
using System.ComponentModel;
using AutoMapper.EquivalencyExpression;
using System.Reflection;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowMechanicViewModel : INotifyPropertyChanged
    {
        public MainWindowMechanicViewModel()
        {
            _mapper = InitMappings();
            FillMechanicsDataGrid();

            AddCommand = new RelayCommand(AddMechanic, () => true);
            EditCommand = new RelayCommand(EditMechanic, () => true);
            DeleteCommand = new RelayCommand(DeleteMechanic, () => true);
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public ObservableCollection<DriverViewModel> Drivers { get; set; }

        private readonly IMapper _mapper;

        
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly SexService _sexService = new SexService();
        private readonly MechanicService _mechanicService = new MechanicService();

        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }

        private readonly IDialogService _dialogService = new DialogService();


        private MechanicsViewModel _selectedEntity;

        public event PropertyChangedEventHandler PropertyChanged;

        public MechanicsViewModel SelectedEntity
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

            });



            return config.CreateMapper();

        }
        private void FillMechanicsDataGrid()
        {
            Mechanics = _mapper.Map<ObservableCollection<MechanicsViewModel>>(_mechanicService.GetAll());
        }
        #region MechanicsCommands
        private void AddMechanic()


        {
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(new MechanicsViewModel());
            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            if (IsAnyNullOrEmpty(viewModel))
            {
                var mechanic = _mapper.Map<Mechanic>(viewModel);

                _mechanicService.Add(mechanic);

                FillMechanicsDataGrid();
            }
            
        }
        private void EditMechanic()
        {
            var viewModel = _mapper.Map<MechanicsWindowViewModel>(SelectedEntity);

            var res = _dialogService.OpenDialog(viewModel);

            if (res != true) return;

            if (IsAnyNullOrEmpty(viewModel))
            {
                var mechanic = _mapper.Map<Mechanic>(viewModel);

                _mechanicService.Update(mechanic);

                FillMechanicsDataGrid();
            }
            
        }
        private void DeleteMechanic()
        {
            if(SelectedEntity != null)
            {
                var viewModel = _mapper.Map<MechanicsWindowViewModel>(SelectedEntity);

                var mechanic = _mapper.Map<Mechanic>(viewModel);

                _mechanicService.Delete(mechanic.Id);

                FillMechanicsDataGrid();
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
