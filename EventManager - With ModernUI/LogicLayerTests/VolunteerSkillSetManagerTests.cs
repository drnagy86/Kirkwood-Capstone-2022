using DataAccessFakes;
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
    public class VolunteerSkillSetManagerTests
    {
        private IVolunteerSkillSetManager _volunteerSkillSetManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerSkillSetManager = new VolunteerSkillSetManager(new VolunteerSkillSetAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test to retrieve a list of skills by volunteerID returns correct amount of skills
        /// </summary>
        [TestMethod]
        public void TestRetrieveSkillSetByVolunteerIDReturnsCorrectAmountOfSkills()
        {
            const int volunteerID = 999999;
            const int expectedCount = 3;
            int actualCount;

            actualCount = _volunteerSkillSetManager.RetrieveSkillSetByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Test to retrieve a list of skills by volunteerID with a bad volunteerID 
        /// returns correct amount of skills
        /// </summary>
        [TestMethod]
        public void TestRetrieveSkillSetByVolunteerIDWithByVolunteerIDReturnsCorrectAmountOfSkills()
        {
            const int volunteerID = 010100101;
            const int expectedCount = 0;
            int actualCount;

            actualCount = _volunteerSkillSetManager.RetrieveSkillSetByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
