using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class LicenseCategoriesDAO : IDAO<LicenseCategory>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<LicenseCategory> GetAll()
        {
            try
            {
                List<LicenseCategory> entities = new List<LicenseCategory>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_category, fuel FROM `license_categories`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LicenseCategory entity = new LicenseCategory
                        {
                            Id = reader.GetInt32(0),
                            Category = reader.GetString(1)
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

        public LicenseCategory getEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_category, category FROM `license_categories` WHERE id_category = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    LicenseCategory entity = new LicenseCategory
                    {
                        Id = reader.GetInt32(0),
                        Category = reader.GetString(1)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM license_categories WHERE `license_categories`.`id_category` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(LicenseCategory entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `license_categories` (`id_category`, `category`)" +
                            $" VALUES(NULL, '{entity.Category}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public void Update(LicenseCategory entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE license_categories SET id_category = '{entity.Id}', category = '{entity.Category}' WHERE license_categories.id_category = '{entity.Id}'", connaction);
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
