using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessWithSQLClient.Repositories
{
    internal class ConnectionStringHelper
    {
        /// <summary>
        /// GetConnectionString generate & return SqlConnectionStringBuilder for SQL connection
        /// </summary>
        /// <returns>Connection String</returns>
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            conn.DataSource = "LAPTOP-9CG2BN0V\\SQLEXPRESS";
            conn.InitialCatalog = "Chinook";
            conn.IntegratedSecurity = true;
            conn.TrustServerCertificate = true;
            return conn.ConnectionString;
        }
    }
}
