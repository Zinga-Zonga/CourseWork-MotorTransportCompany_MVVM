namespace MotorTransportCompany_MVVP.Model
{
    internal interface ITransport
    {
        TransportSpecifications Model { get; set; }
        string Number { get; set; }
        string Department { get; set; }
        TechnicalCondition TechnicalCondition { get; set; }

    }
}
