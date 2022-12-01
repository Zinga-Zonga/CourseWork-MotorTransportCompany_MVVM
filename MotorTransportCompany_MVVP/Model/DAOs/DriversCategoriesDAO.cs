using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DriversCategoriesDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<DriversAndCategories> GetAll()
        {
            List<DriversAndCategories> driversAndCategories = new List<DriversAndCategories>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id, id_driver, id_category FROM `drivers_categories`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DriversAndCategories dac = new DriversAndCategories
                    {
                        Id = reader.GetInt32(0),
                        DriverID = reader.GetInt32(1),
                        CategoryID = reader.GetInt32(2)
                    };
                    driversAndCategories.Add(dac);
                }
            }
            connaction.Close();
            return driversAndCategories;
        }
    }
}
