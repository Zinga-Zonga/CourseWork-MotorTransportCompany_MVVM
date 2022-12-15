using MotorTransportCompany_MVVP.View;
using MotorTransportCompany_MVVP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotorTransportCompany_MVVP.Util.Dialogs
{
    static class DialogViewLocator
    {
        public static Window View(IDialogViewModel viewModel) 
        {
            if (viewModel is MechanicsWindowViewModel)
                return new MechanicsWindow();

            if (viewModel is GarageManagerWindowViewModel)
                return new GarageManagerWindow();

            if (viewModel is TransportDistributionWindowViewModel)
                return new TransportDistributionWindow();

            if (viewModel is DriverWindowViewModel)
                return new DriversWindow();

            if (viewModel is TransportWindowViewModel)
                return new TransportWindow();

            if (viewModel is TransportSpecificationWindowViewModel)
                return new TransportSpecificationWindow();


            return null;
        }
    }
}
