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
        DepartmentDAO dao = new DepartmentDAO();
        
        public List<Department> DAO { get; set; }
        
        public List<Mechanic> DAO2 { get; set; }
        public WindowContext()
        {
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
