using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;


namespace DataAccessFakes
{
    public class EventDateAccessorFake : IEventDateAccessor
    {
        private List<EventDate> _fakeEventDate = new List<EventDate>();
        private List<EventDateVM> _fakeEventDatesForLocation = new List<EventDateVM>();
        private List<TasksVM> _fakeTasks = new List<TasksVM>();
        private List<TaskAssignmentVM> _taskAssignments = new List<TaskAssignmentVM>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Constructor with fake data for testing being added to _fakeEventDates
        /// 
        /// </summary>
        public EventDateAccessorFake()
        {
            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 02),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 03),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });


            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 2,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDate.Add(new EventDate()
            {
                EventDateID = new DateTime(2022, 06, 01),
                EventID = 3,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDatesForLocation.Add(new EventDateVM()
            {
                EventDateID = new DateTime(2022, 06, 01),
                EventName = "Fake Event Number 1",
                LocationID = 100000,
                EventID = 3,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDatesForLocation.Add(new EventDateVM()
            {
                EventDateID = new DateTime(2022, 06, 02),
                EventName = "Fake Event Number 2",
                LocationID = 100000,
                EventID = 3,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeEventDatesForLocation.Add(new EventDateVM()
            {
                EventDateID = new DateTime(2022, 06, 03),
                EventName = "Fake Event Number 1",
                LocationID = 100000,
                EventID = 3,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            });

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1,
                TaskID = 999999,
                TaskEventName = "Test Event 1",
                Name = "Mop",
                Description = "Mop up spilled drink",
                DueDate = DateTime.Today,
                Priority = 3,
                TaskPriority = "High",
                Active = true
            });

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1,
                TaskID = 999998,
                TaskEventName = "Test Event 1",
                Name = "Wash Towels",
                Description = "Wash Towels after Events",
                DueDate = DateTime.Today,
                Priority = 1,
                TaskPriority = "Low",
                Active = true
            });

            _taskAssignments.Add(new TaskAssignmentVM()
            {
                TaskAssignmentID = 1,
                DateAssigned = new DateTime(2022, 1, 20),
                TaskID = 999999,
                RoleID = "Event Planner",
                UserID = 999999,
                Name = "Tess Data"
            });
            _taskAssignments.Add(new TaskAssignmentVM()
            {
                TaskAssignmentID = 2,
                DateAssigned = new DateTime(2022, 2, 3),
                TaskID = 999999,
                RoleID = "Volunteer",
                UserID = 999998,
                Name = "Tess Data"
            });
            _taskAssignments.Add(new TaskAssignmentVM()
            {
                TaskAssignmentID = 1,
                DateAssigned = new DateTime(2022, 3, 7),
                TaskID = 999998,
                RoleID = "Event Planner",
                UserID = 999999,
                Name = "Tess Data"
            });
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Insert fake data about the date of the event for tesing
        /// 
        /// </summary>
        /// <param name="eventDate">An EventDate object</param>
        /// <returns>Number of rows added</returns>
        public int InsertEventDate(EventDate eventDate)
        {
            int rowsAffected = 0;

            foreach (EventDate fakeDate in _fakeEventDate)
            {
                if (fakeDate.EventDateID == eventDate.EventDateID && fakeDate.EventID == eventDate.EventID)
                {
                    throw new ApplicationException("This event already has this date added");
                }

            }

            _fakeEventDate.Add(eventDate);

            rowsAffected++;

            return rowsAffected;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Returns an EventDate with a specific eventDateID and eventID
        /// 
        /// </summary>
        /// <param name="eventDateID">A DateTime object</param>
        /// <param name="eventID">an int</param>
        /// <returns>fake EventDate object</returns>
        public EventDate SelectEventDateByEventDateIDAndEventID(DateTime eventDateID, int eventID)
        {
            try
            {
                foreach (EventDate eventDate in _fakeEventDate)
                {
                    if (eventDate.EventDateID == eventDateID && eventDate.EventID == eventID)
                    {
                        return eventDate;
                    }
                }
                throw new ApplicationException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Return list of fake data about the date of the event for testing
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>Fake date list</returns>
        public List<EventDate> SelectEventDatesByEventID(int eventID)
        {
            List<EventDate> eventDates = new List<EventDate>();

            _fakeEventDate.ForEach(ed =>
           {
               if (ed.EventID == eventID)
               {
                   eventDates.Add(ed);
               }
           });

            return eventDates;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Updates an event date in fake data list
        /// 
        /// </summary>
        /// <returns>int number of records affected</returns>
        public int UpdateEventDate(EventDate oldEventDate, EventDate newEventDate)
        {
            int rowsAffected = 0;

            foreach (var fakeEventDate in _fakeEventDate)
            {
                if (fakeEventDate.EventID == newEventDate.EventID
                    && fakeEventDate.EventDateID == oldEventDate.EventDateID
                    && fakeEventDate.StartTime == oldEventDate.StartTime
                    && fakeEventDate.EndTime == oldEventDate.EndTime
                    && fakeEventDate.Active == oldEventDate.Active)
                {
                    fakeEventDate.EventDateID = newEventDate.EventDateID;
                    fakeEventDate.StartTime = newEventDate.StartTime;
                    fakeEventDate.EndTime = newEventDate.EndTime;
                    fakeEventDate.Active = newEventDate.Active;

                    rowsAffected++;
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The fake accessor for selecting event dates by their locationID
        /// </summary>
        /// /// <param name="locationID"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> SelectEventDatesByLocationID(int locationID)
        {
            List<EventDateVM> eventDatesForLocation = new List<EventDateVM>();

            _fakeEventDatesForLocation.ForEach(ed =>
            {
                if (ed.LocationID == locationID)
                {
                    eventDatesForLocation.Add(ed);
                }
            });

            return eventDatesForLocation;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// The fake accessor for selecting event dates by their userID and eventDate
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventDate"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> SelectEventDateByUserIDAndDate(int userID, DateTime eventDate)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();

            foreach(EventDate fakeEventDate in _fakeEventDate)
            {
                if (fakeEventDate.EventDateID.Date == eventDate.Date)
                {
                    foreach(TasksVM task in _fakeTasks)
                    {
                        if(task.EventID == fakeEventDate.EventID)
                        {
                            foreach(TaskAssignmentVM taskAssignment in _taskAssignments)
                            {
                                if(taskAssignment.TaskID == task.TaskID)
                                {
                                    if(taskAssignment.UserID == userID)
                                    {
                                        eventDates.Add(new EventDateVM() 
                                        {
                                            EventName = task.TaskEventName,
                                            EventID = fakeEventDate.EventID,
                                            StartTime = fakeEventDate.StartTime,
                                            EndTime = fakeEventDate.EndTime
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return eventDates;
        }
    }
}

