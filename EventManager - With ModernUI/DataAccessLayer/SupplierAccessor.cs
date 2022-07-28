using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace DataAccessLayer
{
    public class SupplierAccessor : ISupplierAccessor
    {
        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Select all active suppliers from tadpole_db
        /// 
        /// Kris Howell
        /// Updated: 2022/02/18
        /// 
        /// Description:
        /// Add City, State, ZipCode to match sp
        /// 
        /// </summary>
        /// <returns>List of all active suppliers</returns>
        public List<Supplier> SelectActiveSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_active_suppliers";

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
                        suppliers.Add(new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            TypeID = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
                            Active = true,
                            Approved = true
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

            return suppliers;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of images from the database
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for images</param>
        /// <returns>A list of images for the supplier ID</returns>
        public List<string> SelectSupplierImagesBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_images";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
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

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of reviews from the database
        /// </summary>
        /// 
        /// <update>
        /// Emma Pollock 
        /// Updated: 2022/04/22
        /// 
        /// Description:
        /// Added UserID field
        /// </update>
        /// 
        /// <param name="supplierID">Supplier ID to use to search for reviews</param>
        /// <returns>A list of reviews for the supplier ID</returns>
        public List<Reviews> SelectSupplierReviewsBySupplierID(int supplierID)
        {
            List<Reviews> result = new List<Reviews>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_reviews";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(new Reviews()
                        {
                            ReviewID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            FullName = reader.GetString(2),
                            ReviewType = reader.GetString(3),
                            Rating = reader.GetInt32(4),
                            Review = reader.GetString(5),
                            DateCreated = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7),
                            ForeignID = supplierID
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

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Function to take a supplier ID and get a list of tags from the database
        /// </summary>
        /// <param name="supplierID">Supplier ID to use to search for tags</param>
        /// <returns>A list of tags for the supplier ID</returns>
        public List<string> SelectSupplierTagsBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_tags";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
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

            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given supplierID and date.
        /// 
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Added exception
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
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
                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            TimeStart = DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given supplierID and date.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_supplierID_and_date";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
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
                                    ForeignID = supplierID,
                                    AvailabilityID = reader.GetInt32(0)
                                }
                            };
                        }

                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select availability records matching the given supplierID 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<AvailabilityVM> SelectSupplierAvailabilityBySupplierID(int supplierID)
        {
            List<AvailabilityVM> supplierAvailabilities = new List<AvailabilityVM>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierAvailabilities.Add(new AvailabilityVM()
                        {
                            ForeignID = supplierID,
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select availability exception records matching the given supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierID(int supplierID)
        {
            List<Availability> supplierAvailabilities = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_exception_by_supplierID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierAvailabilities.Add(new Availability()
                        {
                            ForeignID = supplierID,
                            AvailabilityID = reader.GetInt32(0),
                            DateID = reader.GetDateTime(1),
                            TimeStart = reader.IsDBNull(2) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeStart"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture),
                            TimeEnd = reader.IsDBNull(3) ? DateTime.MinValue : DateTime.ParseExact(reader["TimeEnd"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture)
                        });
                        if(supplierAvailabilities.Last().TimeStart == DateTime.MinValue && supplierAvailabilities.Last().TimeEnd == DateTime.MinValue)
                        {
                            supplierAvailabilities.Last().TimeStart = null;
                            supplierAvailabilities.Last().TimeEnd = null;
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

            return supplierAvailabilities;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Selects a supplier from the supplier table.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A supplier with the given supplierId</returns>
        public Supplier SelectSupplierBySupplierID(int supplierID)
        {
            Supplier supplier = null;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_supplier_by_supplierID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplier = new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            TypeID = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            Active = true,
                            ZipCode = reader.GetString(11),
                            Approved = reader.IsDBNull(12) ? (bool?)null : reader.GetBoolean(12)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return supplier;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Accessor that returns a list of dates that the supplier has available for the next three months
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public List<DateTime> SelectSupplierAvailabilityForNextThreeMonths(int supplierID)
        {
            List<DateTime> threeMonthAvailability = new List<DateTime>();
            List<Availability> weeklyAvailability = new List<Availability>();
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_availability_by_supplierID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        weeklyAvailability.Add(new Availability()
                        {
                            ForeignID = supplierID,
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
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            foreach (Availability availability in weeklyAvailability)
            {
                DateTime timeStart = (DateTime)availability.TimeStart;
                // get about 13 weeks of dates, drop any that happen before or after
                for (int i = 0; i < 92; i += 7)
                {
                    int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

                    if (availability.Sunday)
                    {
                        // target day - current day
                        int diff = 0 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Monday)
                    {
                        int diff = 1 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Tuesday)
                    {
                        int diff = 2 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Wednesday)
                    {
                        int diff = 3 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Thursday)
                    {
                        int diff = 4 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Friday)
                    {
                        int diff = 5 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                    if (availability.Saturday)
                    {
                        int diff = 6 - currentDayOfWeek;
                        DateTime date = DateTime.Today.AddDays(diff + i);
                        threeMonthAvailability.Add(date);
                    }
                }
            }

            // remove unwanted dates


            return threeMonthAvailability;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// Inserts a review into the review table and a supplier review into the SupplierReview table
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rowsAffected</returns>
        public int InsertSupplierReview(Reviews review)
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

            cmdTxt = "sp_insert_supplier_review";
            cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReviewID", SqlDbType.Int);
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);

            cmd.Parameters["@ReviewID"].Value = reviewID;
            cmd.Parameters["@SupplierID"].Value = review.ForeignID;
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
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Function to retrieve a list of unapproved suppliers. 
        /// </summary>
        /// <returns>A list of unapproved suppliers</returns>
        public List<Supplier> SelectUnapprovedSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_unapproved_suppliers";
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
                        suppliers.Add(new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            TypeID = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
                            Active = true,
                            Approved = null
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
			
            return suppliers;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Marks a supplier as being approved
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int ApproveSupplier(int supplierID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_approve_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/20
        /// 
        /// Description:
        /// Accessor that returns inserts a new requested supplier
        /// 
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>rows affected</returns>
        public int InsertSupplier(Supplier supplier)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", supplier.UserID);
            cmd.Parameters.AddWithValue("@SupplierName", supplier.Name);
            cmd.Parameters.AddWithValue("@SupplierDescription", supplier.Description);
            cmd.Parameters.AddWithValue("@SupplierPhone", supplier.Phone);
            cmd.Parameters.AddWithValue("@SupplierEmail", supplier.Email);
            //cmd.Parameters.AddWithValue("@SupplierTypeID", supplier.TypeID);
            cmd.Parameters.AddWithValue("@SupplierAddress1", supplier.Address1);
            cmd.Parameters.AddWithValue("@SupplierCity", supplier.City);
            cmd.Parameters.AddWithValue("@SupplierState", supplier.State);
            cmd.Parameters.AddWithValue("@SupplierZipCode", supplier.ZipCode);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }


            return rows;
        }
            

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Marks a supplier as being disapproved
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int DisapproveSupplier(int supplierID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_disapprove_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Marks a supplier as needing adminstrator review again.
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int RequeueSupplier(int supplierID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_requeue_supplier_application";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters["@SupplierID"].Value = supplierID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Grabs a list of all suppliers with a given userID.
        /// 
        /// Christopher Repko
        /// Updated: 2022/05/04
        /// 
        /// Description:
        /// Made TypeID not crash the application when it's null.
        /// </summary>
        /// <param name="userID">UserID to be searched for.</param>
        /// <returns>a list of all suppliers with a given userID</returns>
        public List<Supplier> SelectSuppliersByUserID(int userID)
        {
            List<Supplier> suppliers = new List<Supplier>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_suppliers_by_userID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new Supplier()
                        {
                            SupplierID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            TypeID = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address1 = reader.GetString(7),
                            Address2 = reader.IsDBNull(8) ? null : reader.GetString(8),
                            City = reader.GetString(9),
                            State = reader.GetString(10),
                            ZipCode = reader.GetString(11),
                            Active = true,
                            Approved = reader.IsDBNull(12) ? (bool?)null : reader.GetBoolean(12)
                        });
                    }
                }
            } catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return suppliers;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Method to replace one supplier with another in the data.
        /// </summary>
        /// <param name="oldSupplier">Supplier to replace</param>
        /// <param name="newSupplier">Supplier to replace with</param>
        /// <returns>rows affected</returns>
        public int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSupplierName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@OldSupplierDescription", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@OldSupplierPhone", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@OldSupplierEmail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@OldSupplierAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplierAddress2", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplierCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplierState", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldSupplierZipCode", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@NewSupplierName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@NewSupplierDescription", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@NewSupplierPhone", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@NewSupplierEmail", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewSupplierAddress1", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplierAddress2", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplierCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplierState", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewSupplierZipCode", SqlDbType.NVarChar, 100);

            cmd.Parameters["@SupplierID"].Value = oldSupplier.SupplierID;
            cmd.Parameters["@OldSupplierName"].Value = oldSupplier.Name; 
            if (oldSupplier.Description == null)
            {
                cmd.Parameters["@OldSupplierDescription"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@OldSupplierDescription"].Value = oldSupplier.Description;
            }
            cmd.Parameters["@OldSupplierPhone"].Value = oldSupplier.Phone;
            cmd.Parameters["@OldSupplierEmail"].Value = oldSupplier.Email;
            cmd.Parameters["@OldSupplierAddress1"].Value = oldSupplier.Address1;
            if (oldSupplier.Address2 == null)
            {
                cmd.Parameters["@OldSupplierAddress2"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@OldSupplierAddress2"].Value = oldSupplier.Address1;
            }
            cmd.Parameters["@OldSupplierCity"].Value = oldSupplier.City;
            cmd.Parameters["@OldSupplierState"].Value = oldSupplier.State;
            cmd.Parameters["@OldSupplierZipCode"].Value = oldSupplier.ZipCode;
            cmd.Parameters["@NewSupplierName"].Value = newSupplier.Name;
            if (newSupplier.Description == null)
            {
                cmd.Parameters["@NewSupplierDescription"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@NewSupplierDescription"].Value = newSupplier.Description;
            }
            cmd.Parameters["@NewSupplierPhone"].Value = newSupplier.Phone;
            cmd.Parameters["@NewSupplierEmail"].Value = newSupplier.Email;
            cmd.Parameters["@NewSupplierAddress1"].Value = newSupplier.Address1;
            if(newSupplier.Address2 == null)
            {
                cmd.Parameters["@NewSupplierAddress2"].Value = DBNull.Value;
            } else
            {
                cmd.Parameters["@NewSupplierAddress2"].Value = newSupplier.Address1;
            }
            cmd.Parameters["@NewSupplierCity"].Value = newSupplier.City;
            cmd.Parameters["@NewSupplierState"].Value = newSupplier.State;
            cmd.Parameters["@NewSupplierZipCode"].Value = newSupplier.ZipCode;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

                

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Insert a supplier availability into database
        /// </summary>
        /// <param name="availability">object to insert</param>
        /// <returns></returns>
        public int InsertSupplierAvailability(Availability availability)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_availability_for_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@TimeStart", SqlDbType.Time);
            cmd.Parameters.Add("@TimeEnd", SqlDbType.Time);
            cmd.Parameters.Add("@Sunday", SqlDbType.Bit);
            cmd.Parameters.Add("@Monday", SqlDbType.Bit);
            cmd.Parameters.Add("@Tuesday", SqlDbType.Bit);
            cmd.Parameters.Add("@Wednesday", SqlDbType.Bit);
            cmd.Parameters.Add("@Thursday", SqlDbType.Bit);
            cmd.Parameters.Add("@Friday", SqlDbType.Bit);
            cmd.Parameters.Add("@Saturday", SqlDbType.Bit);

            cmd.Parameters["@SupplierID"].Value = availability.ForeignID;
            cmd.Parameters["@TimeStart"].Value = availability.TimeStart.Value.TimeOfDay;
            cmd.Parameters["@TimeEnd"].Value = availability.TimeEnd.Value.TimeOfDay;
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
