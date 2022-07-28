using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal static class DBConnection
    {
        // The one place in the app where the connection string should appear.
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=tadpole_db;Integrated Security=True";
            var connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
