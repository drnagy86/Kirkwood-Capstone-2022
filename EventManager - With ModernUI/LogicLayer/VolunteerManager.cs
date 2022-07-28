using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerManager class for all volunteer related methods
    /// </summary>
    public class VolunteerManager : IVolunteerManager
    {
        IVolunteerAccessor _volunteerAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Constructor for volunteer manager using the volunteer accessor
        /// </summary>
        public VolunteerManager()
        {
            _volunteerAccessor = new VolunteerAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Constructor that takes an IVolunteerAccessor and sets it to the _volunteerAccessor field. For passing test data for the manager.
        /// </summary>
        /// <param name="volunteerAccessor">The custom accessor being passed through</param>
        public VolunteerManager(IVolunteerAccessor volunteerAccessor)
        {
            _volunteerAccessor = volunteerAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Method to retrieve a list of all volunteer reviews
        /// </summary>
        /// <returns>A list of volunteer data object shells for the volunteer ID and rating</returns>
        public List<Volunteer> RetrieveAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _volunteerAccessor.SelectAllVolunteerReviews();
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Method used to retrieve a list of all volunteers to display on the Volunteers tab / page
        /// </summary>
        /// <returns>A list of volunteer data objects</returns>
        public List<Volunteer> RetrieveAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _volunteerAccessor.SelectAllVolunteers();
            }
            catch (Exception)
            {
                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Retrieve volunteer availability on a given date by VolunteerID 
        /// First tries to get any availability exceptions for the given date.
        /// If it fails to find any, then it retrieves the regular weekly availability.
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <param name="date"></param>
        /// <returns>A list of Availability objects</returns>
        public List<Availability> RetrieveAvailabilityByVolunteerIDAndDate(int volunteerID, DateTime date)
        {
            List<Availability> volunteerAvailabilities = new List<Availability>();

            try
            {
                volunteerAvailabilities = _volunteerAccessor.SelectAvailabilityExceptionByVolunteerIDAndDate(volunteerID, date);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve volunteer availability exceptions", ex);
            }

            // if failed to find any exceptions, get regular weekly availability
            if (volunteerAvailabilities.Count == 0)
            {
                try
                {
                    volunteerAvailabilities = _volunteerAccessor.SelectAvailabilityByVolunteerIDAndDate(volunteerID, date);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to retrieve volunteer availability", ex);
                }
            }

            return volunteerAvailabilities;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Retrieves a volunteer with a specific userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>A Volunteer Object</returns>
        public Volunteer RetrieveVolunteerByUserID(int userID)
        {
            Volunteer volunteer = null;
            try
            {
                volunteer = _volunteerAccessor.SelectVolunteerByUserID(userID);
                if(volunteer == null)
                {
                    throw new ArgumentException();
                }
            } catch(Exception ex)
            {
                throw ex;
            }
            return volunteer;
        }
    }
}
