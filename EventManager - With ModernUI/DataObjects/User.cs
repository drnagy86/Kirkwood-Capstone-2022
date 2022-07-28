using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string EmailAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? Zip { get; set; }
        public string UserPhoto { get; set; }
        public string UserDescription { get; set; }
        public bool Active { get; set; }
        public string DateCreated { get; set; }
        public List<string> Roles { get; set; }

        public User()
        {

        }

        public User(int userID, string givenName, string familyName, string emailAddress, string state,
            string city, int zip)
        {
            UserID = userID;
            GivenName = givenName;
            FamilyName = familyName;
            EmailAddress = emailAddress;
            State = state;
            City = city;
            Zip = zip;
        }


    }
}
