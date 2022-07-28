using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Entrance
    {
        public int EntranceID { get; set; }
        public int LocationID { get; set; }
        public string EntranceName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
