using DataObjects;
using LogicLayerInterfaces;
using MVCPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers
{
    public class HomeController : Controller
    {
        private List<EventVM> _events;
        IEventManager _eventManager;
        IEventDateManager _eventDateManager;
        ILocationManager _locationManager;
        /// <summary>
        /// Vinayak Deshpande
        /// Updated: 2022/04/17
        /// 
        /// Description: Added Functionality for viewing all active events on the home page.
        /// </summary>
        /// <param name="eventManager"></param>
        /// <param name="eventDateManager"></param>
        /// <param name="locationManager"></param>
        public HomeController(IEventManager eventManager, IEventDateManager eventDateManager, ILocationManager locationManager)
        {
            _eventManager = eventManager;
            _eventDateManager = eventDateManager;
            _locationManager = locationManager;
            //_events = _eventManager.RetreieveActiveEvents();
            _events = _eventManager.RetrieveEventListForUpcomingDates();
            foreach (var eventVM in _events)
            {
                List<EventDate> dates = _eventDateManager.RetrieveEventDatesByEventID(eventVM.EventID);
                eventVM.EventDates = dates != null ? dates : new List<EventDate>();
                if (eventVM.LocationID != null)
                {
                    eventVM.Location = _locationManager.RetrieveLocationByLocationID((int)eventVM.LocationID);
                }
            }
        }
        public ActionResult Index()
        {
            return View(_events);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Creates a list of all the devs and then randomizes it
        /// before sending user to the about page which displays the list.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            List<DeveloperViewModel> devs = new List<DeveloperViewModel>();
            DeveloperViewModel drnagy = new DeveloperViewModel
            {
                Name = "Derrick Nagy",
                GitHubName = "drnagy86",
                GitHubLink = "https://github.com/drnagy86",
                DevImageName = "~/Content/Images/drnagy86.jpg"
            };
            devs.Add(drnagy);
            DeveloperViewModel chrepko = new DeveloperViewModel
            {
                Name = "Chris Repko",
                GitHubName = "chrepko",
                GitHubLink = "https://github.com/chrepko",
                DevImageName = "~/Content/Images/chrepko.png"
            };
            devs.Add(chrepko);
            DeveloperViewModel neiftly = new DeveloperViewModel
            {
                Name = "Vinayak Deshpande",
                GitHubName = "Neiftly",
                GitHubLink = "https://github.com/Neiftly",
                DevImageName = "~/Content/Images/Neiftly.png"
            };
            devs.Add(neiftly);
            DeveloperViewModel jacePettinger = new DeveloperViewModel
            {
                Name = "Jace Pettinger",
                GitHubName = "JacePettinger",
                GitHubLink = "https://github.com/JacePettinger",
                DevImageName = "~/Content/Images/JacePettinger.jpg"
            };
            devs.Add(jacePettinger);
            DeveloperViewModel loganb7 = new DeveloperViewModel
            {
                Name = "Logan Baccam",
                GitHubName = "loganb7",
                GitHubLink = "https://github.com/loganb7",
                DevImageName = "~/Content/Images/loganb7.png"
            };
            devs.Add(loganb7);
            DeveloperViewModel mikeCahow = new DeveloperViewModel
            {
                Name = "Mike Cahow",
                GitHubName = "MikeCahow",
                GitHubLink = "https://github.com/MikeCahow",
                DevImageName = "~/Content/Images/MikeCahow.png"
            };
            devs.Add(mikeCahow);
            DeveloperViewModel khowell = new DeveloperViewModel
            {
                Name = "Kris Howell",
                GitHubName = "k-howell",
                GitHubLink = "https://github.com/k-howell",
                DevImageName = "~/Content/Images/k-howell.png"
            };
            devs.Add(khowell);
            DeveloperViewModel emPollock = new DeveloperViewModel
            {
                Name = "Emma Pollock",
                GitHubName = "EmPollock",
                GitHubLink = "https://github.com/EmPollock",
                DevImageName = "~/Content/Images/EmPollock.jpg"
            };
            devs.Add(emPollock);
            DeveloperViewModel austinTimmerman = new DeveloperViewModel
            {
                Name = "Austin Timmerman",
                GitHubName = "AustinTimmerman",
                GitHubLink = "https://github.com/AustinTimmerman",
                DevImageName = "~/Content/Images/AustinTimmerman.jpg"
            };
            devs.Add(austinTimmerman);
            DeveloperViewModel technolationWC = new DeveloperViewModel
            {
                Name = "Alaina Gilson",
                GitHubName = "TechnolationWC",
                GitHubLink = "https://github.com/TechnolationWC",
                DevImageName = "~/Content/Images/TechnolationWC.png"
            };
            devs.Add(technolationWC);
            Random random = new Random();
            var randomizedList = new List<DeveloperViewModel>();
            while (devs.Count != 0)
            {
                var index = random.Next(0, devs.Count);
                randomizedList.Add(devs[index]);
                devs.RemoveAt(index);
            }
            return View(randomizedList);
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}