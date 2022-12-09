using MotorTransportCompany_MVVP.Model.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class GarageManagerViewDAO : IViewDAO<GarageManagerSqlView>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<GarageManagerSqlView> GetAll()
        {
            try
            {

                List<GarageManagerSqlView> entities = new List<GarageManagerSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_garagemanager, department, name, surname, patronymic, birthday, age, sex, passport FROM `garage_managers` JOIN departments ON garage_managers.id_department = departments.id_department JOIN sex_types ON garage_managers.id_sex = sex_types.id_sex", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GarageManagerSqlView entity = new GarageManagerSqlView
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

        public List<GarageManagerSqlView> GetEntityById(int id)
        {
            try
            {

                List<GarageManagerSqlView> entities = new List<GarageManagerSqlView>();
                MySqlConnection connaction = new MySqlConnection(connectionString);
                connaction.Open();
                MySqlCommand command = new MySqlCommand($"SELECT id_garagemanager, department, name, surname, patronymic, birthday, age, sex, passport FROM `garage_managers` JOIN departments ON garage_managers.id_department = departments.id_department JOIN sex_types ON garage_managers.id_sex = sex_types.id_sex WHERE id_garagemanager = {id}", connaction);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GarageManagerSqlView entity = new GarageManagerSqlView
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
