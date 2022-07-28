using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Interface for the VolunteerAccessor
    /// </summary>
    public interface IVolunteerAccessor
    {
        List<Volunteer> SelectAllVolunteers();
        List<Volunteer> SelectAllVolunteerReviews();
        List<Availability> SelectAvailabilityByVolunteerIDAndDate(int volunteerID, DateTime date);
        List<Availability> SelectAvailabilityExceptionByVolunteerIDAndDate(int volunteerID, DateTime date);
        Volunteer SelectVolunteerByUserID(int userID);
    }
}
