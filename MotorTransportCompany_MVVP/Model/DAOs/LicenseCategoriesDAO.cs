using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class LicenseCategoriesDAO
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";


        public List<LicenseCategory> GetAll()
        {
            List<LicenseCategory> licenseCategories = new List<LicenseCategory>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_category, category FROM `license_categories`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    LicenseCategory category = new LicenseCategory
                    {
                        Id = reader.GetInt32(0),
                        Category = reader.GetString(1)
                    };
                    licenseCategories.Add(category);
                }
            }
            connaction.Close();
            return licenseCategories;
        }
    }
}
