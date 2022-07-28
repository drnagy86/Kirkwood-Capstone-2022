using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IVolunteerManager
    {
        List<Volunteer> RetrieveAllVolunteers();
        List<Volunteer> RetrieveAllVolunteerReviews();
        List<Availability> RetrieveAvailabilityByVolunteerIDAndDate(int volunteerID, DateTime date);
        Volunteer RetrieveVolunteerByUserID(int userID);
    }
}
