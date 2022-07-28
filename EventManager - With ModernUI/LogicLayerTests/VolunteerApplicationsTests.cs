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
    public class VolunteerApplicationsTests
    {
        private IVolunteerApplicationsManager _volunteerApplicationsManager = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Sets up manager to use fake data accessor
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerApplicationsManager = new VolunteerApplicationsManager(new VolunteerApplicationsAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Successfully creates a new volunte
        /// </summary>
        [TestMethod]
        public void TestAddVolunteerAndAddAvailabilitySuccess()
        {
            // add volunteer, set availability
            // arrange
            const int userID = 100000;
            const bool expectedResult = true;
            bool actualResult;
            Availability availability = new Availability()
            {
                TimeStart = DateTime.Now,
                TimeEnd = DateTime.Now.AddHours(1),
                Sunday = true,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = false
            };

            // act
            actualResult = _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Throws exception if no user
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVolunteerAndAddAvailabilityThrowsExceptionIfNoUser()
        {
            // arrange
            const int userID = 1;
            Availability availability = new Availability();

            // act
            _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Throws exception if no days of the week were selected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVolunteerAndAddAvailabilityThrowsExceptionIfNoDayOfWeek()
        {
            // arrange
            const int userID = 100000;
            Availability availability = new Availability() 
            {
                TimeStart = DateTime.Now,
                TimeEnd = DateTime.Now.AddHours(1),
                Sunday = false,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = false
            };

            // act
            _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            // nothing to assert, exception testing

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Throws exception if no start time
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVolunteerAndAddAvailabilityThrowsExceptionIfNoStartTime()
        {
            // arrange
            const int userID = 100000;
            Availability availability = new Availability()
            {
                TimeStart = null,
                TimeEnd = DateTime.Now,
                Sunday = true,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = false
            };

            // act
            _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Throws exception if no end time
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVolunteerAndAddAvailabilityThrowsExceptionIfNoEndTime()
        {
            // arrange
            const int userID = 100000;
            Availability availability = new Availability()
            {
                TimeStart = DateTime.Now,
                TimeEnd = null,
                Sunday = true,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = false
            };

            // act
            _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Throws exception if start time is before end time
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddVolunteerAndAddAvailabilityThrowsExceptionIfStartTimeBeforeEndTime()
        {
            // arrange
            const int userID = 100000;
            Availability availability = new Availability()
            {
                TimeStart = DateTime.Now.AddHours(1),
                TimeEnd = DateTime.Now,
                Sunday = true,
                Monday = false,
                Tuesday = false,
                Wednesday = false,
                Thursday = false,
                Friday = false,
                Saturday = false
            };

            // act
            _volunteerApplicationsManager.CreateVolunteerApplication(userID, availability);

            // assert
            // nothing to assert, exception testing

        }
    }
}
