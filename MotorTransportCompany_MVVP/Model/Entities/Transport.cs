namespace MotorTransportCompany_MVVP.Model
{
    internal class Transport : ITransport
    {
        public TransportSpecifications Model { get; set; }
        public string Number { get; set; }
        public string Department { get; set; }
        public TechnicalCondition TechnicalCondition { get; set; }
    }
}
