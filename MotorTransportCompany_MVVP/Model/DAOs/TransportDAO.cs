using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<Transport> GetAll()
        {
            List<Transport> transportList = new List<Transport>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_transport, id_department, id_transport_specifications, id_condition, number FROM `transport`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Transport transport = new Transport
                    {
                        Id = reader.GetInt32(0),
                        Department_ID = reader.GetInt32(1),
                        TransportSpecification_ID = reader.GetInt32(2),
                        TechnicalCondition_ID = reader.GetInt32(3),
                        TransportNumber = reader.GetString(4)
                    };
                    transportList.Add(transport);
                }
            }
            connaction.Close();
            return transportList;
        }
    }
}
