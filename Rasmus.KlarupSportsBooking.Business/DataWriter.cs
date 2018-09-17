using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Business
{
    public class DataWriter
    {
        private KlarupSportsBookingContext db;

        public DataWriter(KlarupSportsBookingContext dB)
        {
            DB = dB;
        }

        public KlarupSportsBookingContext DB
        {
            get { return db; }
            set { db = value; }
        }


        public void CreateUnion(string name, string username, string password, string email, string streetName, int houseNumber, int floor, int zipCode, string city)
        {
            if (!DB.Unions.Any(u => u.UnionName == name))
            {
                if (!DB.Addresses.Any(a => a.City == city && a.StreetName == streetName && a.ZipCode == zipCode && a.Floor == floor && a.HouseNumber == houseNumber))
                    CreateAddress(streetName, houseNumber, floor, zipCode, city);
                if (!DB.E_mails.Any(e => e.E_mailAddress == email))
                    CreateEmail(email);
                Union union = new Union { UnionName = name };
                DB.E_mails.Where(e => e.E_mailAddress == email).SingleOrDefault().Unions.Add(union);
                DB.Addresses.Where(a => a.City == city && a.StreetName == streetName && a.ZipCode == zipCode && a.Floor == floor && a.HouseNumber == houseNumber).SingleOrDefault().Unions.Add(union);
                DB.SaveChanges();
                CreateUnionLogin(username, password, union.UnionName);
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Foreningen eksisterer allerede i databasen");
            }
        }

        public void CreateEmail(string email)
        {
            if (!DB.E_mails.Any(e => e.E_mailAddress == email))
            {
                DB.E_mails.Add(new E_mails { E_mailAddress = email });
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Emailadressen eksisterer allerede i databasen");
            }
        }

        public void CreateAddress(string streetName, int houseNumber, int floor, int zipCode, string city)
        {
            if (!DB.Addresses.Any(a => a.City == city && a.StreetName == streetName && a.ZipCode == zipCode && a.Floor == floor && a.HouseNumber == houseNumber))
            {
                DB.Addresses.Add(new Address { StreetName = streetName, City = city, Floor = floor, HouseNumber = houseNumber, ZipCode = zipCode });
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Adressen eksisterer allerede i databasen");
            }
        }

        public void CreateUnionLogin(string username, string password, string unionName)
        {
            if (!DB.UnionLogins.Any(u => u.Username == username && u.Password == password))
            {
                DB.Unions.Where(u => u.UnionName == unionName).SingleOrDefault().UnionLogins.Add(new UnionLogin { Username = username, Password = password });
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Den kombination af brugernavn og kodeord eksisterer allerede");
            }
        }

        public void CreateAdministrator(string email, string name, string password)
        {
            if (!DB.E_mails.Any(e => e.E_mailAddress == email))
                CreateEmail(email);
            DB.E_mails.Where(e => e.E_mailAddress == email).SingleOrDefault().Administrators.Add(new Administrator { Name = name, Password = password });
            DB.SaveChanges();
        }

        public void CreateBooking(Reservation reservation, TimeSpan startTime, Administrator admin)
        {
            Booking booking = new Booking { StartTime = startTime };
            DB.Reservations.Where(r => r.ID == reservation.ID).SingleOrDefault().Bookings.Add(booking);
            DB.Reservations.Where(r => r.ID == reservation.ID).SingleOrDefault().IsHandled = true;
            DB.Administrators.Where(a => a.ID == admin.ID).SingleOrDefault().Bookings.Add(booking);
            db.SaveChanges();
        }

        public void CreateActivity(string activityName, string hallUsage)
        {
            if (!DB.Activities.Any(a => a.ActivityName == activityName))
            {
                DB.Activities.Add(new Activity { ActivityName = activityName, HallUsage = hallUsage });
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Aktiviteten findes allerede i databasen");
            }
        }
    }
}
