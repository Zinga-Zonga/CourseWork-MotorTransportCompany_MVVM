using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class FuelTypeDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<FuelType> GetAll()
        {
            List<FuelType> fuelTypes = new List<FuelType>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_fuel, fuel FROM `fuel_types`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    FuelType fuelType = new FuelType
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    };
                    fuelTypes.Add(fuelType);
                }
            }
            connaction.Close();
            return fuelTypes;
        }
    }
}
