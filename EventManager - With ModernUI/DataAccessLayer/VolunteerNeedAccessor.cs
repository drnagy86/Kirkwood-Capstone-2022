using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Vinayak Deshpande
/// Created: 2022/03/01
/// 
/// Description: The volunteerneedAccessor
/// </summary>
namespace DataAccessLayer
{
    public class VolunteerNeedAccessor : IVolunteerNeedAccessor
    {
        /// <summary>
        /// Vinayak Deshpande
        /// 2022/02/26
        /// 
        /// Description: deletes a need
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int DeleteVolunteerNeed(int TaskID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_volunteer_need";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = TaskID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/03/26
        /// 
        /// Description: adds a volunteer need. not used mostly
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="numTotalVolunteers"></param>
        /// <returns></returns>
        public int InsertVolunteerNeed(int taskID, int numTotalVolunteers)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_new_volunteer_need";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;
            cmd.Parameters.Add("@NumTotalVolunteers", SqlDbType.Int);
            cmd.Parameters["@NumTotalVolunteers"].Value = numTotalVolunteers;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/03/26
        /// 
        /// Description: selects a need for a task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public VolunteerNeed SelectVolunteerNeedByTaskID(int taskID)
        {
            VolunteerNeed _newNeed = new VolunteerNeed();
            _newNeed.TaskID = taskID;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_volunteer_need_by_taskID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _newNeed.NumTotalVolunteers = reader.GetInt32(0);
                        _newNeed.NumCurrVolunteers = reader.GetInt32(0);
                    }
                }
                else
                {
                    InsertVolunteerNeed(taskID, 0);
                    _newNeed.TaskID = taskID;
                    _newNeed.NumTotalVolunteers = 0;
                    _newNeed.NumCurrVolunteers = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return _newNeed;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/03/26
        /// 
        /// Description: increments the current number of volunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int UpdateAddCurrVolunteers(int taskID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_add_curr_volunteers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/03/26
        /// 
        /// Description: decrements the current number of volunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int UpdateSubtractCurrVolunteers(int taskID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_subtract_curr_volunteers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/03/26
        /// 
        /// Description: updates the total number of needed volunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="numTotalVolunteers"></param>
        /// <returns></returns>
        public int UpdateVolunteerNeed(int taskID, int numTotalVolunteers)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_volunteer_need";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskID", SqlDbType.Int);
            cmd.Parameters["@TaskID"].Value = taskID;
            cmd.Parameters.Add("@NumTotalVolunteers", SqlDbType.Int);
            cmd.Parameters["@NumTotalVolunteers"].Value = numTotalVolunteers;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
    }
}
