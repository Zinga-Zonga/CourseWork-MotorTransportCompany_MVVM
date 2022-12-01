using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System.Collections.Generic;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportDistributionDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<TransportDistribution> GetAll()
        {
            List<TransportDistribution> transportDistribution = new List<TransportDistribution>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id, id_transport, id_driver FROM `transport_distribution`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TransportDistribution td = new TransportDistribution
                    {
                        Id = reader.GetInt32(0),
                        Transport_ID = reader.GetInt32(1),
                        Driver_ID = reader.GetInt32(2),
                    };
                    transportDistribution.Add(td);
                }
            }
            connaction.Close();
            return transportDistribution;
        }
    }
}
