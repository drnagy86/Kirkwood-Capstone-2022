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
    public class EventDateAccessor : IEventDateAccessor
    {

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Insert data about the date of the event into the EventDate table in the tadpole database
        /// 
        /// </summary>
        /// <param name="eventDate">An EventDate object</param>
        /// <returns>Rows added</returns>
        public int InsertEventDate(EventDate eventDate)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event_date";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventDateID", SqlDbType.DateTime2);
            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters.Add("@StartTime", SqlDbType.DateTime2);
            cmd.Parameters.Add("@EndTime", SqlDbType.DateTime2);

            cmd.Parameters["@EventDateID"].Value = eventDate.EventDateID;
            cmd.Parameters["@EventID"].Value = eventDate.EventID;
            cmd.Parameters["@StartTime"].Value = eventDate.StartTime;
            cmd.Parameters["@EndTime"].Value = eventDate.EndTime;


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
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Returna specific date for an event
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="eventDateID"></param>
        /// <returns>An EventDate object</returns>
        public EventDate SelectEventDateByEventDateIDAndEventID(DateTime eventDateID, int eventID)
        {
            EventDate result = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_event_date_by_event_dateID_and_eventID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventDateID", SqlDbType.Date);
            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            cmd.Parameters["@EventID"].Value = eventID;
            cmd.Parameters["@EventDateID"].Value = eventDateID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {                  
                    while (reader.Read())
                    {
                        result = new EventDate()
                        {
                            EventDateID = eventDateID,
                            EventID = eventID,
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Active = true
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
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Return list of dates for an event
        /// 
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns>List of EventDate</returns>
        public List<EventDate> SelectEventDatesByEventID(int eventID)
        {
            List<EventDate> eventDates = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_event_dates_by_eventID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    eventDates = new List<EventDate>();

                    while (reader.Read())
                    {
                        eventDates.Add(new EventDate()
                        {
                            EventDateID = DateTime.Parse(reader["EventDateID"].ToString()),
                            EventID = reader.GetInt32(1),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Active = true
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

            return eventDates;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Updates a record in the EventDate table
        /// 
        /// </summary>
        /// <returns>int rows affected</returns>
        public int UpdateEventDate(EventDate oldEventDate, EventDate newEventDate)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_event_date";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", oldEventDate.EventID);

            cmd.Parameters.Add("@OldEventDateID", SqlDbType.DateTime2);
            cmd.Parameters["@OldEventDateID"].Value = oldEventDate.EventDateID;

            cmd.Parameters.Add("@OldStartTime", SqlDbType.DateTime2);
            cmd.Parameters["@OldStartTime"].Value = oldEventDate.StartTime;

            cmd.Parameters.Add("@OldEndTime", SqlDbType.DateTime2);
            cmd.Parameters["@OldEndTime"].Value = oldEventDate.EndTime;

            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);
            cmd.Parameters["@OldActive"].Value = oldEventDate.Active;

            cmd.Parameters.Add("@NewEventDateID", SqlDbType.DateTime2);
            cmd.Parameters["@NewEventDateID"].Value = newEventDate.EventDateID;

            cmd.Parameters.Add("@NewStartTime", SqlDbType.DateTime2);
            cmd.Parameters["@NewStartTime"].Value = newEventDate.StartTime;

            cmd.Parameters.Add("@NewEndTime", SqlDbType.DateTime2);
            cmd.Parameters["@NewEndTime"].Value = newEventDate.EndTime;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newEventDate.Active;

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
        /// Austin Timmerman
        /// Created: 2022/02/10
        /// 
        /// Description:
        /// Accessor method for selecting event dates by LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> SelectEventDatesByLocationID(int locationID)
        {
            List<EventDateVM> eventDatesForLocation = new List<EventDateVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_event_dates_by_location_id";

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
                        eventDatesForLocation.Add(new EventDateVM()
                        {
                            LocationID = locationID,
                            EventDateID = DateTime.Parse(reader["EventDateID"].ToString()),
                            EventID = reader.GetInt32(1),
                            EventName = reader.GetString(2),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Active = true
                        });

                        eventDatesForLocation.Last().StartTime = new DateTime(eventDatesForLocation.Last().EventDateID.Year, eventDatesForLocation.Last().EventDateID.Month, eventDatesForLocation.Last().EventDateID.Day, eventDatesForLocation.Last().StartTime.Hour, eventDatesForLocation.Last().StartTime.Minute, eventDatesForLocation.Last().StartTime.Second);
                        eventDatesForLocation.Last().EndTime = new DateTime(eventDatesForLocation.Last().EventDateID.Year, eventDatesForLocation.Last().EventDateID.Month, eventDatesForLocation.Last().EventDateID.Day, eventDatesForLocation.Last().EndTime.Hour, eventDatesForLocation.Last().EndTime.Minute, eventDatesForLocation.Last().EndTime.Second);
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

            return eventDatesForLocation;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/01
        /// 
        /// Description:
        /// Accessor method for selecting event dates by userID and date
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="eventDate"></param>
        /// <returns>A list of EventDateVM data objects</returns>
        public List<EventDateVM> SelectEventDateByUserIDAndDate(int userID, DateTime eventDate)
        {
            List<EventDateVM> eventDates = new List<EventDateVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_event_date_by_userID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;

            cmd.Parameters.Add("@EventDate", SqlDbType.Date);

            cmd.Parameters["@EventDate"].Value = eventDate.Date;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventDates.Add(new EventDateVM()
                        {
                            EventName = reader.GetString(0),
                            EventID = reader.GetInt32(1),
                            StartTime = DateTime.ParseExact(reader["StartTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader["EndTime"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            EventDateID = eventDate,
                            Active = true
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

            return eventDates;
        }
    }
}
