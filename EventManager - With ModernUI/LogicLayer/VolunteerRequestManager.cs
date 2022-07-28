/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Requests Manager
/// </summary>

using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VolunteerRequestManager : IVolunteerRequestManager
    {
        private IVolunteerRequestAccessor _volunteerRequestAccessor;

        public VolunteerRequestManager()
        {
            _volunteerRequestAccessor = new VolunteerRequestAccessor();
        }

        public VolunteerRequestManager(IVolunteerRequestAccessor requestAccessor)
        {
            this._volunteerRequestAccessor = requestAccessor;
        }
        public List<VolunteerRequestViewModel> RetrieveVolunteerRequests(int eventID)
        {
            List<VolunteerRequestViewModel> requests = null;
            try
            {
                requests = _volunteerRequestAccessor.SelectVolunteerRequestsByEventID(eventID);
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
        /// Edits the volunteer request of a specific request ID if the old volunteer response and event response
        ///   match the current volunteer response and event response.
        /// </summary>
        /// <param name="oldVolunteerRequest"></param> 
        /// <param name="newVolunteerRequest"></param> 
        /// <returns>A boolean indicating if the update was successful</returns>

        public bool EditVolunteerRequest(VolunteerRequestViewModel oldVolunteerRequest, VolunteerRequestViewModel newVolunteerRequest)
        {
            bool result;
            try
            {
                result = _volunteerRequestAccessor.UpdateVolunteerRequest(oldVolunteerRequest, newVolunteerRequest) == 1;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Emma Pollock
        /// 2022/03/28
        /// 
        /// Description: 
        /// Retrieves all VolunteerRequests for a specific volunteer
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>A List of VolunteerRequestViewModels</returns>
        public List<VolunteerRequestViewModel> RetrieveAllRequestsForVolunteerByVolunteerID(int volunteerID)
        {
            List<VolunteerRequestViewModel> requests = new List<VolunteerRequestViewModel>();
            try
            {
                requests = _volunteerRequestAccessor.SelectAllRequestsForVolunteerByVolunteerID(volunteerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return requests;
        }

        /// <summary>
        /// Emma Pollock
        /// 2022/03/31
        /// 
        /// Description: 
        /// Retrieves a volunteer request with a specific request ID
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns>The VolunteerRequestViewModel with the requestID</returns>
        public VolunteerRequestViewModel RetrieveRequestByRequestID(int requestID)
        {
            VolunteerRequestViewModel request;
            try
            {
                request = _volunteerRequestAccessor.SelectRequestByRequestID(requestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return request;
        }
    }
}
