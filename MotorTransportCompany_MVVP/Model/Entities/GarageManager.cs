using MotorTransportCompany_MVVP.Model.Entities;
using System;

namespace MotorTransportCompany_MVVP.Model
{
    internal class GarageManager : IEntity, IDepartmensWorker, IHuman
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Age { get; set; }
        public SexType Sex { get; set; }
        public int PassportNumber { get; set; }
    }
}
