using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/05
    /// 
    /// Description:
    /// The locationimage data object
    /// </summary>
    public class LocationImage
    {
        public int LocationID { get; set; }
        public string ImageName { get; set; }
    }

    public class ServiceImage
    {
        public int ServiceID { get; set; }
        public string ImageName { get; set; }
    }
}
