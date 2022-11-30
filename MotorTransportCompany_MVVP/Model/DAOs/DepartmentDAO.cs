using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class DepartmentDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `departments`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Department department = new Department
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    departments.Add(department);
                }
            }
            connaction.Close();
            return departments;
        }
    }
}
