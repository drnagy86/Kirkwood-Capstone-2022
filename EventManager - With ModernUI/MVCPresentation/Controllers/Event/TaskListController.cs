using DataObjects;
using LogicLayerInterfaces;
using MVCPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers.Event
{
    /// <summary>
    /// Vinayak Deshpande
    /// Created: 2022/04/26
    /// 
    /// Description:
    /// Controller for looking at list of volunteers assigned to tasks for event.
    /// </summary>
    public class TaskListController : Controller
    {
        // GET: TaskList


        private IEventManager _eventManager;
        private IUserManager _userManager;
        private ITaskManager _taskManager;
        public int _pageSize = 10;
        private List<TaskViewModel> taskViewModels = null;

        public TaskListController(IEventManager eventManager, ITaskManager taskManager, IUserManager userManager)
        {

            _eventManager = eventManager;
            _userManager = userManager;
            _taskManager = taskManager;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/26
        /// 
        /// Description:
        /// Nav Bar Stuff
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public PartialViewResult EventNavBar(int eventID)
        {
            return PartialView(eventID);
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/26
        /// 
        /// Description:
        /// Generates the EventTaskList
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult EventTaskList(int? eventID, int page = 1)
        {

            
            List<TasksVM> tasks = new List<TasksVM>();
            TaskListViewModel model = new TaskListViewModel();
            
            if (taskViewModels == null)
            {
                
                taskViewModels = new List<TaskViewModel>();
                try
                {
                    if (!eventID.HasValue)
                    {
                        throw new Exception();
                    }
                    tasks = _taskManager.RetrieveAllTasksByEventID((int)eventID);
                    foreach (var task in tasks)
                    {
                        TaskViewModel taskViewModel = new TaskViewModel();
                        taskViewModel.Task = task;
                        taskViewModel.TaskAssignments = _taskManager.RetrieveTaskAssignmentsByTaskID(task.TaskID);
                        taskViewModels.Add(taskViewModel);
                    }

                    model = new TaskListViewModel
                    {
                        Tasks = taskViewModels
                                            .Skip((page - 1) * _pageSize)
                                            .Take(_pageSize),
                        PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = _pageSize,
                            TotalItems = taskViewModels.Count()
                        },
                        EventName = _eventManager.RetrieveEventByEventID((int)eventID).EventName,
                        EventID = (int)eventID
                    };
                }
                catch (Exception ex)
                {

                    TempData["errorMessage"] = ex.Message;
                }

                

            }
            return View(model);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// ActionResult that displays the assigned tasks for a user with the selected event
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TasksAssigned(string eventID, string username)
        {
            TaskAssignmentViewModel model = new TaskAssignmentViewModel();
            model.Tasks = new List<TasksVM>();
            if (eventID == null || username == null || username == "")
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                int userID = _userManager.RetrieveUserByEmail(username).UserID;
                int event_ID = Int32.Parse(eventID);
                model.EventID = event_ID;
                List<TasksVM> eventTasks = _taskManager.RetrieveAllActiveTasksByEventID(event_ID);
                List<TaskAssignmentVM> assignedTasks;
                foreach (var task in eventTasks)
                {
                    assignedTasks = _taskManager.RetrieveTaskAssignmentsByTaskID(task.TaskID);
                    if (assignedTasks != null)
                    {
                        foreach (var assignedTask in assignedTasks)
                        {
                            if (assignedTask.UserID == userID)
                            {
                                model.Tasks.Add(task);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }
    }
}