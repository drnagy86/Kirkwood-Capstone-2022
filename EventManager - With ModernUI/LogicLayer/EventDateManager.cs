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
    public class EventDateManager : IEventDateManager
    {
        IEventDateAccessor _eventDateAccessor = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Default constructor for event date manager using the event date accessor
        /// </summary>
        public EventDateManager()
        {
            _eventDateAccessor = new EventDateAccessor();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Constructor for event date manager passing an event date accessor for testing purposes
        /// 
        /// </summary>
        /// <param name="eventDateAccessor">Event Date accessor</param>
        public EventDateManager(IEventDateAccessor eventDateAccessor)
        {
            _eventDateAccessor = eventDateAccessor;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Calls insert event date
        /// </summary>
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Tests for default value of DateTime.Min and throws exception.
        /// Default construtor for EventDate sets the date to DateTime.Min if not given
        /// <param name="eventDate"></param>
        /// <returns>True if added to database</returns>
        public bool CreateEventDate(EventDate eventDate)
        {
            bool result = false;

            // The default value is DateTime.MinVause for new objects so it throws an exception if not set
            if (eventDate.EventDateID == DateTime.MinValue)
            {
                throw new ApplicationException("Event date can not be empty.");
            }
            else
            {
                try
                {
                    result = (1 == _eventDateAccessor.InsertEventDate(eventDate));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Retrieves a list of event dates that are associated with an event
        /// </summary>
        /// <param name="eventID">The EventID</param>
        /// <returns>A list of EventDates</returns>
        public List<EventDate> RetrieveEventDatesByEventID(int eventID)
        {
            List<EventDate> eventDates = null;
            
            try
            {
                eventDates = _eventDateAccessor.SelectEventDatesByEventID(eventID);
                //if (eventDates.Count == 0)
                //{
                //    throw new ApplicationException("No dates found for this event.");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return eventDates;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Updates an Event Date record in data store
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/24
        /// 
        /// Description:
        /// removing logic layer exceptions that are no longer tested due to tests that were removed
        /// 
        /// </summary>
        /// <param name="oldEventDate">The record previously stored</param>
        /// <param name="newEventDate">The new record containing the updates to the old</param>
        /// <returns>True or false if one record was updated</returns>
        public bool UpdateEventDate(EventDate oldEventDate, EventDate newEventDate)
        {
            bool result = false;

            //if (newEventDate.EventDateID == null)
            //{
            //    throw new ApplicationException("Event date can not be empty.");
            //}
            //if (newEventDate.StartTime == null)
            //{
            //    throw new ApplicationException("Start Time can not be empty.");
            //}
            //if (newEventDate.EndTime == null)
            //{
            //    throw new ApplicationException("End Time can not be empty.");
            //}
            if (newEventDate.StartTime >= newEventDate.EndTime)
            {
                throw new ApplicationException("End time cannot be before start time.");
            }
            try
            {
               result = 1 == _eventDateAccessor.UpdateEventDate(oldEventDate, newEventDate);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Method for selecting event dates by LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> RetrieveEventDatesByLocationID(int locationID)
        {
            List<EventDateVM> eventDatesForLocation = new List<EventDateVM>();

            try
            {
                eventDatesForLocation = _eventDateAccessor.SelectEventDatesByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return eventDatesForLocation;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Method for selecting event dates by userID and eventDtae
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventDate"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> RetrieveEventDatesByUserIDAndDate(int userID, DateTime eventDate)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();

            try
            {
                eventDates = _eventDateAccessor.SelectEventDateByUserIDAndDate(userID, eventDate);
            }
            catch (Exception)
            {

                throw;
            }

            return eventDates;
        }
    }
}
