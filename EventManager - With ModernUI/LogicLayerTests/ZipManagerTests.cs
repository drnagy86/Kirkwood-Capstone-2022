using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class ZipManagerTests
    {
        private IZipManager _zipManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _zipManager = new ZipManager(new ZipAccessorFake());
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Description:
        /// returns list of 5 zips when all are asked for.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllZipCodesReturnsFive()
        {
            // arrange
            const int expectedAmount = 5;
            int actual = 0;

            // act
            actual = _zipManager.RetrieveAllZIPCodes().Count;

            // assert

            Assert.AreEqual(expectedAmount, actual);

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Description: 
        /// Returns the correct zip when sent a correct code
        /// </summary>
        [TestMethod]
        public void TestRetrieveCityAndStateReturnsZip()
        {
            // arrange
            Zip expectedZip = new Zip()
            {
                ZIPCode = "11111",
                City = "Fake City 1",
                State = "Fake State"
            };
            Zip result = new Zip();

            // act
            result = _zipManager.RetrieveCityandStateByZIPCode("11111");

            // assert

            Assert.AreEqual(expectedZip.ToString(), result.ToString());
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Description:
        /// Returns null if zipcode does not exist.
        /// </summary>
        [TestMethod]
        public void RetrieveCityAndStateReturnsNullIfZipDoesNotExist()
        {
            const string zipCode = "21111";

            Assert.IsNull(_zipManager.RetrieveCityandStateByZIPCode(zipCode));
        }
    }
}
