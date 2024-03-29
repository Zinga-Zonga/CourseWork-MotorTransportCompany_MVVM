﻿using AutoMapper;
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
        public string DepartmentName { get; set; }
        public string FCS { get; set; }
        
        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int PassportNumber { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
