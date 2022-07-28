using System.Collections.Generic;

namespace MVCPresentation.Models
{
    public class TaskAssignmentViewModel
    {
        public int EventID { get; set; }
        public List<DataObjects.TasksVM> Tasks { get; set; }
    }
}