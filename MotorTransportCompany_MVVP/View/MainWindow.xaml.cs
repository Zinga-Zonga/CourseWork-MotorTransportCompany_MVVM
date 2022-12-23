using MotorTransportCompany_MVVP.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP
{
    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
