using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace DataAccessLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerAccessor data access class for all volunteer data 
    /// </summary>
    public class VolunteerAccessor : IVolunteerAccessor
    {
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Accessor method that that selects all volunteer reviews and returns a list of them in the form of 
        /// a volunteer data object
        /// </summary>
        /// <returns>A list of volunteer data object shells that records the Volunteer ID and rating</returns>
        public List<Volunteer> SelectAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_all_volunteer_reviews";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteers.Add(new Volunteer()
                        {
                            VolunteerID = reader.GetInt32(0),
                            Rating = reader.GetInt32(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Accessor method that that selects all volunteers and returns a list of them in the form of 
        /// a volunteer data object
        /// 
        /// Update:
        /// Jace Pettinger
        /// Updated: 2022/05/04
        /// 
        /// Description:
        /// Added user description to retrieved data
        /// </summary>
        /// <returns>A list volunteer data object</returns>
        public List<Volunteer> SelectAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_all_volunteers";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteers.Add(new Volunteer()
                        {
                            UserID = reader.GetInt32(0),
                            VolunteerID = reader.GetInt32(1),
                            GivenName = reader.GetString(2),
                            FamilyName = reader.GetString(3),
                            State = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            City = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Zip = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            VolunteerType = reader.GetString(7),
                            Email = reader.GetString(8),
                            UserDescription = reader.IsDBNull(9) ? "" :  reader.GetString(9)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Select availability records matching the given volunteerID and date
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Location on a given date</returns>
        /// (Original Author: Kris Howell LocationAccessor.cs)
        public List<Availability> SelectAvailabilityByVolunteerIDAndDate(int volunteerID, DateTime date)
        {
            List<Availability> volunteerAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_volunteerID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);
            cmd.Parameters["@VolunteerID"].Value = volunteerID;
            cmd.Parameters.Add("@AvailabilityDate", SqlDbType.Date);
            cmd.Parameters["@AvailabilityDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteerAvailabilities.Add(new Availability()
                        {
                            ForeignID = volunteerID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
                        });
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

            return volunteerAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given volunteerID and date.
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Volunteer on a given date</returns>
        /// (Original Author: Kris Howell LocationAccessor.cs)
        public List<Availability> SelectAvailabilityExceptionByVolunteerIDAndDate(int volunteerID, DateTime date)
        {
            List<Availability> volunteerAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_volunteerID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);
            cmd.Parameters["@VolunteerID"].Value = volunteerID;
            cmd.Parameters.Add("@ExceptionDate", SqlDbType.Date);
            cmd.Parameters["@ExceptionDate"].Value = date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(1))
                        {
                            // null start time in DB means user made exception to have no availability on this date
                            // return blank availability object so that no availability is displayed on this date per the exception
                            return new List<Availability>(){
                                new Availability()
                                {
                                    ForeignID = volunteerID,
                                    AvailabilityID = reader.GetInt32(0)
                                }
                            };
                        }

                        volunteerAvailabilities.Add(new Availability()
                        {
                            ForeignID = volunteerID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
                        });
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

            return volunteerAvailabilities;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Selects a volunteer with a specific userID
        /// 
        /// Update:
        /// Jace Pettinger
        /// Updated: 2022/05/04
        /// 
        /// Description:
        /// Added user description to retrieved data
        /// </summary>
        /// <returns>A Volunteer object</returns>
        public Volunteer SelectVolunteerByUserID(int userID)
        {
            Volunteer volunteer = null;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_volunteer_by_userID";
            var cmd = new SqlCommand(cmdTxt, conn);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        volunteer = new Volunteer()
                        {
                            UserID = reader.GetInt32(0),
                            VolunteerID = reader.GetInt32(1),
                            GivenName = reader.GetString(2),
                            FamilyName = reader.GetString(3),
                            State = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            City = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Zip = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            VolunteerType = reader.GetString(7),
                            Email = reader.GetString(8),
                            UserDescription = reader.IsDBNull(9) ? "" : reader.GetString(9)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return volunteer;
        }
    }
}
