using System.ComponentModel;

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
