using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEventManager
    {
        int CreateEvent(string eventName, string eventDescription, decimal totalBudget);
        int CreateEventReturnsEventID(string eventName, string eventDescription, decimal totalBudget, int userID);
        
        List<EventVM> RetreieveActiveEvents();
        bool UpdateEvent(Event oldEvent, Event newEvent);
        EventVM RetrieveEventByEventNameAndDescription(string eventName, string eventDescription);

        List<EventVM> RetrieveEventListForUpcomingDates();
        List<EventVM> RetrieveEventListForUpcomingAndPastDates();
        List<EventVM> RetrieveEventListForPastDates();

        List<EventVM> RetrieveEventListForUpcomingDatesForUser(int userID);
        List<EventVM> RetrieveEventListForPastDatesForUser(int userID);
        List<EventVM> RetrieveEventListForPastAndUpcomingDatesForUser(int userID);

        bool UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID);

        bool CheckUserEditPermissionForEvent(int eventID, int userID);
        EventVM RetrieveEventByEventID(int eventID);

        List<User> RetrieveEventPlannersForEvent(int eventID);
        List<EventVM> RetrieveEventListForSearch(string search);
        int DeactivateEventByEventID(int eventID);
    }
}
