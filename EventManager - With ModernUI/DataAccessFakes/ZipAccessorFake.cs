using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class ZipAccessorFake : IZipAccessor
    {
        private List<Zip> _fakeZips = new List<Zip>();

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Descripton: 
        /// Constructor that adds 5 fake zipcodes for testing
        /// </summary>
        public ZipAccessorFake()
        {
            _fakeZips.Add(new Zip()
            {
                ZIPCode = "11111",
                City = "Fake City 1",
                State = "Fake State"
            });
            _fakeZips.Add(new Zip()
            {
                ZIPCode = "11112",
                City = "Fake City 2",
                State = "Fake State"
            }); 
            _fakeZips.Add(new Zip()
            {
                ZIPCode = "11113",
                City = "Fake City 3",
                State = "Fake State"
            }); 
            _fakeZips.Add(new Zip()
            {
                ZIPCode = "11114",
                City = "Fake City 4",
                State = "Fake State"
            });
            _fakeZips.Add(new Zip()
            {
                ZIPCode = "11115",
                City = "Fake City 5",
                State = "Fake State"
            });
        }
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Description: Returns all the fake zips
        /// </summary>
        /// <returns></returns>
        public List<Zip> SelectAllZIPs()
        {
            return _fakeZips;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/14
        /// 
        /// Description: Returns zip if code exists otherwise null
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Zip SelectCityAndStateByZIPCode(string zipCode)
        {
            Zip result = null;
            if (_fakeZips.Exists(z => (z.ZIPCode == zipCode)))
            {
                result = _fakeZips.Find(z => z.ZIPCode == zipCode);
            }
            return result;
        }
    }
}
