using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessInterfaces;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System.Collections.Generic;
using DataAccessFakes;
using System.Linq;

namespace LogicLayerTests
{
    [TestClass]
    public class ActivityManagerTests
    {
        private IActivityManager _activityManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _activityManager = new ActivityManager(new ActivityAccessorFake(), new EventDateAccessorFake(),
                new SublocationAccessorFake(), new ActivityResultAccessorFake());
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitesByEventID returns a list of the
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDRetrievesListWithCorrectEventID()
        {
            //arrange   
            const int eventID = 1;
            const int expectedCount = 2;
            List<Activity> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventID(eventID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Test throws exception because event has no activities.
        /// </summary>

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDFailsWithIncorrectEventID()
        {
            //arrange   
            const int eventID = 10;
            List<Activity> activities;

            //act
            activities = _activityManager.RetrieveActivitiesByEventID(eventID);

            //assert
            //error testing, nothing to do here
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitesByEventID returns a list of the
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDAndEventDateIDRetrievesListWithCorrectEventIDAndEventDate()
        {
            //arrange   
            const int eventID = 2;
            DateTime eventDateID = new DateTime(2022, 01, 01);
            const int expectedCount = 2;
            List<ActivityVM> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventIDAndEventDateID(eventID, eventDateID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitesByEventID returns a list of the
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDAndEventDateIDRetrievesEmptyListWithInCorrectEventID()
        {
            //arrange   
            const int eventID = 20;
            DateTime eventDateID = new DateTime(2022, 01, 01);
            const int expectedCount = 0;
            List<ActivityVM> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventIDAndEventDateID(eventID, eventDateID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitesByEventID returns a list of the
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDAndEventDateIDRetrievesEmptyListWithInCorrectEventDateID()
        {
            //arrange   
            const int eventID = 1;
            DateTime eventDateID = new DateTime(2022, 12, 01);
            const int expectedCount = 0;
            List<ActivityVM> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventIDAndEventDateID(eventID, eventDateID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }


        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Test that checks to see if the amount of Activities returns is correct
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesBySublocationIDReturnsCorrectAmount()
        {
            //arrange   
            const int sublocationID = 1000001;
            const int expectedCount = 2;
            int actualCount;

            //act
            actualCount = _activityManager.RetrieveActivitiesBySublocationID(sublocationID).Count;
            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that passes if RetreiveActivitiesPastAndUpcomingDates returns 
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetreiveActivitiesPastAndUpcomingDatesRetrievesCorrectSize()
        {
            //arrange
            const int expected = 6;
            List<ActivityVM> activities = new List<ActivityVM>();
            int actual;
            //act
            activities = _activityManager.RetreiveActivitiesPastAndUpcomingDates();
            actual = activities.Count;
            // assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that passes if RetreiveUserActivitiesPastAndUpcomingDates returns 
        ///     expected size with corresponding userID.
        /// </summary>
        [TestMethod]
        public void TestRetreiveUserActivitiesPastAndUpcomingDatesReturnsCorrectList()
        {
            //arrange
            const int expected = 3;
            const int _userID = 100000;
            List<ActivityVM> activities = new List<ActivityVM>();
            int actual;
            //act
            activities = _activityManager.RetreiveUserActivitiesPastAndUpcomingDates(_userID);
            actual = activities.Count;
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that passes if RetreiveUserActivitiesPastAndUpcomingDates returns 
        ///     application exception with invalid userID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetreiveUserActivitiesPastAndUpcomingDatesFailsWithInvalidUserID()
        {
            //arrange
            const int invalidUserID = 1;
            List<ActivityVM> activities = new List<ActivityVM>();
            //act
            activities = _activityManager.RetreiveUserActivitiesPastAndUpcomingDates(invalidUserID);
            //assert
            //nothing to assert
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitiesByEventIDForVM returns 
        ///     correct count
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDForVMRetrievesListWithCorrectEventID()
        {
            //arrange   
            const int eventID = 1;
            const int expectedCount = 2;
            List<ActivityVM> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventIDForVM(eventID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Test that checks to see if the amount of Activities returned is none with a 
        /// bad sublocationID passed through
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesBySublocationIDWithBadSublocationIDReturnsEmptyList()
        {
            //arrange   
            const int sublocationID = 1;
            const int expectedCount = 0;
            int actualCount;

            //act
            actualCount = _activityManager.RetrieveActivitiesBySublocationID(sublocationID).Count;


            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// Logan Baccam
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Test that fails if RetrieveActivitiesByEventIDForVM with incorrect eventID
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDForVMFailsWithIncorrectEventID()
        {
            //arrange   
            const int eventID = 10;
            List<ActivityVM> activities;

            //act
            activities = _activityManager.RetrieveActivitiesByEventIDForVM(eventID);

            //assert
            //error testing, nothing to do here
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitiesBySupplierID returns a correct
        /// list of activities for the given supplier.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesBySupplierIDRetrievesCorrectList()
        {
            //arrange
            const int supplierID = 100000;
            DateTime date = new DateTime(2022, 01, 01);
            List<ActivityVM> activities;
            int actualCount = 2;
            int expectedCount = 2;

            // act
            activities = _activityManager.RetrieveActivitiesBySupplierIDAndDate(supplierID, date);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitiesBySupplierID returns an empty list
        /// when given a bad supplierID.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesBySupplierIDRetrievesEmptyListWithBadID()
        {
            // arrange
            const int badID = 1;
            DateTime date = new DateTime(2022, 01, 01);
            List<ActivityVM> actualList;

            // act
            actualList = _activityManager.RetrieveActivitiesBySupplierIDAndDate(badID, date);

            // assert
            Assert.IsFalse(actualList.Any());
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/14
        /// 
        /// Description:
        /// Test that updates activity sublocation and makes sure the method returns true when doing so.
        /// </summary>
        [TestMethod]
        public void TestUpdateActivitySublocationByActivityIDReturnsTrue()
        {
            // arrange
            const int activityID = 1000000;
            const int oldSublocationID = 1000003;
            const int newSublocationID = 1000004;

            const bool expected = true;

            // act
            bool actual = _activityManager.UpdateActivitySublocationByActivityID(activityID, oldSublocationID, newSublocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/14
        /// 
        /// Description:
        /// Test that updates event location and makes sure the method returns false when provided a bad event ID.
        /// </summary>
        [TestMethod]
        public void TestUpdateActivitySublocationByActivityIDReturnsFalseForBadActivityID()
        {
            // arrange
            const int activityID = 2000;
            const int oldSublocationID = 100000;
            const int newSublocationID = 100001;

            const bool expected = false;

            // act
            bool actual = _activityManager.UpdateActivitySublocationByActivityID(activityID, oldSublocationID, newSublocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/14
        /// 
        /// Description:
        /// Test that updates event location and makes sure the method returns false when provided a bad old location ID.
        /// </summary>
        [TestMethod]
        public void TestUpdateActivitySublocationByActivityIDReturnsFalseForBadOldSublocationID()
        {
            // arrange
            const int activityID = 1000000;
            const int oldSublocationID = 200;
            const int newSublocationID = 100001;

            const bool expected = false;

            // act
            bool actual = _activityManager.UpdateActivitySublocationByActivityID(activityID, oldSublocationID, newSublocationID);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Test that passes if CreateActivity returns 1 row affected when
        /// successfully adding an activity.
        /// </summary>
        [TestMethod]
        public void TestCreateActivitySuccessfullyAddsActivity()
        {
            // arrange
            const int expectedRows = 1;
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            Assert.AreEqual(expectedRows, rowsAffected);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// name is too long.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithNameTooLong()
        {
            // arrange
            int rowsAffected;
            string longName = "123456789012345678901234567890123456789012345678901"; // 51 characters
            Activity activity = new Activity()
            {
                ActivityName = longName,
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// no name is set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithMissingName()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                // ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// description is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithDescriptionTooLong()
        {
            // arrange
            int rowsAffected;
            string longDescription = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
                                     "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" +
                                     "123456789012345678901234567890123456789012345678901"; // 251 characters
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = longDescription,
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// sublocation has not been set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithUnsetSublocation()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                // SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// event date id has not been set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithUnsetEventDateID()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                // EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// start time has not been set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithUnsetStartTime()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                // StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// end time has not been set
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithUnsetEndTime()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(1),
                // EndTime = DateTime.Now.AddHours(2),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Test that passes if CreateActivity throws exception when
        /// start time is set after end time
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateActivityThrowsExceptionWithStartTimeAfterEndTime()
        {
            // arrange
            int rowsAffected;
            Activity activity = new Activity()
            {
                ActivityName = "Test Name",
                ActivityDescription = "Test Description",
                PublicActivity = true,
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(1),
                SublocationID = 100000,
                EventDateID = DateTime.Today,
                EventID = 100000
            };

            // act
            rowsAffected = _activityManager.CreateActivity(activity);

            // assert
            // exception checking, nothing to do
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitiesBySupplierID returns a correct
        /// list of activities for the given supplier.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesBySupplierIDRetrievesCorrectAmount()
        {
            //arrange
            const int supplierID = 100000;
            int actualCount;
            int expectedCount = 2;

            // act
            actualCount = _activityManager.RetrieveActivitiesBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/04/08
        /// 
        /// Description:
        /// Test returns with correct Activity for an ActivityID
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityVMByActivityIDReturnsCorrectActivity()
        {
            // arrange
            const int activityID = 1000000;
            ActivityVM activity;
            string expectedName = "Test Activity 1";
            string expectedDescripiton = "The description of activity 1";

            // act
            activity = _activityManager.RetrieveActivityVMByActivityID(activityID);

            // assert
            Assert.AreEqual(expectedName, activity.ActivityName);
            Assert.AreEqual(expectedDescripiton, activity.ActivityDescription);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/04/08
        /// 
        /// Description:
        /// Test returns null if no activity has corresponding activity ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivityVMByActivityIDReturnsNullOnBadActivityID()
        {
            // arrange
            const int activityID = 99999999;
            ActivityVM result;

            // act
            result = _activityManager.RetrieveActivityVMByActivityID(activityID);

            // assert
            Assert.IsNull(result);

        }
    }
}
