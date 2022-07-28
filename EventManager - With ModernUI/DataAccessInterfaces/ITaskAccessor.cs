using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessInterfaces
{
    
    public interface ITaskAccessor
    {
        int InsertTasks(Tasks newTask, int numTotalVolunteers);
        int UpdateTasks(Tasks oldTask, Tasks newTask);
        List<Priority> SelectAllPriorities();
        List<TasksVM> SelectAllActiveTasksByEventID(int eventID);
        bool DeleteTaskByTaskID(int taskID);
        bool UserCanEditDeleteTask(int userID);
        List<TaskAssignmentVM> SelectTaskAssignmentsByTaskID(int taskID);
        int InsertNewTaskAssignmentByTaskID(int taskID);
        int UpdateTaskAssignmentWithUserID(int taskAssignmentID, int userID);
        List<TasksVM> SelectAllTasksByEventID(int eventID);
    }
}
