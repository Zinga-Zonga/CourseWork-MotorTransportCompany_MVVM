using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DriversCategoriesDAO : IDAO<DriversAndCategories>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<DriversAndCategories> GetAll()
        {
            try
            {
                List<DriversAndCategories> entities = new List<DriversAndCategories>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id,id_driver, id_category FROM `drivers_categories`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DriversAndCategories entity = new DriversAndCategories
                        {
                            Id = reader.GetInt32(0),
                            DriverID= reader.GetInt32(1),
                            CategoryID= reader.GetInt32(2)
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

        public DriversAndCategories GetEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id, id_driver, id_category FROM `drivers_categories` WHERE id = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    DriversAndCategories entity = new DriversAndCategories
                    {
                        Id = reader.GetInt32(0),
                        DriverID = reader.GetInt32(1),
                        CategoryID = reader.GetInt32(2)
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM drivers_categories WHERE `drivers_categories`.`id` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(DriversAndCategories entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `drivers_categories` (`id`, 'id_driver', 'id_category')" +
                            $" VALUES(NULL, '{entity.DriverID}', '{entity.CategoryID}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Update(DriversAndCategories entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE drivers_categories SET id = '{entity.Id}', id_driver = '{entity.DriverID}', id_category = '{entity.CategoryID}' WHERE drivers_categories.id = '{entity.Id}'", connaction);
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
