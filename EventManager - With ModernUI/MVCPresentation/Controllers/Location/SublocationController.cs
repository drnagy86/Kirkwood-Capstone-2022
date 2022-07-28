using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentation.Models;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace MVCPresentation.Controllers
{
    public class SublocationController : Controller
    {
        ISublocationManager _sublocationManager;
        ILocationManager _locationManager;

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// 
        /// Description
        /// 
        /// Constructor for controller. Gets ninject bindings for managers.
        /// </summary>
        /// <param name="sublocationManager"></param>
        /// <param name="locationManager"></param>
        public SublocationController(ISublocationManager sublocationManager, ILocationManager locationManager)
        {
            _sublocationManager = sublocationManager;
            _locationManager = locationManager;
        }
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// Description
        /// 
        /// Index GET method for the sublocation controller
        /// </summary>
        /// <param name="locationID">ID of the location being viewed</param>
        /// <returns>The sublocation index view</returns>
        public ActionResult Index(int locationID)
        {
            SublocationModel model = new SublocationModel();
            model.Sublocations = new List<Sublocation>();

            bool canEdit = false;
            try
            {
                List<Sublocation> sublocations = _sublocationManager.RetrieveSublocationsByLocationID(locationID);

                Location location = _locationManager.RetrieveLocationByLocationID(locationID);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser applicationUser = userManager.FindById(User.Identity.GetUserId());
                if (applicationUser != null && applicationUser.UserID == location.UserID || (location.UserID == null && User.IsInRole("Event Planner")))
                {
                    canEdit = true;
                }

                foreach (Sublocation sublocation in sublocations)
                {
                    model.Sublocations.Add(new Sublocation()
                    {
                        SublocationID = sublocation.SublocationID,
                        LocationID = sublocation.LocationID,
                        SublocationName = sublocation.SublocationName,
                        SublocationDescription = sublocation.SublocationDescription
                    });
                }

                model.LocationID = location.LocationID;
                model.LocationName = location.Name;
            } catch(Exception ex)
            {
                ModelState.AddModelError("", "Failed to retrieve areas: " + ex.Message);
            }
            model.CanEdit = canEdit;
            return View(model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// Description
        /// 
        /// Create GET method for the sublocation controller
        /// </summary>
        /// <param name="locationId">ID of the current location</param>
        /// <returns>The sublocation edit view with a special create model loaded</returns>
        public ActionResult Create(int locationId)
        {
            return View("Edit", new EditSublocationModel()
            {
                LocationID = locationId,
                SublocationID = -1,
                SublocationDescription = "",
                NewSublocationDescription = "",
                SublocationName = "",
                NewSublocationName = ""
            });
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// Description
        /// 
        /// Index GET method for the sublocation controller
        /// </summary>
        /// <param name="sublocationId">ID of the sublocation being editted</param>
        /// <returns>The sublocation edit view</returns>
        public ActionResult Edit(int sublocationId)
        {
            try
            {
                Sublocation sublocation = _sublocationManager.RetrieveSublocationBySublocationID(sublocationId);
                EditSublocationModel model = new EditSublocationModel()
                {
                    LocationID = sublocation.LocationID,
                    SublocationID = sublocation.SublocationID,
                    SublocationName = sublocation.SublocationName,
                    SublocationDescription = sublocation.SublocationDescription,
                    NewSublocationName = sublocation.SublocationName,
                    NewSublocationDescription = sublocation.SublocationDescription
                };
                return View(model);
            } catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// Description
        /// 
        /// Index GET method for the sublocation controller
        /// </summary>
        /// <param name="model">Model for editting</param>
        /// <returns>The sublocation index view</returns>
        [HttpPost]
        public ActionResult Edit(EditSublocationModel model)
        {
            if(ModelState.IsValid) {
                try
                {
                    if(_sublocationManager.RetrieveSublocationBySublocationID(model.SublocationID) != null)
                    {
                        var oldSublocation = new Sublocation()
                        {
                            LocationID = model.LocationID,
                            SublocationID = model.SublocationID,
                            SublocationName = model.SublocationName,
                            SublocationDescription = model.SublocationDescription
                        };
                        var newSublocation = new Sublocation()
                        {
                            LocationID = model.LocationID,
                            SublocationID = model.SublocationID,
                            SublocationName = model.NewSublocationName,
                            SublocationDescription = model.NewSublocationDescription
                        };
                        _sublocationManager.EditSublocationBySublocationID(oldSublocation, newSublocation);
                    } else
                    {
                        _sublocationManager.CreateSublocationByLocationID(model.LocationID, model.NewSublocationName, model.NewSublocationDescription);
                    }
                    return RedirectToAction("Index", new { locationId = model.LocationID });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                    return View(model);
                }
            } else
            {
                return View(model);
            }
            
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/13
        /// Description
        /// 
        /// Index GET method for the sublocation controller
        /// </summary>
        /// <param name="sublocationId">ID of the sublocation being deleted</param>
        /// <param name="locationId">ID of the location being viewed</param>
        /// <returns>The sublocation index view</returns>
        [HttpPost]
        public ActionResult Delete(int sublocationId, int locationId)
        {
            try
            {
                _sublocationManager.DeactivateSublocationBySublocationID(sublocationId);

                return RedirectToAction("Index", new { locationId = locationId });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Failed to delete area: " + ex.Message);
                return RedirectToAction("Index", new { locationId = locationId });
            }
        }
    }
}
