using MotorTransportCompany_MVVP.ViewModels;
using System.Windows;

namespace MotorTransportCompany_MVVP
{
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
