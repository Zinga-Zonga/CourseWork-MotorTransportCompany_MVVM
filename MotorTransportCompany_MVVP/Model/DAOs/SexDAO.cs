using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotorTransportCompany_MVVP.Model
{
    internal class SexDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";
        

        public List<SexType> getAll()
        {
            List<SexType> sexTypes = new List<SexType>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `sex_types`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    SexType sexType = new SexType
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    };
                    sexTypes.Add(sexType);
                }
            }
            connaction.Close();
            return sexTypes;
        }

    }
}
