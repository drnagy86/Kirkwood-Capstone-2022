using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEntranceManager
    {
        int CreateEntrance(int locationID, string entranceName, string description);
        List<Entrance> RetrieveEntranceByLocationID(int locationID);
        bool UpdateEntrance(Entrance oldEntrance, Entrance newEntrance);
        int RemoveEntranceByEntranceID(int entranceID);
    }
}
