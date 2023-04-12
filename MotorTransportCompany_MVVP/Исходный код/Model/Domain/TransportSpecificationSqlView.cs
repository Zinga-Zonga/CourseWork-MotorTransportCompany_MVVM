using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Domain
{
    internal class TransportSpecificationSqlView : IEntity
    {
        public int Id { get; set; }
        public string FuelType { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }
    }
}
