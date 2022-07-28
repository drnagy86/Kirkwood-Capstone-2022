using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/02
    /// 
    /// The ServiceAccessor data access class for all service data 
    /// </summary>
    public class ServiceAccessor : IServiceAccessor
    {
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Function to delete a service from the database
        /// </summary>
        /// <param name="serviceID">ID of service to delete</param>
        /// <returns>rows affected</returns>
        public int DeleteService(int serviceID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_service";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@ServiceID", SqlDbType.Int);


            cmd.Parameters["@ServiceID"].Value = serviceID;

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
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Method to create a service
        /// </summary>
        /// <param name="newService">Service to insert into database</param>
        /// <returns>rows affected</returns>
        public int InsertService(Service newService)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_insert_service";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@Price", SqlDbType.Decimal);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@ServiceImageName", SqlDbType.NVarChar, 200);


            cmd.Parameters["@SupplierID"].Value = newService.SupplierID;
            cmd.Parameters["@ServiceName"].Value = newService.ServiceName;
            cmd.Parameters["@Price"].Value = newService.Price;


            if (newService.Description == null)
            {
                cmd.Parameters["@Description"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@Description"].Value = newService.Description;
            }
            if (newService.ServiceImagePath == null)
            {
                cmd.Parameters["@ServiceImageName"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ServiceImageName"].Value = newService.ServiceImagePath;
            }

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
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Selects a service matching a passed serviceID
        /// </summary>
        /// <param name="serviceID">ID to match</param>
        /// <returns>The matching service</returns>
        public Service SelectServiceByServiceID(int serviceID)
        {
            Service result = null;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_service_by_ServiceID";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ServiceID", SqlDbType.Int);
            cmd.Parameters["@ServiceID"].Value = serviceID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new Service()
                        {
                            ServiceID = serviceID,
                            SupplierID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ServiceImagePath = reader.IsDBNull(4) ? null : reader.GetString(4),
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

            return result;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Select all services that match supplier supplierID
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Service objects</returns>
        public List<Service> SelectServicesBySupplierID(int supplierID)
        {
            List<Service> services = new List<Service>();

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_select_services_by_supplierID";

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
                        services.Add(new Service()
                        {
                            SupplierID = supplierID,
                            ServiceID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Description = reader.IsDBNull(3) ? null : reader.GetString(3),
                            ServiceImagePath = reader.IsDBNull(4) ? null : reader.GetString(4),
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

            return services;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Method to edit a service
        /// </summary>
        /// <param name="oldService">The service to replace</param>
        /// <param name="newService">The service to replace with</param>
        /// <returns>The number of rows affected</returns>
        public int UpdateService(Service oldService, Service newService)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_service";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@ServiceID", SqlDbType.Int);
            cmd.Parameters.Add("@OldSupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@OldServiceName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@OldPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@OldServiceImageName", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@NewSupplierID", SqlDbType.Int);
            cmd.Parameters.Add("@NewServiceName", SqlDbType.NVarChar, 160);
            cmd.Parameters.Add("@NewPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 3000);
            cmd.Parameters.Add("@NewServiceImageName", SqlDbType.NVarChar, 200);


            cmd.Parameters["@ServiceID"].Value = oldService.ServiceID;
            cmd.Parameters["@OldSupplierID"].Value = oldService.SupplierID;
            cmd.Parameters["@OldServiceName"].Value = oldService.ServiceName;
            cmd.Parameters["@OldPrice"].Value = oldService.Price;
            cmd.Parameters["@NewSupplierID"].Value = newService.SupplierID;
            cmd.Parameters["@NewServiceName"].Value = newService.ServiceName;
            cmd.Parameters["@NewPrice"].Value = newService.Price;

            if(oldService.Description == null)
            {
                cmd.Parameters["@OldDescription"].Value = DBNull.Value;
            } else
            {
                cmd.Parameters["@OldDescription"].Value = oldService.Description;
            }
            if (oldService.ServiceImagePath == null)
            {
                cmd.Parameters["@OldServiceImageName"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@OldServiceImageName"].Value = oldService.ServiceImagePath;
            }
            if (newService.Description == null)
            {
                cmd.Parameters["@NewDescription"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@NewDescription"].Value = newService.Description;
            }
            if (newService.ServiceImagePath == null)
            {
                cmd.Parameters["@NewServiceImageName"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@NewServiceImageName"].Value = newService.ServiceImagePath;
            }

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
    }
}
