using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/09
    /// 
    /// Test class for UserImageManager
    /// </summary>
    [TestClass]
    public class UserImageManagerTests
    {
        IUserImageManager _userImageManager = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// The test initializer
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _userImageManager = new UserImageManager(new UserImageAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveUserImagesByUserIDReturnsCorrectAmountOfImages() 
        {
            const int userID = 999999;
            const int expectedCount = 1;
            int actualCount;

            actualCount = _userImageManager.RetrieveUserImagesByUserID(userID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveUserImagesByUserIDWithBadUserIDReturnsCorrectAmountOfImages()
        {
            const int userID = 100000000;
            const int expectedCount = 0;
            int actualCount;

            actualCount = _userImageManager.RetrieveUserImagesByUserID(userID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
