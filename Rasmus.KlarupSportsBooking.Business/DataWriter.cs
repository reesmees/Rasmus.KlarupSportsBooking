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


        public void CreateUnion(string name, string email, string streetName, int houseNumber, int floor, int zipCode, string city)
        {
            if (!DB.Addresses.Any(a => a.City == city && a.StreetName == streetName && a.ZipCode == zipCode && a.Floor == floor && a.HouseNumber == houseNumber))
                CreateAddress(streetName, houseNumber, floor, zipCode, city);
            if (!DB.E_mails.Any(e => e.E_mailAddress == email))
                CreateEmail(email);
            Unions unions = new Unions { UnionName = name };
            DB.E_mails.Where(e => e.E_mailAddress == email).SingleOrDefault().Unions.Add(unions);
            DB.Addresses.Where(a => a.City == city && a.StreetName == streetName && a.ZipCode == zipCode && a.Floor == floor && a.HouseNumber == houseNumber).SingleOrDefault().Unions.Add(unions);
            DB.SaveChanges();
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
                DB.Addresses.Add(new Addresses { StreetName = streetName, City = city, Floor = floor, HouseNumber = houseNumber, ZipCode = zipCode });
                DB.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Adressen eksisterer allerede i databasen");
            }
        }
    }
}
