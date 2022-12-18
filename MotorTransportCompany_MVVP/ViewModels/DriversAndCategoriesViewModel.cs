using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriversAndCategoriesViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string FCS { get; set; }
        public int LicenseNumber { get; set; }
        public string Categories { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
