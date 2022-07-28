using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessInterfaces
{
    public interface IEventDateAccessor
    {
        int InsertEventDate(EventDate eventDate);
        List<EventDate> SelectEventDatesByEventID(int eventID);
        EventDate SelectEventDateByEventDateIDAndEventID(DateTime eventDateID, int eventID);
        int UpdateEventDate(EventDate oldEventDate, EventDate newEventDate);
        List<EventDateVM> SelectEventDatesByLocationID(int locationID);
        // FOR VOLUNTEERS. SELECTS THEM BY IF THEY'RE PARTICIPATING IN A TASK ON THIS DATE
        List<EventDateVM> SelectEventDateByUserIDAndDate(int userID, DateTime eventDate);

        // copied over and waiting to implement when appropriate
        //int DeactivateEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
        //int DeleteEventDateByEventDateIDandEventID(DateTime eventDateID, int eventID);
    }
}
