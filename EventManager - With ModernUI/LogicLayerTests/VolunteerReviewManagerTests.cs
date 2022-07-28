using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using LogicLayerInterfaces;
using DataObjects;
using LogicLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/10
    /// 
    /// Description:
    /// The VolunteerReviewManagerTests test class for all Volunteer Review tests
    /// </summary>
    [TestClass]
    public class VolunteerReviewManagerTests
    {
        IVolunteerReviewManager _volunteerReviewManager = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The test initializer 
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerReviewManager = new VolunteerReviewManager(new VolunteerReviewAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test method which checks to see if the passed in volunteerID returns the correct amount of
        /// reviews
        /// </summary>
        [TestMethod]
        public void TestRetrieveVolunteerReviewsByVolunteerIDReturnsCorrectAmount()
        {
            const int volunteerID = 999999;
            const int expectedCount = 2;
            int actualCount;

            actualCount = _volunteerReviewManager.RetrieveVolunteerReviewsByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test method which checks to see if the bad volunteerID passed in returns the correct amount of
        /// reviews
        /// </summary>
        [TestMethod]
        public void TestRetrieveVolunteerReviewsByVolunteerIDWithBadVolunteerIDReturnsCorrectAmount()
        {
            const int volunteerID = 1001010;
            const int expectedCount = 0;
            int actualCount;

            actualCount = _volunteerReviewManager.RetrieveVolunteerReviewsByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test to make sure that CreateVolunteerReview returns the correct rowsAffected
        /// </summary>
        [TestMethod]
        public void TestCreateVolunteerReviewReturnsCorrectRowsAffected()
        {
            // arrange
            const int expectedRowsAffected = 1;
            Reviews review = new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100021,
                UserID = 999999,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 2,
                Review = "Awful :(",
                DateCreated = DateTime.Now,
                Active = true
            };
            int actualRowsAffected;

            // act
            actualRowsAffected = _volunteerReviewManager.CreateVolunteerReview(review);

            // assert
            Assert.AreEqual(expectedRowsAffected, actualRowsAffected);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test to make sure that CreateVolunteerReview returns the correct rowsAffected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateVolunteerReviewFailsWithBadVolunteerID()
        {
            // arrange
            const int expectedRowsAffected = 1;
            Reviews review = new Reviews()
            {
                ForeignID = -1,
                ReviewID = 100021,
                UserID = 999999,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 2,
                Review = "Awful :(",
                DateCreated = DateTime.Now,
                Active = true
            };
            int actualRowsAffected;

            // act
            actualRowsAffected = _volunteerReviewManager.CreateVolunteerReview(review);

            // assert
            // nothing to do here, exception checking
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test to make sure that CreateVolunteerReview throws error for rating below 1
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateVolunteerReviewFailsWithBadRatingMin()
        {
            // arrange
            const int expectedRowsAffected = 1;
            Reviews review = new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100021,
                UserID = 999999,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 0,
                Review = "Awful :(",
                DateCreated = DateTime.Now,
                Active = true
            };
            int actualRowsAffected;

            // act
            actualRowsAffected = _volunteerReviewManager.CreateVolunteerReview(review);

            // assert
            // nothing to do here, exception checking
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test to make sure that CreateVolunteerReview throws error for rating above 5
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateVolunteerReviewFailsWithBadRatingMax()
        {
            // arrange
            const int expectedRowsAffected = 1;
            Reviews review = new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100021,
                UserID = 999999,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 6,
                Review = "Awful :(",
                DateCreated = DateTime.Now,
                Active = true
            };
            int actualRowsAffected;

            // act
            actualRowsAffected = _volunteerReviewManager.CreateVolunteerReview(review);

            // assert
            // nothing to do here, exception checking
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test to make sure that CreateSupplierReview throws error for review longer than
        ///  3000 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateVolunteerReviewFailsWithLongReview()
        {
            // arrange
            const int expectedRowsAffected = 1;
            Reviews review = new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100021,
                UserID = 999999,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 1,
                Review = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean ut eros et nisl sagittis vestibulum. Nullam nulla eros, ultricies sit amet, nonummy id, imperdiet feugiat, pede. Sed lectus. Donec mollis hendrerit risus. Phasellus nec sem in justo pellentesque facilisis. Etiam imperdiet imperdiet orci. Nunc nec neque. Phasellus leo dolor, tempus non, auctor et, hendrerit quis, nisi. Curabitur ligula sapien, tincidunt non, euismod vitae, posuere imperdiet, leo. Maecenas malesuada. Praesent congue erat at massa. Sed cursus turpis vitae tortor. Donec posuere vulputate arcu. Phasellus accumsan cursus velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed aliquam, nisi quis porttitor congue, elit erat euismod orci, ac placerat dolor lectus quis orci. Phasellus consectetuer vestibulum elit. Aenean tellus metus, bibendum sed, posuere ac, mattis non, nunc. Vestibulum fringilla pede sit amet augue. In turpis. Pellentesque posuere. Praesent turpis. Aenean posuere, tort",
                DateCreated = DateTime.Now,
                Active = true
            };
            int actualRowsAffected;

            // act
            actualRowsAffected = _volunteerReviewManager.CreateVolunteerReview(review);

            // assert
            // nothing to do here, exception checking
        }
    }
}
