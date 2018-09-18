using System;
using System.Collections.Generic;
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
    }
}
