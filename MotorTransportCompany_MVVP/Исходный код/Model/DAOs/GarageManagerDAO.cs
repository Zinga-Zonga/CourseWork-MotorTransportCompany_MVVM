using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class GarageManagerDAO : IDAO<GarageManager>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<GarageManager> GetAll()
        {
            try
            {
                List<GarageManager> entities = new List<GarageManager>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_garagemanager, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `garage_managers`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GarageManager entity = new GarageManager
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

        public GarageManager GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_garagemanager, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `garage_managers` WHERE id_garagemanager = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    GarageManager entity = new GarageManager
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM garage_managers WHERE `garage_managers`.`id_garagemanager` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(GarageManager entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `garage_managers` (`id_garagemanager`, `id_department`, `name`, `surname`, `patronymic`, `birthday`, `age`, `id_sex`, `passport`)" +
                            $" VALUES(NULL, '{entity.Department_id}', '{entity.Name}', '{entity.Surname}', '{entity.Patronymic}', '{entity.BirthdayDate}', '{entity.Age}', '{entity.IdSex}', '{entity.PassportNumber}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(GarageManager entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE garage_managers SET id_garagemanager = '{entity.Id}', id_department = '{entity.Department_id}', name = '{entity.Name}', surname = '{entity.Surname}', patronymic = '{entity.Patronymic}', birthday = '{entity.BirthdayDate}', age = '{entity.Age}', id_sex = '{entity.IdSex}', passport = '{entity.PassportNumber}' WHERE garage_managers.id_garagemanager = '{entity.Id}'", connaction);
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
