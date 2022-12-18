namespace MotorTransportCompany_MVVP.Model.Domain
{
    internal class DriversAndCategoriesSqlView : IEntity
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int LicenseNumber { get; set; }
        public string Categories { get; set; }
    }
}
