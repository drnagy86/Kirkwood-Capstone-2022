using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class VolunteerRequestManagerTests
    {
        private IVolunteerRequestManager _volunteerRequestManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerRequestManager = new VolunteerRequestManager(new VolunteerRequestAccessorFake());
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created 2022/01/28
        /// Description
        /// Recreation of Tests that check the request accessor
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllRequests()
        {
            const int expectedAmount = 5;
            int actualamount = 0;

            actualamount = _volunteerRequestManager.RetrieveVolunteerRequests(999999).Count;

            Assert.AreEqual(expectedAmount, actualamount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/28
        /// Description
        /// Returns the expected list with an existing volunteerID
        /// </summary>        
        [TestMethod]
        public void TestRetrieveAllRequestsForVolunteerByVolunteerIDReturnsCorrectList()
        {
            // arrange
            List<VolunteerRequestViewModel> expectedRequests = new List<VolunteerRequestViewModel>();
            expectedRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            });
            const int volunteerID = 999999;
            List<VolunteerRequestViewModel> actualRequests;

            // act
            actualRequests = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(volunteerID);

            // assert
            ReflectionHelper.AssertReflectiveEqualsEnumerable(expectedRequests, actualRequests);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/28
        /// Description
        /// Returns an empty list with a volunteerID with no requests
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllRequestsForVolunteerByVolunteerIDReturnsEmptyListWithBadVolunteerID()
        {
            // arrange
            const int expectedCount = 0;
            const int volunteerID = 1;
            int actualCount;

            // act
            actualCount = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(volunteerID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/28
        /// Description
        /// Returns an empty list with a volunteerID who's request has a false event response
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllRequestsForVolunteerByVolunteerIDReturnsEmptyListWithFalseEventResponse()
        {
            // arrange
            const int expectedCount = 0;
            const int volunteerID = 999995;
            int actualCount;

            // act
            actualCount = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(volunteerID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Edit Voluntter Request returns true because there is a volunteer request matching the old request
        /// </summary>
        [TestMethod]
        public void TestEditVolunteerRequestSucceedsWithValidOldRequest()
        {
            // arrange
            const bool expected = true;
            VolunteerRequestViewModel oldVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel newVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            bool actual;

            // act
            actual = _volunteerRequestManager.EditVolunteerRequest(oldVolunteerRequest, newVolunteerRequest);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Edit Voluntter Request returns false because there is not a volunteer request matching the old request
        /// </summary>
        [TestMethod]
        public void TestEditVolunteerRequestFailsWithBadRequestID()
        {
            // arrange
            const bool expected = false;
            VolunteerRequestViewModel oldVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel newVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            bool actual;

            // act
            actual = _volunteerRequestManager.EditVolunteerRequest(oldVolunteerRequest, newVolunteerRequest);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Edit Voluntter Request returns false because there is not a volunteer request matching the old request
        /// </summary>
        [TestMethod]
        public void TestEditVolunteerRequestFailsWithBadOldVolunteerResponse()
        {
            // arrange
            const bool expected = false;
            VolunteerRequestViewModel oldVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel newVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            bool actual;

            // act
            actual = _volunteerRequestManager.EditVolunteerRequest(oldVolunteerRequest, newVolunteerRequest);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Edit Voluntter Request returns false because there is not a volunteer request matching the old request
        /// </summary>
        [TestMethod]
        public void TestEditVolunteerRequestFailsWithBadOldEventResponse()
        {
            // arrange
            const bool expected = false;
            VolunteerRequestViewModel oldVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = false,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel newVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            bool actual;

            // act
            actual = _volunteerRequestManager.EditVolunteerRequest(oldVolunteerRequest, newVolunteerRequest);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Edit Volunteer Request returns false because there is not a volunteer request matching the old request
        /// </summary>
        [TestMethod]
        public void TestEditVolunteerRequestFailsWithBadEventResponse()
        {
            // arrange
            const bool expected = false;
            VolunteerRequestViewModel oldVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = false,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel newVolunteerRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            bool actual;

            // act
            actual = _volunteerRequestManager.EditVolunteerRequest(oldVolunteerRequest, newVolunteerRequest);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// Description
        /// Retrieves the request with the correct id
        /// </summary>
        [TestMethod]
        public void TestRetrieveRequestByRequestIDSucceedsWithCorrectID()
        {
            // arrange
            const int requestID = 999999;
            VolunteerRequestViewModel expectedRequest = new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            };
            VolunteerRequestViewModel actualRequest;

            // act
            actualRequest = _volunteerRequestManager.RetrieveRequestByRequestID(requestID);

            // assert
            ReflectionHelper.AssertReflectiveEquals(expectedRequest, actualRequest);

        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/04/13
        /// Description
        /// Throws an error because the request ID is invalid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveRequestByRequestIDFailsWithInvalidID()
        {
            // arrange
            const int requestID = 1;
            VolunteerRequestViewModel actualRequest;

            // act
            actualRequest = _volunteerRequestManager.RetrieveRequestByRequestID(requestID);

            // assert
            //nothing to do here; error checking
        }
    }
}
