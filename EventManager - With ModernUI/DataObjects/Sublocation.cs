using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Sublocation
    {
        public int SublocationID { get; set; }
        public int LocationID { get; set; }
        public String SublocationName { get; set; }
        public String SublocationDescription { get; set; }
        public bool Active { get; set; }
    }
}
