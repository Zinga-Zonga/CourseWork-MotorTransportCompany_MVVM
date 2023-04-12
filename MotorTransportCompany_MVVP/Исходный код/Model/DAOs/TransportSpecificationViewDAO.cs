using MotorTransportCompany_MVVP.Model.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportSpecificationViewDAO : IViewDAO<TransportSpecificationSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<TransportSpecificationSqlView> GetAll()
        {

            try
            {

                List<TransportSpecificationSqlView> entities = new List<TransportSpecificationSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport_specifications.id_transport_specifications, fuel_types.fuel, transport_specifications.model, transport_specifications.trunk_volume, transport_specifications.fuel_consumption FROM transport_specifications INNER JOIN fuel_types ON transport_specifications.id_fuel = fuel_types.id_fuel", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportSpecificationSqlView entity = new TransportSpecificationSqlView
                        {
                            Id = reader.GetInt32(0),
                            FuelType = reader.GetString(1),
                            Model = reader.GetString(2),
                            TrunkVolume = reader.GetDouble(3),
                            FuelConsumption = reader.GetDouble(4)

                        };
                        entities.Add(entity);
                    }
                }
                connaction.Close();
                return entities;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public List<TransportSpecificationSqlView> GetEntityById(int id)
        {
            try
            {

                List<TransportSpecificationSqlView> entities = new List<TransportSpecificationSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport_specifications.id_transport_specifications, fuel_types.fuel, transport_specifications.model, transport_specifications.trunk_volume, transport_specifications.fuel_consumption FROM transport_specifications INNER JOIN fuel_types ON transport_specifications.id_fuel = fuel_types.id_fuel WHERE transport_specifications.id_transport_specifications = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportSpecificationSqlView entity = new TransportSpecificationSqlView
                        {
                            Id = reader.GetInt32(0),
                            FuelType = reader.GetString(1),
                            Model = reader.GetString(2),
                            TrunkVolume = reader.GetDouble(3),
                            FuelConsumption = reader.GetDouble(4)
                        };
                        entities.Add(entity);
                    }
                }
                connaction.Close();
                return entities;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
    }
}
