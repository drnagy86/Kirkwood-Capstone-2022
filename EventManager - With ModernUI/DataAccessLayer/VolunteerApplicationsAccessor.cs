using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class VolunteerApplicationsAccessor : IVolunteerApplicationsAccessor
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Data accessor that calls sp_insert_volunteer_application
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="availability"></param>
        /// <returns></returns>
        public int InsertVolunteerApplication(int userID, Availability availability)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_volunteer_application";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;
                        
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@TimeStart", SqlDbType.Time);
            cmd.Parameters.Add("@TimeEnd", SqlDbType.Time);
            cmd.Parameters.Add("@Sunday", SqlDbType.Bit);
            cmd.Parameters.Add("@Monday", SqlDbType.Bit);
            cmd.Parameters.Add("@Tuesday", SqlDbType.Bit);
            cmd.Parameters.Add("@Wednesday", SqlDbType.Bit);
            cmd.Parameters.Add("@Thursday", SqlDbType.Bit);
            cmd.Parameters.Add("@Friday", SqlDbType.Bit);
            cmd.Parameters.Add("@Saturday", SqlDbType.Bit);

            string dbFriendlyTimeStart = ((DateTime)availability.TimeStart).Hour.ToString() + ":" + ((DateTime)availability.TimeStart).Minute.ToString();

            string dbFriendlyTimeEnd = ((DateTime)availability.TimeEnd).Hour.ToString() + ":" + ((DateTime)availability.TimeEnd).Minute.ToString();

            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@TimeStart"].Value =dbFriendlyTimeStart;
            cmd.Parameters["@TimeEnd"].Value = dbFriendlyTimeEnd;
            cmd.Parameters["@Sunday"].Value = availability.Sunday;
            cmd.Parameters["@Monday"].Value = availability.Monday;
            cmd.Parameters["@Tuesday"].Value = availability.Tuesday;
            cmd.Parameters["@Wednesday"].Value = availability.Wednesday;
            cmd.Parameters["@Thursday"].Value = availability.Thursday;
            cmd.Parameters["@Friday"].Value = availability.Friday;
            cmd.Parameters["@Saturday"].Value = availability.Saturday;

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
