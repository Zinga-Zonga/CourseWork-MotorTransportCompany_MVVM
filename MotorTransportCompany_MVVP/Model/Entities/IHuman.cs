using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal interface IHuman
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Patronymic { get; set; }
        DateTime BirthdayDate { get; set; }
        int Age { get; set; }
        Sex Sex { get; set; }
        int PassportNumber { get; set; }
    }
}
