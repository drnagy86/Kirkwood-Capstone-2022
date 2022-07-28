using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public bool PublicActivity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityImageName { get; set; }
        public int? SublocationID { get; set; }
        public DateTime EventDateID { get; set; }
        public int EventID { get; set; }
    }

    public class ActivityVM : Activity
    {
        public List<ActivityResult> ActivityResults { get; set; }
        public Sublocation ActivitySublocation { get; set; }
        public EventDate EventDate { get; set; }
        public int UserID { get; set; }
        public string SublocationName { get; set; }
        public string DisplayTimeStart
        {
            get
            {
                return StartTime.ToString("hh:mm tt");
            }
        }
        public string DisplayTimeEnd
        {
            get
            {
                return EndTime.ToString("hh:mm tt");
            }
        }
        public string DisplayEventDate
        {
            get
            {
                return EventDateID.ToString("d");
            }
        }
    }
}

