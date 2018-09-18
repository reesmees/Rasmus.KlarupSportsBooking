using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Business
{
    /// <summary>
    /// Class used to get specific data from the database
    /// </summary>
    public class DataReader
    {
        private KlarupSportsBookingContext db;

        public DataReader(KlarupSportsBookingContext dB)
        {
            DB = dB;
        }

        public KlarupSportsBookingContext DB
        {
            get { return db; }
            set { db = value; }
        }

        public Union CalculateMostActiveUnion()
        {
            return DB.Unions.OrderByDescending(u => u.Reservations.Count()).FirstOrDefault();
        }

        public List<Activity> OrderActivityUsage()
        {
            return DB.Activities.OrderByDescending(a => a.Reservations.Count()).ToList();
        }

        public DateTime FindNextAvailableTime()
        {
            throw new NotImplementedException();            
        }

        public decimal CalculateCoveragePercentageByDay(DateTime date)
        {
            List<Booking> bookings = DB.Bookings.Where(b => DbFunctions.TruncateTime(b.Reservation.Date) == DbFunctions.TruncateTime(date)).ToList();
            decimal reservedMinutes = 0;
            decimal reservedPercentage = 0;
            bookings.OrderBy(b => b.Reservation.ReservationLength).OrderBy(b => b.StartTime);
            TimeSpan bookingEndTime;
            while (bookings.Count() > 0)
            {
                reservedMinutes += bookings[0].Reservation.ReservationLength;
                bookingEndTime = bookings[0].EndTime;
                bookings = bookings.Where(b => b.StartTime > bookingEndTime).ToList();
            }
            if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                reservedPercentage = reservedMinutes / 720 * 100;
            }
            else
            {
                reservedPercentage = reservedMinutes / 840 * 100;
            }
            return reservedPercentage;
        }
    }
}
