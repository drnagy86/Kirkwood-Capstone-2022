using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{

    [TestClass]
    public class EntranceManagerTests
    {
        private IEntranceManager _entranceManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _entranceManager = new EntranceManager(new EntranceAccessorFake());
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/02/27
        /// 
        /// Description:
        /// Test that returns number of rows added, should be one
        /// </summary>
        [TestMethod]
        public void TestCreateEntranceReturnsOneIfCreated()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "Test Description";
            const int expected = 1;
            int acutal = 0;

            // act
            acutal = _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert

            Assert.AreEqual(expected, acutal);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if there is no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNoName()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "";
            const string description = "Test Description";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if there is no description
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if name is over 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNameOver100Characters()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "fhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwa";
            const string description = "Test Description";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if description is over 255 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfDescriptionOver255Characters()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "fhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwafhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwafhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwa";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Test method that returns the correct list of entrance(s) for a location
        /// </summary>
        [TestMethod]
        public void TestRetrieveEntranceByLocationIDReturnsCorrectList()
        {
            // arrange
            int locationID = 100000;
            const int expectedResult = 2;
            int actualResult;

            // act
            actualResult = _entranceManager.RetrieveEntranceByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test method that returns true if it is updated successfully
        /// </summary>
        [TestMethod]
        public void TestUpdateEntranceReturnsTrueIfSuccessful()
        {
            // arrange
            Entrance oldEntrance = new Entrance()
            {
                EntranceID = 100000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            };

            Entrance newEntrance = new Entrance()
            {
                EntranceID = 100000,
                EntranceName = "Test Entrance",
                Description = "Description"
            };

            bool expected = true;
            bool actual;

            // act
            actual = _entranceManager.UpdateEntrance(oldEntrance, newEntrance);

            // assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test method that throws exception if update has no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEntranceThrowsApplicationExceptionIfNoName()
        {
            // arrange
            Entrance oldEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            };

            Entrance newEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "",
                Description = "Description"
            };

            // act
            _entranceManager.UpdateEntrance(oldEntrance, newEntrance);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test method that throws exception if update has a name over 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEntranceThrowsApplicationExceptionIfNameOver100Characters()
        {
            // arrange
            Entrance oldEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            };

            Entrance newEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "hfasjldfhjdslahfuidsahfjkdshafjilhdsuialfheuisahfjdsklahfldshaflhduslahfsjkdafhjskdlahfuidshaukjfeblafkndhsajkl",
                Description = "Description"
            };

            // act
            _entranceManager.UpdateEntrance(oldEntrance, newEntrance);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test method that throws exception if update has no description
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEntranceThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            Entrance oldEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            };

            Entrance newEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance",
                Description = ""
            };

            // act
            _entranceManager.UpdateEntrance(oldEntrance, newEntrance);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test method that throws exception if update description is over 255 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEntranceThrowsApplicationExceptionIfDescriptionOver255Characters()
        {
            // arrange
            Entrance oldEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            };

            Entrance newEntrance = new Entrance()
            {
                EntranceID = 1000000,
                EntranceName = "Test Entrance 1",
                Description = "fdshajklfhdsuailhfuidsahlfjkdshjskdlhfdskalhfjkdslahfudslahfjkdslafhjksdlahfjkdslahfjkdsafhljkdhueilashfjkdlshafjkdlhsaufihldasjlkhfjdkslahfjkdslahfudsilahfejklafhjkdashlfjkdhsafjklhdsajkflhdsjkalfhsmanfdmsafhjldhabfjiehaulfhjdskafhjdhsuaifnejkalfdhsljfjdsahfuieoawhjfdsjalkfhdsialhfuegabjklfgsulaifhjsaldhfjdslahfuilshafjelshajfhsldafheiwahfdsjkalhfuieslahfjelsahdueilwhafesjalkhfues"
            };

            // act
            _entranceManager.UpdateEntrance(oldEntrance, newEntrance);

            // assert
            // nothing to assert, exception testing
        }
        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/06
        /// 
        /// Description:
        /// Test method that removes the correct entrance 
        /// </summary>
        [TestMethod]
        public void TestRemoveEntranceByEntranceIDRemovesCorrectEntrance()
        {
            // arrange
            const int entranceID = 100000;
            const int expected = 1;
            // act
            int actual = _entranceManager.RemoveEntranceByEntranceID(entranceID);
            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/06
        /// 
        /// Description:
        /// Test method that fails if invalid entranceID if attempted to be removed
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestRemoveEntranceByEntranceIDRemovesFailsWithInvalidEntranceID()
        {
            // arrange
            const int entranceID = 10;
            // act
            int actual = _entranceManager.RemoveEntranceByEntranceID(entranceID);
            // assert
        }
    }
}
