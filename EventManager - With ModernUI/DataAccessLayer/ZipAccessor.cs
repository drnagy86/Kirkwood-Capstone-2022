using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ZipAccessor : IZipAccessor
    {
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/13
        /// 
        /// Description:
        /// returns a list of all zip codes in the database
        /// </summary>
        /// <returns></returns>
        public List<Zip> SelectAllZIPs()
        {
            List<Zip> zips = new List<Zip>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_all_zip_codes";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        zips.Add(new Zip()
                        {
                            ZIPCode = reader.GetString(0),
                            City = reader.GetString(1),
                            State = reader.GetString(2),
                        });
                    }
                }
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
        /// Description: Returns city and state when given zipcode. or at least it should
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public Zip SelectCityAndStateByZIPCode(string zipCode)
        {
            Zip zip = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_city_and_states_by_zipcode";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        zip = new Zip();

                        zip.ZIPCode = zipCode;
                        zip.City = reader.GetString(0);
                        zip.State = reader.GetString(1);
                        
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return zip;
        }
    }
}
