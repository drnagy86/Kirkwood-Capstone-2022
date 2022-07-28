using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEventAccessor
    {
        int InsertEvent(string eventName, string eventDescription, decimal totalBudget);
        int InsertEventReturnsEventID(string eventName, string eventDescription, decimal totalBudget, int userID);

        List<EventVM> SelectActiveEvents();
        int UpdateEvent(Event oldEvent, Event newEvent);
        EventVM SelectEventByEventNameAndDescription(string eventName, string eventDescription);
        
        List<EventVM> SelectEventsUpcomingDates();
        List<EventVM> SelectEventsUpcomingAndPastDates();
        List<EventVM> SelectEventsPastDates();

        List<EventVM> SelectUserEventsForUpcomingDates(int userID);
        List<EventVM> SelectUserEventsForPastDates(int userID);
        List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID);

        int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID);

        bool CheckUserEditPermissionForEvent(int eventID, int userID);
        EventVM SelectEventByEventID(int eventID);

        List<User> SelectEventPlannersForEvent(int eventID);
        List<EventVM> SelectEventsForSearch(string search);
        int DeactivateEventByEventID(int eventID);
    }
}



