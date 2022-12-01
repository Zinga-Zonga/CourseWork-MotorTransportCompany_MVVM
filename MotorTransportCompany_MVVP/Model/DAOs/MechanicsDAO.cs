using MotorTransportCompany_MVVP.Model.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal class MechanicsDAO : IDAO<Mechanic>
    {
        static string connectionString = "server=localhost;port=3306;username=root;password=root;database=motortransportcompany";

        public List<Mechanic> GetAll()
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();

            MySqlCommand command = new MySqlCommand("SELECT id_mechanic, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `mechanics`", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Mechanic mech = new Mechanic
                    {
                        Id = reader.GetInt32(0),
                        Department_id = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Surname = reader.GetString(3),
                        Patronymic = reader.GetString(4),
                        BirthdayDate = reader.GetDateTime(5),
                        Age = reader.GetInt32(6),
                        IdSex = reader.GetInt32(7),
                        PassportNumber = reader.GetInt32(8)
                    };
                    mechanics.Add(mech);
                }
            }
            connaction.Close();
            return  mechanics;
        }

        public Mechanic getEntityById(int id)
        {
            Mechanic nullMech = new Mechanic();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();
            MySqlCommand command = new MySqlCommand($"SELECT id_mechanic, id_department, name, surname, patronymic, birthday, age, id_sex, passport FROM `mechanics` WHERE id_mechanic = {id}", connaction);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Mechanic mech = new Mechanic
                    {
                        Id = reader.GetInt32(0),
                        Department_id = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Surname = reader.GetString(3),
                        Patronymic = reader.GetString(4),
                        BirthdayDate = reader.GetDateTime(5),
                        Age = reader.GetInt32(6),
                        IdSex = reader.GetInt32(7),
                        PassportNumber = reader.GetInt32(8)
                    };
                    return mech;
                }
            }
            connaction.Close();
            return nullMech;
        }
        public void Delete(int id)
        {
            Mechanic nullMech = new Mechanic();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();
            MySqlCommand command = new MySqlCommand($"DELETE FROM mechanics WHERE `mechanics`.`id_mechanic` = {id}", connaction);
            connaction.Close();
            
        }

        public void Add(Mechanic entity)
        {
            Mechanic nullMech = new Mechanic();
            MySqlConnection connaction = new MySqlConnection(connectionString);
            connaction.Open();
            MySqlCommand command = new MySqlCommand($"INSERT INTO `mechanics` (`id_mechanic`, `id_department`, `name`, `surname`, `patronymic`, `birthday`, `age`, `id_sex`, `passport`) VALUES('3', '1', 'Test', 'Test', 'Test', '2002-10-18', '20', '2', '3333333')", connaction);
            connaction.Close();
        }

        public void Update(Mechanic entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
