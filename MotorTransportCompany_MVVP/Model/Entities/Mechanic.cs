using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal class Mechanic : IEntity, IDepartmensWorker
    {
        public int Id { get; set; }
        public int Department_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public int IdSex { get; set; }
        public int PassportNumber { get; set; }
    }
}
