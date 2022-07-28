using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class VolunteerApplicationsAccessorFake : IVolunteerApplicationsAccessor
    {
        private List<User> users = new List<User>();
        private List<Volunteer> volunteers = new List<Volunteer>();
        private List<Availability> availabilities = new List<Availability>();
        
        private int volunteerIDCount = 100000;
        private int availabilityIDCount = 100000;



        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Constructor that creates fake user entries
        /// 
        /// </summary>
        public VolunteerApplicationsAccessorFake()
        {
            this.users.Add(new User()
            {
                UserID = 100000,
                EmailAddress = "user1@company.com",
                GivenName = "First1",
                FamilyName = "Last1",
                State = "CA",
                City = "Los Angeles",
                Zip = 12345,
                Roles = new List<String>(),
                Active = true
            });
            this.users.Add(new User()
            {
                UserID = 100001,
                EmailAddress = "user2@company.com",
                GivenName = "First2",
                FamilyName = "Last2",
                State = "NV",
                City = "Reno",
                Zip = null,
                Roles = new List<String>(),
                Active = true
            });
            this.users.Add(new User()
            {
                UserID = 100002,
                EmailAddress = "user3@company.com",
                GivenName = "First3",
                FamilyName = "First3",
                State = "IA",
                City = "Cedar Rapids",
                Zip = 12345,
                Roles = new List<String>(),
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Finds the user from fake list of users, adds them to volunteer table with availability
        /// </summary>
        /// <param name="userID">The User ID</param>
        /// <param name="availability">An Availability object showing the user's availability</param>
        /// <returns></returns>
        public int InsertVolunteerApplication(int userID, Availability availability)
        {
            int rowsAdded = 0;

            User volunteerUser = users.Find(u => u.UserID == userID);

            if (volunteerUser != null)
            {
                volunteers.Add(new Volunteer()
                {
                    VolunteerID = volunteerIDCount,
                    UserID = userID,
                    VolunteerType = "Open", // this would insert a row in the real database
                    GivenName = volunteerUser.GivenName,
                    FamilyName = volunteerUser.FamilyName,
                    Email = volunteerUser.EmailAddress,
                    PasswordHash = "",
                    State = volunteerUser.State,
                    City = volunteerUser.City,
                    Zip =  (volunteerUser.Zip != null)?(int)volunteerUser.Zip : 0,
                    UserPhoto = volunteerUser.UserPhoto,
                    UserDescription = volunteerUser.UserDescription,
                    Rating = 0,
                    Active = volunteerUser.Active,
                    DateCreated = DateTime.Now,
                    Approved = false
                });
                
                // this mimics the row count increment that happens when a record is inserted into the volunteers table and volunteer types table
                rowsAdded += 2;

                availability.ForeignID = volunteerIDCount;
                volunteerIDCount++;
                availability.AvailabilityID = availabilityIDCount;
                availabilityIDCount++;
                availabilities.Add(availability);

                // this mimics the row count increment that happens when a record is inserted into
                // both the availablity table and the volunteer availablity table
                rowsAdded += 2;                

            }

            return rowsAdded;
        }
    }

}
