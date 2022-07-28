using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MVCPresentation.Controllers.Event
{
    [Authorize]
    public class EventCreateController : Controller
    {
        private EventVM _eventVM;
        private IEventManager _eventManager;
        private ILocationManager _locationManager;
        private IUserManager _userManager;
        private IEventDateManager _eventDateManager;

        public EventCreateController(IEventManager eventManager, IUserManager userManger, IEventDateManager eventDateManager, ILocationManager locationManager)
        {
            _eventManager = eventManager;
            _userManager = userManger;
            _eventDateManager = eventDateManager;
            _locationManager = locationManager;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/21
        /// 
        /// Description:
        /// Controller for getting the form to create an event
        /// </summary>
        /// <returns>View for the form</returns>
        public ViewResult EventCreate()
        {

            return View();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/21
        /// 
        /// Description:
        /// Controller for posting the form to create an event
        /// 
        /// Updated: 2022/04/25
        /// Logan Baccam
        /// 
        /// Description:
        /// Added ViewBag Data to populate verified locations
        /// 
        /// </summary>
        /// <param name="eventVM"></param>
        /// <returns>View to edit the rest of the form</returns>
        [HttpPost]
        public ActionResult CreateEvent(EventVM eventVM)
        {
            _eventVM = eventVM;

            if (ModelState.IsValid)
            {
                try
                {
                    string currentUserName = User.Identity.GetUserName();
                    int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;

                    _eventVM.EventID = _eventManager.CreateEventReturnsEventID(_eventVM.EventName, _eventVM.EventDescription, _eventVM.TotalBudget, userID);
                    TempData["eventID"] = _eventVM.EventID;
                }
                catch (Exception ex)
                {

                    TempData["errorMessage"] = ex.Message;
                    return View("EventCreate", _eventVM);

                }
            }


            // should return to details page
            // or page to add date, location, etc
            List<Location> listlocation = _locationManager.RetrieveActiveLocations();
            var locations = listlocation.Select(s => new
            {
                LocationID = s.LocationID,
                Description = "Name: " + s.Name + ", City: " + s.City + ", State: " + s.State + ", Address: " + s.Address1 + "|"
            }).ToList();
            ViewBag.LocationList = new SelectList(locations, "LocationID", "Description");
            return View("AddLocation", _eventVM);

        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/25
        /// 
        /// Description:
        /// Controller for getting the form to add Location to an event
        /// </summary>
        /// <returns>View for the form</returns>
        [HttpGet]
        public ViewResult AddLocation()
        {
            return View();
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/25
        /// 
        /// Description:
        /// Controller for getting the form to add Location to an event
        /// </summary>
        /// <returns>View for the form</returns>
        [HttpPost]
        public ActionResult AddLocation(EventVM eventVM)
        {
            Location location = null;
            _eventVM = _eventManager.RetrieveEventByEventID(eventVM.EventID);
            if (eventVM.Location is null || eventVM.Location.Name == "")
            {
                return View("AddDate", _eventVM);
            }
            try
            {

                location = _locationManager.RetrieveLocationByNameAndAddress(eventVM.Location.Name, eventVM.Location.Address1);
                _eventVM.Location = location;
                _eventVM.LocationID = location.LocationID;
                _eventManager.UpdateEventLocationByEventID(_eventVM.EventID, eventVM.LocationID, _eventVM.LocationID);
                return View("AddDate", _eventVM);

            }
            catch
            {
                try
                {
                    eventVM.Location.Active = false;
                    if (_locationManager.CreateLocation(eventVM.Location.Name, eventVM.Location.Address1, eventVM.Location.City,
                        eventVM.Location.State, eventVM.Location.ZipCode) == 1)
                    {
                        location = _locationManager.RetrieveLocationByNameAndAddress(eventVM.Location.Name, eventVM.Location.Address1);
                        _eventVM.Location = location;
                        _eventVM.LocationID = location.LocationID;
                        _eventManager.UpdateEventLocationByEventID(_eventVM.EventID, eventVM.LocationID, _eventVM.LocationID);

                        return View("AddDate", _eventVM);

                    }
                }
                catch
                {
                    List<Location> listlocation = _locationManager.RetrieveActiveLocations();
                    var locations = listlocation.Select(s => new
                    {
                        LocationID = s.LocationID,
                        Description = "Name: " + s.Name + ", City: " + s.City + ", State: " + s.State + ", Address: " + s.Address1 + "|"
                    }).ToList();
                    ViewBag.LocationList = new SelectList(locations, "LocationID", "Description");
                    ModelState.AddModelError("", "");
                    return View();
                }


            }

            return View();
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/25
        /// 
        /// Description:
        /// Controller for getting the form to add dates to an event
        /// </summary>
        /// <returns>View for the form</returns>
        [HttpGet]
        public ViewResult AddDate()
        {
            return View();
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/25
        /// 
        /// Description:
        /// Controller for adding dates to an event
        /// </summary>
        /// <returns>View for the form</returns>
        [HttpPost]
        public JsonResult AddDate(List<EventDate> eventDates)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    foreach (EventDate eventDate in eventDates)
                    {
                        _eventDateManager.CreateEventDate(eventDate);
                    }

                }
                catch
                {
                    ModelState.AddModelError("", "");
                    return Json("No dates added.", JsonRequestBehavior.DenyGet);
                }

            }

            

            return Json("Date(s) Successfully Added.", JsonRequestBehavior.AllowGet);
        }
        

        

    }
}