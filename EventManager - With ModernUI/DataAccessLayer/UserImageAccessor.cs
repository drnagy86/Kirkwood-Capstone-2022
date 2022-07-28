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
    public class UserImageAccessor : IUserImageAccessor
    {
        public List<UserImage> SelectUserImagesByUserID(int userID)
        {
            List<UserImage> userImages = new List<UserImage>();

            var conn = DBConnection.GetConnection();

            var cmdText = "sp_select_userimages_by_userID";

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
                        userImages.Add(new UserImage() 
                        {
                            ImageID = reader.GetInt32(0),
                            UserID = userID,
                            ImageName = reader.GetString(1),
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


            return userImages;
        }
    }
}
