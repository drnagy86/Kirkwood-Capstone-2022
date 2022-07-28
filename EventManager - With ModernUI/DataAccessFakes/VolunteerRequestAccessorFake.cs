using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Vinayak Deshpande
/// Created 2022/01/28
/// 
/// Description
/// Accessor Fake for Volunteer Requests. Recreated after deletion.
/// </summary>
namespace DataAccessFakes
{
    public class VolunteerRequestAccessorFake : IVolunteerRequestAccessor
    {
        List<VolunteerRequestViewModel> _fakeRequests = new List<VolunteerRequestViewModel>();

        public VolunteerRequestAccessorFake()
        {
            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999999,
                VolunteerID = 999999,
                TaskID = 999999,
                VolunteerResponse = false,
                EventResponse = true,
                VolunteerName = "Abe Zed",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999998,
                VolunteerID = 999998,
                TaskID = 999999,
                VolunteerResponse = true,
                EventResponse = true,
                VolunteerName = "Bell Yotta",
                TaskName = "Test Task 1",
                EventID = 1000000,
                EventName = "Test Event 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999997,
                VolunteerID = 999997,
                TaskID = 999998,
                VolunteerResponse = true,
                EventResponse = null,
                VolunteerName = "Chaos Xylophone",
                TaskName = "Test Task 2",
                EventID = 1000000,
                EventName = "Test Event 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999996,
                VolunteerID = 999996,
                TaskID = 999998,
                VolunteerResponse = null,
                EventResponse = true,
                VolunteerName = "Digger Wiggum",
                TaskName = "Test Task 2",
                EventID = 1000000,
                EventName = "Test Event 1"
            });

            this._fakeRequests.Add(new VolunteerRequestViewModel()
            {
                RequestID = 999995,
                VolunteerID = 999995,
                TaskID = 999998,
                VolunteerResponse = true,
                EventResponse = false,
                VolunteerName = "Elf Verdant",
                TaskName = "Test Task 2",
                EventID = 1000000,
                EventName = "Test Event 1"
            });


        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/28
        /// 
        /// Description
        /// Retrieves VolunteerRequest fakes for a specific volunteer
        /// </summary>
        /// <param name="volunteerID"></param>    
        public List<VolunteerRequestViewModel> SelectAllRequestsForVolunteerByVolunteerID(int volunteerID)
        {
            List<VolunteerRequestViewModel> volunteerRequestViewModels = new List<VolunteerRequestViewModel>();

            foreach (var fakeRequest in _fakeRequests)
            {
                if (fakeRequest.VolunteerID == volunteerID && fakeRequest.EventResponse == true)
                {
                    volunteerRequestViewModels.Add(fakeRequest);
                }
            }

            return volunteerRequestViewModels;
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// 
        /// Description
        /// Retrieves a fake volunteer request with a specific requestID
        /// </summary>
        /// <param name="requestID"></param>  
        public VolunteerRequestViewModel SelectRequestByRequestID(int requestID)
        {
            VolunteerRequestViewModel request = null;

            foreach (var fakeRequest in _fakeRequests)
            {
                if (fakeRequest.RequestID == requestID)
                {
                    request = fakeRequest;
                }
            }

            if (request == null)
            {
                throw new ArgumentException("Unable to find request.");
            }

            return request;
        }

        public List<VolunteerRequestViewModel> SelectVolunteerRequestsByEventID(int EventID)
        {
            List<VolunteerRequestViewModel> requests = new List<VolunteerRequestViewModel>();

            try
            {
                requests = _fakeRequests;
            }
            catch (Exception)
            {
                throw;
            }

            return requests;
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// 
        /// Description
        /// Updates the fake volunteer request of a specific request ID if the old volunteer response and event response
        ///   match the current volunteer response and event response.
        /// </summary>
        /// <param name="oldVolunteerRequest"></param> 
        /// <param name="newVolunteerRequest"></param> 
        public int UpdateVolunteerRequest(VolunteerRequestViewModel oldVolunteerRequest, VolunteerRequestViewModel newVolunteerRequest)
        {
            int rowsAffected = 0;
            foreach (VolunteerRequestViewModel request in _fakeRequests)
            {
                if (request.RequestID == newVolunteerRequest.RequestID
                    && request.VolunteerResponse == oldVolunteerRequest.VolunteerResponse
                    && request.EventResponse == oldVolunteerRequest.EventResponse)
                {
                    request.VolunteerResponse = newVolunteerRequest.VolunteerResponse;
                    request.EventResponse = newVolunteerRequest.EventResponse;
                    rowsAffected++;
                }
            }
            return rowsAffected;
        }
    }
}
