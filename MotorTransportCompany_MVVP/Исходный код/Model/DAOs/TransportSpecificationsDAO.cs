using MySqlConnector;
using Senparc.CO2NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportSpecificationsDAO : IDAO<TransportSpecification>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<TransportSpecification> GetAll()
        {
            try
            {
                List<TransportSpecification> entities = new List<TransportSpecification>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_transport_specifications, id_fuel, model, trunk_volume, fuel_consumption FROM `transport_specifications`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportSpecification entity = new TransportSpecification
                        {
                            Id = reader.GetInt32(0),
                            FuelType_ID = reader.GetInt32(1),
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

        public TransportSpecification GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_transport_specifications, id_fuel, model, trunk_volume, fuel_consumption FROM `transport_specifications` WHERE id_transport_specifications = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    TransportSpecification entity = new TransportSpecification
                    {
                        Id = reader.GetInt32(0),
                        FuelType_ID = reader.GetInt32(1),
                        Model = reader.GetString(2),
                        TrunkVolume = reader.GetDouble(3),
                        FuelConsumption = reader.GetDouble(4)
                    };
                    return entity;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }


        }

        public void Delete(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"DELETE FROM transport_specifications WHERE `transport_specifications`.`id_transport_specifications` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(TransportSpecification entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `transport_specifications` (`id_transport_specifications`, `id_fuel`, `model`, `trunk_volume`, `fuel_consumption`) VALUES (NULL, '{entity.FuelType_ID}', '{entity.Model}', '{entity.TrunkVolume}', '{entity.FuelConsumption}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(TransportSpecification entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE `transport_specifications` SET `id_fuel` = '{entity.FuelType_ID}', `model` = '{entity.Model}', `trunk_volume` = '{entity.TrunkVolume.ToString().Replace(',','.')}', `fuel_consumption` = '{entity.FuelConsumption.ToString().Replace(',', '.')}' WHERE `transport_specifications`.`id_transport_specifications` = {entity.Id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
    }
}
