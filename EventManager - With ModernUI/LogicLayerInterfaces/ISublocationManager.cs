using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/22
    /// 
    /// Description:
    /// Interface for handling Sublocation manager class methods
    /// </summary>
    public interface ISublocationManager
    {
        Sublocation RetrieveSublocationBySublocationID(int sublocationID);
        List<Sublocation> RetrieveSublocationsByLocationID(int locationID);
        int EditSublocationBySublocationID(Sublocation oldSublocation, Sublocation newSublocation);
        int CreateSublocationByLocationID(int locationID, string sublocationName, string sublocationDesc);
        int DeactivateSublocationBySublocationID(int sublocationID);
    }
}
