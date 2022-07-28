using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
/// <summary>
/// Vinayak Deshpande
/// Created: 2022/03/15
/// 
/// Description: Tests for the VolunteerNeedManager
/// </summary>
namespace LogicLayerTests
{
    [TestClass]
    public class VolunteerNeedManagerTests
    {
        private IVolunteerNeedManager _needManager = null;
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Test Setup
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _needManager = new VolunteerNeedManager(new VolunteerNeedAccessorFake());
        }
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Returns true if the need is created
        /// </summary>
        [TestMethod]
        public void TestInsertVolunteerNeedReturnsTrueIfCreated()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999996,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 0
            };
            bool expectedResult = true;
            bool actualresult;

            //act
            actualresult = _needManager.AddVolunteerNeed(need);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Returns true if total volunteers is changed.
        /// </summary>
        [TestMethod]
        public void TestUpdateVolunteerNeedReturnsTrue()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999996,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            };
            int numTotalVolunteers = 1;
            bool expectedResult = true;
            bool actualresult;

            //act
            actualresult = _needManager.UpdateVolunteerNeed(need, numTotalVolunteers);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Fails if the new numtotalvolunteers is out of parameter
        /// </summary>
        [TestMethod]
        public void TestUpdateVolunteerNeedReturnsFalseIfNumTotalVolunteersBad()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999996,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 0
            };
            bool expectedResult = false;
            bool actualresult;
            int numTotalVolunteers = 11;

            //act
            actualresult = _needManager.UpdateVolunteerNeed(need, numTotalVolunteers);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns true if numcurr increases
        /// </summary>
        [TestMethod]
        public void TestUpdateCurrVolunteersAddReturnsTrue()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999996,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 0
            };
            bool expectedResult = true;
            bool actualresult;


            //act
            actualresult = _needManager.UpdateCurrVolunteers(need, 1);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Returns false if currVolunteers is at 0
        /// </summary>
        [TestMethod]
        public void TestUpdateCurrVolunteersAddReturnsFalseIfTooManyVolunteers()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999997,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            };
            bool expectedResult = false;
            bool actualresult;


            //act
            actualresult = _needManager.UpdateCurrVolunteers(need, 1);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: Returns true if currVolunteers decreased
        /// </summary>
        [TestMethod]
        public void TestUpdateCurrVolunteersSubtractReturnsTrue()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999998,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 1
            };
            bool expectedResult = true;
            bool actualresult;


            //act
            actualresult = _needManager.UpdateCurrVolunteers(need, -1);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns false if currVolunteers was at 0
        /// </summary>
        [TestMethod]
        public void TestUpdateCurrVolunteersSubtractReturnsFalse()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999999,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            };
            bool expectedResult = false;
            bool actualresult;


            //act
            actualresult = _needManager.UpdateCurrVolunteers(need, -1);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        //Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns true if need was found
        [TestMethod]
        public void TestRetrieveVolunteerNeedByTaskIDReturnsTrueIfSucceeds()
        {
            //arrange
            


            //act
            VolunteerNeed retreivedNeed = _needManager.RetrieveVolunteerNeedByTaskID(999998);

            //assert
            Assert.IsNotNull(retreivedNeed);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns false if need dne
        /// </summary>
        [TestMethod]
        public void TestRetrieveVolunteerNeedByTaskIDReturnsFalseiIfBadTaskID()
        {
            //arrange
            

            //act
            VolunteerNeed retreivedNeed = _needManager.RetrieveVolunteerNeedByTaskID(999990);

            //assert
            Assert.IsNull(retreivedNeed);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns true if need is deleted
        /// </summary>
        [TestMethod]
        public void TestDeleteVolunteerNeedReturnsTrueIfSucceeds()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999999,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            };
            bool expectedResult = true;
            bool actualresult;


            //act
            actualresult = _needManager.DeleteVolunteerNeed(need);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: returns false if need dne
        /// </summary>
        [TestMethod]
        public void TestDeleteVolunteerNeedReturnsFalseIfBadTaskID()
        {
            //arrange
            VolunteerNeed need = new VolunteerNeed()
            {
                TaskID = 999900,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            };
            bool expectedResult = false;
            bool actualresult;


            //act
            actualresult = _needManager.DeleteVolunteerNeed(need);

            //assert
            Assert.AreEqual(expectedResult, actualresult);
        }
    }
}
