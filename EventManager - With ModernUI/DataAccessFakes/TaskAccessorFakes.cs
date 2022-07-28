using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/24
    /// 
    /// Description:
    /// Class of fakes used to test the methods related to Tasks
    /// </summary>
    /// /// <remarks>
	/// Emma Pollock
	/// Updated: 2022/03/10
    /// added taskAssignements
	/// </remarks>
    public class TaskAccessorFakes : ITaskAccessor
    {
        private List<TasksVM> _fakeTasks = new List<TasksVM>();
        private List<Priority> _priorities = new List<Priority>();
        private List<TaskAssignmentVM> _taskAssignments = new List<TaskAssignmentVM>();
        private Dictionary<User, Role> _fakeUserRoles = new Dictionary<User, Role>();
        private List<TaskAssignment> _fakeTaskAssignments = new List<TaskAssignment>();

        public TaskAccessorFakes()
        {
            addFakeUserRoles();

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
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
                EventID = 1000000,
                TaskID = 999998,
                TaskEventName = "Test Event 1",
                Name = "Wash Towels",
                Description = "Wash Towels after Events",
                DueDate = DateTime.Today,
                Priority = 1,
                TaskPriority = "Low",
                Active = true
            });

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskID = 999997,
                TaskEventName = "Test Event 1",
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                TaskPriority = "High",
                Active = true
            });
            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskID = 999996,
                TaskEventName = "Test Event 1",
                Name = "Clean Trash",
                Description = "Make sure trash is cleared from spectator area",
                DueDate = DateTime.Now,
                Priority = 2,
                TaskPriority = "Medium",
                Active = false
            });

            

            /// <summary>
            /// Mike Cahow
            /// Created: 2022/01/24
            /// 
            /// Description:
            /// Creating a "fake" list of priorities to use for testing purposes
            /// </summary>
            _priorities.Add(new Priority()
            {
                PriorityID = 1,
                Description = "Low"
            });
            _priorities.Add(new Priority()
            {
                PriorityID = 2,
                Description = "Medium"
            });
            _priorities.Add(new Priority()
            {
                PriorityID = 3,
                Description = "High"
            });

            /// <summary>
            /// Emma Pollock
            /// Created: 2022/03/10
            /// 
            /// Description:
            /// Creating a "fake" list of taskAssignments to use for testing purposes
            /// </summary>
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

            /// Jace Pettinger
            /// Created: 2022/01/24
            /// 
            /// Description:
            /// Creating a "fake" list of task assignments to use for testing purposes
            /// </summary>
            _fakeTaskAssignments.Add(new TaskAssignment() 
            {
                TaskAssignmentID = 999999,
                DateAssigned = DateTime.Now,
                TaskID = 999999,
                UserID = 999999
               // RoleID = 
            });
            _fakeTaskAssignments.Add(new TaskAssignment()
            {
                TaskAssignmentID = 999998,
                DateAssigned = DateTime.Now,
                TaskID = 999998,
                UserID = 999999
                // RoleID = 
            });
            _fakeTaskAssignments.Add(new TaskAssignment()
            {
                TaskAssignmentID = 999997,
                DateAssigned = DateTime.Now,
                TaskID = 999999
                // RoleID = 
            });
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Fake insert tasks method used for testing
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int rowsAffected</returns>
        public int InsertTasks(Tasks newTask, int numTotalVolunteers = 0)
        {
            int taskID = 999995;

            try
            {
                _fakeTasks.Add(new TasksVM()
                {
                    TaskID = taskID,
                    Name = "Clean",
                    Description = "Clean this room.",
                    DueDate = DateTime.Now,
                    Priority = 3
                });            
            }
            catch (Exception)
            {

                throw;
            }

            return taskID;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Fake update tasks method for testing
        /// </summary>
        /// <param name="oldTask"></param>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public int UpdateTasks(Tasks oldTask, Tasks newTask)
        {
            int rowsAffected = 0;

            foreach(var fakeTasks in _fakeTasks)
            {
                if(fakeTasks.TaskID == oldTask.TaskID && fakeTasks.Name == oldTask.Name
                    && fakeTasks.Description == oldTask.Description && fakeTasks.DueDate.Equals(oldTask.DueDate)
                    && fakeTasks.Priority == oldTask.Priority && fakeTasks.Active == oldTask.Active)
                {
                    fakeTasks.Name = newTask.Name;
                    fakeTasks.Description = newTask.Description;
                    fakeTasks.DueDate = newTask.DueDate;
                    fakeTasks.Priority = newTask.Priority;
                    fakeTasks.Active = newTask.Active;
                    rowsAffected = 1;
                    break;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// A method that returns a fake list of priorites
        /// </summary>
        /// <returns>List Priority</returns>
        public List<Priority> SelectAllPriorities()
        {
            List<Priority> priorities = new List<Priority>();

            foreach (var priority in _priorities)
            {
                priorities.Add(priority);
            }

            return priorities;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Select method that grabs a fake list of all tasks for an event
        /// </summary>
        /// <returns>List Tasks</returns>
        public List<TasksVM> SelectAllActiveTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            foreach(var task in _fakeTasks)
            {
                if(task.EventID == eventID && task.Active == true)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select method that gets a list of the fake taskAssignments 
        ///     for a task.
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>List TaskAssignments</returns>
        public List<TaskAssignmentVM> SelectTaskAssignmentsByTaskID(int taskID)
        {
            List<TaskAssignmentVM> assignments = new List<TaskAssignmentVM>();
            foreach(TaskAssignmentVM taskAssignment in _taskAssignments)
            {
                if(taskAssignment.TaskID == taskID)
                {
                    assignments.Add(taskAssignment);
                }
            }
            return assignments;
        }

        public bool UserCanEditDeleteTask(int userID)
        {
            bool result = false;

            foreach (var userRole in _fakeUserRoles)
            {
                if (userRole.Key.UserID == userID && (userRole.Value.RoleID == "Event Planner" || userRole.Value.RoleID == "Event Manager"))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void addFakeUserRoles()
        {
            _fakeUserRoles.Add(new User { UserID = 100000 }, new Role { RoleID = "Event Planner" });
            _fakeUserRoles.Add(new User { UserID = 100001 }, new Role { RoleID = "Test" });
            _fakeUserRoles.Add(new User { UserID = 100002 }, new Role { RoleID = "Test" });
            _fakeUserRoles.Add(new User { UserID = 100003 }, new Role { RoleID = "Event Planner" });
            _fakeUserRoles.Add(new User { UserID = 100004 }, new Role { RoleID = "Attendee" });
            _fakeUserRoles.Add(new User { UserID = 100000 }, new Role { RoleID = "Attendee" });
        }

        public bool DeleteTaskByTaskID(int taskID)
        {
            bool result = false;

            foreach (var fakeTask in _fakeTasks)
            {
                if (fakeTask.TaskID == taskID)
                {
                    _fakeTasks.Remove(fakeTask);
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// Jace Pettinger
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Adds a new task assignment into the fake task asiignment list with a fake task id
        /// </summary>
        /// <returns>List Tasks</returns>
        public int InsertNewTaskAssignmentByTaskID(int taskID)
        {
            int taskAssignmentID = 999996;

            _fakeTaskAssignments.Add(new TaskAssignment()
            {
                TaskAssignmentID = taskAssignmentID,
                DateAssigned = DateTime.Now,
                TaskID = taskID,
                // RoleID = 
            });
            taskAssignmentID = 999996;

            return taskAssignmentID;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/31
        /// 
        /// Description:
        /// Updates task assignment to add a volunteer
        /// </summary>
        /// <returns>List Tasks</returns>
        public int UpdateTaskAssignmentWithUserID(int taskAssignmentID, int userID)
        {
            int rowsAffected = 0;

            foreach (var taskAssignment in _fakeTaskAssignments)
            {
                if (taskAssignmentID == taskAssignment.TaskAssignmentID)
                {
                    taskAssignment.UserID = userID;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/05
        /// 
        /// Description: Returns all the tasks based on eventID
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public List<TasksVM> SelectAllTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            foreach (var task in _fakeTasks)
            {
                if (task.EventID == eventID)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }
    }
}
