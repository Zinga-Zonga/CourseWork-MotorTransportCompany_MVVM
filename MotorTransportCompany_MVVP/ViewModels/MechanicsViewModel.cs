using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MotorTransportCompany_MVVP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class MechanicsViewModel : INotifyPropertyChanged
    {
        
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Fcs 
        {
            get { return $"{this.Surname} {this.Name} {this.Patronymic}"; }
            set 
            {
                Fcs = value;
            }
        }
        
        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int PassportNumber { get; set; }
        public int LicenseNumber { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
