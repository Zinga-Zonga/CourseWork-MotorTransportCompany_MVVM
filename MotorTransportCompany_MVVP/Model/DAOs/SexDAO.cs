using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotorTransportCompany_MVVP.Model
{
    internal class SexDAO : IDAO<SexType>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<SexType> GetAll()
        {
            try
            {
                List<SexType> entities = new List<SexType>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_sex, fuel FROM `sex_types`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SexType entity = new SexType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
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

        public SexType GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_sex, fuel FROM `sex_types` WHERE id_sex = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    SexType entity = new SexType
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM sex_types WHERE `sex_types`.`id_sex` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(SexType entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `sex_types` (`id_sex`, `sex`)" +
                            $" VALUES(NULL, '{entity.Name}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(SexType entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE sex_types SET id_sex = '{entity.Id}', sex = '{entity.Name}' WHERE sex_types.id_sex = '{entity.Id}'", connaction);
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
