using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class MechanicsDAO : IDAO<Mechanic>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<Mechanic> GetAll()
        {
            try
            {
                List<Mechanic> entities = new List<Mechanic>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_mechanic, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `mechanics`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Mechanic entity = new Mechanic
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
            catch(MySqlException ex)
            {
                throw ex;
            }
            
        }

        public Mechanic getEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_mechanic, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `mechanics` WHERE id_mechanic = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Mechanic entity = new Mechanic
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
            catch(MySqlException ex)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM mechanics WHERE `mechanics`.`id_mechanic` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(Mechanic entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `mechanics` (`id_mechanic`, `id_department`, `name`, `surname`, `patronymic`, `birthday`, `age`, `id_sex`, `passport`)" +
                            $" VALUES(NULL, '{entity.Department_id}', '{entity.Name}', '{entity.Surname}', '{entity.Patronymic}', '{entity.BirthdayDate}', '{entity.Age}', '{entity.IdSex}', '{entity.PassportNumber}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public void Update(Mechanic entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE mechanics SET id_mechanic = '{entity.Id}', id_department = '{entity.Department_id}', name = '{entity.Name}', surname = '{entity.Surname}', patronymic = '{entity.Patronymic}', birthday = '{entity.BirthdayDate}', age = '{entity.Age}', id_sex = '{entity.IdSex}', passport = '{entity.PassportNumber}' WHERE mechanics.id_mechanic = '{entity.Id}'", connaction);
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
