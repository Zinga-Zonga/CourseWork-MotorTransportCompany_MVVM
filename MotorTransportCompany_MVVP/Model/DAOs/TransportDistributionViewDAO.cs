using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportDistributionSqlViewDAO : IViewDAO<TransportDistributionSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";
        public List<TransportDistributionSqlView> GetAll()
        {
            try
            {

                List<TransportDistributionSqlView> entities = new List<TransportDistributionSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport_distribution.id, departments.department, drivers.name, drivers.surname, drivers.patronymic, transport.number, transport_specifications.model FROM transport_distribution INNER JOIN transport ON transport_distribution.id_transport = transport.id_transport INNER JOIN drivers ON transport_distribution.id_driver = drivers.id_driver INNER JOIN departments ON drivers.id_department = departments.id_department AND transport.id_department = departments.id_department INNER JOIN transport_specifications ON transport.id_transport_specification = transport_specifications.id_transport_specifications", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportDistributionSqlView entity = new TransportDistributionSqlView
                        {
                            Id = reader.GetInt32(0),
                            Department = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            Number = reader.GetString(5),
                            Model = reader.GetString(6)
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

        public List<TransportDistributionSqlView> GetEntityById(int id)
        {
            try
            {

                List<TransportDistributionSqlView> entities = new List<TransportDistributionSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT transport_distribution.id, departments.department, drivers.name, drivers.surname, drivers.patronymic, transport.number, transport_specifications.model FROM transport_distribution INNER JOIN transport ON transport_distribution.id_transport = transport.id_transport INNER JOIN drivers ON transport_distribution.id_driver = drivers.id_driver INNER JOIN departments ON drivers.id_department = departments.id_department AND transport.id_department = departments.id_department INNER JOIN transport_specifications ON transport.id_transport_specification = transport_specifications.id_transport_specifications WHERE transport_distribution.id = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportDistributionSqlView entity = new TransportDistributionSqlView
                        {
                            Id = reader.GetInt32(0),
                            Department = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            Number = reader.GetString(5),
                            Model = reader.GetString(6)
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
