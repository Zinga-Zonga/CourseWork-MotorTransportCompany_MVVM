using MotorTransportCompany_MVVP.Model;
using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    //class WindowContext
    //{
    //    MechanicService dao = new MechanicService();
    //    DepartmentService dao2 = new DepartmentService();
    //    public ObservableCollection<MechanicSqlView> DAO { get; } = new ObservableCollection<MechanicSqlView>();
        
    //    public WindowContext()
    //    {
    //        DAO = new ObservableCollection<MechanicSqlView>(dao.GetAll());
            
    //    }

    //}

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
