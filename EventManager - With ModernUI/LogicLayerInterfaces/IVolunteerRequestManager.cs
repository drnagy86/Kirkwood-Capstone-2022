/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Request Manager Interface
/// </summary>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayerInterfaces
{
    public interface IVolunteerRequestManager
    {
        List<VolunteerRequestViewModel> RetrieveVolunteerRequests(int eventID);
        List<VolunteerRequestViewModel> RetrieveAllRequestsForVolunteerByVolunteerID(int volunteerID);
        bool EditVolunteerRequest(VolunteerRequestViewModel oldVolunteerRequest, VolunteerRequestViewModel newVolunteerRequest);
        VolunteerRequestViewModel RetrieveRequestByRequestID(int requestID);
    }
}
