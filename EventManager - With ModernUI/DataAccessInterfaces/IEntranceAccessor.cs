using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEntranceAccessor
    {
        int InsertEntrance(int locationID, string entranceName, string description);
        List<Entrance> SelectEntranceByLocationID(int locationID);
        int UpdateEntrance(Entrance oldEntrance, Entrance newEntrance);
        int DeactivateEntranceByEntranceID(int entranceID);
    }
}
