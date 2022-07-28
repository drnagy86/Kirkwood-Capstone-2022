using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class TaskViewModel
    {
        public TasksVM Task { get; set; }
        public List<TaskAssignmentVM> TaskAssignments { get; set; }
        
    }
}