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

            return null;
        }
    }
}
