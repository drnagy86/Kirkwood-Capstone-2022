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
    /// Kris Howell
    /// Created: 2022/02/03
    /// 
    /// The LocationAccessor data access class for all location data 
    /// </summary>
    public class LocationAccessor : ILocationAccessor
    {
        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Select all active locations from tadpole_db
        /// 
        /// </summary>
        /// <returns>List of all active locations</returns>
        public List<Location> SelectActiveLocations()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_locations";

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
                        locations.Add(new Location()
                        {
                            LocationID = reader.GetInt32(0),
                            UserID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PricingInfo = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Phone = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Email = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
                            ImagePath = reader.IsDBNull(12) ? null : reader.GetString(12),
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

            return locations;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Accessor method that that selects the location matching the locationID provided and returns a location 
        /// data object. Will be null if no location is found
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A Location object</returns>
        public Location SelectLocationByLocationID(int locationID)
        {
            Location location = new Location();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_by_locationID";

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
                        location = new Location()
                        {
                            LocationID = locationID,
                            UserID = reader.IsDBNull(0) ? null : (int?)reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            PricingInfo = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Address1 = reader.GetString(6),
                            Address2 = reader.IsDBNull(7) ? null : reader.GetString(7),
                            City = reader.IsDBNull(8) ? null : reader.GetString(8),
                            State = reader.IsDBNull(9) ? null : reader.GetString(9),
                            ZipCode = reader.IsDBNull(10) ? null : reader.GetString(10),
                            ImagePath = reader.IsDBNull(11) ? null : reader.GetString(11),
                            // Active = reader.GetBoolean(12)
                        };
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

            return location;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Accessor method that that selects the location images matching the locationID provided and returns a list of location image
        /// data objects. Will be null if no location images are found
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A List of LocationImage objects</returns>
        public List<LocationImage> SelectLocationImagesByLocationID(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_images";

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
                        locationImages.Add(new LocationImage()
                        {
                            LocationID = locationID,
                            ImageName = reader.GetString(0)
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

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Accessor method that that selects the location reviews matching the locationID provided and returns a list of location review
        /// data objects. Will be null if no location reviews are found
        /// </summary>
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/27
        /// Added UserID field
        /// </update>
        /// 
        /// <param name="locationID"></param>
        /// <returns>A List of LocationReview objects</returns>
        public List<Reviews> SelectLocationReviews(int locationID)
        {
            List<Reviews> locationReviews = new List<Reviews>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_location_reviews";

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
                        locationReviews.Add(new Reviews()
                        {
                            ForeignID = locationID,
                            UserID = reader.GetInt32(0),
                            ReviewID = reader.GetInt32(1),
                            FullName = reader.GetString(2),
                            ReviewType = reader.GetString(3),
                            Rating = reader.GetInt32(4),
                            Review = reader.IsDBNull(5) ? null : reader.GetString(5),
                            DateCreated = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7)
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

            return locationReviews;
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Insert event into tadpole_db
        /// 
        /// </summary>
        /// <param name="locationName">Name of the Location</param>
        /// <param name="address">address of the location</param>
        /// <param name="locationCity">address of the location</param>
        /// <param name="locationState">address of the location</param>
        /// <param name="locationZipCode">address of the location</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_location_by_name_address_city_state_zip";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@LocationAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationState", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@locationZipCode", SqlDbType.NVarChar, 100);

            cmd.Parameters["@LocationName"].Value = locationName;
            cmd.Parameters["@LocationAddress1"].Value = address;
            cmd.Parameters["@locationCity"].Value = locationCity;
            cmd.Parameters["@locationState"].Value = locationState;
            cmd.Parameters["@locationZipCode"].Value = locationZipCode;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Returns a matching location record
        /// 
        /// </summary>
        /// <param name="locationName">Name of the Location</param>
        /// <param name="address">address of the location</param>
        public Location SelectLocationByLocationNameAndAddress(string locationName, string address)
        {
            Location _matchingLocation = new Location();

            var conn = DBConnection.GetConnection();
            string cmdText = "sp_select_location_by_name_and_address";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@LocationAddress1", SqlDbType.NVarChar, 100);

            cmd.Parameters["@LocationName"].Value = locationName;
            cmd.Parameters["@LocationAddress1"].Value = address;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _matchingLocation = new Location()
                        {
                            LocationID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            PricingInfo = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Address1 = reader.GetString(6),
                            Active = true
                        };
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                conn.Close();
            }
            return _matchingLocation;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Accessor method that that deactivates a location in the data store by its locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>int number of rows affected</returns>
        public int DeactivateLocationByLocationID(int locationID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_deactivate_location_by_locationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;

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
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given locationID and date.
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Location on a given date</returns>
        public List<Availability> SelectLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date)
        {
            List<Availability> locationAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_locationID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;
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
                        locationAvailabilities.Add(new Availability()
                        {
                            ForeignID = locationID,
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

            return locationAvailabilities;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given locationID and date.
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Location on a given date</returns>
        public List<Availability> SelectLocationAvailabilityExceptionByLocationIDAndDate(int locationID, DateTime date)
        {
            List<Availability> locationAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_locationID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = locationID;
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
                                    ForeignID = locationID,
                                    AvailabilityID = reader.GetInt32(0)
                                }
                            };
                        }

                        locationAvailabilities.Add(new Availability()
                        {
                            ForeignID = locationID,
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

            return locationAvailabilities;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// update a location bio information (description, phone, email, address, pricing)
        /// for a location in the location table
        /// 
        /// </summary>
        /// <returns>List of all active locations</returns>
        public int UpdateLocationBioByLocationID(Location oldLocation, Location newLocation)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_location_bio_by_locationID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = oldLocation.LocationID;
           
            if(oldLocation.Description != null && oldLocation.Description != "")
            {
                cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 3000);
                cmd.Parameters["@OldDescription"].Value = oldLocation.Description;
            } 

            if(oldLocation.Phone != null || oldLocation.Phone != "")
            {
                cmd.Parameters.Add("@OldPhone", SqlDbType.NVarChar, 15);
                cmd.Parameters["@OldPhone"].Value = oldLocation.Phone;
            } 

            if(oldLocation.Email != null && oldLocation.Email != "")
            {
                cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 250);
                cmd.Parameters["@OldEmail"].Value = oldLocation.Email;
            }

            cmd.Parameters.Add("@OldAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters["@OldAddress1"].Value = oldLocation.Address1;
            
            if(oldLocation.Address2 != null && oldLocation.Address2 != "")
            {
                cmd.Parameters.Add("@OldAddress2", SqlDbType.NVarChar, 100);
                cmd.Parameters["@OldAddress2"].Value = oldLocation.Address2;
            } 

            if (oldLocation.PricingInfo != null && oldLocation.PricingInfo != "")
            {
                cmd.Parameters.Add("@OldPricing", SqlDbType.NVarChar, 3000);
                cmd.Parameters["@OldPricing"].Value = oldLocation.PricingInfo;
            }
            
            if (newLocation.Description != null && newLocation.Description != "")
            {
                cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 3000);
                cmd.Parameters["@NewDescription"].Value = newLocation.Description;
            }
           
            if (newLocation.Phone != null && newLocation.Phone != "")
            {
                cmd.Parameters.Add("@NewPhone", SqlDbType.NVarChar, 15);
                cmd.Parameters["@NewPhone"].Value = newLocation.Phone;
            }
           
            if (newLocation.Email != null && newLocation.Email != "")
            {
                cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 250);
                cmd.Parameters["@NewEmail"].Value = newLocation.Email;
            }

            cmd.Parameters.Add("@NewAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters["@NewAddress1"].Value = newLocation.Address1;

            if (newLocation.Address2 != null && newLocation.Address2 != "")
            {
                cmd.Parameters.Add("@NewAddress2", SqlDbType.NVarChar, 100);
                cmd.Parameters["@NewAddress2"].Value = newLocation.Address2;
            }
            
            if (newLocation.PricingInfo != null && newLocation.PricingInfo != "")
            {
                cmd.Parameters.Add("@NewPricing", SqlDbType.NVarChar, 3000);
                cmd.Parameters["@NewPricing"].Value = newLocation.PricingInfo;
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

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given locationID;
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of availability objects for a Location</returns>
        public List<AvailabilityVM> SelectLocationAvailabilityByLocationID(int locationID)
        {
            List<AvailabilityVM> locationAvailabilities = new List<AvailabilityVM>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_locationID";

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
                        locationAvailabilities.Add(new AvailabilityVM()
                        {
                            ForeignID = locationID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            Sunday = reader.GetBoolean(3),
                            Monday = reader.GetBoolean(4),
                            Tuesday = reader.GetBoolean(5),
                            Wednesday = reader.GetBoolean(6),
                            Thursday = reader.GetBoolean(7),
                            Friday = reader.GetBoolean(8),
                            Saturday = reader.GetBoolean(9)
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

            return locationAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of availability objects for a Location</returns>
        public List<Availability> SelectLocationAvailabilityExceptionByLocationID(int locationID)
        {
            List<Availability> locationAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_locationID";
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
                        locationAvailabilities.Add(new Availability()
                        {
                            ForeignID = locationID,
                            AvailabilityID = reader.GetInt32(0),
                            DateID = reader.GetDateTime(1),
                            TimeStart = reader.IsDBNull(2) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = reader.IsDBNull(3) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
                        });
                        if (locationAvailabilities.Last().TimeStart == DateTime.MinValue && locationAvailabilities.Last().TimeEnd == DateTime.MinValue)
                        {
                            locationAvailabilities.Last().TimeStart = null;
                            locationAvailabilities.Last().TimeEnd = null;
                        }
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

            return locationAvailabilities;
		}

        /// Logan Baccam
        /// Created: 2022/04/03
        /// 
        /// Description:
        /// returns a list of tags associated with the location
        /// 
        /// </summary>
        /// <param name="locationID"
        /// <returns>List of all active locations</returns>
        public List<string> SelectTagsbyLocationID(int locationID)
        {
            List<string> tags = new List<string>();

            var cmdText = "sp_select_tags_by_locationID";
            var conn = DBConnection.GetConnection();
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
                        tags.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tags;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Inserts a review into the review table and a location review into the LocationReview table
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rowsAffected</returns>
        public int InsertLocationReview(Reviews review)
        {
            int rowsAffected = 0;

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_insert_review";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ReviewType", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Rating", SqlDbType.Int);
            cmd.Parameters.Add("@Review", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime);

            cmd.Parameters["@UserID"].Value = review.UserID;
            cmd.Parameters["@ReviewType"].Value = review.ReviewType;
            cmd.Parameters["@Rating"].Value = review.Rating;
            cmd.Parameters["@Review"].Value = review.Review;
            cmd.Parameters["@DateCreated"].Value = review.DateCreated;


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

            // connection
            conn = DBConnection.GetConnection();

            int reviewID = 0;
            cmdTxt = "sp_select_review_id_by_review";
            cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ReviewType", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Rating", SqlDbType.Int);
            cmd.Parameters.Add("@Review", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime);

            cmd.Parameters["@UserID"].Value = review.UserID;
            cmd.Parameters["@ReviewType"].Value = review.ReviewType;
            cmd.Parameters["@Rating"].Value = review.Rating;
            cmd.Parameters["@Review"].Value = review.Review;
            cmd.Parameters["@DateCreated"].Value = review.DateCreated;


            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reviewID = reader.GetInt32(0);
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


            // connection
            conn = DBConnection.GetConnection();

            cmdTxt = "sp_insert_location_review";
            cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReviewID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationID", SqlDbType.Int);

            cmd.Parameters["@ReviewID"].Value = reviewID;
            cmd.Parameters["@LocationID"].Value = review.ForeignID;


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