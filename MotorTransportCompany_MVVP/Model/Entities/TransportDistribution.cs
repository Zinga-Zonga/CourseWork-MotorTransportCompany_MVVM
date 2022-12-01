using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Entities
{
    internal class TransportDistribution : IEntity
    {
        public int Id { get; set; }
        public int Transport_ID { get; set; }
        public int Driver_ID { get; set; }
    }
}
