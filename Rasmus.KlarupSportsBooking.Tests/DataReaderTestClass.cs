using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rasmus.KlarupSportsBooking.Business;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Tests
{
    [TestClass]
    public class DataReaderTestClass
    {
        DataHandler handler = new DataHandler();
        [TestMethod]
        public void CalculateCoveragePercentageByDayTest()
        {
            DateTime date = DateTime.Today;
            bool loop = true;
            while (loop)
            {
                if (handler.DB.Bookings.Any(b => DbFunctions.TruncateTime(b.Reservation.Date) == DbFunctions.TruncateTime(date)))
                {
                    date = date.AddDays(1);
                }
                else
                {
                    loop = false;
                }
            }
            decimal beforeCoveragePercentage = handler.Reader.CalculateCoveragePercentageByDay(date);
            List<Administrator> admins = handler.DB.Administrators.ToList();
            Activity activity = handler.DB.Activities.ToList()[0];
            Union union = handler.DB.Unions.ToList()[0];
            int reservationLength = 90;
            handler.Writer.CreateReservation(activity, union, date, reservationLength);
            Reservation reservation = handler.DB.Reservations.Where(r => DbFunctions.TruncateTime(r.Date) == DbFunctions.TruncateTime(date)).SingleOrDefault();
            handler.Writer.CreateBooking(reservation, new TimeSpan(09, 00, 00), admins[0]);

            decimal afterCoveragePercentage = handler.Reader.CalculateCoveragePercentageByDay(date);

            Assert.AreNotEqual(beforeCoveragePercentage, afterCoveragePercentage);
        }
    }
}
