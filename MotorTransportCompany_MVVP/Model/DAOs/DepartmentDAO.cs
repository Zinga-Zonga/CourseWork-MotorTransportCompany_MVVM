using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DepartmentDAO : IDAO<Department>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<Department> GetAll()
        {
            try
            {
                List<Department> entities = new List<Department>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();

                MySqlCommand command = new MySqlCommand("SELECT id_department, department FROM `departments`", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department entity = new Department
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

        public Department getEntityById(int id)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_department, department FROM `departments` WHERE id_department = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Department entity = new Department
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
                MySqlCommand command = new MySqlCommand($"DELETE FROM departments WHERE `departments`.`id_department` = {id}", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }

        public void Add(Department entity)
        {
            try
            {
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO `departments` (id_department, department)" +
                            $" VALUES(NULL, '{entity.Name}')", connaction);
                command.ExecuteNonQuery();
                connaction.Close();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public void Update(Department entity)
        {
            try
            {

                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE departments SET id_department = '{entity.Id}', name = '{entity.Name}' WHERE departments.id_department = '{entity.Id}'", connaction);
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
