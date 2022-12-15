using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportDistributionDAO : IDAO<TransportDistribution>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<TransportDistribution> GetAll()
        {
            try
            {
                List<TransportDistribution> entities = new List<TransportDistribution>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id, id_transport, id_driver FROM `transport_distribution`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportDistribution entity = new TransportDistribution
                        {
                            Id = reader.GetInt32(0),
                            Transport_ID = reader.GetInt32(1),
                            Driver_ID = reader.GetInt32(2)
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

        public TransportDistribution GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id, id_transport, id_driver FROM `transport_distribution` WHERE id = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    TransportDistribution entity = new TransportDistribution
                    {
                        Id = reader.GetInt32(0),
                        Transport_ID = reader.GetInt32(1),
                        Driver_ID = reader.GetInt32(2)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM transport_distribution WHERE `transport_distribution`.`id` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(TransportDistribution entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `transport_distribution` (`id`, `id_transport`, `id_driver`) VALUES (NULL, '{entity.Transport_ID}', '{entity.Driver_ID}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(TransportDistribution entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE transport_distribution SET id = '{entity.Id}', id_transport = '{entity.Transport_ID}', id_driver = '{entity.Driver_ID}' WHERE transport_distribution.id = '{entity.Id}'", connaction);
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
