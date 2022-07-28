using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/07
    /// 
    /// Description:
    /// The volunteer's skill set data object
    /// </summary>
    public class VolunteerSkillSet
    {
        public int VolunteerID { get; set; }
        public string SkillSetID { get; set; }
        public string SkillSetDescription { get; set; }
    }
}
