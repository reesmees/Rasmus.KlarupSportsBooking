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
    }
}
