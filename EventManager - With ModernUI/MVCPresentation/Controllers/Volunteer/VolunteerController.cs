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
using Microsoft.AspNet.Identity;

namespace MVCPresentation.Controllers
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/04/04
    /// 
    /// Interaction logic for VolunteerController
    /// </summary>
    public class VolunteerController : Controller
    {
        IVolunteerManager _volunteerManager;
        IVolunteerRequestManager _volunteerRequestManager;
        IUserManager _userManager;
        IVolunteerReviewManager _volunteerReviewManager;
        public int _pageSize = 10;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Constructor that sets the _volunteerManager
        /// </summary>
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/28
        /// 
        /// Added IVolunteerReviewManager
        /// </update>
        /// 
        /// <param name="locationManager"></param>
        public VolunteerController(IVolunteerManager volunteerManager, IVolunteerRequestManager volunteerRequestManager, IUserManager userManager, IVolunteerReviewManager volunteerReviewManager)
        {
            _volunteerManager = volunteerManager;
            _volunteerRequestManager = volunteerRequestManager;
            _userManager = userManager;
            _volunteerReviewManager = volunteerReviewManager;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/14
        /// 
        /// Description
        /// Controller passthrough for the navbar
        /// </summary>
        /// <param name="volunteerID">ID of volunteer so that the navbar can navigate properly</param>
        /// <returns></returns>
        public PartialViewResult VolunteerNavBar(int volunteerID)
        {
            return PartialView(volunteerID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// For the View Volunteers page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewVolunteers(int page = 1)
        {
            List<DataObjects.Volunteer> volunteers = new List<DataObjects.Volunteer>();
            List<DataObjects.Volunteer> volunteerReviews = new List<DataObjects.Volunteer>();
            VolunteerListViewModel model = null;
            try
            {
                volunteers = _volunteerManager.RetrieveAllVolunteers();
                volunteerReviews = _volunteerManager.RetrieveAllVolunteerReviews();
            }
            catch (Exception ex)
            {
                return View();
            }

            for (int i = 0; i < volunteers.Count; i++)
            {
                for (int j = 0; j <= volunteerReviews.Count; j++)
                {
                    if (j == volunteerReviews.Count)
                    {
                        volunteers[i].Rating = 0;
                        break;
                    }

                    if (volunteers[i].VolunteerID == volunteerReviews[j].VolunteerID)
                    {
                        volunteers[i].Rating = volunteerReviews[j].Rating;
                        break;
                    }
                }

            }

            model = new VolunteerListViewModel
            {
                Volunteers = volunteers
                            .OrderBy(p => p.VolunteerID)
                            .Skip((page - 1) * _pageSize)
                            .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = volunteers.Count()
                }
            };

            return View(model);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/30
        /// 
        /// Description:
        ///     Sends user to a list of their incoming volunteer requests if they are logged in.
        /// </summary>
        /// 
        /// <returns>ActionResult</returns>
        [Authorize]
        public ActionResult ViewRequests()
        {          
            IEnumerable<VolunteerRequestViewModel> requestViewModels;
            string currentUserName = User.Identity.GetUserName();

            try
            {                
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                DataObjects.Volunteer volunteer = _volunteerManager.RetrieveVolunteerByUserID(userID);
                requestViewModels = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(volunteer.VolunteerID);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                requestViewModels = new List<VolunteerRequestViewModel>();
            }

            return View(requestViewModels);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/31
        /// 
        /// Description:
        ///     Updates the volunteer approval for a volunteer request if they are logged in and their 
        ///         volunteerID matches the incoming volunteerID
        /// </summary>
        /// 
        /// <param name="id">requestID of the request to be updated</param>
        /// <param name="approve">new volunteer approval value</param>
        /// <param name="volunteerID">the volunteer's id</param>
        /// <returns>ActionResult</returns>
        public ActionResult Approve(int id, bool approve, int volunteerID)
        {
            string currentUserName = User.Identity.GetUserName();
            IEnumerable<VolunteerRequestViewModel> requestViewModels;
            try
            {
                int userID = _userManager.RetrieveUserByEmail(currentUserName).UserID;
                DataObjects.Volunteer currentVolunteer = _volunteerManager.RetrieveVolunteerByUserID(userID);
                VolunteerRequestViewModel oldRequest = _volunteerRequestManager.RetrieveRequestByRequestID(id);
                VolunteerRequestViewModel newRequest = _volunteerRequestManager.RetrieveRequestByRequestID(id);
                if (currentVolunteer.VolunteerID == volunteerID && oldRequest.VolunteerID == volunteerID)
                {                    
                    newRequest.VolunteerResponse = approve;

                    bool success = _volunteerRequestManager.EditVolunteerRequest(oldRequest, newRequest);
                }
                requestViewModels = _volunteerRequestManager.RetrieveAllRequestsForVolunteerByVolunteerID(currentVolunteer.VolunteerID);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                requestViewModels = new List<VolunteerRequestViewModel>();
            }
            return View("ViewRequests", requestViewModels);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Moves user to the Create Review page
        /// </summary>
        /// <param name="volunteerID"></param>
        [Authorize(Roles = "Event Planner")]
        [HttpGet]
        public ViewResult CreateReview(int volunteerID = 0)
        {
            if (volunteerID == 0)
            {
                return View("ViewVolunteers", volunteerID);
            }
            return View(new Reviews() { ForeignID = volunteerID });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
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
            Reviews review = new Reviews() { ForeignID = ForeignID, UserID = user.UserID, Rating = Rating, Review = Review, ReviewType = "Volunteer", DateCreated = DateTime.Now };
            if (ModelState.IsValid)
            {
                try
                {
                    _volunteerReviewManager.CreateVolunteerReview(review);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(review);
                }
                return RedirectToAction("ViewVolunteers", "Volunteer", review.ForeignID);
            }
            return View(review);
        }    

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Action result for viewing a specific volunteer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult VolunteerDetails(string id)
        {
            if (id == null || id == "")
            {
                return RedirectToAction("ViewVolunteers");
            }
            List<DataObjects.Volunteer> volunteers = new List<DataObjects.Volunteer>();
            List<DataObjects.Volunteer> volunteerReviews = new List<DataObjects.Volunteer>();
            DataObjects.Volunteer selectedVolunteer = null;
            try
            {
                int volunteerID = int.Parse(id);
                volunteers = _volunteerManager.RetrieveAllVolunteers();
                volunteerReviews = _volunteerManager.RetrieveAllVolunteerReviews();
                foreach (var volunteer in volunteers)
                {
                    if (volunteer.UserID == volunteerID)
                    {
                        selectedVolunteer = volunteer;
                    }
                }
                if (selectedVolunteer == null)
                {
                    return RedirectToAction("ViewVolunteers");
                }
                foreach (var review in volunteerReviews)
                {
                    if (review.VolunteerID == selectedVolunteer.VolunteerID)
                    {
                        selectedVolunteer.Rating = review.Rating;
                    }
                }
            }
            catch (Exception)
            {
                // don't crash, just go back
                return RedirectToAction("ViewVolunteers");
            }
            return View(selectedVolunteer);
        }
    }
}   