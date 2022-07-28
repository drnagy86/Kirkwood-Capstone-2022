using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class EntranceAccessor : IEntranceAccessor
    {
        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/06
        /// 
        /// Description:
        /// method that deactivates an entrance from the Entrance table.
        /// <param name="entranceID"/>
        /// </summary>
        public int DeactivateEntranceByEntranceID(int entranceID)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_deactivate_entrance_by_entranceID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EntranceID", SqlDbType.Int);
            cmd.Parameters["@EntranceID"].Value = entranceID;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rows;
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Insert entrance into tadpole_db
        /// 
        /// </summary>
        /// <param name="locationID">ID of associated location</param>
        /// <param name="entranceName">Name of entrance location</param>
        /// <param name="description">Description of entrance, may include directions</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEntrance(int locationID, string entranceName, string description)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_entrance";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters.Add("@EntranceName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);

            cmd.Parameters["@LocationID"].Value = locationID;
            cmd.Parameters["@EntranceName"].Value = entranceName;
            cmd.Parameters["@Description"].Value = description;


            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Select that returns entrances based off a location ID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Entrance> SelectEntranceByLocationID(int locationID)
        {
            List<Entrance> entrances = new List<Entrance>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_entrance_by_locationID";
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
                    while(reader.Read())
                    {
                        entrances.Add(new Entrance()
                        {
                            EntranceID = reader.GetInt32(0),
                            LocationID = reader.GetInt32(1),
                            EntranceName = reader.GetString(2),
                            Description = reader.GetString(3)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return entrances;
        }


        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Update entrance using entrance ID
        /// 
        /// </summary>
        /// <param name="oldEntrance"></param>
        /// <param name="newEntrance"></param>
        /// <returns>Number of rows affected</returns>
        public int UpdateEntrance(Entrance oldEntrance, Entrance newEntrance)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string cmdTxt = "sp_update_entrance_by_entranceID";
            var cmd = new SqlCommand(cmdTxt, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EntranceID", oldEntrance.EntranceID);

            cmd.Parameters.Add("@OldEntranceName", SqlDbType.NVarChar, 100);
            cmd.Parameters["@OldEntranceName"].Value = oldEntrance.EntranceName;
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters["@OldDescription"].Value = oldEntrance.Description;

            cmd.Parameters.Add("@NewEntranceName", SqlDbType.NVarChar, 100);
            cmd.Parameters["@NewEntranceName"].Value = newEntrance.EntranceName;
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters["@NewDescription"].Value = newEntrance.Description;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
