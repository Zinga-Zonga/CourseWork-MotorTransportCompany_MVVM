namespace MotorTransportCompany_MVVP.Model
{
    internal class TransportSpecification : ITransportSpecification, IEntity
    {
        public int Id { get; set; }
        public int FuelType_ID { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public double TrunkVolume { get; set; }
        
    }
}
