using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Util.Dialogs
{
    internal class DialogService : IDialogService
    {
        public bool? OpenDialog(IDialogViewModel dialogViewModel)
        {
            var window = DialogViewLocator.View(dialogViewModel);
            if (window == null) return false;

            window.DataContext = dialogViewModel;
            window.ShowDialog();

            return dialogViewModel.DialogResult;
        }
    }
}
