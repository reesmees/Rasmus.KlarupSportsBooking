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
            int testHouseNumber = addressCount + 1;
            string testStreetName = "TestStreet";

            handler.Writer.CreateAddress(testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(addressCount + 1, handler.DB.Addresses.Count());
        }

        [TestMethod]
        public void CreateEmailTest()
        {
            int emailCount = handler.DB.E_mails.Count();
            string testEmail = $"testEmail{emailCount+1}";

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
            string testName = $"TestUnionName{unionCount+1}";
            string testUsername = $"TestUsername{unionCount+1}";
            string testPassword = $"TestPassword{unionCount+1}";
            

            handler.Writer.CreateUnion(testName, testUsername, testPassword, testEmail, testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(unionCount + 1, handler.DB.Unions.Count());
            Assert.AreEqual(emailCount, handler.DB.E_mails.Count());
            Assert.AreEqual(addressCount, handler.DB.Addresses.Count());
        }

        [TestMethod]
        public void CreateUnionWithExistingEmailAndNewAddressTest()
        {
            int unionCount = handler.DB.Unions.Count();
            int emailCount = handler.DB.E_mails.Count();
            int addressCount = handler.DB.Addresses.Count();
            string testCity = "TestCity";
            int testZipCode = 10;
            int testFloor = 0;
            int testHouseNumber = addressCount + 1;
            string testStreetName = "TestStreet";
            string testEmail = "testEmail";
            string testName = $"TestUnionName{unionCount + 1}";
            string testUsername = $"TestUsername{unionCount + 1}";
            string testPassword = $"TestPassword{unionCount + 1}";


            handler.Writer.CreateUnion(testName, testUsername, testPassword, testEmail, testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(unionCount + 1, handler.DB.Unions.Count());
            Assert.AreEqual(emailCount, handler.DB.E_mails.Count());
            Assert.AreEqual(addressCount + 1, handler.DB.Addresses.Count());
        }

        [TestMethod]
        public void CreateUnionWithNewEmailAndExistingAddressTest()
        {
            int unionCount = handler.DB.Unions.Count();
            int emailCount = handler.DB.E_mails.Count();
            int addressCount = handler.DB.Addresses.Count();
            string testCity = "TestCity";
            int testZipCode = 10;
            int testFloor = 0;
            int testHouseNumber = 1;
            string testStreetName = "TestStreet";
            string testEmail = $"testEmail{emailCount+1}";
            string testName = $"TestUnionName{unionCount + 1}";
            string testUsername = $"TestUsername{unionCount + 1}";
            string testPassword = $"TestPassword{unionCount + 1}";


            handler.Writer.CreateUnion(testName, testUsername, testPassword, testEmail, testStreetName, testHouseNumber, testFloor, testZipCode, testCity);

            Assert.AreEqual(unionCount + 1, handler.DB.Unions.Count());
            Assert.AreEqual(emailCount + 1, handler.DB.E_mails.Count());
            Assert.AreEqual(addressCount, handler.DB.Addresses.Count());
        }

        [TestMethod]
        public void CreateAdministratorWithNewEmailTest()
        {
            int administratorCount = handler.DB.Administrators.Count();
            int emailCount = handler.DB.E_mails.Count();
            string testName = $"Test Name{administratorCount + 1}";
            string testPassword = $"TestPassword{administratorCount+1}";
            string testEmail = $"TestEmail{emailCount + 1}";

            handler.Writer.CreateAdministrator(testEmail, testName, testPassword);

            Assert.AreEqual(administratorCount + 1, handler.DB.Administrators.Count());
            Assert.AreEqual(emailCount + 1, handler.DB.E_mails.Count());
        }
    }
}
