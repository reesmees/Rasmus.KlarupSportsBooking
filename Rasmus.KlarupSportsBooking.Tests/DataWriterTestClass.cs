using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rasmus.KlarupSportsBooking.Business;

namespace Rasmus.KlarupSportsBooking.Tests
{
    [TestClass]
    public class DataWriterTestClass
    {
        public DataHandler handler = new DataHandler();

        [TestMethod]
        public void CreateAddressTest()
        {
            int addressCount = handler.DB.Addresses.Count();
            string testCity = "TestCity";
            int testZipCode = 10;
            int testFloor = 0;
            int testHouseNumber = 1;
            string testStreetName = "TestStreet";

            handler.Writer.CreateAddress(testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(addressCount + 1, handler.DB.Addresses.Count());
        }

        [TestMethod]
        public void CreateEmailTest()
        {
            int emailCount = handler.DB.E_mails.Count();
            string testEmail = "testEmail";

            handler.Writer.CreateEmail(testEmail);

            Assert.AreEqual(emailCount + 1, handler.DB.E_mails.Count());
        }

        [TestMethod]
        public void CreateUnionWithExistingEmailAndAddressTest()
        {
            int unionCount = handler.DB.Unions.Count();
            int emailCount = handler.DB.E_mails.Count();
            int addressCount = handler.DB.Addresses.Count();
            string testCity = "TestCity";
            int testZipCode = 10;
            int testFloor = 0;
            int testHouseNumber = 1;
            string testStreetName = "TestStreet";
            string testEmail = "testEmail";
            string testName = "TestUnionName";

            handler.Writer.CreateUnion(testName, testEmail, testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(unionCount + 1, handler.DB.Unions.Count());
            Assert.AreEqual(emailCount, handler.DB.E_mails.Count());
            Assert.AreEqual(addressCount, handler.DB.Addresses.Count());
        }
    }
}
