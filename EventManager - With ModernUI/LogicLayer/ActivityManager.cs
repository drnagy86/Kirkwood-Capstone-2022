using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessFakes;
using DataAccessLayer;

namespace LogicLayer
{
    public class ActivityManager : IActivityManager
    {
        IActivityAccessor _activityAccessor = null;
        IEventDateAccessor _eventDateAccessor = null;
        ISublocationAccessor _sublocationAccessor = null;
        IActivityResultAccessor _activityResultAccessor = null;

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Default constructor for ActivityManager using real data accessor
        /// </summary>
        public ActivityManager()
        {
            _activityAccessor = new ActivityAccessor();
            _eventDateAccessor = new EventDateAccessor();
            _sublocationAccessor = new SublocationAccessor();
            _activityResultAccessor = new ActivityResultAccessor();
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Constructor for ActivityManger passing data accessors for testing purposes
        /// 
        /// </summary>
        /// <param name="activityAccessor"></param>
        /// <param name="eventDateAccessor"></param>
        /// <param name="sublocationAccessor"></param>
        /// <param name="activityResultAccessor"></param>

        public ActivityManager(IActivityAccessor activityAccessor, IEventDateAccessor eventDateAccessor, 
            ISublocationAccessor sublocationAccessor, IActivityResultAccessor activityResultAccessor)
        {
            _activityAccessor = activityAccessor;
            _eventDateAccessor = eventDateAccessor;
            _sublocationAccessor = sublocationAccessor;
            _activityResultAccessor = activityResultAccessor;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Retrieves a list of all public Activities in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetreiveActivitiesPastAndUpcomingDates()
        {
            List<ActivityVM> result = new List<ActivityVM>();

            try
            {
                result = _activityAccessor.SelectActivitiesPastAndUpcomingDates();
            }
            catch (Exception ex) 
            { 
                throw; 
            }

            return result;

        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/25
        /// 
        /// Description:
        /// Retrieves a list of all user Activities in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetreiveUserActivitiesPastAndUpcomingDates(int userID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                result = _activityAccessor.SelectUserActivitiesPastAndUpcomingDates(userID);
            }
            catch (Exception ex) { throw ex; }

            return result;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Retrieves a list of Activity View Models that are associated with an event
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <returns>A list of ActivityVMs</returns>
        /// /// <summary>
        /// Logan Baccam
        /// Updated: 2022/02/25
        /// Description:
        /// Reverted changes
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of Activity objects</returns>
        public List<Activity> RetrieveActivitiesByEventID(int eventID)
        {
            List<Activity> result = new List<Activity>();
            try{
                List<Activity> activities =_activityAccessor.SelectActivitiesByEventID(eventID);

                foreach (Activity activity in activities)
                {
                    //set list of ActivityResults
                    List<ActivityResult> activityResults = _activityResultAccessor.SelectActivityResultsByActivityID(activity.ActivityID);

                    //set ActivitySublocation
                    Sublocation activitySublocation;
                    if (activity.SublocationID.HasValue)
                    {
                        activitySublocation = _sublocationAccessor.SelectSublocationBySublocationID((int)activity.SublocationID);
                    }
                    else
                    {
                        activitySublocation = null;
                    }

                    //set EventDate
                    EventDate activityEventDate = _eventDateAccessor.SelectEventDateByEventDateIDAndEventID(activity.EventDateID, eventID);

                    result.Add(new ActivityVM()
                    {
                        ActivityID = activity.ActivityID,
                        ActivityName = activity.ActivityName,
                        ActivityDescription = activity.ActivityDescription,
                        PublicActivity = activity.PublicActivity,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ActivityImageName = activity.ActivityImageName,
                        SublocationID = activity.SublocationID,
                        EventDateID = activity.EventDateID,
                        EventID = activity.EventID,
                        ActivityResults = activityResults,
                        ActivitySublocation = activitySublocation,
                        EventDate = activityEventDate
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Retrieves a list of Activity View Models that are associated with 
        ///     an event and event date
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <param name="eventDateID">The EventDateID</param>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetrieveActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                List<Activity> activities = _activityAccessor.SelectActivitiesByEventIDAndEventDateID(eventID, eventDateID);

                foreach (Activity activity in activities)
                {
                    //set list of ActivityResults
                    List<ActivityResult> activityResults = _activityResultAccessor.SelectActivityResultsByActivityID(activity.ActivityID);

                    //set ActivitySublocation
                    Sublocation activitySublocation;
                    if (activity.SublocationID.HasValue)
                    {
                        activitySublocation = _sublocationAccessor.SelectSublocationBySublocationID((int)activity.SublocationID);
                    }
                    else
                    {
                        activitySublocation = null;
                    }

                    //set EventDate
                    EventDate activityEventDate = _eventDateAccessor.SelectEventDateByEventDateIDAndEventID(activity.EventDateID, eventID);

                    result.Add(new ActivityVM()
                    {
                        ActivityID = activity.ActivityID,
                        ActivityName = activity.ActivityName,
                        ActivityDescription = activity.ActivityDescription,
                        PublicActivity = activity.PublicActivity,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ActivityImageName = activity.ActivityImageName,
                        SublocationID = activity.SublocationID,
                        EventDateID = activity.EventDateID,
                        EventID = activity.EventID,
                        ActivityResults = activityResults,
                        ActivitySublocation = activitySublocation,
                        EventDate = activityEventDate
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/23
        /// 
        /// Description:
        /// Retrieves a list of Activities that match the sublocationID parameter
        /// </summary>
        /// <param name="sublocationID">The EventID</param>
        /// <returns>A list of Activities</returns>
        public List<Activity> RetrieveActivitiesBySublocationID(int sublocationID)
        {
            List<Activity> activities = new List<Activity>();

            try
            {
                activities = _activityAccessor.SelectActivitiesBySublocationID(sublocationID);
            }
            catch (Exception)
            {

                throw;
            }

            return activities;
		}
		
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/14
        /// 
        /// Description:
        /// Retrieves a list of all Activities for an event in View Model 
        /// </summary>
        /// <returns>A list of ActivityVMs</returns>
        public List<ActivityVM> RetrieveActivitiesByEventIDForVM(int eventID)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                result = _activityAccessor.SelectActivitiesByEventIDForVM(eventID);
            }
            catch (Exception ex) { throw ex; }

            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Retrieves a list of activity view model objects that are associated
        /// with a specific supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of ActivityVM objects</returns>
        public List<ActivityVM> RetrieveActivitiesBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<ActivityVM> result = new List<ActivityVM>();
            try
            {
                List<Activity> activities = _activityAccessor.SelectActivitiesBySupplierIDAndDate(supplierID, date);

                foreach (Activity activity in activities)
                {
                    //set list of ActivityResults
                    List<ActivityResult> activityResults = _activityResultAccessor.SelectActivityResultsByActivityID(activity.ActivityID);

                    //set ActivitySublocation
                    Sublocation activitySublocation;
                    if (activity.SublocationID.HasValue)
                    {
                        activitySublocation = _sublocationAccessor.SelectSublocationBySublocationID((int)activity.SublocationID);
                    }
                    else
                    {
                        activitySublocation = null;
                    }

                    //set EventDate
                    EventDate activityEventDate = _eventDateAccessor.SelectEventDateByEventDateIDAndEventID(activity.EventDateID, activity.EventID);

                    result.Add(new ActivityVM()
                    {
                        ActivityID = activity.ActivityID,
                        ActivityName = activity.ActivityName,
                        ActivityDescription = activity.ActivityDescription,
                        PublicActivity = activity.PublicActivity,
                        StartTime = activity.StartTime,
                        EndTime = activity.EndTime,
                        ActivityImageName = activity.ActivityImageName,
                        SublocationID = activity.SublocationID,
                        EventDateID = activity.EventDateID,
                        EventID = activity.EventID,
                        ActivityResults = activityResults,
                        ActivitySublocation = activitySublocation,
                        EventDate = activityEventDate
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/14
        /// 
        /// Description: Allows for changing of sublocation id to new or null
        /// </summary>
        /// <param name="activityID"></param>
        /// <param name="oldSublocationID"></param>
        /// <param name="newSublocationID"></param>
        /// <returns></returns>
        public bool UpdateActivitySublocationByActivityID(int activityID, int? oldSublocationID, int? newSublocationID)
        {
            bool result = false;

            try
            {
                result = 1 == _activityAccessor.UpdateActivitySublocationByActivityID(activityID, oldSublocationID, newSublocationID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update activity sublocation", ex);
            }

            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Creates a new activity object and inserts it into the database
        /// </summary>
        /// <param name="activity">New activity object to be created</param>
        /// <returns>int number of rows affected</returns>
        public int CreateActivity(Activity activity)
        {
            int rowsAffected;

            if (activity.ActivityName == null || activity.ActivityName == "")
            {
                throw new ApplicationException("Activity name cannot be empty");
            }
            if (activity.ActivityName.Length > 50)
            {
                throw new ApplicationException("Activity name cannot be longer than 50 characters");
            }
            if (activity.ActivityDescription.Length > 250)
            {
                throw new ApplicationException("Activity description cannot be longer than 250 characters");
            }
            if (activity.StartTime == new DateTime()) // defaults to 01/01/0001 if it had never been set
            {
                throw new ApplicationException("Activity must have a start time");
            }
            if (activity.EndTime == new DateTime())
            {
                throw new ApplicationException("Activity must have an end time");
            }
            if (activity.StartTime.CompareTo(activity.EndTime) >= 0)
            {
                throw new ApplicationException("Activity start time must be before its end time");
            }
            if (activity.SublocationID == null)
            {
                throw new ApplicationException("Activity must have a sublocation");
            }
            if (activity.EventDateID == new DateTime())
            {
                throw new ApplicationException("Activity must have a date");
            }

            try
            {
                rowsAffected = _activityAccessor.InsertActivity(activity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select activities matching the given supplierID 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of activity objects for a Supplier</returns>
        public List<Activity> RetrieveActivitiesBySupplierID(int supplierID)
        {
            List<Activity> activities = new List<Activity>();

            try
            {
                activities = _activityAccessor.SelectActivitiesBySupplierID(supplierID);
            }
            catch (Exception)
            {

                throw;
            }

            return activities;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/04/08
        /// 
        /// Description:
        /// Method that grabs an activity by the supplied ActivityID
        /// </summary>
        /// <param name="activityID"></param>
        /// <returns></returns>
        public ActivityVM RetrieveActivityVMByActivityID(int activityID)
        {
            ActivityVM result = new ActivityVM();

            try
            {
                result = _activityAccessor.SelectActivityByActivityID(activityID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
