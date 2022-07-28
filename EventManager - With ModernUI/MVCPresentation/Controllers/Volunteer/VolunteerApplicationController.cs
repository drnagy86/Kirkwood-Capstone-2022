using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayerInterfaces;
using Microsoft.AspNet.Identity;

namespace MVCPresentation.Controllers.Volunteer
{
    [Authorize]
    public class VolunteerApplicationController : Controller
    {
        private IUserManager _userManager;
        private IVolunteerApplicationsManager _volunteerApplicationsManager;
        private IVolunteerManager _volunteerManager;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Constructor for controller, depends on IUserManager and IVolunteerApplicationManager
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="volunteerApplicationsManager"></param>
        public VolunteerApplicationController(IUserManager userManager, IVolunteerApplicationsManager volunteerApplicationsManager, IVolunteerManager volunteerManager)
        {
            _userManager = userManager;
            _volunteerApplicationsManager = volunteerApplicationsManager;
            _volunteerManager = volunteerManager;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Sets up form for volunteering
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VolunteerApplication()
        {

            bool result = false;
            try
            {
                // add volunteer, set availability
                string currentUserName = User.Identity.GetUserName();
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;

                try
                {
                    DataObjects.Volunteer volunteer = _volunteerManager.RetrieveVolunteerByUserID(userID);
                }
                catch (Exception ex)
                {
                    // good, not a volunteer already
                    Availability availability = new Availability()
                    {
                        DateID = DateTime.Now,
                        Sunday = true,
                        Monday = true,
                        Tuesday = true,
                        Wednesday = true,
                        Thursday = true,
                        Friday = true,
                        Saturday = true
                    };

                    return View(availability);
                }

                TempData["successMessage"] = "We already have you as a volunteer. Thanks!";

                return Redirect("~/");
            }
            catch (Exception ex)
            {
                //TempData["errorMessage"] = ex.Message;
                return RedirectToAction("VolunteerApplication");
            }

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Controller for creating new volunteer with availability
        /// </summary>
        /// <param name="volunteerAvailability"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VolunteerApplication(Availability volunteerAvailability)
        {
            bool result = false;
            try
            {
                // add volunteer, set availability

                string currentUserName = User.Identity.GetUserName();
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                result = _volunteerApplicationsManager.CreateVolunteerApplication(userID, volunteerAvailability);

                TempData["successMessage"] = "Your application was successfully created. Please wait to be contacted about approval.";

                return Redirect("~/");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("VolunteerApplication");
            }
        }

 
    }
}
