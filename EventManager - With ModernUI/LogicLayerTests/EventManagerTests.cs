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
    public class EventManagerTests
    {

        private IEventManager _eventManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventManager = new EventManager(new EventAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that returns number of rows added(should be 1)
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        public void TestCreateEventReturnsOneIfCreated()
        {
            // arrange
            const string eventName = "Test";
            const string eventDescription = "Test Description";
            const decimal totalBudget = 1000.00m;
            const int expected = 1;
            int acutal = 0;

            // act
            acutal = _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert

            Assert.AreEqual(expected, acutal);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if there is no name
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNoName()
        {
            // arrange
            const string eventName = "";
            const string eventDescription = "Test Description";
            const decimal totalBudget = 1000.00m;

            // act
            _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if name is too long
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNameOver50Char()
        {
            // arrange
            const string eventName = "jDWAAHKGh6r3JQwRW7IPVHDJunFb8b5tfgYfGz8vauaNJ2tM1z";
            const string eventDescription = "Test Description";
            const decimal totalBudget = 1000.00m;

            // act
            _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the description is an empty string or null
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            const string eventName = "Test Event";
            const string eventDescription = "";
            const decimal totalBudget = 1000.00m;

            // act
            _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert
            // nothing to assert, exception testing

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the description is too long, over 1000 char
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfDescriptionTooLong()
        {
            // arrange
            const string eventName = "Test Event";
            const string eventDescription = "CG7RqtCb0qMq3CUwXSTQvuOKIVZgUdUS33qHKpKqXwHhdAJx1pVLwRWvWd2Y24v1RGQjMqwGMpbMhavquvuyitARU2Omrm4gIPSWaS0TCpbo4oI83WKvWSW2qOPk9SFUK77x48NVmp0QbCts1KrbDx01v4g1iSwumHHNmg33vo6GpUKhU3j2iYnaXyncIGiVrRoqDCVUhU7qwwioIMBuYASWnsWMMsKVNROFlwQkzdDWS5zRDpiEnKXMcXIcfbm9VIKbWYh2j1uozqNgRcxv6DbxDgC9CyVAToCuYuURoBrfK3k5ClHIGAmpeHM6S9aIwDJ3rtesuprRrjd4K2t5ZrtuRsLO8ZtnQz2SrZntqBRJqjf9d5GGjvM2tfq5Tq94AS075HGUXg7da1swsTgj8zRB31TcW4jZ98rXlyIiwsvJn06UJWybWveN2NM9LGqOyd6jL0IzkXYMBhm5wN8vqxvpUPYayChPgEDITBr0WVahkd8Ev0SVn89finbKKSCTcNLWMijRBZ5lo7pOiLz2j7RTwRDjIxPECGK5efJcicLU8E4hAwmaX5AJbtxXLEF5m1mkPbSinzsS4Nl5YvP7lDkhjdtGK9DwfGmXAsXMSPY42r2cTBpbSptU0w9XVgMNWUe9V4Use0aPu5ZahBLjwFy3gDCOW7L7vh75HawP1I4BctBgHc6csf8Kdhq70LKtTvkEPO7vdnmaQXTfSugSRgzQ2JZiTAmRrnT40nK4X5whoi63g7PKrXAuqVsgOrUkWlgtmWRjXfdTRWRPMewPoUCFwe6DuJ22okqUiQ0t1ZlvqlfzhZfyya4GPsZeZD7Fs5203eEZq3dvhJWZ6BpRgrJ168yZNBfHBDFKVdyBd0epClPSG4A0O38RVDkiuiFtuYwBtSs7o8VOfhGcxm01XzsTWIbcbXUc6HX4qfGCfXxBe4OfeC6pobrTpL2o8G6DGcOYExHjHHCSKyOs9Y4LT36lkiCOCet2Pp0ALA8r";
            const decimal totalBudget = 1000.00m;

            // act
            _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Test that throws application exception if total budget is less than 0
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfTotalBudgetLessThanZero()
        {
            // arrange
            const string eventName = "Test";
            const string eventDescription = "Test Description";
            const decimal totalBudget = -10m;

            // act
            _eventManager.CreateEvent(eventName, eventDescription, totalBudget);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Test that returns list of active events (currently 4 in fake data)
        /// </summary>
        [TestMethod]
        public void TestRetrieveActiveEventsReturnsCorrectList()
        {
            // arrange
            const int expectedCount = 4;
            int actualCount;
            // act
            actualCount = (_eventManager.RetreieveActiveEvents().Count);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the event name for update is 
        /// an empty string or null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNoName()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "",
                EventDescription = "A description of test event 1",
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if event name for update is too long
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNameOver50Char()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "jDWAAHKGh6r3JQwRW7IPVHDJunFb8b5tfgYfGz8vauaNJ2tM1z",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if the description for update 
        /// is an empty string or null
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "",
                TotalBudget = 100.00m,
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if the description for update 
        /// is too long, over 1000 char
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfDescriptionTooLong()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "CG7RqtCb0qMq3CUwXSTQvuOKIVZgUdUS33qHKpKqXwHhdAJx1pVLwRWvWd2Y24v1RGQjMqwGMpbMhavquvuyitARU2Omrm4gIPSWaS0TCpbo4oI83WKvWSW2qOPk9SFUK77x48NVmp0QbCts1KrbDx01v4g1iSwumHHNmg33vo6GpUKhU3j2iYnaXyncIGiVrRoqDCVUhU7qwwioIMBuYASWnsWMMsKVNROFlwQkzdDWS5zRDpiEnKXMcXIcfbm9VIKbWYh2j1uozqNgRcxv6DbxDgC9CyVAToCuYuURoBrfK3k5ClHIGAmpeHM6S9aIwDJ3rtesuprRrjd4K2t5ZrtuRsLO8ZtnQz2SrZntqBRJqjf9d5GGjvM2tfq5Tq94AS075HGUXg7da1swsTgj8zRB31TcW4jZ98rXlyIiwsvJn06UJWybWveN2NM9LGqOyd6jL0IzkXYMBhm5wN8vqxvpUPYayChPgEDITBr0WVahkd8Ev0SVn89finbKKSCTcNLWMijRBZ5lo7pOiLz2j7RTwRDjIxPECGK5efJcicLU8E4hAwmaX5AJbtxXLEF5m1mkPbSinzsS4Nl5YvP7lDkhjdtGK9DwfGmXAsXMSPY42r2cTBpbSptU0w9XVgMNWUe9V4Use0aPu5ZahBLjwFy3gDCOW7L7vh75HawP1I4BctBgHc6csf8Kdhq70LKtTvkEPO7vdnmaQXTfSugSRgzQ2JZiTAmRrnT40nK4X5whoi63g7PKrXAuqVsgOrUkWlgtmWRjXfdTRWRPMewPoUCFwe6DuJ22okqUiQ0t1ZlvqlfzhZfyya4GPsZeZD7Fs5203eEZq3dvhJWZ6BpRgrJ168yZNBfHBDFKVdyBd0epClPSG4A0O38RVDkiuiFtuYwBtSs7o8VOfhGcxm01XzsTWIbcbXUc6HX4qfGCfXxBe4OfeC6pobrTpL2o8G6DGcOYExHjHHCSKyOs9Y4LT36lkiCOCet2Pp0ALA8r",
                TotalBudget = 1000.00m,
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Test that throws application exception if the budget for updating an event
        /// is less than zero
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfTotalBudgetIsLessThanZero()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = -1000.00m,
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that returns true if update successful
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        public void TestUpdateEventReturnsTrueIfUpdateSuccessful()
        {
            // arrange
            EventVM oldEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                TotalBudget = 1000.00m,
                Active = true
            };

            EventVM newEvent = new EventVM()
            {
                EventID = 1000000,
                EventName = "Test Name",
                EventDescription = "Description",
                TotalBudget = 1000.00m,
                Active = false
            };

            bool expected = true;
            bool actual;

            // act
            actual = _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that returns an event object and tests to see if it is the correct ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventByEventNameAndDescriptionReturnsCorrectEventID()
        {
            // arrange
            const string eventName = "Test Event 1";
            const string eventDescription = "A description of test event 1";
            const int expectedEventID = 1000000;
            Event actualEvent = null;
            int actualEventID;

            // act
            actualEvent = _eventManager.RetrieveEventByEventNameAndDescription(eventName, eventDescription);
            actualEventID = actualEvent.EventID;
            // assert

            Assert.AreEqual(expectedEventID, actualEventID);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that returns and event object and tests to see if it is the correct ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveEventByEventNameAndDescriptionThrowsExceptionWhenNotFound()
        {
            // arrange
            const string eventName = "Test Event xxxxxxxxxx";
            const string eventDescription = "A description of test event 1";
            
            // act
            _eventManager.RetrieveEventByEventNameAndDescription(eventName, eventDescription);
            
            // assert
            // notthing to assert, exception testing
            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Test that returns a list of event view objects for upcoming events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingEvents()
        {
            // arrange
            const int expectedCount = 3;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForUpcomingDates();
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Test that returns a list of event view objects for upcoming and past events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingAndPastEvents()
        {
            // arrange
            const int expectedCount = 4;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForUpcomingAndPastDates();
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Test that returns a list of event view objects for past events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingPastEvents()
        {
            // arrange
            const int expectedCount = 1;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForPastDates();
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for upcoming events for a user
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingEventsForUser()
        {
            // arrange
            const int userID = 100000;
            const int expectedCount = 3;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForUpcomingDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for upcoming events for a user with no events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingEventsForUserWithNoEvents()
        {
            // arrange
            const int userID = -1;
            const int expectedCount = 0;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForUpcomingDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for past events for a user
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMPastEventsForUser()
        {
            // arrange
            const int userID = 100001;
            const int expectedCount = 1;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForPastDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for upcoming events for a user with no events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMPastEventsForUserWithNoEvents()
        {
            // arrange
            const int userID = -1;
            const int expectedCount = 0;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForPastDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for past and upcoming events for a user
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMPastAndUpcomingEventsForUser()
        {
            // arrange
            const int userID = 100001;
            const int expectedCount = 3;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForPastAndUpcomingDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Test that returns a list of event view objects for past and upcoming events for a user with no events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMPastAndUpcomingEventsForUserWithNoEvents()
        {
            // arrange
            const int userID = -1;
            const int expectedCount = 0;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForPastAndUpcomingDatesForUser(userID);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Test that updates event location and makes sure the method returns true when doing so.
        /// </summary>
        [TestMethod]
        public void TestUpdateEventLocationByLocationIDReturnsTrue()
        {
            // arrange
            const int eventID = 1000000;
            const int oldLocationID = 100000;
            const int newLocationID = 1500000;

            const bool expected = true;

            // act
            bool actual = _eventManager.UpdateEventLocationByEventID(eventID, oldLocationID, newLocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Test that updates event location and makes sure the method returns false when provided a bad event ID.
        /// </summary>
        [TestMethod]
        public void TestUpdateEventLocationByLocationIDReturnsFalseForBadEventID()
        {
            // arrange
            const int eventID = 2000;
            const int oldLocationID = 100000;
            const int newLocationID = 1500000;

            const bool expected = false;

            // act
            bool actual = _eventManager.UpdateEventLocationByEventID(eventID, oldLocationID, newLocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Test that updates event location and makes sure the method returns false when provided a bad old location ID.
        /// </summary>
        [TestMethod]
        public void TestUpdateEventLocationByLocationIDReturnsFalseForBadOldLocationID()
        {
            // arrange
            const int eventID = 1000000;
            const int oldLocationID = 200;
            const int newLocationID = 1500000;

            const bool expected = false;

            // act
            bool actual = _eventManager.UpdateEventLocationByEventID(eventID, oldLocationID, newLocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that returns the event id
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// </summary>
        [TestMethod]
        public void TestCreateEventWithUserReturnsEventIDIfCreated()
        {
            // arrange
            const string eventName = "Test";
            const string eventDescription = "Test Description";
            const decimal totalBudget = 1000.00m;
            const int expectedEventID = 1000004;
            const int userID = 100000;
            int actualEventID = 0;

            // act            
            actualEventID = _eventManager.CreateEventReturnsEventID(eventName, eventDescription, totalBudget, userID );

            // assert
            Assert.AreEqual(expectedEventID, actualEventID);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that throws an application exception if there is no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventWithUserReturnsEventIDThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            const string eventName = "Test";
            const string eventDescription = null;
            const decimal budget = 1;
            const int userID = 100000;

            // act
            _eventManager.CreateEventReturnsEventID(eventName, eventDescription, budget, userID);


            // assert
            // nothing to assert, exception testing
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Test that returns true if the user can edit the event
        /// </summary>
        [TestMethod]
        public void TestEventEditPermissionsReturnsTrueIFUserCanEdit()
        {
            // arrange
            const int eventID = 100000;
            const int userID = 100000;
            const bool expected = true;
            bool actual;

            // act
            actual = _eventManager.CheckUserEditPermissionForEvent(eventID, userID);

            // assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Test that returns true if the user can edit the event
        /// </summary>
        [TestMethod]
        public void TestEventEditPermissionsReturnsFalseIfUserCannotEdit()
        {
            // arrange
            const int eventID = 100000;
            const int userID = 100002;
            const bool expected = false;
            bool actual;

            // act
            actual = _eventManager.CheckUserEditPermissionForEvent(eventID, userID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that an event has a location when calling RetrieveEventListForUpcomingDates()
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMUpcomingEventsHasLocation()
        {
            // arrange

            Location expectedLocation = new Location()
            {
                LocationID = 100004,
                UserID = 100000,
                Name = "Test Location 5 Inactive",
                Description = "Description of Inactive Test Location 5 goes here.",
                PricingInfo = "Pricing information for renting inactive Test Location 5 goes here.",
                Phone = "555-555-5555",
                Email = "testLocation5@locations.com",
                Address1 = "Test Location 5 Street",
                Address2 = "Apt 55",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                ImagePath = "http://imagehost.com/testlocation5.png",
                Active = false
            };


            List<EventVM> eventList = null;
            

            // act
            eventList = _eventManager.RetrieveEventListForUpcomingDates();
            Location actualLocation = eventList.Find(e => e.EventID == 100000).Location;

            // assert
            Assert.AreEqual(expectedLocation.LocationID, actualLocation.LocationID);
            Assert.AreEqual(expectedLocation.UserID, actualLocation.UserID);
            Assert.AreEqual(expectedLocation.Name, actualLocation.Name);
            Assert.AreEqual(expectedLocation.Description, actualLocation.Description);
            Assert.AreEqual(expectedLocation.PricingInfo, actualLocation.PricingInfo);
            Assert.AreEqual(expectedLocation.Phone, actualLocation.Phone);
            Assert.AreEqual(expectedLocation.Email, actualLocation.Email);
            Assert.AreEqual(expectedLocation.Address1, actualLocation.Address1);
            Assert.AreEqual(expectedLocation.Address2, actualLocation.Address2);
            Assert.AreEqual(expectedLocation.City, actualLocation.City);
            Assert.AreEqual(expectedLocation.State, actualLocation.State);
            Assert.AreEqual(expectedLocation.ZipCode, actualLocation.ZipCode);
            Assert.AreEqual(expectedLocation.ImagePath, actualLocation.ImagePath);
            Assert.AreEqual(expectedLocation.Active, actualLocation.Active);

            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Tests to make sure that the event managers for an event is returned
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventManagers()
        {
            // Arrange
            const int eventID = 100000;
            const int expectedCount = 1;
            int actualCount = 0;

            // Act
            List<User> actualUserList = _eventManager.RetrieveEventPlannersForEvent(eventID);
            actualCount = actualUserList.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Tests to make sure that the event managers for an event is returned
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveEventManagersThrowsExceptionWhenNoUserIsFound()
        {
            // Arrange
            const int eventID = 1000000;
            // Act
            _eventManager.RetrieveEventPlannersForEvent(eventID);            

            // Assert
            
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/06
        /// 
        /// Description:
        /// Test that returns a list of event view objects for a search criteria
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMForSearch()
        {
            // arrange
            const string search = "test";
            const int expectedCount = 4;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForSearch(search);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/06
        /// 
        /// Description:
        /// Test that returns a list of event view objects for a name, description, location that contains the number 4
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventVMForSearchSearchesForNumber4()
        {
            // arrange
            const string search = "4";
            const int expectedCount = 1;
            List<EventVM> actualList = null;
            int actualCount;

            // act
            actualList = _eventManager.RetrieveEventListForSearch(search);
            actualCount = actualList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/06
        /// 
        /// Description:
        /// Test that throws an error if the search word is longer than 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveEventVMForSearchThrowsExceptionIfTooLongOfSearch()
        {
            // arrange
            const string search = "1Bc7fBGBhzfcZ47naVEiSnZ0rlOjxxNSxINj72IwprJiQpLZlw1";

            // act
            _eventManager.RetrieveEventListForSearch(search);
            // assert
            
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/01
        /// Description:
        /// returns true if it can return the event
        /// </summary>
        [TestMethod]
        public void TestRetreieveEventByEventIDReturnsTrueIfEventExists()
        {
            //arrange
            const int eventID = 1000000;
            EventVM expectedEvent = new EventVM()
            {
                EventID = 1000000,
                LocationID = 100000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                TotalBudget = 1000.00m,
                Active = true
            };
            EventVM actualEvent;

            // act
            actualEvent = _eventManager.RetrieveEventByEventID(eventID);

            // assert
            Assert.AreEqual(expectedEvent.EventID, actualEvent.EventID);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/01
        /// Description:
        /// throws exception if event can't be found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveEventByEventIDThrowsExceptionWhenNotFound()
        {
            // arrange
            const int eventID = 2000000;
            EventVM actualEvent;

            // act
            actualEvent = _eventManager.RetrieveEventByEventID(eventID);

            // assert
            // no assert should throw error

        }
    }
}
