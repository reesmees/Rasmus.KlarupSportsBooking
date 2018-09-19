using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using Rasmus.KlarupSportsBooking.Business;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.GUI
{
    /// <summary>
    /// Interaction logic for AdminLoginWindow.xaml
    /// </summary>
    public partial class AdminLoginWindow : Window
    {
        public DataHandler handler;
        public AdminLoginWindow(DataHandler handler)
        {
            InitializeComponent();
            this.handler = handler;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!handler.DB.Administrators.Any(a => a.E_mails.E_mailAddress == tbxEmail.Text && a.Password == psbxPassword.Password))
                {
                    MessageBox.Show("Forkert e-mailadresse eller kodeord");
                    tbxEmail.Text = "";
                    psbxPassword.Password = "";
                }
                else
                {
                    Administrator admin = handler.DB.Administrators.Where(a => a.E_mails.E_mailAddress == tbxEmail.Text && a.Password == psbxPassword.Password).SingleOrDefault();
                    AdminWindow adminWindow = new AdminWindow(handler, admin);
                    adminWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }
    }
}
