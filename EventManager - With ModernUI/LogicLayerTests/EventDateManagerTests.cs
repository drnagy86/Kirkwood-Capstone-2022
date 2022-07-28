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
    public class EventDateManagerTests
    {
        private IEventDateManager _eventDateManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventDateManager = new EventDateManager(new EventDateAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that returns true if a date was created for the event
        /// </summary>
        [TestMethod]
        public void TestCreateEvenDateReturnsTrueIfCreated()
        {
            // arrange
            EventDate eventDate = new EventDate()
            {
                EventDateID = DateTime.Now,
                EventID = 99,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(8),
                Active = true
            };
            const bool expected = true;
            bool actual;

            // act
            actual = _eventDateManager.CreateEventDate(eventDate);

            // assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that throws exception if duplicate date for event
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEvenDateThrowsExceptionIfDuplicateDateForEvent()
        {
            // arrange
            EventDate eventDate = new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };
                        
            // act
            _eventDateManager.CreateEventDate(eventDate);

            // assert
            // exception testing, nothing to assert

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that that show manager throws an application exception if the EventDateID is null
        /// 
        /// Updates:
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test now throw exception if the EventDateID field isn't set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventDateNoDateDateEventDateIDThrowsException()
        {
            // arrange            
            EventDate eventDate = new EventDate()
            {
                // EventDateID not set and is therefor null
                // Default is DateTime.Min 
                // Logic layer is checking for that time
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };

            // act
            _eventDateManager.CreateEventDate(eventDate);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id returns the correct amount of event dates
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByEventIDReturnsCorrectListOfDatesForEvent()
        {
            // arrange
            const int eventID = 1;
            const int expectedCount = 3;
            List<EventDate> eventDateList = null;
            int actualCount;

            // act
            eventDateList = _eventDateManager.RetrieveEventDatesByEventID(eventID);
            actualCount = eventDateList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id throws an exception if the event dates list is empty
        /// </summary>
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/02/06/2022
        /// 
        /// Description:
        /// Throwing an exception is not the desired behavior
        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestRetrieveEventDatesByEventIDThrowsExceptionForEmptyList()
        //{
        //    // arrange
        //    const int eventID = -1;


        //    // act
        //    _eventDateManager.RetrieveEventDatesByEventID(eventID);

        //    // assert
        //    // exception testing, nothing to assert

        //}


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id throws an exception if the event dates list is empty
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByEventIDReturnsEmptyListForEmptyList()
        {
            // arrange
            const int eventID = -1;
            const int expectedCount = 0;
            List<EventDate> eventDateList = null;
            int actualCount;

            // act
            eventDateList = _eventDateManager.RetrieveEventDatesByEventID(eventID);
            actualCount = eventDateList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns true if update successful
        /// </summary>
        [TestMethod]
        public void TestUpdateEventReturnsTrueIfUpdateSuccessful()
        {
            // arrange
            EventDate oldEventDate = new EventDate()
            {
                EventID = 1,
                EventDateID = new DateTime(2022, 01, 01),
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };

            EventDate newEventDate = new EventDate()
            {
                EventID = 1,
                EventDateID = new DateTime(2022, 02, 01),
                StartTime = new DateTime(2022, 01, 01, 9, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 21, 0, 0),
                Active = false
            };

            bool expected = true;
            bool actual;

            // act
            actual = _eventDateManager.UpdateEventDate(oldEventDate, newEventDate);

            // assert

            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        ///// Jace Pettinger
        ///// Created: 2022/02/08
        /////
        //// Update:
        //// Jace Pettinger
        //// Created: 2022/02/11
        //// 
        //// Description:
        //// test cannot run because eventDateID cannot be null. Throwing build error
        ////
        ///// Description:
        ///// Test that trys to update and throws exception if Event Date ID is null
        ///// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestUpdateEventThrowsExceptionIfEventDateIDNull()
        //{
        //    // arrange
        //    EventDate oldEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = new DateTime(2022, 01, 01),
        //        StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
        //        EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
        //        Active = true
        //    };

        //    EventDate newEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = null,
        //        StartTime = new DateTime(2022, 01, 01, 9, 0, 0),
        //        EndTime = new DateTime(2022, 01, 01, 21, 0, 0),
        //        Active = false
        //    };

        // act
        // _eventDateManager.UpdateEventDate(oldEventDate, newEventDate);

        // assert
        // nothing to do, exception thrown

        // }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        //// Update:
        //// Jace Pettinger
        //// Created: 2022/02/11
        //// 
        //// Description:
        //// test cannot run because StartTime cannot be null. Throwing build errorr
        ///
        /// Description:
        /// Test that trys to update and throws exception if Start Time is null
        /// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestUpdateEventThrowsExceptionIfStartTimeNull()
        //{
        //    // arrange
        //    EventDate oldEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = new DateTime(2022, 01, 01),
        //        StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
        //        EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
        //        Active = true
        //    };

        //    EventDate newEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = new DateTime(2022, 02, 01),
        //        StartTime = null,
        //        EndTime = new DateTime(2022, 01, 01, 21, 0, 0),
        //        Active = false
        //    };

        //    // act
        //    _eventDateManager.UpdateEventDate(oldEventDate, newEventDate);

        //    // assert
        //    // nothing to do, exception thrown

        //}

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        //// Update:
        //// Jace Pettinger
        //// Created: 2022/02/11
        //// 
        //// Description:
        //// test cannot run because EndTime cannot be null. Throwing build errorr
        /// Description:
        /// Test that trys to update and throws exception if End Time is null
        /// </summary>
        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestUpdateEventThrowsExceptionIfEndTimeNull()
        //{
        //    // arrange
        //    EventDate oldEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = new DateTime(2022, 01, 01),
        //        StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
        //        EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
        //        Active = true
        //    };

        //    EventDate newEventDate = new EventDate()
        //    {
        //        EventID = 1,
        //        EventDateID = new DateTime(2022, 02, 01),
        //        StartTime = new DateTime(2022, 01, 01, 9, 0, 0),
        //        EndTime = null,
        //        Active = false
        //    };

        //    // act
        //    _eventDateManager.UpdateEventDate(oldEventDate, newEventDate);

        //    // assert
        //    // nothing to do, exception thrown

        //}

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that trys to update and throws exception if End Time beofre start time
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsExceptionIfEndTimeBeforeStartTime()
        {
            // arrange
            EventDate oldEventDate = new EventDate()
            {
                EventID = 1,
                EventDateID = new DateTime(2022, 01, 01),
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };

            EventDate newEventDate = new EventDate()
            {
                EventID = 1,
                EventDateID = new DateTime(2022, 02, 01),
                StartTime = new DateTime(2022, 01, 01, 9, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 8, 0, 0),
                Active = false
            };

            // act
            _eventDateManager.UpdateEventDate(oldEventDate, newEventDate);

            // assert
            // nothing to do, exception thrown

        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Test that retrieves event dates by locationID and tests the count retrieved
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByLocationIDReturnsCorrectAmount()
        {
            // arrange
            const int locationID = 100000;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _eventDateManager.RetrieveEventDatesByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Test that retrieves event dates by userID and date and tests the count retrieved
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByUserIDAndDateReturnsCorrectAmount()
        {
            // arrange
            const int userID = 999999;
            DateTime eventDate = new DateTime(2022, 01, 02);
            const int expectedCount = 2;
            int actualCount;

            // act
            actualCount = _eventDateManager.RetrieveEventDatesByUserIDAndDate(userID, eventDate).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Test that retrieves event dates by userID and date and tests the count retrieved
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByUserIDAndDateWithBadDateReturnsCorrectAmount()
        {
            // arrange
            const int userID = 999999;
            DateTime eventDate = new DateTime(1999, 01, 02);
            const int expectedCount = 0;
            int actualCount;

            // act
            actualCount = _eventDateManager.RetrieveEventDatesByUserIDAndDate(userID, eventDate).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
