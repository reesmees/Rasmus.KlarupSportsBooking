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
            dgrdReservations.ItemsSource = handler.DB.Reservations.Local.Where(r => r.IsHandled == false);
            dgrdRecurringReservations.ItemsSource = handler.DB.RecurringReservations.Local.Where(r => r.IsHandled == false);
            dgrdBookings.ItemsSource = handler.DB.Bookings.Local;
            dgrdRecurringBookings.ItemsSource = handler.DB.RecurringBookings.Local;
        }

        private void btnCreateBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgrdReservations.SelectedItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(tbxReservationStartTime.Text))
                    {
                        if (TimeSpan.TryParse(tbxReservationStartTime.Text, out TimeSpan startTime))
                        {
                            Reservation reservation = (Reservation)dgrdReservations.SelectedItem;
                            handler.Writer.CreateBooking(reservation, startTime, admin);
                            UpdateSources();
                        }
                        else
                        {
                            MessageBox.Show("Kunne ikke parse starttiden");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Indtast en starttid");
                    }
                }
                else
                {
                    MessageBox.Show("Vælg en reservation at godkende");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgrdReservations.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Er du sikker på du vil afvise den valgte reservation?", "Bekræft afvisning", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Reservation reservation = (Reservation)dgrdReservations.SelectedItem;
                        reservation.IsHandled = true;
                        handler.DB.SaveChanges();
                        UpdateSources();
                        MessageBox.Show("Reservation afvist");
                    }
                }
                else
                {
                    MessageBox.Show("Vælg en reservation at afvise");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void btnCalculateCoveragePercentage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double coveragePercent = 0;
                if (dpckStartDate.SelectedDate == null)
                {
                    MessageBox.Show("Vælg en startdato");
                }
                else if (dpckEndDate.SelectedDate == null)
                {
                    coveragePercent = handler.Reader.CalculateCoveragePercentageByDay((DateTime)dpckStartDate.SelectedDate);
                    MessageBox.Show($"Belægningsprocenten på {dpckStartDate.SelectedDate} er {coveragePercent}%", "Resultat");
                }
                else if (dpckEndDate.SelectedDate < dpckStartDate.SelectedDate)
                {
                    MessageBox.Show("Slutdato kan ikke være før startdato");
                }
                else
                {
                    coveragePercent = handler.Reader.CalculateCoveragePercentageByDateRange((DateTime)dpckStartDate.SelectedDate, (DateTime)dpckEndDate.SelectedDate);
                    MessageBox.Show($"Belægningsprocenten mellem {dpckStartDate.SelectedDate} og {dpckEndDate.SelectedDate} er {coveragePercent}%", "Resultat");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl{Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }
    }
}
