using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace VirtualArtGallery.Util
{
    public static class DBConnection
    {
        public static SqlConnection getDBConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyTrainingConnection"].ConnectionString;

            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
