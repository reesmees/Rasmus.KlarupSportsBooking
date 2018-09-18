using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rasmus.KlarupSportsBooking.DataAccess;

namespace Rasmus.KlarupSportsBooking.Business
{
    /// <summary>
    /// Class used to insert data into the database
    /// </summary>
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

        /// <summary>
        /// Method used to create a new union in the database.
        /// Creates a new address and/or email if the information given does not already exist in the database.
        /// </summary>
        /// <param name="name">Name of the union</param>
        /// <param name="username">Username for the union to login with</param>
        /// <param name="password">Password for the union to login with</param>
        /// <param name="email">Email address of the union</param>
        /// <param name="streetName">Street name of the union's address</param>
        /// <param name="houseNumber">House number of the union's address</param>
        /// <param name="floor">Floor number of the union's address</param>
        /// <param name="zipCode">Zip code of the union's address</param>
        /// <param name="city">City of the union's address</param>
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
        /// <summary>
        /// Method used to create a new email in the database.
        /// Throws argument exception if the email already exists in the database.
        /// </summary>
        /// <param name="email">Email address to be added to the database</param>
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
        /// <summary>
        /// Method used to create a new address in the database.
        /// Throws an argument exception if an address with the same information already exists in the database.
        /// </summary>
        /// <param name="streetName">Street name of the address</param>
        /// <param name="houseNumber">House number of the address</param>
        /// <param name="floor">Floor number of the address</param>
        /// <param name="zipCode">Zip code of the address</param>
        /// <param name="city">City of the address</param>
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
        /// <summary>
        /// Method used to create login information for a union.
        /// Throws an argument exception if the union already has login information.
        /// Throws an argument exception if the combination of username and password already exists.
        /// </summary>
        /// <param name="username">Username for the union</param>
        /// <param name="password">Password for the union</param>
        /// <param name="unionName">Name of the union to receive the login information</param>
        public void CreateUnionLogin(string username, string password, string unionName)
        {
            if (DB.Unions.Where(u => u.UnionName == unionName).SingleOrDefault().UnionLogins.Count() == 0)
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
            else
            {
                throw new ArgumentException("Den forening har allerede login information");
            }
        }
        /// <summary>
        /// Method used to create new administrator in the database.
        /// Creates a new email if the email address given does not exist in the database already.
        /// </summary>
        /// <param name="email">Email of the administrator. Used to log in as administrator.</param>
        /// <param name="name">Full name of the administrator</param>
        /// <param name="password">Password for the administrator. Used to log in as administrator.</param>
        public void CreateAdministrator(string email, string name, string password)
        {
            if (!DB.E_mails.Any(e => e.E_mailAddress == email))
                CreateEmail(email);
            DB.E_mails.Where(e => e.E_mailAddress == email).SingleOrDefault().Administrators.Add(new Administrator { Name = name, Password = password });
            DB.SaveChanges();
        }
        /// <summary>
        /// Method used to create a new booking in the database.
        /// Changes isHandled of the given reservation to true.
        /// Throws an argument exception if isHandled on the given reservation is true.
        /// </summary>
        /// <param name="reservation">The reservation to be approved</param>
        /// <param name="startTime">The time at which the booking will start</param>
        /// <param name="admin">Administrator performing the approval of the reservation</param>
        public void CreateBooking(Reservation reservation, TimeSpan startTime, Administrator admin)
        {
            if (!reservation.IsHandled)
            {
                TimeSpan endTime = startTime + TimeSpan.FromMinutes(reservation.ReservationLength);
                Booking booking = new Booking { StartTime = startTime, EndTime = endTime };
                DB.Reservations.Where(r => r.ID == reservation.ID).SingleOrDefault().Bookings.Add(booking);
                DB.Reservations.Where(r => r.ID == reservation.ID).SingleOrDefault().IsHandled = true;
                DB.Administrators.Where(a => a.ID == admin.ID).SingleOrDefault().Bookings.Add(booking);
                db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Den reservation er allerede behandlet");
            }
        }

        /// <summary>
        /// Method used to create a new activity in the database.
        /// Throws an argument exception if an activity with the given name already exists in the database.
        /// </summary>
        /// <param name="activityName">Name of the activity</param>
        /// <param name="hallUsage">How much of the hall the activity takes up written in fractions</param>
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
        /// <summary>
        /// Method used to create a new reservation in the database.
        /// </summary>
        /// <param name="activity">The activity of the reservation</param>
        /// <param name="union">The union making the reservation</param>
        /// <param name="date">The date of the reservation</param>
        /// <param name="reservationLength">The length of the reservation in minutes</param>
        public void CreateReservation(Activity activity, Union union, DateTime date, int reservationLength)
        {
            Reservation reservation = new Reservation { Date = date, ReservationLength = reservationLength, IsHandled = false };
            DB.Unions.Where(u => u.ID == union.ID).SingleOrDefault().Reservations.Add(reservation);
            DB.Activities.Where(a => a.ID == activity.ID).SingleOrDefault().Reservations.Add(reservation);
            DB.SaveChanges();
        }
    }
}
