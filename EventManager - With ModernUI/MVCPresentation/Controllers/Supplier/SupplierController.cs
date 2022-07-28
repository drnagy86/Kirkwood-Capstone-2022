using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using WPFPresentation;
using DataAccessInterfaces;
using DataAccessFakes;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class SupplierController : Controller
    {
        ISupplierManager _supplierManager = null;
        IActivityManager _activityManager = null;
        IServiceManager _serviceManager = null;
        IUserManager _userManager;
        IEmailProvider _emailProvider;
        SupplierScheduleViewModel _supplierSchedule = new SupplierScheduleViewModel();
        public int _pageSize = 10;


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// Default constructor for the Supplier controller
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/27
        /// 
        /// Added IUserManager
        /// </update>
        /// </summary>
        public SupplierController(ISupplierManager supplierManager, IActivityManager activityManager, IServiceManager serviceManager, IUserManager userManager, IEmailProvider emailProvider)

        {
            _supplierManager = supplierManager;
            _activityManager = activityManager;
            _serviceManager = serviceManager;
            _userManager = userManager;
            _emailProvider = emailProvider;
        }

        public PartialViewResult SupplierNav(int eventId)
        {
            return PartialView(eventId);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// For the View Suppliers page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSuppliers(int page = 1)
        {
            List<Supplier> _suppliers = new List<Supplier>();
            List<Reviews> _supplierReviews = new List<Reviews>();
            SupplierListViewModel _model = null;

            if (_suppliers is null || _suppliers.Count == 0)
            {
                try
                {
                    _suppliers = _supplierManager.RetrieveActiveSuppliers();
                    foreach (Supplier supplier in _suppliers)
                    {
                        int avg = 0;
                        int total = 0;
                        _supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplier.SupplierID);
                        if (_supplierReviews.Count != 0)
                        {
                            foreach (Reviews review in _supplierReviews)
                            {
                                avg += review.Rating;
                                total++;
                            }
                            int sum = avg / total;
                            supplier.AverageRating = sum;
                        }
                    }
                    _model = new SupplierListViewModel()
                    {
                        Suppliers = _suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = _suppliers.Count()
                        }
                    };
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                }
            }
            return View("ViewSuppliers", _model);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// For the View Supplier Schedule page
        /// 
        /// Logan Baccam
        /// Updated: 2022/04/14
        /// Description:
        /// Changed parameter from Supplier object
        /// to supplierID and changed Model type to
        /// SupplierDetailsViewModel
        /// 
        /// 
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierSchedule(int supplierID = 0)
        {
            //Request.Params["supplier"];
            if (supplierID == 0)
            {

                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            Supplier supplier = new Supplier();
            supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);

            _supplierSchedule.Supplier = supplier;
            GetAvailability(_supplierSchedule.Supplier.SupplierID);

            return View("~/Views/Supplier/ViewSupplierSchedule.cshtml", _supplierSchedule);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting activities by the supplier id passed to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult GetActivities(string id)
        {
            int supplierID;
            try
            {
                supplierID = int.Parse(id);
            }
            catch (Exception)
            {

                supplierID = 0;
            }
            List<Activity> activities;
            if (supplierID == 0)
            {
                activities = new List<Activity>();
            }
            else
            {
                activities = _activityManager.RetrieveActivitiesBySupplierID(supplierID);
            }
            return new JsonResult { Data = activities, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// For getting availability by the location id passed to it and sets it to the 
        /// _supplierSchedule
        /// 
        /// Logan Baccam 
        /// </summary>
        /// <param name="id"></param>
        public void GetAvailability(int supplierID = 0)
        {

            List<AvailabilityVM> availability;
            List<Availability> availabilityException;
            if (supplierID == 0)
            {
                availability = new List<AvailabilityVM>();
            }
            else
            {
                availability = _supplierManager.RetrieveSupplierAvailabilityBySupplierID(supplierID);
            }
            if (supplierID == 0)
            {
                availabilityException = new List<Availability>();
            }
            else
            {
                availabilityException = _supplierManager.RetrieveSupplierAvailabilityExceptionBySupplierID(supplierID);
            }

            _supplierSchedule.Availability = availability;
            _supplierSchedule.AvailabilityException = availabilityException;
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/02
        /// 
        /// Description:
        /// For the Supplier details page
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/14
        /// Fixed some issues with wrong comparators, also made tags queried from database
        /// </summary>
        /// <param name="page"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ViewSupplierDetails(int supplierID = 0)
        {
            SupplierDetailsViewModel model = new SupplierDetailsViewModel();
            Supplier supplier = new Supplier();
            List<string> supplierImages = new List<string>();
            List<Reviews> supplierReviews = new List<Reviews>();
            List<string> supplierTags = new List<string>();
            model.CanEdit = false;
            if (supplierID == 0)
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            try
            {
                supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                supplierImages = _supplierManager.RetrieveSupplierImagesBySupplierID(supplierID);
                supplierTags = _supplierManager.RetrieveSupplierTagsBySupplierID(supplierID);
                supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplierID);
                if (supplierImages.Count == 0 || supplierImages is null)
                {
                    supplierImages.Add("");
                }
                if (supplierTags.Count == 0)
                {
                    supplierTags.Add("");
                }
                if (supplierReviews.Count == 0)
                {
                    supplierReviews.Add(new Reviews()
                    {
                        Rating = 0,
                        FullName = ""
                    });
                }
                int sum = 0;
                int total = 0;

                foreach (Reviews review in supplierReviews)
                {
                    sum += review.Rating;
                    total++;
                }
                int avg = sum / total;
                supplier.AverageRating = avg;


                model = new SupplierDetailsViewModel();
                model.Supplier = supplier;
                model.SupplierImages = supplierImages;
                model.SupplierReviews = supplierReviews;
                model.SupplierTags = supplierTags;

                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser applicationUser = userManager.FindById(User.Identity.GetUserId());
                if (applicationUser != null && applicationUser.UserID == supplier.UserID)
                {
                    model.CanEdit = true;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Supplier not found. Please refresh the page and try again.");
                return this.ViewSuppliers();
            }
            return View("ViewSupplierDetails", model);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// For getting the supplier services page
        /// </summary>
        /// <param name="supplier"></param>
        public ActionResult ViewSupplierServices(int supplierID = 0)
        {
            if (supplierID == 0)
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }
            SupplierServicesViewModel model = new SupplierServicesViewModel();
            List<Service> services = new List<Service>();
            model.CanEdit = false;
            try
            {
                services = _serviceManager.RetrieveServicesBySupplierID(supplierID);
                model.Supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                List<ServiceVM> serviceVMs = new List<ServiceVM>();
                foreach (Service service in services)
                {
                    serviceVMs.Add(new ServiceVM()
                    {
                        ServiceID = service.ServiceID,
                        SupplierID = service.SupplierID,
                        ServiceName = service.ServiceName,
                        Price = service.Price,
                        Description = service.Description,
                        ServiceImagePath = service.ServiceImagePath
                    });
                }
                model.Services = serviceVMs;
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser applicationUser = userManager.FindById(User.Identity.GetUserId());
                if (applicationUser != null && applicationUser.UserID == model.Supplier.UserID)
                {
                    model.CanEdit = true;
                }
            } catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            


            return View("ViewSupplierServices", model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler to view supplier applications awaiting approval
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult ViewSupplierApplications(int page = 1)
        {
            List<Supplier> suppliers = new List<Supplier>();
            SupplierListViewModel model;

            try
            {
                suppliers = _supplierManager.RetrieveUnapprovedSuppliers();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            model = new SupplierListViewModel()
            {
                Suppliers = suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = suppliers.Count()
                }
            };
            return View("ViewSupplierApplications", model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler for approving supplier requests. 
        /// </summary>
        /// <param name="supplierID">ID of supplier being approved</param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Approve(int supplierID)
        {
            try
            {
                _supplierManager.ApproveSupplier(supplierID);
                Supplier supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);

                // This is messy, but there isn't much of a better way.
                User desktopUser = _userManager.RetrieveUserByUserID(supplier.UserID ?? 0);
                if(desktopUser != null)
                {
                    _userManager.AddUserRole(supplier.UserID ?? 0, "Supplier");
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    ApplicationUser user = userManager.FindByEmail(desktopUser.EmailAddress);
                    if(user != null)
                    {
                        userManager.AddToRole(user.Id, "Supplier");
                    }
                }
                _emailProvider.SendEmail("Supplier Application", "Your supplier request has been approved and added to the supplier listing.", supplier.Email);
            } catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return ViewSupplierApplications();
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler for denying supplier requests
        /// </summary>
        /// <param name="supplierID">ID of supplier being denied</param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Deny(int supplierID)
        {
            try
            {
                _supplierManager.DisapproveSupplier(supplierID);
                Supplier supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                _emailProvider.SendEmail("Supplier Application", "Your supplier request has been denied. You can find the application in your user profile. Please review the information entered for accuracy and fix any mistakes.", supplier.Email);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return ViewSupplierApplications();
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// For getting to the CreateSupplier page
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult CreateSupplier()
        {
            return View();
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// Method for creating a new Supplier
        /// <param name="_supplier"/>
        /// </summary>
        [Authorize]
        [HttpPost]
        public ActionResult CreateSupplier(Supplier supplier)
        {
            SupplierDetailsViewModel model = new SupplierDetailsViewModel();
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (supplier.UserID == null)
                    {
                        var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                        supplier.UserID = user.UserID;
                    }
                    if (_supplierManager.CreateSupplier(supplier) == 1)
                    {
                        model.Supplier = _supplierManager.RetrieveUnapprovedSuppliers().Single(x => x.Name == supplier.Name && x.Description == supplier.Description);
                        TempData["Message"] = "success";
                        return RedirectToAction("ViewSupplierDetails", new { supplierID = model.Supplier.SupplierID });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler for supplier service editting
        /// </summary>
        /// <param name="supplierID">ID of supplier containing the service</param>
        /// <param name="serviceID">ID of service.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSupplierService(int supplierID, int serviceID)
        {
            SupplierServiceEditModel model = new SupplierServiceEditModel();
            try
            {
                Service service = _serviceManager.RetrieveServiceByServiceID(serviceID);
                model = new SupplierServiceEditModel()
                {
                    Description = service.Description,
                    NewDescription = service.Description,
                    ServiceName = service.ServiceName,
                    NewName = service.ServiceName,
                    Price = service.Price,
                    NewPrice = service.Price,
                    ServiceID = serviceID,
                    ServiceImagePath = service.ServiceImagePath,
                    SupplierID = service.SupplierID
                };
            } catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model); 
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler for supplier service creation
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateSupplierService(int supplierID)
        {
            SupplierServiceEditModel model = new SupplierServiceEditModel();
            try
            {
                model = new SupplierServiceEditModel()
                {
                    Description = "",
                    NewDescription = "",
                    ServiceName = "",
                    NewName = "",
                    Price = 0.0m,
                    NewPrice = 0.0m,
                    ServiceID = -1,
                    ServiceImagePath = "",
                    SupplierID = supplierID
                };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("EditSupplierService", model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// post handler for supplier service editting
        /// </summary>
        /// <param name="model">model containing edit/create data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSupplierService(SupplierServiceEditModel model)
        {
            if(ModelState.IsValid)
            {

                try
                {
                    string newImageName = model.ServiceImagePath;
                    if (model.NewImage != null)
                    {
                        string uuid = Guid.NewGuid().ToString();
                        model.NewImage.SaveAs(Server.MapPath("~/Content/Images/LocationImages/") + uuid + Path.GetExtension(model.NewImage.FileName));
                        newImageName = uuid + Path.GetExtension(model.NewImage.FileName);
                    }
                    Service newService = new Service()
                    {
                        Description = model.NewDescription,
                        Price = model.NewPrice,
                        ServiceID = model.ServiceID,
                        SupplierID = model.SupplierID,
                        ServiceImagePath = newImageName,
                        ServiceName = model.NewName
                    };
                    if (model.ServiceID == -1)
                    {
                        if (_serviceManager.CreateService(newService))
                        {
                            return RedirectToAction("ViewSupplierServices", new { supplierID = model.SupplierID });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to create service.");
                        }
                    }
                    else
                    {
                        Service oldService = new Service()
                        {
                            Description = model.Description,
                            Price = model.Price,
                            ServiceID = model.ServiceID,
                            ServiceImagePath = model.ServiceImagePath,
                            ServiceName = model.ServiceName,
                            SupplierID = model.SupplierID
                        };
                        if (_serviceManager.EditService(oldService, newService))
                        {
                            return RedirectToAction("ViewSupplierServices", new { supplierID = model.SupplierID });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to update service.");
                        }
                    }
                } catch(Exception ex) {

                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Post handler for supplier service Deletion
        /// </summary>
        /// <param name="serviceID">Id of service to delete</param>
        /// <param name="supplierID">ID of supplier to return to</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteService(int serviceID, int supplierID)
        {
            try
            {
                bool result = _serviceManager.DeleteService(serviceID);
            } catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return this.ViewSupplierServices(supplierID);
            }
            return RedirectToAction("ViewSupplierServices", new { supplierID = supplierID });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Moves user to the Create Review page
        /// </summary>
        /// <param name="supplierID"></param>
        [Authorize(Roles = "Event Planner")]
        [HttpGet]
        public ViewResult CreateReview(int supplierID = 0)
        {
            if(supplierID == 0)
            {
                return View("ViewSuppliers", supplierID);
            }
            return View(new Reviews() { ForeignID = supplierID });
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
            Reviews review = new Reviews() {ForeignID = ForeignID, UserID = user.UserID, Rating = Rating, Review = Review, ReviewType = "Supplier", DateCreated = DateTime.Now}; 
            if (ModelState.IsValid)
            {
                try
                {
                    _supplierManager.CreateSupplierReview(review);
                } catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(review);
                }
                return RedirectToAction("ViewSuppliers", "Supplier", review.ForeignID);
            }
            return View(review);    
        }

        /// <summary>        
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description
        /// Get handler for ViewUserSuppliers view
        /// </summary>
        /// <param name="userID">ID of user to use for lookup</param>
        /// <param name="page">what page of results to show.</param>
        /// <returns>The ViewSuppliers view with the suppliers related to a specific user loaded.</returns>
        [HttpGet]
        public ActionResult ViewUserSuppliers(int userID, int page=1)
        {
            List<Supplier> _suppliers = new List<Supplier>();
            List<Reviews> _supplierReviews = new List<Reviews>();
            SupplierListViewModel _model = null;

            if (_suppliers is null || _suppliers.Count == 0)
            {
                try
                {
                    _suppliers = _supplierManager.RetrieveSuppliersByUserID(userID);
                    foreach (Supplier supplier in _suppliers)
                    {
                        int avg = 0;
                        int total = 0;
                        _supplierReviews = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplier.SupplierID);
                        if (_supplierReviews.Count != 0)
                        {
                            foreach (Reviews review in _supplierReviews)
                            {
                                avg += review.Rating;
                                total++;
                            }
                            int sum = avg / total;
                            supplier.AverageRating = sum;
                        }
                    }
                    _model = new SupplierListViewModel()
                    {
                        Suppliers = _suppliers.OrderBy(x => x.SupplierID)
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),
                        PagingInfo = new PagingInfo()
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = _suppliers.Count()
                        }
                    };
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("ViewSuppliers", _model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Get handler for editting suppliers
        /// </summary>
        /// <param name="supplierID">ID of supplier to edit.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSupplier(int supplierID)
        {
            EditSupplierModel model = new EditSupplierModel();
            try
            {
                Supplier supplier = _supplierManager.RetrieveSupplierBySupplierID(supplierID);
                model.Active = supplier.Active;
                model.Address1 = supplier.Address1;
                model.Address2 = supplier.Address2;
                model.Approved = supplier.Approved;
                model.City = supplier.City;
                model.Description = supplier.Description;
                model.Email = supplier.Email;
                model.Name = supplier.Name;
                model.Phone = supplier.Phone;
                model.State = supplier.State;
                model.SupplierID = supplier.SupplierID;
                model.UserID = supplier.UserID;
                model.ZipCode = supplier.ZipCode;

                model.NewAddress1 = supplier.Address1;
                model.NewAddress2 = supplier.Address2;
                model.NewCity = supplier.City;
                model.NewDescription = supplier.Description;
                model.NewEmail = supplier.Email;
                model.NewName = supplier.Name;
                model.NewPhone = supplier.Phone;
                model.NewState = supplier.State;
                model.NewZipCode = supplier.ZipCode;
            } catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return this.ViewSupplierDetails(supplierID);
            }
            return View(model);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Post handler for editting suppliers
        /// </summary>
        /// <param name="model">Model containing edit information</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSupplier(EditSupplierModel model)
        {
            if(ModelState.IsValid)
            {
                Supplier oldSupplier = new Supplier()
                {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    Description = model.Description,
                    Email = model.Email,
                    Name = model.Name,
                    Phone = model.Phone,
                    State = model.State,
                    SupplierID = model.SupplierID,
                    UserID = model.UserID,
                    ZipCode = model.ZipCode
                };
                Supplier newSupplier = new Supplier()
                {
                    Address1 = model.NewAddress1,
                    Address2 = model.NewAddress2,
                    City = model.NewCity,
                    Description = model.NewDescription,
                    Email = model.NewEmail,
                    Name = model.NewName,
                    Phone = model.NewPhone,
                    State = model.NewState,
                    SupplierID = model.SupplierID,
                    UserID = model.UserID,
                    ZipCode = model.NewZipCode
                };
                try
                {
                    _supplierManager.EditSupplier(oldSupplier, newSupplier);
                    return RedirectToAction("ViewSupplierDetails", new { supplierID = model.SupplierID });
                } catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Failed to update supplier. Please try again later.");
            return View(model);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// GET method for add supplier availability form.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public ActionResult AddAvailability(int supplierID = 0)
        {
            if (supplierID == 0)
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }

            ViewBag.SupplierID = supplierID;
            return View();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// POST method to attempt to add a new supplier availability
        /// with form data
        /// </summary>
        /// <param name="add">new availability object to insert</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAvailability(Availability add)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _supplierManager.CreateSupplierAvailability(add);
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    ViewBag.SupplierID = add.ForeignID;
                    return View("AddAvailability", add);
                }
            }

            if (add.ForeignID != 0)
            {
                return RedirectToAction("ViewSupplierSchedule", "Supplier", new { supplierID = add.ForeignID });
            }
            else
            {
                return RedirectToAction("ViewSuppliers", "Supplier");
            }
        }
    }
}