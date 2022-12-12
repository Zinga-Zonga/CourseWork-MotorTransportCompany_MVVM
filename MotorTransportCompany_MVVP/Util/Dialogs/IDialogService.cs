using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Util.Dialogs
{
    internal interface IDialogService
    {
        bool? OpenDialog(IDialogViewModel dialogViewModel);
    }
}
