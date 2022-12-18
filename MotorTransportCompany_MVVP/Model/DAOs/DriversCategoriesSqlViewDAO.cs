using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DriversCategoriesSqlViewDAO : IViewDAO<DriversAndCategoriesSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<DriversAndCategoriesSqlView> GetAll()
        {
            try
            {

                List<DriversAndCategoriesSqlView> entities = new List<DriversAndCategoriesSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT drivers.id_driver, departments.department, drivers.name, drivers.surname, drivers.patronymic, drivers.license, ( SELECT GROUP_CONCAT( license_categories.category SEPARATOR ', ' ) ) FROM drivers INNER JOIN drivers_categories ON drivers.id_driver = drivers_categories.id_driver INNER JOIN license_categories ON drivers_categories.id_category = license_categories.id_category INNER JOIN departments ON drivers.id_department = departments.id_department GROUP BY drivers.id_driver", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DriversAndCategoriesSqlView entity = new DriversAndCategoriesSqlView
                        {
                            Id = reader.GetInt32(0),
                            Department = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            LicenseNumber = reader.GetInt32(5),
                            Categories = reader.GetString(6)
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

        public List<DriversAndCategoriesSqlView> GetEntityById(int id)
        {
            try
            {

                List<DriversAndCategoriesSqlView> entities = new List<DriversAndCategoriesSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_mechanic, department, name, surname, patronymic, birthday, age, sex, passport FROM `mechanics` JOIN departments ON mechanics.id_department = departments.id_department JOIN sex_types ON mechanics.id_sex = sex_types.id_sex WHERE id_mechanic = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DriversAndCategoriesSqlView entity = new DriversAndCategoriesSqlView
                        {
                            Id = reader.GetInt32(0),
                            Department = reader.GetString(1),
                            Name = reader.GetString(2),
                            Surname = reader.GetString(3),
                            Patronymic = reader.GetString(4),
                            LicenseNumber = reader.GetInt32(5),
                            Categories = reader.GetString(6)
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
