using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
namespace DataAccessFakes
{
    public class EventAccessorFake : IEventAccessor
    {
        private List<EventVM> _fakeEvents = new List<EventVM>();
        private List<EventVM> _fakeEventVMs = new List<EventVM>();
        private List<fakeUserEvent> _fakeRolesForUsersForEvents = new List<fakeUserEvent>();
        private List<Location> _fakeLocations = new List<Location>();

        // This list only contains the values for the user ID first, and the EventID second
        private List<int[]> _fakeUserEvents = new List<int[]>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Constructor that adds fake events to _fakeEvents list for tesing purposes
        /// 
        /// </summary>
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Added fakeVM and UserEvent data 
        /// 
        /// Update:
        /// Jace Pettinger
        /// Created: 2022/02/15
        /// 
        /// Description:
        /// Removed location object from fake events
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// Derrick Nagy
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Added fake location data
        /// 
        public EventAccessorFake()
        {
            createFakeLocations();

               _fakeEvents.Add(new EventVM()
            {
                EventID = 1000000,
                LocationID= 100000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                TotalBudget = 1000.00m,
                Active = true,
                Location = _fakeLocations[0]
               
            });

            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000001,
                LocationID = 100001,
                EventName = "Test Event 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                TotalBudget = 120.00m,
                Active = true,
                Location = _fakeLocations[1]
            });

            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000002,
                LocationID = 100002,
                EventName = "Test Event 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                TotalBudget = 222.00m,
                Active = true,
                Location = _fakeLocations[2]
            });


            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000003,
                LocationID = 100003,
                EventName = "Test Event 4",
                EventDescription = "A description of test event 4",
                EventCreatedDate = DateTime.Now.AddMinutes(3),
                TotalBudget = 2938.00m,
                Active = true,
                Location = _fakeLocations[3]
            });


            // fake eventVM 1000000
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100000,
                EventName = "Test EventVM 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                TotalBudget = 1000.00m,
                Active = true,
                EventDates = new List<EventDate>(),
                Location = _fakeLocations[4]
            });

            // fake eventVM 100000 Dates
            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 01),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 02),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 03),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            // Fake VM 100001
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100001,
                EventName = "Test EventVM 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                TotalBudget = 120.00m,
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100001 PAST DATES            
            _fakeEventVMs[1].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 100001,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[1].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 100001,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            // Fake VM 100002
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100002,
                EventName = "Test EventVM 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                TotalBudget = 222.00m,
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100002 add date
            _fakeEventVMs[2].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 02, 01),
                EventID = 100002,
                StartTime = new DateTime(2023, 02, 01, 9, 0, 0),
                EndTime = new DateTime(2023, 02, 01, 17, 0, 0),
                Active = true
            });

            // Fake VM 100003
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100003,
                EventName = "Test EventVM 4",
                EventDescription = "A description of test event 4",
                EventCreatedDate = DateTime.Now.AddMinutes(3),
                TotalBudget = 2938.00m,
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100003 add date
            _fakeEventVMs[3].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 02, 01),
                EventID = 100003,
                StartTime = new DateTime(2023, 03, 01, 11, 0, 0),
                EndTime = new DateTime(2023, 03, 01, 11, 0, 0),
                Active = true
            });


            // fake Event User data
            // [UserID, EventID]
            _fakeUserEvents.Add(new int[2] { 100000, 100000 });
            _fakeUserEvents.Add(new int[2] { 100000, 100002 });
            _fakeUserEvents.Add(new int[2] { 100000, 100003 });

            // user 2 has 3 events, 2 upcoming and 1 in the past
            _fakeUserEvents.Add(new int[2] { 100001, 100000 });
            _fakeUserEvents.Add(new int[2] { 100001, 100001 });
            _fakeUserEvents.Add(new int[2] { 100001, 100002 });

            // UserID 100002 has 1 event in the past
            _fakeUserEvents.Add(new int[2] { 100002, 100001 });

            addFakeUserEventData();

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Insert fake event for testing
        /// 
        /// </summary>
        /// <param name="eventName">Name of the event</param>
        /// <param name="eventDescription">Description fo the event</param>
        /// <param name="totalBudget">Total budget planned for event</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEvent(string eventName, string eventDescription, decimal totalBudget)
        {
            int rowsAffected = 0;
            int eventID = _fakeEvents.Last().EventID + 1;


            _fakeEvents.Add(new EventVM()
            {
                EventID = eventID,
                EventName = eventName,
                EventDescription = eventDescription,
                EventCreatedDate = DateTime.Now,
                TotalBudget = totalBudget,
                Active = true
            });

            rowsAffected++;

            return rowsAffected;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Inserts a fake event into the database and returns the auto-increment value created for the event id
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventDescription"></param>
        /// <param name="totalBudget"></param>
        /// <returns></returns>
        public int InsertEventReturnsEventID(string eventName, string eventDescription, decimal totalBudget, int userID)
        {
            int eventID = _fakeEvents.Last().EventID + 1;

            _fakeEvents.Add(new EventVM()
            {
                EventID = eventID,
                EventName = eventName,
                EventDescription = eventDescription,
                EventCreatedDate = DateTime.Now,
                TotalBudget = totalBudget,
                Active = true,
                LocationID = null
            });

            return eventID;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Selects all active events in fake data list
        /// 
        /// </summary>
        /// <returns>List of active events</returns>
        public List<EventVM> SelectActiveEvents()
        {
            List<EventVM> events = new List<EventVM>();

            foreach (var fakeEvent in _fakeEvents)
            {
                if (fakeEvent.Active == true)
                {
                    events.Add(fakeEvent);
                }
            }

            return events;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Select fake event for testing
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventDescription"></param>
        /// <returns></returns>
        public EventVM SelectEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            EventVM fakeEvent = null;

            if (_fakeEvents.Exists(e => (e.EventName == eventName) && (e.EventDescription == eventDescription)))
            {
                fakeEvent = _fakeEvents.First(e => (e.EventName == eventName) && (e.EventDescription == eventDescription));
            }

            return fakeEvent;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Select fake past event view moodels for testing
        /// 
        /// </summary>
        /// <returns>Fake event view models</returns>
        public List<EventVM> SelectEventsPastDates()
        {
            List<EventVM> sortedEventVMList = new List<EventVM>();

            foreach (EventVM eventVM in _fakeEventVMs)
            {
                foreach (EventDate eventDate in eventVM.EventDates)
                {
                    if (eventDate.EventDateID < DateTime.Now && !sortedEventVMList.Contains(eventVM))
                    {
                        sortedEventVMList.Add(eventVM);
                    }
                }
            }

            return sortedEventVMList;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Select fake past and upcoming event view moodels for testing
        /// 
        /// </summary>
        /// <returns>Fake event view models</returns>
        public List<EventVM> SelectEventsUpcomingAndPastDates()
        {
            return _fakeEventVMs;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/06
        /// 
        /// Description:
        /// Select fake event view moodels for testing upcoming dates
        /// 
        /// </summary>
        /// <returns>Fake event view models</returns>
        public List<EventVM> SelectEventsUpcomingDates()
        {
            List<EventVM> sortedEventVMList = new List<EventVM>();

            foreach (EventVM eventVM in _fakeEventVMs)
            {
                foreach (EventDate eventDate in eventVM.EventDates)
                {
                    if (eventDate.EventDateID >= DateTime.Now && !sortedEventVMList.Contains(eventVM))
                    {
                        sortedEventVMList.Add(eventVM);
                    }
                }
            }

            return sortedEventVMList;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select fake past and upcoming event view moodels for a certain user for testing
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Fake event view models</returns>        
        public List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID)
        {
            List<int[]> eventIDs = _fakeUserEvents.FindAll(fakeUserEvent => fakeUserEvent[0] == userID);
            List<EventVM> events = new List<EventVM>();

            foreach (EventVM eventVM in _fakeEventVMs)
            {
                for (int i = 0; i < eventIDs.Count; i++)
                {
                    if (eventVM.EventID == eventIDs[i][1])
                    {
                        events.Add(eventVM);
                    }
                }
            }
            return events;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select fake past event view moodels for a certain user for testing
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Fake event view models</returns>        
        public List<EventVM> SelectUserEventsForPastDates(int userID)
        {
            List<int[]> eventIDs = _fakeUserEvents.FindAll(fakeUserEvent => fakeUserEvent[0] == userID);
            List<EventVM> events = new List<EventVM>();

            foreach (EventVM eventVM in _fakeEventVMs)
            {
                for (int i = 0; i < eventIDs.Count; i++)
                {
                    if (eventVM.EventID == eventIDs[i][1] && eventVM.EventDates[0].EventDateID <= DateTime.Now)
                    {
                        events.Add(eventVM);
                    }
                }
            }
            return events;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select fake upcoming event view moodels for a certain user for testing
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Fake event view models</returns>        
        public List<EventVM> SelectUserEventsForUpcomingDates(int userID)
        {
            List<int[]> eventIDs = _fakeUserEvents.FindAll(fakeUserEvent => fakeUserEvent[0] == userID);
            List<EventVM> events = new List<EventVM>();

            foreach (EventVM eventVM in _fakeEventVMs)
            {
                for (int i = 0; i < eventIDs.Count; i++)
                {
                    if (eventVM.EventID == eventIDs[i][1] && eventVM.EventDates[0].EventDateID >= DateTime.Now)
                    {
                        events.Add(eventVM);
                    }
                }
            }
            return events;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Updates an event in fake data list
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// </summary>
        /// <returns>int number of records affected</returns>
        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int rowsAffected = 0;

            foreach (var fakeEvent in _fakeEvents)
            {
                if (fakeEvent.EventID == newEvent.EventID && fakeEvent.EventName == oldEvent.EventName
                    && fakeEvent.EventDescription == oldEvent.EventDescription
                    && fakeEvent.TotalBudget == oldEvent.TotalBudget
                    && fakeEvent.Active == oldEvent.Active)
                {
                    fakeEvent.EventName = newEvent.EventName;
                    fakeEvent.EventDescription = newEvent.EventDescription;
                    fakeEvent.TotalBudget = newEvent.TotalBudget;
                    fakeEvent.Active = newEvent.Active;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Updates the location of a fake event record.
        /// </summary>
        /// Update:
        /// Jace Pettinger
        /// Created: 2022/02/15
        /// 
        /// Description:
        /// Removed location object and just used the locationId in the event object
        /// <param name="eventID">Event ID of the event</param>
        /// <param name="oldLocationID">ID of the old location</param>
        /// <param name="newLocationID">ID of the new location</param>
        /// <returns>int - rows affected</returns>
        public int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID)
        {
            int rowsAffected = 0;
            foreach (var fakeEvent in _fakeEvents)
            {
                if (fakeEvent.EventID == eventID && fakeEvent.LocationID == oldLocationID)
                {
                    fakeEvent.LocationID = (int)newLocationID;
                    rowsAffected++;
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Checks to see if the user has permission to edit the event
        /// 
        /// </summary>
        /// <param name="eventID">The event id</param>
        /// <param name="userID">The user id</param>
        /// <returns>True if the user can edit, false if not</returns>
        public bool CheckUserEditPermissionForEvent(int eventID, int userID)
        {
            bool result = false;

            foreach (var item in _fakeRolesForUsersForEvents)
            {
                if (item.EventID == eventID && item.UserID == userID && (item.RoleID == "Event Manager" || item.RoleID == "Event Planner"))
                {
                    result = true;
                    break;
                }
            }

            return result;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Adds fake events to _fakeEvents list for testing purposes
        /// 
        /// </summary>
        private void addFakeEvenVMs()
        {
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100000,
                EventName = "Test EventVM 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                Active = true,
                EventDates = new List<EventDate>()
            });

            // fake eventVM 100000 Dates
            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 01),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 02),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[0].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 01, 03),
                EventID = 100000,
                StartTime = new DateTime(2023, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 01, 20, 0, 0),
                Active = true
            });

            // Fake VM 100001
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100001,
                EventName = "Test EventVM 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100001 PAST DATES            
            _fakeEventVMs[1].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 100001,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventVMs[1].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 100001,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            // Fake VM 100002
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100002,
                EventName = "Test EventVM 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100002 add date
            _fakeEventVMs[2].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 02, 01),
                EventID = 100002,
                StartTime = new DateTime(2023, 02, 01, 9, 0, 0),
                EndTime = new DateTime(2023, 02, 01, 17, 0, 0),
                Active = true
            });

            // Fake VM 100003
            _fakeEventVMs.Add(new EventVM()
            {
                EventID = 100003,
                EventName = "Test EventVM 4",
                EventDescription = "A description of test event 4",
                EventCreatedDate = DateTime.Now.AddMinutes(3),
                Active = true,
                EventDates = new List<EventDate>()
            });

            // Fake VM 100003 add date
            _fakeEventVMs[3].EventDates.Add(new EventDate()
            {
                EventDateID = new DateTime(2023, 02, 01),
                EventID = 100003,
                StartTime = new DateTime(2023, 03, 01, 11, 0, 0),
                EndTime = new DateTime(2023, 03, 01, 11, 0, 0),
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Adds fake events to _fakeEventVMs list for testing purposes
        /// 
        /// </summary>
        private void addFakeEvents()
        {
            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000000,
                LocationID = 100000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                EventCreatedDate = DateTime.Now,
                Active = true
            });

            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000001,
                LocationID = 100001,
                EventName = "Test Event 2",
                EventDescription = "A description of test event 2",
                EventCreatedDate = DateTime.Now.AddMinutes(1),
                Active = true
            });

            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000002,
                LocationID = 100002,
                EventName = "Test Event 3",
                EventDescription = "A description of test event 3",
                EventCreatedDate = DateTime.Now.AddMinutes(2),
                Active = true
            });

            _fakeEvents.Add(new EventVM()
            {
                EventID = 1000003,
                LocationID = 100003,
                EventName = "Test Event 4",
                EventDescription = "A description of test event 4",
                EventCreatedDate = DateTime.Now.AddMinutes(3),
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Adds fake a fake list of objects that mocks the EventRoles table in the database
        /// 
        /// </summary>
        private void addFakeUserEventData()
        {
            _fakeRolesForUsersForEvents.Add(new fakeUserEvent()
            {
                EventID = 100000,
                UserID = 100000,
                RoleID = "Event Manager"
            });

            _fakeRolesForUsersForEvents.Add(new fakeUserEvent()
            {
                EventID = 100000,
                UserID = 100001,
                RoleID = "Event Planner"
            });

            _fakeRolesForUsersForEvents.Add(new fakeUserEvent()
            {
                EventID = 100000,
                UserID = 100002,
                RoleID = "Attendee"
            });

            _fakeRolesForUsersForEvents.Add(new fakeUserEvent()
            {
                EventID = 100001,
                UserID = 100002,
                RoleID = "Event Manager"
            });

            _fakeRolesForUsersForEvents.Add(new fakeUserEvent()
            {
                EventID = 100003,
                UserID = 100000,
                RoleID = "Attendee"
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Adds fake list of locations. Copied and pasted original code from 
        /// Kris Howell, who wrote it on 2022/01/27
        /// Original method in LocationAccessor Fakes
        /// 
        /// </summary>
        private void createFakeLocations()
        {
            _fakeLocations.Add(new Location()
            {
                LocationID = 100000,
                UserID = 100000,
                Name = "Test Location 1",
                Description = "Description of Test Location 1 goes here.",
                PricingInfo = "Pricing information for renting Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240",
                ImagePath = "http://imagehost.com/testlocation1.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100001,
                UserID = 100000,
                Name = "Test Location 2",
                Description = "Description of Test Location 2 goes here.",
                PricingInfo = "Pricing information for renting Test Location 2 goes here.",
                Phone = "222-222-2222",
                Email = "testLocation2@locations.com",
                Address1 = "Test Location 2 Street",
                Address2 = "Apt 2",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                ImagePath = "http://imagehost.com/testlocation2.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100002,
                UserID = 100000,
                Name = "Test Location 3",
                Description = "Description of Test Location 3 goes here.",
                PricingInfo = "Pricing information for renting Test Location 3 goes here.",
                Phone = "333-333-3333",
                Email = "testLocation3@locations.com",
                Address1 = "Test Location 3 Street",
                Address2 = "Apt 33",
                City = "Chicago",
                State = "Illinois",
                ZipCode = "60007",
                ImagePath = "http://imagehost.com/testlocation3.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100003,
                UserID = 100000,
                Name = "Test Location 4",
                Description = "Description of Test Location 4 goes here.",
                PricingInfo = "Pricing information for renting Test Location 4 goes here.",
                Phone = "444-444-4444",
                Email = "testLocation4@locations.com",
                Address1 = "Test Location 4 Street",
                Address2 = "Apt 44",
                City = "New York City",
                State = "New York",
                ZipCode = "10036",
                ImagePath = "http://imagehost.com/testlocation4.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100004,
                UserID = 100000,
                Name = "Test Location 5 Inactive",
                Description = "Description of Inactive Test Location 5 goes here.",
                PricingInfo = "Pricing information for renting inactive Test Location 5 goes here.",
                Phone = "555-555-5555",
                Email = "testLocation5@locations.com",
                Address1 = "Test Location 5 Street",
                Address2 = "Apt 55",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                ImagePath = "http://imagehost.com/testlocation5.png",
                Active = false
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100005,
                Name = "Test Location 6 Min Info",
                Address1 = "Test Location 6 Street",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                Active = true
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// 2022/03/24
        /// 
        /// Description:
        /// Fake method for testing if it returns Users with the event planner role
        /// 
        /// </summary>
        /// <param name="eventID">The Event ID</param>
        /// <returns></returns>
        public List<User> SelectEventPlannersForEvent(int eventID)
        {
            List<User> users = new List<User>();

            foreach (var item in _fakeRolesForUsersForEvents)
            {
                if (item.EventID == eventID && item.RoleID == "Event Planner")
                {
                    users.Add(new User()
                    {
                        UserID = item.UserID
                    });
                }
            }

            return users;



        }
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/01
        /// 
        /// Description: returns fake event with the correct id
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public EventVM SelectEventByEventID(int eventID)
        {
            EventVM fakeEvent = null;

            if (_fakeEvents.Exists(e => (e.EventID == eventID)))
            {
                fakeEvent = _fakeEvents.First(e => (e.EventID == eventID));
            }

            return fakeEvent;
        }

        /// <summary>
        /// Derrick Nagy
        /// 2022/04/06
        /// 
        /// Description:
        /// Fake method for testing if it returns a list that includes the search word
        /// in the event name, description, or in the location name, city, or state
        /// 
        /// </summary>
        /// <param name="search">Search criteria</param>
        /// <returns>List of EventVMs that contain search criteria</returns>
        public List<EventVM> SelectEventsForSearch(string search)
        {
            List<EventVM> events = new List<EventVM>();

            search = search.ToLower();

            foreach (EventVM eventV in _fakeEvents)
            {
                eventV.EventName = eventV.EventName.ToLower();
                eventV.EventDescription = eventV.EventDescription.ToLower();
                eventV.Location.Name = eventV.Location.Name.ToLower();
                eventV.Location.City = eventV.Location.City.ToLower();
                eventV.Location.State = eventV.Location.State.ToLower();

                if (eventV.EventName.Contains(search) ||
                    eventV.EventDescription.Contains(search) ||
                    eventV.Location.Name.Contains(search) ||
                    eventV.Location.City.Contains(search) ||
                    eventV.Location.State.Contains(search))
                {
                    events.Add(eventV);
                }
            }

            return events;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Searches for an eventVM with the given eventID, and deactivates it
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>number of events deactivated</returns>
        public int DeactivateEventByEventID(int eventID)
        {
            int rowsAffected = 0;

            foreach (EventVM eventVM in _fakeEvents)
            {
                if (eventVM.EventID == eventID)
                {
                    eventVM.Active = false;
                    rowsAffected++;
                    break;
                }
            }

            return rowsAffected;
        }
    }


    internal class fakeUserEvent
    {
        internal int EventID { get; set; }
        internal int UserID { get; set; }
        internal string RoleID { get; set; }
    }
}
