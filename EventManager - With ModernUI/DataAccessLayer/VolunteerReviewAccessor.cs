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
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/10
    /// 
    /// Description:
    /// The VolunteerReviewAccessor class for all accessor methods relating to Volunteer Reviews
    /// </summary>
    public class VolunteerReviewAccessor : IVolunteerReviewAccessor
    {
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Method which select all volunteer reviews by the passed in volunteerID
        /// 
        /// <update>
        /// Emma Pollock
        /// Updated: 2022/04/27
        /// Added UserID field
        /// </update>
        /// </summary>
        public List<Reviews> SelectVolunteerReviewsByVolunteerID(int volunteerID)
        {
            List<Reviews> volunteerReviews = new List<Reviews>();

            var conn = DBConnection.GetConnection();

            // next, we need command text.
            var cmdText = "sp_select_volunteer_reviews_by_volunteerID";

            // we create a command object;
            var cmd = new SqlCommand(cmdText, conn);

            // load arguments to the command.
            cmd.CommandType = CommandType.StoredProcedure;

            // We need to add parameters to the command's parameter collection
            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);

            // The parameters need their values to be set.
            cmd.Parameters["@VolunteerID"].Value = volunteerID;

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
                        volunteerReviews.Add(new Reviews()
                        {
                            ForeignID = volunteerID,
                            UserID = reader.GetInt32(0),
                            ReviewID = reader.GetInt32(1),
                            Rating = reader.GetInt32(2),
                            FullName = reader.GetString(3),
                            ReviewType = reader.GetString(4),
                            Review = reader.IsDBNull(5) ? "" : reader.GetString(4),
                            DateCreated = DateTime.Parse(reader["DateCreated"].ToString())
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


            return volunteerReviews;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Inserts a review into the review table and a volunteer review into the VolunteerReview table
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rowsAffected</returns>
        public int InsertVolunteerReview(Reviews review)
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

            cmdTxt = "sp_insert_volunteer_review";
            cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReviewID", SqlDbType.Int);
            cmd.Parameters.Add("@VolunteerID", SqlDbType.Int);

            cmd.Parameters["@ReviewID"].Value = reviewID;
            cmd.Parameters["@VolunteerID"].Value = review.ForeignID;


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
