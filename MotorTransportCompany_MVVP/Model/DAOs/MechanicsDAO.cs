using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class MechanicsDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<Mechanic> getAll()
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `mechanics`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Mechanic mech = new Mechanic
                    {
                        Id = reader.GetInt32(0),
                        DepartmentName = reader.GetString(1);
                        Name = reader.GetString(1),
                    };
                    mechanics.Add(mech);
                }
            }
            connaction.Close();
            return  mechanics;
        }
    }
}
