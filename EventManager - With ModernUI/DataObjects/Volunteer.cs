using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Description:
    /// The volunteer data object (which is very similar to the user data object and may need to be changed
    /// with some discussions)
    /// 
    /// Derrick Nagy
    /// Created: 2022/04/28
    /// 
    /// Description:
    /// Added approved field
    /// </summary>
    public class Volunteer
    {
        public int VolunteerID { get; set; }
        public int UserID { get; set; }
        public string VolunteerType { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string UserPhoto { get; set; }
        public string UserDescription { get; set; }
        public int Rating { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Approved { get; set; }
    }

    //public class VolunteerVM : Volunteer
    //{
    //    public List<VolunteerSkillSet> Skills { get; set; }
    //    public List<Reviews> Reviews { get; set; }
        
    //}
}
