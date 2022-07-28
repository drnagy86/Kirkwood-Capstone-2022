using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Jace Pettinger
    /// Created: 2022/03/31
    /// 
    /// Description:
    /// Data object for the Task Assignment table
    /// </summary>
    public class TaskAssignment
    {
        public int TaskAssignmentID { get; set; }
        public DateTime DateAssigned { get; set; }
        public int TaskID { get; set; }
        public string RoleID { get; set; }
        public int UserID { get; set; }
    }

    public class TaskAssignmentVM : TaskAssignment
    {
        public String Name { get; set; }
    }
}
