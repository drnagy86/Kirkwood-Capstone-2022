using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Vinayak Deshpande
/// Created 2022/02/24
/// 
/// the Volunteer need object
/// </summary>
namespace DataObjects
{
    public class VolunteerNeed
    {
        public int TaskID { get; set; }
        public int NumTotalVolunteers { get; set; }
        public int NumCurrVolunteers { get; set; }

    }
    public class VolunteerNeedVM : VolunteerNeed
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CompletionDate { get; set; }
        public int ProofID { get; set; }
        public bool isDone { get; set; }
        public int EventID { get; set; }
        public bool Active { get; set; }
        public string TaskPriority { get; set; }
        public string TaskEventName { get; set; }
    }
}
