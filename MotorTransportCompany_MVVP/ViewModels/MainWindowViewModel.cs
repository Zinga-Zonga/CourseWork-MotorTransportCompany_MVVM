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
        private ObservableCollection<MechanicsViewModel> Mechanics;

        private readonly IMapper _mapper;
        private IMapper InitMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();

                cfg.CreateMap<MechanicSqlView, MechanicsViewModel>()
                   .ForMember(m => m.FCS, opt => opt.MapFrom(f => $"{f.Surname} {f.Name} {f.Patronymic}"));
                   
                   

                //cfg.CreateMap<Driver, DriverWindowViewModel>()
                //   .ForMember(m => m.CarNo, opt => opt.MapFrom(f => f.Car.No))
                //   .ForMember(m => m.CarModel, opt => opt.MapFrom(f => f.Car.Model))
                //   .ForMember(m => m.CarMake, opt => opt.MapFrom(f => f.Car.Make))
                //   .ReverseMap();
            });

            return config.CreateMapper();

        }
        public MainWindowViewModel()
        {
            _mapper = InitMapping();
            FillMechanicsDataGrid();
        }

        

        private void FillMechanicsDataGrid()
        {
            Mechanics = _mapper.Map<ObservableCollection<MechanicsViewModel>>(_mechanicService.GetAll());
            //Mechanics = new ObservableCollection<MechanicSqlView>(_mechanicService.GetAll());
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
