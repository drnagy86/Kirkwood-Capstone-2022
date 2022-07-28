using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Data object for the Tasks table
    /// </summary>
    public class Tasks
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CompletionDate { get; set; }
        public int ProofID { get; set; }
        public bool isDone { get; set; }
        public int EventID { get; set; }
        public bool Active { get; set; }
    }

    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/31
    /// 
    /// Description:
    /// Data object for a Task View Model 
    /// </summary>
    public class TasksVM : Tasks
    {
        public string TaskPriority { get; set; }
        public string TaskEventName { get; set; }
    }
}
