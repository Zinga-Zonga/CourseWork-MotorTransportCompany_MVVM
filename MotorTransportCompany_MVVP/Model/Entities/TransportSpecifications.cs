using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal class TransportSpecifications : ITransportSpecification, IEntity
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }
        public FuelType FuelType { get; set; }
    }
}
