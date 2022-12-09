using MotorTransportCompany_MVVP.Model.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DriversSqlViewDAO : IViewDAO<DriverSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<DriverSqlView> GetAll()
        {
            try
            {

                List<DriverSqlView> entities = new List<DriverSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT drivers.id_driver, departments.department, drivers.name, drivers.surname, drivers.patronymic, drivers.birthday, drivers.age, sex_types.sex, drivers.passport, drivers.license, (SELECT GROUP_CONCAT(license_categories.category SEPARATOR ', ')) FROM drivers INNER JOIN departments ON drivers.id_department = departments.id_department INNER JOIN drivers_categories ON drivers.id_driver = drivers_categories.id_driver INNER JOIN license_categories ON drivers_categories.id_category = license_categories.id_category INNER JOIN sex_types ON drivers.id_sex = sex_types.id_sex group by drivers.id_driver", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DriverSqlView entity = new DriverSqlView
                        {
                            Id = reader.GetInt32(0),
                            DepartmentName = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            BirthdayDate = reader.GetString(5),
                            Age = reader.GetInt32(6),
                            Sex = reader.GetString(7),
                            PassportNumber = reader.GetInt32(8),
                            
                            
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

        public List<DriverSqlView> GetEntityById(int id)
        {
            try
            {
                
                List<DriverSqlView> entities = new List<DriverSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT drivers.id_driver, departments.department, drivers.name, drivers.surname, drivers.patronymic, drivers.birthday, drivers.age, sex_types.sex, drivers.passport, drivers.license, (SELECT GROUP_CONCAT(license_categories.category SEPARATOR ', ')) FROM drivers INNER JOIN departments ON drivers.id_department = departments.id_department INNER JOIN drivers_categories ON drivers.id_driver = drivers_categories.id_driver INNER JOIN license_categories ON drivers_categories.id_category = license_categories.id_category INNER JOIN sex_types ON drivers.id_sex = sex_types.id_sex group by drivers.id_driver WHERE id_driver = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DriverSqlView entity = new DriverSqlView
                        {
                            Id = reader.GetInt32(0),
                            DepartmentName = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            BirthdayDate = reader.GetString(5),
                            Age = reader.GetInt32(6),
                            Sex = reader.GetString(7),
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
    }
}
