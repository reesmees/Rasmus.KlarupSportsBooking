using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Business
{
    /// <summary>
    /// Class used to delete data in the database
    /// </summary>
    public class DataDeleter
    {
        private KlarupSportsBookingContext db;

        public DataDeleter(KlarupSportsBookingContext dB)
        {
            DB = dB;
        }

        public KlarupSportsBookingContext DB
        {
            get { return db; }
            set { db = value; }
        }
    }
}
