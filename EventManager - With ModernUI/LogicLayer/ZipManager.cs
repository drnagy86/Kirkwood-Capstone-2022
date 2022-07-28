using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ZipManager : IZipManager
    {
        private IZipAccessor _zipAccessor = null;
        public ZipManager()
        {
            _zipAccessor = new ZipAccessor();
        }

        public ZipManager(IZipAccessor zipAccessor)
        {
            _zipAccessor = zipAccessor;
        }
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// returns a list of all zipcodes
        /// </summary>
        /// <returns></returns>
        public List<Zip> RetrieveAllZIPCodes()
        {
            List<Zip> zips = new List<Zip>();
            try
            {
                zips = _zipAccessor.SelectAllZIPs();
            }
            catch (Exception)
            {

                throw;
            }

            return zips;

        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// returns a zipcode object when give the code.
        /// or at least it is supposed to.
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Zip RetrieveCityandStateByZIPCode(string zipCode)
        {
            Zip zip = new Zip();
            try
            {
                zip = _zipAccessor.SelectCityAndStateByZIPCode(zipCode);
            }
            catch (Exception)
            {

                throw;
            }

            return zip;
        }
    }
}
