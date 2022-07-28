using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/01/29
    /// 
    /// Description:
    /// Class for event date
    /// 
    /// Derrick Nagy
    /// Update: 2022/03/24
    /// 
    /// Description:
    /// Added display format
    /// </summary>
    public class EventDate
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EventDateID { get; set; }
        public int EventID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Active { get; set; }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Default constuctor that sets DateTime objects to DateTime.MinValue,
        /// used for testing when left blank
        /// </summary>
        public EventDate()
        {
            EventDateID = DateTime.MinValue;
            EventID = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            Active = true;
        }
    }

    public class EventDateVM : EventDate
    {
        new public DateTime EventDateID { get; set; }
        public string EventName { get; set; }
        public int LocationID { get; set; }
    }
}
