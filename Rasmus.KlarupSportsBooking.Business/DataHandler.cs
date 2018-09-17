using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Business
{
    public class DataHandler
    {
        private KlarupSportsBookingContext db;
        private DataReader reader;
        private DataWriter writer;
        private DataUpdater updater;
        private DataDeleter deleter;

        public DataHandler()
        {
            DB = new KlarupSportsBookingContext();
            Reader = new DataReader(DB);
            Writer = new DataWriter(DB);
            Updater = new DataUpdater(DB);
            Deleter = new DataDeleter(DB);
        }

        public DataDeleter Deleter
        {
            get { return deleter; }
            set { deleter = value; }
        }

        public DataUpdater Updater
        {
            get { return updater; }
            set { updater = value; }
        }

        public DataWriter Writer
        {
            get { return writer; }
            set { writer = value; }
        }

        public DataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        public KlarupSportsBookingContext DB
        {
            get { return db; }
            set { db = value; }
        }
    }
}
