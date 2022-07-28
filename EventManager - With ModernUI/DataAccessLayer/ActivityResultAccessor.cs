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
    public class ActivityResultAccessor : IActivityResultAccessor
    {
        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns the list of Activities for an event
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>A list of Activity objects</returns>
        public List<ActivityResult> SelectActivityResultsByActivityID(int activityID)
        {
            List<ActivityResult> result = new List<ActivityResult>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_activity_results_by_activityID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ActivityID", SqlDbType.Int);

            cmd.Parameters["@ActivityID"].Value = activityID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                                [ActivityResultRank],
			                    [ActivityResultName]	
                        */
                        result.Add(new ActivityResult()
                        {
                            ActivityResultRank = reader.GetInt32(0),
                            ActivityResultName = reader.GetString(1),
                            ActivityID = activityID                            
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
    }
}
