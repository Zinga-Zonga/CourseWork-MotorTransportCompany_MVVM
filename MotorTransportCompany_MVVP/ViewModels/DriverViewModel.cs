﻿using System.ComponentModel;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriverViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string FCS { get; set; }

        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int PassportNumber { get; set; }
        public int LicenseNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
