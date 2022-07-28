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
    public class ParkingLotAccessor : IParkingLotAccessor
    {


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a parking lot object into the database
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns>The inserted parking lot id</returns>
        public int InsertParkingLot(ParkingLot parkingLot)
        {
            int parkingLotID = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_parking_lot";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = parkingLot.LocationID;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 160);
            cmd.Parameters["@Name"].Value = parkingLot.Name;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 3000);

            if (parkingLot.Description == "" || parkingLot.Description == null)
            {
                cmd.Parameters["@Description"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@Description"].Value = parkingLot.Description;
            }
            
            cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar, 200);

            if (parkingLot.ImageName == null || parkingLot.ImageName == "")
            {
                cmd.Parameters["@ImageName"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ImageName"].Value = parkingLot.ImageName;
            }

            try
            {
                conn.Open();
                Object result = cmd.ExecuteScalar();
                parkingLotID = (int)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return parkingLotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the list of parking lots for the location ID
        /// </summary>
        /// <param name="locationID">The locationID</param>
        /// <returns>The ParkingLotVMs for the location </returns>
        public List<ParkingLotVM> SelectParkingLotByLocationID(int locationID)
        {
            List<ParkingLotVM> parkingLots = new List<ParkingLotVM>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_parking_lots_by_locationID";

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
                        parkingLots.Add(new ParkingLotVM()
                        {
                            LotID = reader.GetInt32(0),
                            LocationID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.IsDBNull(3)? null : reader.GetString(3),
                            ImageName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Active = true,
                            LocationName = reader.GetString(5)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return parkingLots;

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Deletes parking lot
        /// </summary>
        /// <remarks>
        /// Emma Pollock
        /// Updated: 2022/03/24
        /// Removed unnecessary empty try-catch block
        /// </remarks>
        /// 
        /// <param name="lotID">The lot to delete</param>
        /// <returns>True is removed, false if not</returns>
        public bool DeleteParkingLotByLotID(int lotID)
        {
            bool isDeleted = false;
            

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_delete_parking_lot";
            var cmd = new SqlCommand(cmdTxt, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LotID", SqlDbType.Int);

            cmd.Parameters["@LotID"].Value = lotID;

            try
            {
                conn.Open();
                isDeleted = (1 == cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return isDeleted;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Checks to see if the user can edit the parking lot
        /// </summary>
        /// <param name="userID">The ID for the user</param>
        /// <returns>True if removed, false if not</returns>
        public bool UserCanEditParkingLot(int userID)
        {
            bool result = false;
            List<Role> roles = new List<Role>();

            // connection
            var conn = DBConnection.GetConnection();

            string cmdTxt = "sp_select_user_roles_from_event_users_table";
            var cmd = new SqlCommand(cmdTxt, conn);

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
                if (role.RoleID == "Event Planner")
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Updates a parking lot by the LotID
        /// </summary>
        /// <param name="lotID">ID of Parking lot being edited</param>
        /// <param name="oldParkingLot">Parking lot object before edit</param>
        /// <param name="newParkingLot">Parking lot object after edit</param>
        /// <returns>Number of rows affected. Should only be one if successful</returns>
        public int UpdateParkingLotByLotID(int lotID, ParkingLot oldParkingLot, ParkingLot newParkingLot)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_parking_lot_by_lotID";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LotID", oldParkingLot.LotID);

            cmd.Parameters.Add("@OldLocationID", SqlDbType.Int);
            cmd.Parameters["@OldLocationID"].Value = oldParkingLot.LocationID;

            cmd.Parameters.Add("@OldLotName", SqlDbType.NVarChar, 160);
            cmd.Parameters["@OldLotName"].Value = oldParkingLot.Name;

            cmd.Parameters.Add("@OldLotDescription", SqlDbType.NVarChar, 3000);
            if (oldParkingLot.Description == "" || oldParkingLot.Description == null)
            {
                cmd.Parameters["@OldLotDescription"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@OldLotDescription"].Value = oldParkingLot.Description;
            }

            cmd.Parameters.Add("@OldLotImagePath", SqlDbType.NVarChar, 200);
            if (oldParkingLot.ImageName == null || oldParkingLot.ImageName == "")
            {
                cmd.Parameters["@OldLotImagePath"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@OldLotImagePath"].Value = oldParkingLot.ImageName;
            }

            cmd.Parameters.Add("@LocationID", SqlDbType.Int);
            cmd.Parameters["@LocationID"].Value = newParkingLot.LocationID;

            cmd.Parameters.Add("@LotName", SqlDbType.NVarChar, 160);
            cmd.Parameters["@LotName"].Value = newParkingLot.Name;

            cmd.Parameters.Add("@LotDescription", SqlDbType.NVarChar, 3000);
            if (newParkingLot.Description == "" || newParkingLot.Description == null)
            {
                cmd.Parameters["@LotDescription"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@LotDescription"].Value = newParkingLot.Description;
            }

            cmd.Parameters.Add("@LotImagePath", SqlDbType.NVarChar, 200);
            if (newParkingLot.ImageName == null || newParkingLot.ImageName == "")
            {
                cmd.Parameters["@LotImagePath"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@LotImagePath"].Value = newParkingLot.ImageName;
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
        /// Mike Cahow
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Method that returns the Parking lot VM with the corresponding lot ID
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/13
        /// Removed exception causing issues in UI.
        /// </summary>
        /// <param name="lotID">ID of selected parking lot</param>
        /// <returns>Parking lot object with matching lotID</returns>
        public ParkingLotVM SelectParkingLotByLotID(int lotID)
        {
            ParkingLotVM parkingLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_parking_lot_by_lotID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LotID", SqlDbType.Int);
            cmd.Parameters["@LotID"].Value = lotID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ParkingLotVM lotVM = new ParkingLotVM();
                        lotVM.LotID = reader.GetInt32(0);
                        lotVM.LocationID = reader.GetInt32(1);
                        lotVM.Name = reader.GetString(2);
                        lotVM.Description = reader.IsDBNull(3) ? null : reader.GetString(3);
                        lotVM.ImageName = reader.IsDBNull(4) ? null : reader.GetString(4);
                        lotVM.Active = true;

                        parkingLot = lotVM;
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


            return parkingLot;
        }
    }
    
}
