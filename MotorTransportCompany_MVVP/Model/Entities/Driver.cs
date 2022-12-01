using MotorTransportCompany_MVVP.Model.Entities;
using System;

namespace MotorTransportCompany_MVVP.Model
{
    internal class Driver : IHuman, IEntity, IDepartmensWorker
    {

        public int Id { get; set; }
        public int Department_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Age { get; set; }
        public int IdSex { get; set; }
        public int PassportNumber { get; set; }
        public int LicenseNumber { get; set; }

        public SpecializationsTypes specialization = SpecializationsTypes.Водитель;

        Transport Transport { get; set; }
    }
}
