using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created 2022/03/08
    /// 
    /// Description
    /// Accessor Fake for Volunteer Skill Set
    /// </summary>
    public class VolunteerSkillSetAccessorFake : IVolunteerSkillSetAccessor
    {
        private List<VolunteerSkillSet> _fakeVolunteerSkills = new List<VolunteerSkillSet>();

        /// <summary>
        /// Austin Timmerman
        /// Created 2022/03/08
        /// 
        /// Description
        /// The fake accessor constructor that fills the _fakeVolunteerSkills with fakes
        /// </summary>
        public VolunteerSkillSetAccessorFake()
        {
            _fakeVolunteerSkills.Add(new VolunteerSkillSet()
            {
                VolunteerID = 999999,
                SkillSetID = "Fake Chef",
                SkillSetDescription = "The number one fake chef in the world."
            });

            _fakeVolunteerSkills.Add(new VolunteerSkillSet()
            {
                VolunteerID = 999999,
                SkillSetID = "Fake Driver",
                SkillSetDescription = "The number one fake driver in the world."
            });

            _fakeVolunteerSkills.Add(new VolunteerSkillSet()
            {
                VolunteerID = 999999,
                SkillSetID = "Fake Singer",
                SkillSetDescription = "The number one fake singer in the world."
            });
        }

        /// <summary>
        /// Austin Timmerman
        /// Created 2022/03/08
        /// 
        /// Description
        /// Method to select the skills that match a volunteerID passed to it
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>List of VolunteerSkillSet objects</returns>
        public List<VolunteerSkillSet> SelectSkillSetByVolunteerID(int volunteerID)
        {
            List<VolunteerSkillSet> volunteerSkills = new List<VolunteerSkillSet>();

            try
            {
                foreach(VolunteerSkillSet skill in _fakeVolunteerSkills)
                {
                    if(skill.VolunteerID == volunteerID)
                    {
                        volunteerSkills.Add(skill);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return volunteerSkills;
        }
    }
}
