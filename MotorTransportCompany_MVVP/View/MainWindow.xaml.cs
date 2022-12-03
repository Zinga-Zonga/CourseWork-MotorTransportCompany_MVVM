using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MotorTransportCompany_MVVP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    class WindowContext
    {
        MechanicsDAO dao = new MechanicsDAO();
        Mechanic mech1 = new Mechanic
        {
            Id = 3,
            Department_id = 2,
            Name = "FUC222KING FAI2222L",
            Surname = "FUCKING FAIL22222",
            Patronymic = "FUCKING FAIL",
            IdSex = 2,
            Age = 13,
            BirthdayDate = DateTime.Now.ToString("yyyy-MM-dd"),
            PassportNumber = 6666666
        };
        Mechanic mech2 = new Mechanic
        {
            
            Department_id = 2,
            Name = "FUCKING FAIL",
            Surname = "FUCKING FAIL",
            Patronymic = "FUCKING FAIL",
            IdSex = 2,
            Age = 13,
            BirthdayDate = DateTime.Now.ToString("yyyy-MM-dd"),
            PassportNumber = 6666666
        };
        public List<Mechanic> DAO { get; set; }
        
        public List<Mechanic> DAO2 { get; set; }
        public WindowContext()
        {
            dao.Update(mech1);
            dao.Add(mech2);
            dao.Delete(2);
            DAO = dao.GetAll();
            

        }

    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowContext();
        }
    }
}
