using MotorTransportCompany_MVVP.Model.Entities;
using System;

namespace MotorTransportCompany_MVVP.Model
{
    internal interface IHuman
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Patronymic { get; set; }
        DateTime BirthdayDate { get; set; }
        int Age { get; set; }
        int IdSex { get; set; }
        int PassportNumber { get; set; }
    }
}
