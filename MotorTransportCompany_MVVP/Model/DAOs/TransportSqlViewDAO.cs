using MotorTransportCompany_MVVP.Model.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportSqlViewDAO : IViewDAO<TransportSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<TransportSqlView> GetAll()
        {
            try
            {

                List<TransportSqlView> entities = new List<TransportSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport.id_transport, departments.department, transport.number, transport_specifications.model, fuel_types.fuel, transport_specifications.trunk_volume, transport_specifications.fuel_consumption, technical_conditions.technical_condition " +
                    $"FROM fuel_types " +
                    $"INNER JOIN transport_specifications ON fuel_types.id_fuel = transport_specifications.id_fuel " +
                    $"INNER JOIN transport ON transport.id_transport_specification = transport_specifications.id_transport_specifications " +
                    $"INNER JOIN technical_conditions ON transport.id_condition = technical_conditions.id_condition " +
                    $"INNER JOIN departments ON transport.id_department = departments.id_department;", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportSqlView entity = new TransportSqlView
                        {
                            Id = reader.GetInt32(0),
                            Department = reader.GetString(1),
                            Number = reader.GetString(2),
                            Model = reader.GetString(3),
                            Fuel = reader.GetString(4),
                            TrunkVolume = reader.GetDouble(5),
                            FuelConsumption = reader.GetDouble(6),
                            TechnicalCondition = reader.GetString(7)
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

        public TransportSqlView GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport.id_transport, departments.department, transport.number, transport_specifications.model, fuel_types.fuel, transport_specifications.trunk_volume, transport_specifications.fuel_consumption, technical_conditions.technical_condition FROM fuel_types INNER JOIN transport_specifications ON fuel_types.id_fuel = transport_specifications.id_fuel INNER JOIN transport ON transport.id_transport_specification = transport_specifications.id_transport_specifications INNER JOIN technical_conditions ON transport.id_condition = technical_conditions.id_condition INNER JOIN departments ON transport.id_department = departments.id_department WHERE transport.id_transport = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    TransportSqlView entity = new TransportSqlView
                    {
                        Id = reader.GetInt32(0),
                        Department = reader.GetString(1),
                        Number = reader.GetString(2),
                        Model = reader.GetString(3),
                        Fuel = reader.GetString(4),
                        TrunkVolume = reader.GetDouble(5),
                        FuelConsumption = reader.GetDouble(6),
                        TechnicalCondition = reader.GetString(7)



                    };
                    connaction.Close();
                    return entity;
                   
                }

            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
    }
}
