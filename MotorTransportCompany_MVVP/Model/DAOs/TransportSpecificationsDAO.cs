using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportSpecificationsDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<TransportSpecification> GetAll()
        {
            List<TransportSpecification> transportSpecifications = new List<TransportSpecification>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_transport_specifications, id_fuel, model, trunk_volume, fuel_consumption FROM `transport_specifications`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TransportSpecification ts = new TransportSpecification
                    {
                        Id = reader.GetInt32(0),
                        FuelType_ID = reader.GetInt32(1),
                        Model = reader.GetString(2),
                        TrunkVolume = reader.GetDouble(3),
                        FuelConsumption = reader.GetDouble(4)
                    };
                    transportSpecifications.Add(ts);
                }
            }
            connaction.Close();
            return transportSpecifications;
        }
    }
}
