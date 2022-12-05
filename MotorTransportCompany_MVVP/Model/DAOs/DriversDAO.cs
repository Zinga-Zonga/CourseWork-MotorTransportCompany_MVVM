using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DriversDAO : IDAO<Driver>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<Driver> GetAll()
        {
            try
            {
                List<Driver> entities = new List<Driver>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_driver, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `drivers`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Driver entity = new Driver
                        {
                            Id = reader.GetInt32(0),
                            Department_id = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            BirthdayDate = reader.GetString(5),
                            Age = reader.GetInt32(6),
                            IdSex = reader.GetInt32(7),
                            PassportNumber = reader.GetInt32(8)
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

        public Driver GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_driver, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `drivers` WHERE id_driver = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Driver entity = new Driver
                    {
                        Id = reader.GetInt32(0),
                        Department_id = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Surname = reader.GetString(3),
                        Patronymic = reader.GetString(4),
                        BirthdayDate = reader.GetString(5),
                        Age = reader.GetInt32(6),
                        IdSex = reader.GetInt32(7),
                        PassportNumber = reader.GetInt32(8)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM drivers WHERE `drivers`.`id_driver` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(Driver entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `drivers` (`id_driver`, `id_department`, `name`, `surname`, `patronymic`, `birthday`, `age`, `id_sex`, `passport`)" +
                            $" VALUES(NULL, '{entity.Department_id}', '{entity.Name}', '{entity.Surname}', '{entity.Patronymic}', '{entity.BirthdayDate}', '{entity.Age}', '{entity.IdSex}', '{entity.PassportNumber}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(Driver entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE drivers SET id_driver = '{entity.Id}', id_department = '{entity.Department_id}', name = '{entity.Name}', surname = '{entity.Surname}', patronymic = '{entity.Patronymic}', birthday = '{entity.BirthdayDate}', age = '{entity.Age}', id_sex = '{entity.IdSex}', passport = '{entity.PassportNumber}' WHERE drivers.id_driver = '{entity.Id}'", connaction);
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