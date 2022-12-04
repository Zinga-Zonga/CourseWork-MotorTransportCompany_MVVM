using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class TechnicalConditionsDAO : IDAO<TechnicalCondition>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<TechnicalCondition> GetAll()
        {
            try
            {
                List<TechnicalCondition> entities = new List<TechnicalCondition>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_condition, fuel FROM `technical_conditions`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TechnicalCondition entity = new TechnicalCondition
                        {
                            Id = reader.GetInt32(0),
                            Condition = reader.GetString(1)
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

        public TechnicalCondition getEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_condition, fuel FROM `technical_conditions` WHERE id_condition = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    TechnicalCondition entity = new TechnicalCondition
                    {
                        Id = reader.GetInt32(0),
                        Condition = reader.GetString(1)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM technical_conditions WHERE `technical_conditions`.`id_condition` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(TechnicalCondition entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `technical_conditions` (`id_condition`, `technical_condition`)" +
                            $" VALUES(NULL, '{entity.Condition}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(TechnicalCondition entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE technical_conditions SET id_condition = '{entity.Id}', technical_condition = '{entity.Condition}' WHERE technical_conditions.id_condition = '{entity.Id}'", connaction);
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
