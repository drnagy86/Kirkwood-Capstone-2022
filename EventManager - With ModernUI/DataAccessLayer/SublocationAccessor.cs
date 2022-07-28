using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class SublocationAccessor : ISublocationAccessor
    {
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Deactivates a sublocation from sublocation table.
        /// </summary>
        /// <param name="sublocationID">ID of sublocation to be deleted.</param>
        /// <returns></returns>
        public int DeactivateSublocationBySublocationID(int sublocationID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_deactivate_sublocation";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SublocationID", SqlDbType.Int);

            cmd.Parameters["@SublocationID"].Value = sublocationID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/26
        /// 
        /// Description:
        /// Inserts a sublocation into the sublocation table
        ///
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <param name="sublocationName"></param>
        /// <param name="sublocationDescription"></param>
        /// <returns>the rows affected</returns>
        public int InsertSublocationByLocationID(int locationID, string sublocationName, string sublocationDesc)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_sublocation_by_locationID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;

            cmd.Parameters.Add("@SublocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters["@SublocationName"].Value = sublocationName;

            cmd.Parameters.Add("@SublocationDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@SublocationDescription"].Value = sublocationDesc ?? "";

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { throw; }

            return rows;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns a specific Sublocation
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/24
        /// 
        /// Updated to remove default sublocation. Also reenabled the location ID
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <returns>A sublocation object</returns>
        public Sublocation SelectSublocationBySublocationID(int sublocationID)
        {
            Sublocation result = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_sublocation_by_sublocationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SublocationID", SqlDbType.Int);

            cmd.Parameters["@SublocationID"].Value = sublocationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new Sublocation()
                        {
                            SublocationID = sublocationID,
                            SublocationName = reader.GetString(0),
                            SublocationDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2),
                            LocationID = reader.GetInt32(3)
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Returns a list of sublocations based on a locationID
        /// </summary>
        /// <param name="locationID">The locationID to get sublocations matching.</param>
        /// <returns>A list of sublocations matching the location ID passed in.</returns>

        public List<Sublocation> SelectSublocationsByLocationID(int locationID)
        {
            List<Sublocation> result = new List<Sublocation>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_sublocations_by_locationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);

            cmd.Parameters["@LocationID"].Value = locationID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Sublocation()
                        {
                            LocationID = locationID,

                            SublocationName = reader.GetString(0),
                            SublocationDescription = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Active = reader.GetBoolean(2),
                            SublocationID = reader.GetInt32(3)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Updates a sublocation record.
        /// 
        /// Christopher Repko
        /// Updated: 2022/05/04
        /// 
        /// Description: 
        /// Made null description play nice with the database.
        /// </summary>
        /// <param name="oldSublocation">Sublocation to replace</param>
        /// <param name="newSublocation">Sublocation to replace with</param>
        /// <returns>integer representing the number of rows affected.</returns>
        public int UpdateSublocation(Sublocation oldSublocation, Sublocation newSublocation)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_sublocation";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SublocationID", SqlDbType.Int);
            cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSublocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@OldSublocationDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@NewLocationID", SqlDbType.Int);
            cmd.Parameters.Add("@NewSublocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@NewSublocationDescription", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@SublocationID"].Value = oldSublocation.SublocationID;
            cmd.Parameters["@OldLocationID"].Value = oldSublocation.LocationID;
            cmd.Parameters["@OldSublocationName"].Value = oldSublocation.SublocationName;
            cmd.Parameters["@OldSublocationDescription"].Value = oldSublocation.SublocationDescription ?? "";
            cmd.Parameters["@NewLocationID"].Value = newSublocation.LocationID;
            cmd.Parameters["@NewSublocationName"].Value = newSublocation.SublocationName;
            cmd.Parameters["@NewSublocationDescription"].Value = newSublocation.SublocationDescription ?? "";

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
