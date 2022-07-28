
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
/// <summary>
/// Vinayak Deshpande
/// 2022/01/26
/// 
/// Volunteer Request Accessor
/// </summary>

namespace DataAccessLayer
{
    public class VolunteerRequestAccessor : IVolunteerRequestAccessor
    {
        /// <summary>
        /// Emma Pollock
        /// 2022/03/28
        /// 
        /// Description: 
        /// Retrieves all VolunteerRequests for a specific volunteer
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>A List of VolunteerRequestViewModels</returns>
        public List<VolunteerRequestViewModel> SelectAllRequestsForVolunteerByVolunteerID(int volunteerID)
        {
            List<VolunteerRequestViewModel> requests = new List<VolunteerRequestViewModel>();

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_all_requests_for_volunteer_by_volunteerID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);

            cmd.Parameters["@VolunteerID"].Value = volunteerID;

            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                var reader = cmd.ExecuteReader();

                // Process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                         [VolunteerRequest].[RequestID],
                         [VolunteerRequest].[TaskID],
                         [VolunteerRequest].[VolunteerResponse],
                         [VolunteerRequest].[EventResponse],
                         CONCAT([Users].[GivenName], " ", [Users].[FamilyName]),
                         [Task].[Name],
                         [Event].[EventName],
			             [Event].[EventID]
                        */
                        var request = new VolunteerRequestViewModel()
                        {
                            RequestID = reader.GetInt32(0),
                            TaskID = reader.GetInt32(1),
                            VolunteerResponse = HelperMethods.IsNullCheck(reader, 2),
                            EventResponse = HelperMethods.IsNullCheck(reader, 3),
                            VolunteerName = reader.GetString(4),
                            TaskName = reader.GetString(5),
                            EventName = reader.GetString(6),
                            EventID = reader.GetInt32(7),
                            VolunteerID = volunteerID
                        };


                        requests.Add(request);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return requests;
        }       

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// 
        /// Description
        /// Retrieves a fake volunteer request with a specific requestID
        /// </summary>
        /// <param name="requestID"></param>  
        public VolunteerRequestViewModel SelectRequestByRequestID(int requestID)
        {
            VolunteerRequestViewModel request = null;

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_request_by_requestID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RequestID", SqlDbType.Int);

            cmd.Parameters["@RequestID"].Value = requestID;

            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                var reader = cmd.ExecuteReader();

                // Process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                         [VolunteerRequest].[VolunteerID],
                         [VolunteerRequest].[TaskID],
                         [VolunteerRequest].[VolunteerResponse],
                         [VolunteerRequest].[EventResponse],
                         CONCAT([Users].[GivenName], " ", [Users].[FamilyName]),
                         [Task].[Name],
                         [Event].[EventName],
			             [Event].[EventID]
                        */
                        request = new VolunteerRequestViewModel()
                        {
                            VolunteerID = reader.GetInt32(0),
                            TaskID = reader.GetInt32(1),
                            VolunteerResponse = HelperMethods.IsNullCheck(reader, 2),
                            EventResponse = HelperMethods.IsNullCheck(reader, 3),
                            VolunteerName = reader.GetString(4),
                            TaskName = reader.GetString(5),
                            EventName = reader.GetString(6),
                            EventID = reader.GetInt32(7),
                            RequestID = requestID
                        };
                    }
                }
                if (request == null)
                {
                    throw new ArgumentException("Unable to find request.");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return request;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// 2022/01/26
        /// 
        /// Description: returns all requests for an event
        /// </summary>
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/03/30 
        /// Added EventName and EventID
        /// </update>
        /// 
        /// <param name="eventID"></param>
        /// <returns></returns>
        public List<VolunteerRequestViewModel> SelectVolunteerRequestsByEventID(int eventID)
        {
            List<VolunteerRequestViewModel> requests = new List<VolunteerRequestViewModel>();

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_all_requests_by_eventID";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            // The parameters need their values to be set.
            cmd.Parameters["@EventID"].Value = eventID;

            // Now that we have the command set up, we can execute the command.
            // Always use a try block because the connection is unsafe.
            try
            {
                // Open the connection
                conn.Open();

                // execute appropriately and capture the results.
                var reader = cmd.ExecuteReader();

                // Process results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var tempRequest = new VolunteerRequest();

                        tempRequest.RequestID = reader.GetInt32(0);
                        tempRequest.VolunteerID = reader.GetInt32(1);
                        tempRequest.TaskID = reader.GetInt32(2);
                        tempRequest.VolunteerResponse = HelperMethods.IsNullCheck(reader, 3);
                        tempRequest.EventResponse = HelperMethods.IsNullCheck(reader, 4);
                        var volunteerName = reader.GetString(5);
                        var taskName = reader.GetString(6);
                        var EventName = reader.GetString(7);
                        var tempRequestVM = new VolunteerRequestViewModel(tempRequest, volunteerName, taskName);
                        tempRequestVM.EventName = EventName;
                        tempRequestVM.EventID = eventID;
                        requests.Add(tempRequestVM);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return requests;
        }

        /// <summary>
        /// Emma Pollock
        /// Created 2022/03/31
        /// 
        /// Description
        /// Updates the volunteer request of a specific request ID if the old volunteer response and event response
        ///   match the current volunteer response and event response.
        /// </summary>
        /// <param name="oldVolunteerRequest"></param> 
        /// <param name="newVolunteerRequest"></param> 
        /// <returns>Number of rows affected</returns>
        public int UpdateVolunteerRequest(VolunteerRequestViewModel oldVolunteerRequest, VolunteerRequestViewModel newVolunteerRequest)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_volunteer_request";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            /*
             @RequestID				[int],
	         @OldVolunteerResponse	[bit],
	         @NewVolunteerResponse	[bit],
	         @OldEventResponse		[bit],
	         @NewEventResponse		[bit]
            */
            cmd.Parameters.AddWithValue("@RequestID", newVolunteerRequest.RequestID);

            if(oldVolunteerRequest.VolunteerResponse is null)
            {
                cmd.Parameters.Add("@OldVolunteerResponse", SqlDbType.Bit);
                cmd.Parameters["@OldVolunteerResponse"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@OldVolunteerResponse", SqlDbType.Bit);
                cmd.Parameters["@OldVolunteerResponse"].Value = oldVolunteerRequest.VolunteerResponse;
            }
            

            if(newVolunteerRequest.VolunteerResponse is null)
            {
                cmd.Parameters.Add("@NewVolunteerResponse", SqlDbType.Bit);
                cmd.Parameters["@NewVolunteerResponse"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@NewVolunteerResponse", SqlDbType.Bit);
                cmd.Parameters["@NewVolunteerResponse"].Value = newVolunteerRequest.VolunteerResponse;
            }
            
            if(oldVolunteerRequest.EventResponse is null)
            {
                cmd.Parameters.Add("@OldEventResponse", SqlDbType.Bit);
                cmd.Parameters["@OldEventResponse"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@OldEventResponse", SqlDbType.Bit);
                cmd.Parameters["@OldEventResponse"].Value = oldVolunteerRequest.EventResponse;
            }
            
            if(newVolunteerRequest.EventResponse is null)
            {
                cmd.Parameters.Add("@NewEventResponse", SqlDbType.Bit);
                cmd.Parameters["@NewEventResponse"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@NewEventResponse", SqlDbType.Bit);
                cmd.Parameters["@NewEventResponse"].Value = newVolunteerRequest.EventResponse;
            }
            

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
