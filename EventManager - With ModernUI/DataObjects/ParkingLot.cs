using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/03/01
    /// 
    /// Description:
    /// Storage Model for ParkingLot
    /// 
    /// </summary>
    public class ParkingLot
    {
        public int LotID { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
    }

    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/03/01
    /// 
    /// Description:
    /// View Model for ParkingLot
    /// 
    /// </summary>
    public class ParkingLotVM: ParkingLot
    {
        public string LocationName { get; set; }
    }
}
