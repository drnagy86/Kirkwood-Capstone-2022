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
    public class EventAccessor : IEventAccessor
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Insert event into tadpole_db
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated include TotalBudget field
        /// 
        /// </summary>
        /// <param name="eventName">Name of the event</param>
        /// <param name="eventDescription">Description fo the event</param>
        /// <param name="totalBudget">Total budget planned for event</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEvent(string eventName, string eventDescription, decimal totalBudget)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@TotalBudget", SqlDbType.Money);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;
            cmd.Parameters["@TotalBudget"].Value = totalBudget;


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
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Select active events from tadpole_db
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event object
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// </summary>
        /// <returns>List of active events</returns>
        public List<EventVM> SelectActiveEvents()
        {
            List<EventVM> events = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_events";

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
                        events.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            Active = true
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return events;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Updates a record in the Event table
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// </summary>
        /// <returns>int rows affected</returns>
        public int UpdateEvent(Event oldEvent, Event newEvent)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_event_by_eventID";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", oldEvent.EventID);

            cmd.Parameters.Add("@OldEventName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@OldEventName"].Value = oldEvent.EventName;

            cmd.Parameters.Add("@OldEventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@OldEventDescription"].Value = oldEvent.EventDescription;

            cmd.Parameters.Add("@OldTotalBudget", SqlDbType.Money);
            cmd.Parameters["@OldTotalBudget"].Value = oldEvent.TotalBudget;

            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);
            cmd.Parameters["@OldActive"].Value = oldEvent.Active;

            cmd.Parameters.Add("@NewEventName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewEventName"].Value = newEvent.EventName;

            cmd.Parameters.Add("@NewEventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@NewEventDescription"].Value = newEvent.EventDescription;

            cmd.Parameters.Add("@NewTotalBudget", SqlDbType.Money);
            cmd.Parameters["@NewTotalBudget"].Value = newEvent.TotalBudget;

            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters["@NewActive"].Value = newEvent.Active;

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
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Retrieve an event object by its nabe and Description
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event object
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// </summary>
        /// <param name="eventName">The name of the event</param>
        /// <param name="eventDescription">The description of the event</param>
        /// <returns></returns>
        public EventVM SelectEventByEventNameAndDescription(string eventName, string eventDescription)
        {
            EventVM eventToGet = null;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_event_by_event_name_and_description";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventToGet = new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            Active = true
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
            return eventToGet;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Select list of upcoming dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Select list of upcoming dates with location and event planners
        /// 
        /// Derrick Nagy
        /// Update: 2022/04/17
        /// 
        /// Description:
        /// Updated the fields that the location objects retrieves data from.
        /// Added null checking.
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsUpcomingDates()
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_upcoming_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;

        }

        private List<EventVM> getManagersForEvents(List<EventVM> eventListRef)
        {
            foreach (var item in eventListRef)
            {
                try
                {
                    item.EventManagers = SelectEventPlannersForEvent(item.EventID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return eventListRef;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Select list of upcoming and past dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// Derrick Nagy
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Select list of upcoming and past dates with location and event managers
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsUpcomingAndPastDates()
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_and_future_event_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Select list of past dates
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field

        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Select list of past dates with location and event managers
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectEventsPastDates()
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_dates";
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
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of upcoming dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Select list of upcoming dates for a user with location and event managers
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/26
        /// 
        /// Description:
        /// Added events that have no date
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForUpcomingDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();
            List<EventVM> eventListRefNoDates = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_upcoming_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            string cmdText2 = "sp_select_active_events_with_no_dates_for_user";
            var cmd2 = new SqlCommand(cmdText2, conn);
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd2.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd2.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRefNoDates.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = DateTime.MinValue,
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                UserID = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6),
                                Name = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Description = reader.IsDBNull(8) ? null : reader.GetString(8),
                                PricingInfo = reader.IsDBNull(9) ? null : reader.GetString(9),
                                Phone = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Email = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Address1 = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address2 = reader.IsDBNull(13) ? null : reader.GetString(13),
                                City = reader.IsDBNull(14) ? null : reader.GetString(14),
                                State = reader.IsDBNull(15) ? null : reader.GetString(15),
                                ZipCode = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ImagePath = reader.IsDBNull(17) ? null : reader.GetString(17),
                                Active = true
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRefNoDates = eventDateWithNoDatesVMHelper(eventListRefNoDates);

            eventListRef.AddRange(eventListRefNoDates);

            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of past dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Select list of past dates for a user with location and event managers
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForPastDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/08
        /// 
        /// Description:
        /// Select list of past and upcoming dates for a user
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID to the returned event objects
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Select list of past and upcoming dates for a user with location and event managers
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Event view models</returns>
        public List<EventVM> SelectUserEventsForPastAndUpcomingDates(int userID)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_for_past_and_upcoming_dates_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;

        }

        /// Christopher Repko
        /// Created: 2022/02/09
        /// 
        /// Description:
        /// Updates the location of an event record
        /// </summary>
        /// <param name="eventID">ID of the event</param>
        /// <param name="oldLocationID">The ID of the old location</param>
        /// <param name="newLocationID">The ID of the new location</param>
        /// <returns>int - rows affected</returns>
        public int UpdateEventLocationByEventID(int eventID, int? oldLocationID, int? newLocationID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_update_event_location_by_event_id";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", eventID);
            if (oldLocationID is null)
            {
                cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
                cmd.Parameters["@OldLocationID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
                cmd.Parameters["@OldLocationID"].Value = oldLocationID;
            }
            if (newLocationID is null)
            {

                cmd.Parameters.Add("@LocationID", SqlDbType.Int);
                cmd.Parameters["@LocationID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@LocationID", SqlDbType.Int);
                cmd.Parameters["@LocationID"].Value = newLocationID;
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


            return rowsAffected;


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Removes duplicates and adds the dates to the appropriate event
        /// 
        /// Jace Pettinger
        /// Updated: 2022/02/15
        /// 
        /// Description:
        /// Adding locationID variable to the event list
        /// 
        /// Alaina Gilson
        /// Updated: 2022/02/22
        /// 
        /// Description:
        /// Updated to include TotalBudget field
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Updated to include location information
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="eventListRef">Takes an eventvm list</param>
        /// <returns>A list of Events with no duplicate EventIDs and all the EventDates in a list in the Event object</returns>
        private List<EventVM> eventDateVMHelper(List<EventVM> eventListRef)
        {
            List<EventVM> eventList = new List<EventVM>();
            List<EventDate> allDates = new List<EventDate>();
            if (eventListRef.Count > 0)
            {
                foreach (EventVM item in eventListRef)
                {
                    allDates.Add(item.EventDates[0]);

                    eventList.Add(new EventVM()
                    {
                        EventID = item.EventID,
                        EventName = item.EventName,
                        EventDescription = item.EventDescription,
                        EventCreatedDate = item.EventCreatedDate,
                        TotalBudget = item.TotalBudget,
                        LocationID = item.LocationID,
                        EventDates = new List<EventDate>(),
                        Location = (item.LocationID == null) ? new Location() : new Location()
                        {
                            LocationID = (int)item.LocationID,
                            UserID = item.Location.UserID,
                            Name = item.Location.Name,
                            Description = item.Location.Description,
                            PricingInfo = item.Location.PricingInfo,
                            Phone = item.Location.Phone,
                            Email = item.Location.Email,
                            Address1 = item.Location.Address1,
                            Address2 = item.Location.Address2,
                            City = item.Location.City,
                            State = item.Location.State,
                            ZipCode = item.Location.ZipCode,
                            ImagePath = item.Location.ImagePath,
                            Active = item.Location.Active
                        }
                    });
                }
            }

            //remove duplicates
            List<EventVM> noDuplicates = eventList.GroupBy(e => e.EventID).Select(e => e.First()).ToList();

            foreach (EventVM item in eventList)
            {
                for (int i = 0; i < allDates.Count; i++)
                {                    

                    if (item.EventID == allDates[i].EventID && allDates[i].EventDateID != DateTime.MinValue)
                    {
                        item.EventDates.Add(allDates[i]);
                    }
                }
            }

            // take out no dates
            List<EventVM> noDates = noDuplicates.FindAll(e => e.EventDates.Count == 0);
            List<EventVM> eventsWithDates = noDuplicates.FindAll(e => e.EventDates.Count > 0);

            // sort by earliest date
            eventsWithDates.Sort((ev1, ev2) => ev1.EventDates[0].EventDateID.CompareTo(ev2.EventDates[0].EventDateID));

            noDates.AddRange(eventsWithDates);

            return noDates;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// Removes duplicates and adds the dates to the appropriate event
        /// These events have no dates
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="eventListRef">Takes an eventvm list</param>
        /// <returns>A list of Events with no duplicate EventIDs and all the EventDates in a list in the Event object</returns>
        private List<EventVM> eventDateWithNoDatesVMHelper(List<EventVM> eventListRef)
        {
            List<EventVM> eventList = new List<EventVM>();
            List<EventDate> allDates = new List<EventDate>();
            if (eventListRef.Count > 0)
            {
                foreach (EventVM item in eventListRef)
                {
                    allDates.Add(item.EventDates[0]);

                    eventList.Add(new EventVM()
                    {
                        EventID = item.EventID,
                        EventName = item.EventName,
                        EventDescription = item.EventDescription,
                        EventCreatedDate = item.EventCreatedDate,
                        TotalBudget = item.TotalBudget,
                        LocationID = item.LocationID,
                        EventDates = new List<EventDate>(),
                        Location = (item.LocationID == null) ? new Location() : new Location()
                        {
                            LocationID = (int)item.LocationID,
                            UserID = item.Location.UserID,
                            Name = item.Location.Name,
                            Description = item.Location.Description,
                            PricingInfo = item.Location.PricingInfo,
                            Phone = item.Location.Phone,
                            Email = item.Location.Email,
                            Address1 = item.Location.Address1,
                            Address2 = item.Location.Address2,
                            City = item.Location.City,
                            State = item.Location.State,
                            ZipCode = item.Location.ZipCode,
                            ImagePath = item.Location.ImagePath,
                            Active = item.Location.Active
                        }
                    });
                }
            }

            //remove duplicates
            List<EventVM> noDuplicates = eventList.GroupBy(e => e.EventID).Select(e => e.First()).ToList();

            foreach (EventVM item in eventList)
            {
                for (int i = 0; i < allDates.Count; i++)
                {

                    if (item.EventID == allDates[i].EventID && allDates[i].EventDateID != DateTime.MinValue)
                    {
                        item.EventDates.Add(allDates[i]);
                    }
                }
            }

            // take out no dates
            List<EventVM> noDates = noDuplicates.FindAll(e => e.EventDates.Count == 0);
            List<EventVM> eventsWithDates = noDuplicates.FindAll(e => e.EventDates.Count > 0);


            noDates.AddRange(eventsWithDates);

            return noDates;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/18
        /// 
        /// Description:
        /// Inserts an event into the database and returns the auto-increment value created for the event id
        /// </summary>
        /// <param name="eventName">The name of the evnet</param>
        /// <param name="eventDescription">The description of the event</param>
        /// <param name="totalBudget">The budget for the event</param>
        /// <param name="userID">The userID of the user who created the event</param>
        /// <returns>Event ID as an int</returns>
        public int InsertEventReturnsEventID(string eventName, string eventDescription, decimal totalBudget, int userID)
        {
            int eventID = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_event_with_user_ID_return_event_id";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@EventDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@TotalBudget", SqlDbType.Money);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@EventName"].Value = eventName;
            cmd.Parameters["@EventDescription"].Value = eventDescription;
            cmd.Parameters["@TotalBudget"].Value = totalBudget;
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                Object result = cmd.ExecuteScalar();
                eventID = (int)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return eventID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Checks to see if the user has permission to edit the event
        /// 
        /// </summary>
        /// <param name="eventID">The event id</param>
        /// <param name="userID">The user id</param>
        /// <returns>True if the user can edit, false if not</returns>
        public bool CheckUserEditPermissionForEvent(int eventID, int userID)
        {

            bool result = false;
            List<Role> roles = new List<Role>();

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_user_roles_for_event";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@EventId"].Value = eventID;
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0)
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

            foreach (Role role in roles)
            {
                if (role.RoleID == "Event Manager" || role.RoleID == "Event Planner")
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// 2022/03/24
        /// 
        /// Description:
        /// Returns Users with the event Manager role for an event
        /// 
        /// </summary>
        /// <param name="eventID">The Event ID</param>
        /// <returns>List of event managers</returns>
        public List<User> SelectEventPlannersForEvent(int eventID)
        {
            List<User> users = new List<User>();

            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_event_planners_for_event";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventId"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            UserID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            EmailAddress = reader.GetString(3),
                            State = reader.GetString(4),
                            City = reader.GetString(5),
                            Zip = reader.GetInt32(6),
                            Active = reader.GetBoolean(7)
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

            return users;
        }

        /// <summary>
        /// Derrick Nagy
        /// 2022/04/06
        /// 
        /// Description:
        /// Returns a list that includes the search word
        /// in the event name, description, or in the location name, city, or state
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/17
        /// 
        /// Description:
        /// Null checking for location objects
        /// 
        /// 
        /// </summary>
        /// <param name="search">Search criteria</param>
        /// <returns>List of EventVMs that contain search criteria</returns>
        public List<EventVM> SelectEventsForSearch(string search)
        {
            List<EventVM> eventListRef = new List<EventVM>();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_active_events_by_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 50);
            cmd.Parameters["@Search"].Value = search;


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventListRef.Add(new EventVM()
                        {
                            EventID = reader.GetInt32(0),
                            EventName = reader.GetString(1),
                            EventDescription = reader.GetString(2),
                            EventCreatedDate = reader.GetDateTime(3),
                            TotalBudget = reader.GetDecimal(4),
                            LocationID = reader.IsDBNull(5) ? null : (int?)reader.GetInt32(5),
                            EventDates = new List<EventDate>()
                                    {
                                        new EventDate()
                                        {
                                            EventDateID = reader.GetDateTime(6),
                                            EventID = reader.GetInt32(0),
                                            Active = true
                                        }
                                    },
                            Active = true,
                            Location = reader.IsDBNull(5) ? new Location() : new Location()
                            {
                                LocationID = reader.GetInt32(5),
                                UserID = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                                Name = reader.GetString(8),
                                Description = reader.IsDBNull(9) ? null : reader.GetString(9),
                                PricingInfo = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Phone = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Email = reader.IsDBNull(12) ? null : reader.GetString(12),
                                Address1 = reader.GetString(13),
                                Address2 = reader.IsDBNull(14) ? null : reader.GetString(14),
                                City = reader.IsDBNull(15) ? null : reader.GetString(15),
                                State = reader.IsDBNull(16) ? null : reader.GetString(16),
                                ZipCode = reader.IsDBNull(17) ? null : reader.GetString(17),
                                ImagePath = reader.IsDBNull(18) ? null : reader.GetString(18),
                                Active = reader.GetBoolean(19)
                            }
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

            eventListRef = eventDateVMHelper(eventListRef);
            eventListRef = getManagersForEvents(eventListRef);

            return eventListRef;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/04/01
        /// 
        /// Description: returns event using eventID
        /// 
        /// Derrick Nagy
        /// Updated: 2022/04/24
        /// 
        /// Description:
        /// Fixed index error when returning the Location ID field
        /// 
        /// 
        /// </summary>
        /// <param name="eventID">The event ID</param>
        /// <returns>An event view model object</returns>
        public EventVM SelectEventByEventID(int eventID)
        {
            EventVM eventToGet = null;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_event_by_event_id";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            cmd.Parameters["@EventID"].Value = eventID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        eventToGet = new EventVM()
                        {
                            EventID = eventID,
                            EventName = reader.GetString(0),
                            EventDescription = reader.GetString(1),
                            EventCreatedDate = reader.GetDateTime(2),
                            TotalBudget = reader.GetDecimal(3),
                            LocationID = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                            Active = true
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
            return eventToGet;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Deactivate event with given eventID
        /// </summary>
        /// <param name="eventID">id of event to deactivate</param>
        /// <returns></returns>
        public int DeactivateEventByEventID(int eventID)
        {
            int rowsAffected;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_deactivate_event_by_eventID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters["@EventID"].Value = eventID;

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
