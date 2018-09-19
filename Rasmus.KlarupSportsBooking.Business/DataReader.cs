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

        /// <summary>
        /// Method to calculate which union has created the most reservations
        /// </summary>
        /// <returns>The union that has created the most reservations</returns>
        public Union CalculateMostActiveUnion()
        {
            return DB.Unions.OrderByDescending(u => u.Reservations.Count()).FirstOrDefault();
        }

        /// <summary>
        /// Method to order activities by how often they are reserved
        /// </summary>
        /// <returns>List of activites in descending order of usage</returns>
        public List<Activity> OrderActivityUsage()
        {
            return DB.Activities.OrderByDescending(a => a.Reservations.Count()).ToList();
        }

        public DateTime FindNextAvailableTime()
        {
            throw new NotImplementedException();            
        }

        /// <summary>
        /// Method to calculate the percentage of opening hours on any given day, that has been booked
        /// </summary>
        /// <param name="date">The day on which to calculate</param>
        /// <returns>The percentage of opening hours on the given day, that has been booked</returns>
        public double CalculateCoveragePercentageByDay(DateTime date)
        {
            double nonBookedMinutes = CalculateNonBookedMinutesByDay(date);
            double openMinutes = CalculateTotalMinutesOpenByDay(date);
            double reservedMinutes = openMinutes - nonBookedMinutes;
            double reservedPercentage = reservedMinutes / openMinutes * 100;

            return reservedPercentage;
        }

        /// <summary>
        /// Method to calculate the amount of minutes within operating hours on any given day, that are not booked.
        /// </summary>
        /// <param name="date">The day to calculate nonbooked minutes on</param>
        /// <returns>The amount of minutes within operating hours on the given day, that are not booked</returns>
        public double CalculateNonBookedMinutesByDay(DateTime date)
        {
            List<Booking> bookings = DB.Bookings.Where(b => DbFunctions.TruncateTime(b.Reservation.Date) == DbFunctions.TruncateTime(date)).OrderBy(b => b.EndTime).OrderBy(b => b.StartTime).ToList();
            double unreservedMinutes = 0;
            TimeSpan openingTime;
            TimeSpan closingTime;
            if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                openingTime = new TimeSpan(09, 00, 00);
                closingTime = new TimeSpan(21, 00, 00);
            }
            else
            {
                openingTime = new TimeSpan(08, 00, 00);
                closingTime = new TimeSpan(22, 00, 00);
            }
            if (bookings.Count() == 0)
            {
                unreservedMinutes += (closingTime - openingTime).TotalMinutes;
            }
            else if (bookings.Count() == 1)
            {
                unreservedMinutes += (bookings[0].StartTime - openingTime).TotalMinutes;
                unreservedMinutes += (closingTime - bookings[0].EndTime).TotalMinutes;
            }
            else if (bookings.Count() > 1)
            {
                unreservedMinutes += (bookings[0].StartTime - openingTime).TotalMinutes;
                for (int i = 1; i < bookings.Count(); i++)
                {
                    if (bookings[i-1].EndTime < bookings[i].StartTime)
                    {
                        unreservedMinutes += (bookings[i].StartTime - bookings[i - 1].EndTime).TotalMinutes;
                    }
                }
                unreservedMinutes += (closingTime - bookings[bookings.Count() - 1].EndTime).TotalMinutes;
            }
            return unreservedMinutes;
        }

        /// <summary>
        /// Method to calculate how many minutes the hall is open for on any given day
        /// </summary>
        /// <param name="date">The date to find opening hours of</param>
        /// <returns>The amount of minutes the hall is open for on the given day</returns>
        public double CalculateTotalMinutesOpenByDay(DateTime date)
        {
            TimeSpan openingTime;
            TimeSpan closingTime;
            if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                openingTime = new TimeSpan(09, 00, 00);
                closingTime = new TimeSpan(21, 00, 00);
            }
            else
            {
                openingTime = new TimeSpan(08, 00, 00);
                closingTime = new TimeSpan(22, 00, 00);
            }
            return (closingTime - openingTime).TotalMinutes;
        }

        /// <summary>
        /// Method to calculate the percentage of time between two dates that are booked
        /// </summary>
        /// <param name="startDate">The inclusive start date to calculate on</param>
        /// <param name="endDate">The inclusive end date to calculate on</param>
        /// <returns>The percentage of time between the two given dates that is booked</returns>
        public double CalculateCoveragePercentageByDateRange(DateTime startDate, DateTime endDate)
        {
            double unreservedMinutes = 0;
            double openMinutes = 0;
            List<DateTime> dates = FindDatesInDateRange(startDate, endDate);
            foreach (DateTime day in dates)
            {
                openMinutes += CalculateTotalMinutesOpenByDay(day);
                unreservedMinutes += CalculateNonBookedMinutesByDay(day);
            }
            double reservedMinutes = openMinutes - unreservedMinutes;
            double reservedPercentage = reservedMinutes / openMinutes * 100;
            

            return reservedPercentage;
        }

        /// <summary>
        /// Method to return a list of all days between two dates
        /// Throws an argument exception if endDate is earlier than startDate
        /// </summary>
        /// <param name="startDate">The inclusive start date to return in a list</param>
        /// <param name="endDate">The inclusive end date to return in a list</param>
        /// <returns>A list of dates including both startDate and endDate</returns>
        public List<DateTime> FindDatesInDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("Slutdato må ikke være før startdato");
            }
            List<DateTime> dateRange = new List<DateTime>();
            for (DateTime day = startDate; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                dateRange.Add(day);
            }
            return dateRange;
        }

        /// <summary>
        /// Method to find the union that has made the most reservation in any given date range
        /// </summary>
        /// <param name="startDate">The inclusive start date to count reservations from</param>
        /// <param name="endDate">The inclusive end date to count reservations from</param>
        /// <returns>The union that has made the most reservations between the two dates</returns>
        public Union CalculateMostActiveUnionByDateRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = FindDatesInDateRange(startDate, endDate);
            List<Union> unions = DB.Unions.ToList();
            foreach (Union union in unions)
            {
                union.Reservations = DB.Reservations.Where(r => r.Union.ID == union.ID && dates.Contains(r.Date)).ToList();
            }
            return unions.OrderByDescending(u => u.Reservations.Count()).FirstOrDefault();
        }
    }
}
