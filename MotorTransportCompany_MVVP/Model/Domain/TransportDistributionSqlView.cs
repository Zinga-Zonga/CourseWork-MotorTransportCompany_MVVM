using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Domain
{
    internal class TransportDistributionSqlView : IEntity
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
    }
}
