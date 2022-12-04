using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TransportDAO : IDAO<Transport>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<Transport> GetAll()
        {
            try
            {
                List<Transport> entities = new List<Transport>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT `id_transport`, `id_department`, `id_transport_specification`, `id_condition`, `number` FROM `transport`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transport entity = new Transport
                        {
                            Id = reader.GetInt32(0),
                            TransportSpecification_ID = reader.GetInt32(1),
                            TechnicalCondition_ID = reader.GetInt32(2),
                            Department_ID = reader.GetInt32(3),
                            TransportNumber = reader.GetString(4)
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

        public Transport getEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT `id_transport`, `id_department`, `id_transport_specification`, `id_condition`, `number` FROM `transport` WHERE id_transport = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Transport entity = new Transport
                    {
                        Id = reader.GetInt32(0),
                        TransportSpecification_ID = reader.GetInt32(1),
                        TechnicalCondition_ID = reader.GetInt32(2),
                        Department_ID = reader.GetInt32(3),
                        TransportNumber = reader.GetString(4)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM transport WHERE `transport`.`id_transport` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(Transport entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `transport` (`id_transport`, `id_department`, `id_transport_specification`, `id_condition`, `number`)" +
                            $" VALUES(NULL, '{entity.Department_ID}', '{entity.TransportSpecification_ID}', '{entity.TechnicalCondition_ID}', '{entity.TransportNumber}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public void Update(Transport entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE transport SET id_transport = '{entity.Id}', id_department = '{entity.Department_ID}', id_transport_specification = '{entity.TransportSpecification_ID}', surname = '{entity.Surname}', patronymic = '{entity.Patronymic}', birthday = '{entity.BirthdayDate}', age = '{entity.Age}', id_sex = '{entity.IdSex}', passport = '{entity.PassportNumber}' WHERE transport.id_transport = '{entity.Id}'", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("LOL");
            }
        }
    }
}
