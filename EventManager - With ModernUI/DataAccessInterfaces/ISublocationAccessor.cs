using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ISublocationAccessor
    {
        Sublocation SelectSublocationBySublocationID(int sublocationID);
        List<Sublocation> SelectSublocationsByLocationID(int locationID);
        int UpdateSublocation(Sublocation oldSublocation, Sublocation newSublocation);
        int InsertSublocationByLocationID(int locationID, string sublocationName, string sublocationDesc);
        int DeactivateSublocationBySublocationID(int sublocationID);
    }
}
