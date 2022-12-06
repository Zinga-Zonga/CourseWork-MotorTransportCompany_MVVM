namespace MotorTransportCompany_MVVP.Model
{
    interface ITransportSpecification
    {
        string Model { get; set; }
        double FuelConsumption { get; set; }
        double TrunkVolume { get; set; }
        int FuelType_ID { get; set; }
    }
}