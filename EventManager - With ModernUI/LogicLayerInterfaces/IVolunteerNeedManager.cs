using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IVolunteerNeedManager
    {
        bool AddVolunteerNeed(VolunteerNeed volunteerNeed);
        bool UpdateVolunteerNeed(VolunteerNeed volunteerNeed, int newTotalVolunteers);
        bool DeleteVolunteerNeed(VolunteerNeed volunteerNeed);
        bool UpdateCurrVolunteers(VolunteerNeed volunteerNeed, int newCurrVolunteers);
        VolunteerNeed RetrieveVolunteerNeedByTaskID(int TaskID);
    }
}
