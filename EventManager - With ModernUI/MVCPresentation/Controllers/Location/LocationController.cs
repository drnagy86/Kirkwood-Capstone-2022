using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using WPFPresentation;
using DataAccessInterfaces;
using DataAccessFakes;
using MVCPresentation.Models;


namespace MVCPresentation.Controllers.Locations
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/04/04
    /// 
    /// Interaction logic for LocationController
    /// </summary>
    public class LocationController : Controller
    {
        ILocationManager _locationManager = null;
        IEventDateManager _eventDateManager = null;
        IUserManager _userManager;
        IEventManager _eventManager = new EventManager();
        EntranceManager _entranceManager = new EntranceManager();
        LocationScheduleViewModel _locationSchedule = new LocationScheduleViewModel();
        ISublocationManager _sublocationManager = new SublocationManager();
        IActivityManager _activityManager = new ActivityManager();

        public int _pageSize = 10;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Constructor that sets the _locationManager
        /// </summary>
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/27
        /// 
        /// Added IUserManager
        /// </update>
        /// 
        /// <param name="locationManager"></param>
        public LocationController(ILocationManager locationManager, IEventDateManager eventDateManager, IUserManager userManager)
        {
            _locationManager = locationManager;
            _eventDateManager = eventDateManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Returns the navivagion bar for a selected
        /// locations pages
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/11
        /// Added a necessary model field to the view
        /// 
        /// </summary>
        public PartialViewResult LocationNav(int locationId)
        {
            return PartialView(locationId);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// For the View Locations page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocations(int page = 1)
        {
            List<Location> locations = new List<Location>();
            List<Reviews> locationReviews = new List<Reviews>();
            LocationListViewModel model = null;
            try
            {
                locations = _locationManager.RetrieveActiveLocations();
            }
            catch (Exception)
            {
                return View();
            }

            model = new LocationListViewModel
            {
                Locations = locations
                            .OrderBy(p => p.LocationID)
                            .Skip((page - 1) * _pageSize)
                            .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = locations.Count()
                }
            };

            return View(model);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// For the View Location Schedule page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocationSchedule(int locationID = 0)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();
            //eventDates = _eventDateManager.RetrieveEventDatesByLocationID(location.LocationID);
            //_location = location;
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            Location location = new Location();
            location = _locationManager.RetrieveLocationByLocationID(locationID);
            _locationSchedule.Location = location;
            GetAvailability(location.LocationID);
            return View("~/Views/Location/ViewLocationSchedule.cshtml", _locationSchedule);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting events by the location id passed to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult GetEvents(string id)
        {
            int locationID;
            try
            {
                locationID = int.Parse(id);
            }
            catch (Exception)
            {

                locationID = 0;
            }
            List<EventDateVM> eventDates;
            if (locationID == 0)
            {
                eventDates = new List<EventDateVM>();
            }
            else
            {
                eventDates = _eventDateManager.RetrieveEventDatesByLocationID(locationID);
            }
            return new JsonResult { Data = eventDates, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/24
        /// 
        /// Description:
        /// For getting events by the sublocation id passed to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult GetSublocationEvents(string id)
        {
            int locationID;
            try
            {
                locationID = int.Parse(id);
            }
            catch (Exception)
            {

                locationID = 0;
            }

            List<Sublocation> sublocations = _sublocationManager.RetrieveSublocationsByLocationID(locationID);
            List<Activity> activities = new List<Activity>();

            foreach(Sublocation sublocation in sublocations)
            {
                List<Activity> activities1 = _activityManager.RetrieveActivitiesBySublocationID(sublocation.SublocationID);
                foreach(Activity activity in activities1)
                {
                    activities.Add(activity);
                }                
            }
            
            return new JsonResult { Data = activities, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting availability by the location id passed to it and sets it to the 
        /// _locationSchedule
        /// </summary>
        /// <param name="id"></param>
        public void GetAvailability(int id)
        {
            List<AvailabilityVM> availability;
            List<Availability> availabilityException;
            if (id == 0)
            {
                availability = new List<AvailabilityVM>();
            }
            else
            {
                availability = _locationManager.RetrieveLocationAvailabilityByLocationID(id);
            }
            if (id == 0)
            {
                availabilityException = new List<Availability>();
            }
            else
            {
                availabilityException = _locationManager.RetrieveLocationAvailabilityExceptionByLocationID(id);
            }

            _locationSchedule.Availability = availability;
            _locationSchedule.AvailabilityException = availabilityException;
		}
        
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Method that returns user to ViewLocationDetails View
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewLocationDetails(int locationID = 0)
        {
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            Location location = null;
            LocationDetailsViewModel model = null;
            List<Reviews> locationReviews = null;
            List<LocationImage> locationImages = null;
            List<string> locationTags = null;
            try
            {
                ViewBag.Title = "Location Details";
                location = _locationManager.RetrieveLocationByLocationID(locationID);
                locationReviews = _locationManager.RetrieveLocationReviews(locationID);
                locationTags = _locationManager.RetrieveTagsByLocationID(locationID);
                locationImages = _locationManager.RetrieveLocationImagesByLocationID(locationID);

                if (locationReviews.Count != 0)
                {
                    int avg = 0;
                    int total = 0;
                    foreach (Reviews review in locationReviews)
                    {
                        avg += review.Rating;
                        total++;
                    }
                    int sum = avg / total;
                    location.AverageRating = sum;
                }

                model = new LocationDetailsViewModel()
                {
                    Location = location,
                    LocationReviews = locationReviews,
                    LocationTags = locationTags,
                    LocationImages = locationImages
                };
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location not found.");
            }
            return View(model);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Returns a location from the details page in edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, LocationEdit View</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult LocationEdit(int locationID = 0)
        {
            Location location = null;
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            try
            {
                location = _locationManager.RetrieveLocationByLocationID(locationID);
                ViewBag.Title = "Edit " + location.Name;
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location not found.");
            }
            return View(location);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Returns a location from the details page in edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, LocationEdit View</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult LocationEdit(Location location)
        {
            if (location.LocationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            try
            {
                Location oldLocation = _locationManager.RetrieveLocationByLocationID(location.LocationID);
                if (_locationManager.UpdateLocationBioByLocationID(oldLocation, location) == 1)
                {
                    return RedirectToAction("ViewLocationDetails", new { locationID = location.LocationID });
                }
                else
                {
                    ModelState.AddModelError("", "Location could not be updated."); 
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Location could not be found.");
            }
            return View(location);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Method that deactivates a location from the edit page
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>ActionResult, ViewLocations View</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult DeleteLocation(int locationID = 0)
        {
            if (locationID == 0)
            {
                return RedirectToAction("ViewLocations", "Location");
            }
            if (ModelState.IsValid)
            {
                try
                {

                    _locationManager.DeactivateLocationByLocationID(locationID);
                    return RedirectToAction("ViewLocations", new { page = 1 });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Location not found.");
                    return View();
                }
            }
            return View();

        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/17
        /// 
        /// Description:
        /// Method that returns the view for Creating a Location
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "Administrator, Event Planner, Supplier")]
        public ActionResult CreateLocation()
        {
            return View();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/18
        /// 
        /// Description:
        /// Method that creates a new location listing
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            LocationDetailsViewModel locationDetails = new LocationDetailsViewModel();

            if (ModelState.IsValid)
            {
                try
                {
                    if (_locationManager.CreateLocation(location.Name, location.Address1, location.City, location.State, location.ZipCode) == 1)
                    {
                        Location createdLocation = _locationManager.RetrieveLocationByNameAndAddress(location.Name, location.Address1);
                        _locationManager.UpdateLocationBioByLocationID(createdLocation, location);
                        return RedirectToAction("ViewLocationDetails", new { createdLocation.LocationID });
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    if(ex.Message.Equals("The INSERT statement conflicted with the FOREIGN KEY constraint \"fk_ZIPCode_Location\". The conflict occurred in database \"tadpole_db\", table \"dbo.ZIP\", column 'ZIPCode'.\r\nThe statement has been terminated."))
                    {
                        ModelState.AddModelError("", "Invalid Zip Code");
                        return View();
                    }
                    else if(ex.Message.Equals("Value cannot be null.\r\nParameter name: input"))
                    {
                        ModelState.AddModelError("", "Incorrect value");
                        return View();
                    }
                    ModelState.AddModelError("", "Location already exists.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Displays all entrances for a specific location
        /// 
        /// Christopher Repko
        /// Updated: 2022-05-04
        /// 
        /// Fixed an issue that prevented it from showing entrances when a location has none.
        /// </summary>
        /// <param name="locationID"></param>
        // GET: Location/EntranceIndex
        public ActionResult EntranceIndex(int locationID)
        {
            ViewEntrancesModel model = new ViewEntrancesModel()
            {
                Entrances = new List<Entrance>(),
                LocationID = locationID,
                LocationName = ""
            };
            try
            {
                List<Entrance> entrances = _entranceManager.RetrieveEntranceByLocationID(locationID);
                model.Entrances = entrances;

                Location location = _locationManager.RetrieveLocationByLocationID(locationID);
                model.LocationName = location.Name;

            } catch(Exception ex)
            {
                ModelState.AddModelError("", "Failed to retrieve location entrances. Please try again later.");
            }
            return View(model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Redirects to EntranceEdit if a user clicks on the "create" buttons
        /// </summary>
        /// <param name="locationID"></param>
        // GET: Location/EntranceCreate
        public ActionResult EntranceCreate(int locationID)
        {
            ViewBag.Name = "Create Entrance";
            EditEntranceModel model = new EditEntranceModel()
            {
                LocationID = locationID,
                EntranceID = -1,
                OldEntranceName = "",
                OldDescription = "",
                EntranceName = "",
                Description = ""
            };
            return View("EntranceEdit", model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// Displays proper labels depending on create or edit mode
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="entranceID"></param>
        // GET: Location/EntranceEdit
        public ActionResult EntranceEdit(int locationID, int entranceID)
        {
            ViewBag.Name = "Edit Entrance";
            Entrance entrance = _entranceManager.RetrieveEntranceByLocationID(locationID).FirstOrDefault(x => x.EntranceID == entranceID);
            EditEntranceModel model = new EditEntranceModel()
            {
                LocationID = locationID,
                EntranceID = entranceID,
                OldEntranceName = entrance.EntranceName,
                OldDescription = entrance.Description,
                EntranceName = entrance.EntranceName,
                Description = entrance.Description
            };
            return View(model);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022-04-11
        /// 
        /// Description:
        /// POST method to allow user to edit a current entrance or create a new one
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EntranceEdit(EditEntranceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_entranceManager.RetrieveEntranceByLocationID(model.LocationID).Where(e => e.EntranceID == model.EntranceID).Count() != 0)
                    {
                        Entrance oldEntrance = new Entrance()
                        {
                            LocationID = model.LocationID,
                            EntranceID = model.EntranceID,
                            EntranceName = model.OldEntranceName,
                            Description = model.OldDescription
                        };
                        Entrance newEntrance = new Entrance()
                        {
                            LocationID = model.LocationID,
                            EntranceID = model.EntranceID,
                            EntranceName = model.EntranceName,
                            Description = model.Description
                        };

                        _entranceManager.UpdateEntrance(oldEntrance, newEntrance);
                    }
                    else
                    {
                        _entranceManager.CreateEntrance(model.LocationID, model.EntranceName, model.Description);
                    }

                    return RedirectToAction("EntranceIndex", new { locationID = model.LocationID });
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Alaina Gilson 
        /// Created: 2022-04-11
        /// 
        /// Description: 
        /// Allows "deleting" of an entrance
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="entranceID"></param>
        [HttpPost]
        public ActionResult EntranceDelete(int locationID, int entranceID)
        {
            Entrance entrance = _entranceManager.RetrieveEntranceByLocationID(locationID).FirstOrDefault(x => x.EntranceID == entranceID);

            if (entrance != null)
            {
                _entranceManager.RemoveEntranceByEntranceID(entranceID);
            }

            return RedirectToAction("EntranceIndex", new { locationID = locationID });
        }

        public JsonResult DeleteEventOrActivity(string eventID, string eventType, string locationID)
        {
            try
            {
                int id = int.Parse(eventID);
                int locID = int.Parse(locationID);
                if (eventType.Equals("Event"))
                {
                    _eventManager.UpdateEventLocationByEventID(id, locID, null);
                    //_eventDatesForLocation.Remove(selectedEventDateVM);  
                }
                else
                {
                    _activityManager.UpdateActivitySublocationByActivityID(id, locID, null);
                    //_activitiesForSublocation.Remove(selectedActivity);
                }
                //_eventManager.UpdateEventLocationByEventID(id, locID, null);
            }
            catch (Exception)
            {

                return new JsonResult {JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Moves user to the Create Review page
        /// </summary>
        /// <param name="locationID"></param>
        [Authorize(Roles = "Event Planner")]
        [HttpGet]
        public ViewResult CreateReview(int locationID = 0)
        {
            if (locationID == 0)
            {
                return View("ViewLocations", locationID);
            }
            return View(new Reviews() { ForeignID = locationID });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Processes the review to be created and either returns a validation error or creates the review
        /// </summary>
        /// 
        /// <param name="ForeignID"></param>
        /// <param name="Rating"></param>
        /// <param name="Review"></param>
        [HttpPost]
        public ActionResult CreateReview(int ForeignID, int Rating, String Review)
        {
            User user = _userManager.RetrieveUserByEmail(User.Identity.Name);
            Reviews review = new Reviews() { ForeignID = ForeignID, UserID = user.UserID, Rating = Rating, Review = Review, ReviewType = "Location", DateCreated = DateTime.Now };
            if (ModelState.IsValid)
            {
                try
                {
                    _locationManager.CreateLocationReview(review);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(review);
                }
                return RedirectToAction("ViewLocationDetails", "Location", review.ForeignID);
            }
            return View(review);
        }
    }
}