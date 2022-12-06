namespace MotorTransportCompany_MVVP.Model
{
    internal class Transport : ITransport, IEntity
    {
        public int Id { get; set; }
        public int TransportSpecification_ID { get; set; }
        public int Department_ID { get; set; }
        public int TechnicalCondition_ID { get; set; }
        public string TransportNumber { get; set; }
    }
}
