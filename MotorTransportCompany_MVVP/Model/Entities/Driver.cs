using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal class Driver : IHuman, IEntity, IDepartmensWorker
    {

        public int Id { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int PassportNumber { get; set; }

        public SpecializationsTypes specialization = SpecializationsTypes.Водитель;

        Transport Transport { get; set; }
    }
}
