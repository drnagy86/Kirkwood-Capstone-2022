/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Request Object Class
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataObjects
{
    public class VolunteerRequest
    {
        public int RequestID { get; set; }
        public int VolunteerID { get; set; }
        public int TaskID { get; set; }
        public bool? VolunteerResponse { get; set; }
        public bool? EventResponse { get; set; }

        public VolunteerRequest()
        {

        }
        public VolunteerRequest(int requestID, int volunteerID, int taskID, bool? volunteerResponse, bool? eventResponse)
        {
            RequestID = requestID;
            VolunteerID = volunteerID;
            TaskID = taskID;
            VolunteerResponse = volunteerResponse;
            EventResponse = eventResponse;

        }
    }

    public class VolunteerRequestViewModel : VolunteerRequest
    {
        public string VolunteerName { get; set; }
        public string TaskName { get; set; }
        public string StrVolunteerResponse { get; set; }
        public string StrEventResponse { get; set; }
        public int EventID { get; set; }
        [DisplayName("Event")]
        public String EventName { get; set; }


        public VolunteerRequestViewModel()
        {

        }

        public VolunteerRequestViewModel(VolunteerRequest volunteerRequest, string volunteerName, string taskName)
        {
            RequestID = volunteerRequest.RequestID;
            VolunteerID = volunteerRequest.VolunteerID;
            TaskID = volunteerRequest.TaskID;
            VolunteerResponse = volunteerRequest.VolunteerResponse;
            EventResponse = volunteerRequest.EventResponse;
            VolunteerName = volunteerName;
            TaskName = taskName;
            if ((bool)VolunteerResponse.HasValue)
            {
                StrVolunteerResponse = (bool)VolunteerResponse ? "Yes" : "No";
            }
            else
            {
                StrVolunteerResponse = "N/A";
            }
            if ((bool)EventResponse.HasValue)
            {
                StrEventResponse = (bool)EventResponse ? "Yes" : "No";
            }
            else
            {
                StrEventResponse = "N/A";
            }
        }
    }
}
