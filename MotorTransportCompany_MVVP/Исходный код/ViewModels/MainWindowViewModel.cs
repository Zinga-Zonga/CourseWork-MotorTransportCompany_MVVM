using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Security;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        // Если обьект к которому мы при привязываемся имеет Такую-то вью модель, то мы даём такой-то функционал
        public RelayCommand MainWindowDriverCommand { get; set; }
        public MainWindowDriverViewModel MainWindowDriverVM { get; set; }
        private void SetDriverVM()
        {
            SelectedViewModel = MainWindowDriverVM;
        }

        public MainWindowMechanicViewModel MainWindowMechanicVM { get; set; }
        public RelayCommand MainWindowMechanicCommand { get; set; }
        private void SetMechanicsVM()
        {
            SelectedViewModel = MainWindowMechanicVM;
        }


        public RelayCommand MainWindowGarageManagerCommand { get; set; }
        public MainWindowGarageManagerViewModel MainWindowGarageManagerVM { get; set; }
        private void SetGarageManagerVM()
        {
            SelectedViewModel = MainWindowGarageManagerVM;
        }

        public MainWindowTransportViewModel MainWindowTransportVM { get; set; }
        public RelayCommand MainWindowTransportCommand { get; set; }
        private void SetTransportVM()
        {
            SelectedViewModel = MainWindowTransportVM;
        }

        public MainWindowTransportSpecificationViewModel MainWindowTransportSpecificationVM { get; set; }
        public RelayCommand MainWindowTransportSpecificationCommand { get; set; }
        private void SetTransportSpecificationVM()
        {
            SelectedViewModel = MainWindowTransportSpecificationVM;
        }

        public MainWindowTransportDistributionViewModel MainWindowTransportDistributionVM { get; set; }
        public RelayCommand MainWindowTransportDistributionCommand { get; set; }
        private void SetTransportDistributionVM()
        {
            SelectedViewModel = MainWindowTransportDistributionVM;
        }

        public MainWindowDriversAndCategoriesViewModel MainWindowDriversAndCategoriesVM { get; set; }
        public RelayCommand MainWindowDriversAndCategoriesCommand { get; set; }
        private void SetDriversAndCategoriesVM()
        {
            SelectedViewModel = MainWindowDriversAndCategoriesVM;
        }












        public MainWindowViewModel()
        {
            
            MainWindowDriverVM = new MainWindowDriverViewModel();
            MainWindowGarageManagerVM = new MainWindowGarageManagerViewModel();
            MainWindowMechanicVM = new MainWindowMechanicViewModel();
            MainWindowTransportVM = new MainWindowTransportViewModel();
            MainWindowTransportSpecificationVM = new MainWindowTransportSpecificationViewModel();
            MainWindowTransportDistributionVM = new MainWindowTransportDistributionViewModel();
            MainWindowDriversAndCategoriesVM = new MainWindowDriversAndCategoriesViewModel();

            SelectedViewModel = MainWindowDriverVM;

            MainWindowDriverCommand = new RelayCommand(SetDriverVM, () => true);
            MainWindowGarageManagerCommand = new RelayCommand(SetGarageManagerVM, () => true);
            MainWindowMechanicCommand = new RelayCommand(SetMechanicsVM, () => true);
            MainWindowTransportCommand = new RelayCommand(SetTransportVM, () => true);
            MainWindowTransportSpecificationCommand = new RelayCommand(SetTransportSpecificationVM, () => true);
            MainWindowTransportDistributionCommand = new RelayCommand(SetTransportDistributionVM, () => true);
            MainWindowDriversAndCategoriesCommand = new RelayCommand(SetDriversAndCategoriesVM, () => true);


    }
        
        public object SelectedViewModel { get; set; }

        


        public event PropertyChangedEventHandler PropertyChanged;

    }

}
