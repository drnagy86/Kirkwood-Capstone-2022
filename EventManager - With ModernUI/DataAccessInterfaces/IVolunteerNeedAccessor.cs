using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IVolunteerNeedAccessor
    {
        int InsertVolunteerNeed(int taskID, int numTotalVolunteers);
        int UpdateVolunteerNeed(int taskID, int numTotalVolunteers);
        int DeleteVolunteerNeed(int taskID);
        int UpdateAddCurrVolunteers(int taskID);
        int UpdateSubtractCurrVolunteers(int taskID);
        VolunteerNeed SelectVolunteerNeedByTaskID(int taskID);
    }
}
