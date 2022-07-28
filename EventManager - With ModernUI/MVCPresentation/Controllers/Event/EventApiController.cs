using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCPresentation.Controllers.Event
{
    public class EventApiController : ApiController
    {

        private IEventManager _eventManager;


        /// <summary>
        /// Derrick Nagy
        /// Updated: 2022/04/24
        /// 
        /// Description:
        /// API controller to get all events
        /// Route: /api/EventApi
        /// </summary>
        /// <returns>List of event view models</returns>
        public IHttpActionResult GetAllEvents()
        {
            // GET: /api/EventApi
            _eventManager = new EventManager();

            List<EventVM> eventList = null;

            try
            {
                eventList = _eventManager.RetrieveEventListForUpcomingDates();
            }
            catch (ApplicationException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);                
            }


            return Ok(eventList);
        }

        /// <summary>
        /// Derrick Nagy
        /// Updated: 2022/04/24
        /// 
        /// Description:
        /// API controller to return an event view model given an event id
        /// Route: /api/EventApi/100000
        /// 
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Event View Model</returns>
        public IHttpActionResult GetEvent(int id)
        {
            // GET: /api/EventApi/100000
            _eventManager = new EventManager();
            EventVM eventVM = null;

            try
            {
                eventVM = _eventManager.RetrieveEventByEventID(id);
            }
            catch(ApplicationException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);                
            }

            return Ok(eventVM);
        }

    }
}
