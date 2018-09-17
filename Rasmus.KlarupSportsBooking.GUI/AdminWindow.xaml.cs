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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public DataHandler handler;
        public Administrator admin;
        public AdminWindow(DataHandler handler, Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            this.handler = handler;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                handler.DB.Reservations.Load();
                handler.DB.RecurringReservations.Load();
                handler.DB.Bookings.Load();
                handler.DB.RecurringBookings.Load();
                UpdateSources();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void UpdateSources()
        {
            dgrdReservations.ItemsSource = handler.DB.Reservations.Local;
            dgrdRecurringReservations.ItemsSource = handler.DB.RecurringReservations.Local;
            dgrdBookings.ItemsSource = handler.DB.Bookings.Local;
            dgrdRecurringBookings.ItemsSource = handler.DB.RecurringBookings.Local;
        }

        private void btnCreateBooking_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
