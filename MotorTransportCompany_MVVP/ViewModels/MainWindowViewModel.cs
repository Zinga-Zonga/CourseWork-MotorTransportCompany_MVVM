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
        private ObservableCollection<MechanicsViewModel> _mechanics;
        public ObservableCollection<MechanicsViewModel> Mechanics { get; set; }
        public ObservableCollection<MechanicsViewModel> GarageManagers { get; set; }


        private readonly IMapper _mapper;
        
        public MainWindowViewModel()
        {
            _mapper = InitMappings();
            Mechanics = _mapper.Map<ObservableCollection<MechanicsViewModel>>(_mechanicService.GetAll());
            FillMechanicsDataGrid();
        }


        private IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                cfg.CreateMap<MechanicSqlView, MechanicsViewModel>()
                    .ForMember(m => m.FCS, opt => opt.MapFrom(f => string.Format("{0} {1} {2}", f.Surname, f.Name, f.Patronymic)));
            });

            return config.CreateMapper();

        }

        private void FillMechanicsDataGrid()
        {
            Mechanics = _mapper.Map<ObservableCollection<MechanicsViewModel>>(_mechanicService.GetAll());
            //Mechanics = new ObservableCollection<MechanicSqlView>(_mechanicService.GetAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
