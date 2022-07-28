using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IZipAccessor
    {
        List<Zip> SelectAllZIPs();
        Zip SelectCityAndStateByZIPCode(string zipCode);
    }
}
