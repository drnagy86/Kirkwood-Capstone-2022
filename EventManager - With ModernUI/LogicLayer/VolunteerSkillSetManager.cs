using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/08
    /// 
    /// Description:
    /// Logic layer class for the Volunteer Skill Set manager methods
    /// </summary>
    public class VolunteerSkillSetManager : IVolunteerSkillSetManager
    {
        private IVolunteerSkillSetAccessor _volunteerSkillSetAccessor = null;

        public VolunteerSkillSetManager()
        {
            _volunteerSkillSetAccessor = new VolunteerSkillSetAccessor();
        }

        public VolunteerSkillSetManager(IVolunteerSkillSetAccessor volunteerSkillSetAccessor)
        {
            _volunteerSkillSetAccessor = volunteerSkillSetAccessor;
        }

        public List<VolunteerSkillSet> RetrieveSkillSetByVolunteerID(int volunteerID)
        {
            List<VolunteerSkillSet> volunteerSkills = new List<VolunteerSkillSet>();

            try
            {
                volunteerSkills = _volunteerSkillSetAccessor.SelectSkillSetByVolunteerID(volunteerID);
            }
            catch (Exception)
            {

                throw;
            }

            return volunteerSkills;
        }
    }
}
