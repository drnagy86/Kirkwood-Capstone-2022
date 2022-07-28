/// <summary>
/// Vinayak Deshpande
/// Created 2022/01/26
/// 
/// Interface for Volunteer Requests Accessor
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{

    public interface IVolunteerRequestAccessor
    {
        List<VolunteerRequestViewModel> SelectVolunteerRequestsByEventID(int EventID);
        List<VolunteerRequestViewModel> SelectAllRequestsForVolunteerByVolunteerID(int volunteerID);
        int UpdateVolunteerRequest(VolunteerRequestViewModel oldVolunteerRequest, VolunteerRequestViewModel newVolunteerRequest);
        VolunteerRequestViewModel SelectRequestByRequestID(int RequestID);
    }
}
