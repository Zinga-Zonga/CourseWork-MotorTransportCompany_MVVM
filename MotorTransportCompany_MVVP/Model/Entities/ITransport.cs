namespace MotorTransportCompany_MVVP.Model
{
    internal interface ITransport
    {
        int Department_ID { get; set; }
        int TransportSpecification_ID { get; set; }
        int TechnicalCondition_ID { get; set; }
        string TransportNumber { get; set; }

    }
}
