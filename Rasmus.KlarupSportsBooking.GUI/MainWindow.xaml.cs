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
using Rasmus.KlarupSportsBooking.Business;

namespace Rasmus.KlarupSportsBooking.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataHandler handler;
        public MainWindow()
        {
            InitializeComponent();
            handler = new DataHandler();
        }

        private void btnOpenUnionWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AdminLoginWindow adminLogin = new AdminLoginWindow(handler);
                adminLogin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }
    }
}
