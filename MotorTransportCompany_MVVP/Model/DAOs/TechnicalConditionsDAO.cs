using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TechnicalConditionsDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<TechnicalCondition> GetAll()
        {
            List<TechnicalCondition> technicalConditions = new List<TechnicalCondition>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_transport_specifications, id_fuel, model, trunk_volume, fuel_consumption FROM `transport_specifications`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TechnicalCondition tc = new TechnicalCondition
                    {
                        Id = reader.GetInt32(0),
                        Condition = reader.GetString(1)
                    };
                    technicalConditions.Add(tc);
                }
            }
            connaction.Close();
            return technicalConditions;
        }
    }
}
