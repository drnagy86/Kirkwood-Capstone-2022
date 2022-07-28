using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class ParkingLotManagerTests
    {

        private IParkingLotManager _parkingLotManager = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test initializer
        /// 
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _parkingLotManager = new ParkingLotManager(new ParkingLotAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Returns Parking Lot View model correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestReturnParkingLotVMForLocationReturnsCorrectList()
        {
            // arrange
            const int locationID = 100000;
            const int expectedCount = 3;
            List<ParkingLotVM> actualParkingLots = null;
            int acutalCount = 0;

            // act
            actualParkingLots = _parkingLotManager.RetrieveParkingLotByLocationID(locationID);
            acutalCount = actualParkingLots.Count;

            // assert
            Assert.AreEqual(expectedCount, acutalCount);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Returns 0 Parking Lot View models
        /// 
        /// </summary>
        [TestMethod]
        public void TestReturnParkingLotVMForLocationReturnsEmptyListForLocationID()
        {
            // arrange
            const int locationID = 100099;
            const int expectedCount = 0;
            List<ParkingLotVM> actualParkingLots = null;
            int acutalCount = 0;

            // act
            actualParkingLots = _parkingLotManager.RetrieveParkingLotByLocationID(locationID);
            acutalCount = actualParkingLots.Count;

            // assert
            Assert.AreEqual(expectedCount, acutalCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Tests to see if create parking lot returns an id
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateParkingLotReturnsLotID()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "Test",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };
            const int expectedLotID = 100005;
            int actualLotID;


            // act
            actualLotID = _parkingLotManager.CreateParkingLot(parking);

            // assert
            Assert.AreEqual(expectedLotID, actualLotID);

        }
        
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if there is not location id
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNoLocationID()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                // No LocationID
                Name = "Test",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if there is not a parking lot name
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNoName()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if the parking lot name is too long
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNameTooLong()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Tests to see if a user can edit a parking lot
        /// 
        /// </summary>
        [TestMethod]
        public void TestUserCanEditParkingLot()
        {
            // arrange
            const int userID = 100000;
            const bool expected = true;
            bool actual;


            // act
            actual = _parkingLotManager.UserCanEditParkingLot(userID);

            // assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Tests to see if a user can edit a parking lot
        /// 
        /// </summary>
        [TestMethod]
        public void TestUserCanEditParkingLotReturnsFalse()
        {
            // arrange
            const int userID = 100001;
            const bool expected = false;
            bool actual;

            // act
            actual = _parkingLotManager.UserCanEditParkingLot(userID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Tests to see if a user can delete a parking lot
        /// 
        /// </summary>
        [TestMethod]
        public void TestDeleteParkingLotReturnsTrue()
        {
            // arrange
            const int lotID = 100000;
            const bool expected = true;
            bool actual;

            // act
            actual = _parkingLotManager.RemoveParkingLotByLotID(lotID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test that throws an error for a none existent lot id
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestDeleteParkingLotReturnsFalse()
        {
            // arrange
            const int lotID = 0;
            

            // act
            _parkingLotManager.RemoveParkingLotByLotID(lotID);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test that returns one row affected if update succeeds
        /// </summary>
        [TestMethod]
        public void TestUpdateParkingLotReturnsOneRowAffectedIfSuccessful()
        {
            // arrange
            const int expectedResult = 1;
            int actualResult;
            int lotID = 100000;

            ParkingLot oldParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null
            };

            ParkingLot newParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100001,
                Name = "Test Name",
                Description = "Test Description",
                ImageName = null
            };

            // act
            actualResult = _parkingLotManager.EditParkingLotByLotID(lotID, oldParkingLot, newParkingLot);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test Update Parking Lot fails with no Location ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateParkingLotFailsWithNoLocationID()
        {
            // arrange
            int lotID = 100000;

            ParkingLot oldParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null
            };

            ParkingLot newParkingLot = new ParkingLot()
            {
                LotID = lotID,
                Name = "Test Name",
                Description = "Test Description",
                ImageName = null
            };

            // act
            _parkingLotManager.EditParkingLotByLotID(lotID, oldParkingLot, newParkingLot);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test Update parking lot fails with no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateParkingLotFailsWithNoNameValue()
        {
            // arrange
            int lotID = 100000;

            ParkingLot oldParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null
            };

            ParkingLot newParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100001,
                Name = "",
                Description = "Test Description",
                ImageName = null
            };

            // act
            _parkingLotManager.EditParkingLotByLotID(lotID, oldParkingLot, newParkingLot);

            // assert
            // exception checking, nothing to do

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test Update Parking lot fails with too long of a name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateParkingLotFailsWithLongName()
        {
            // arrange
            int lotID = 100000;

            ParkingLot oldParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null
            };

            ParkingLot newParkingLot = new ParkingLot()
            {
                LotID = lotID,
                LocationID = 100001,
                Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAaaaaaaaaaaaaaaaaaaAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah",
                Description = "Test Description",
                ImageName = null
            };

            // act
            _parkingLotManager.EditParkingLotByLotID(lotID, oldParkingLot, newParkingLot);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Test that returns correct Parking lot with expected lot ID
        /// </summary>
        [TestMethod]
        public void TestSelectParkingLotByLotIDReturnsCorrectParkingLot()
        {
            // arrange
            ParkingLotVM parkingLot = null;
            const int expectedLotID = 100000;
            string expectedLotName = "Test Parking Lot A";
            string actualLotName = "";

            // act
            parkingLot = _parkingLotManager.RetrieveParkingLotByLotID(expectedLotID);
            actualLotName = parkingLot.Name;

            // assert
            Assert.AreEqual(expectedLotName, actualLotName);

        }

        [TestMethod]
        public void TestSelectParkingLotByLotIDReturnsNullWithBadLotID()
        {
            // arrange
            ParkingLotVM expected = null;
            ParkingLotVM parkingLot = null;
            const int badLotID = -1;

            // act
            parkingLot = _parkingLotManager.RetrieveParkingLotByLotID(badLotID);

            // assert
            Assert.AreEqual(expected, parkingLot);

        }
           
    }
}
