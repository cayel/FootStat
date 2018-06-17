using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootStatSeed
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            //string datasource = @"192.168.205.135\SQLEXPRESS";
            string datasource = @"DESKTOP-E49BGJL\SQLEXPRESS";

            string database = "FootStatDb";
            string username = "";
            string password = "";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
