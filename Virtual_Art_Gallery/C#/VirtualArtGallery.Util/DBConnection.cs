using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Util
{
    public class DBConnection
    {
        public static SqlConnection getDBConnection()
        {
            SqlConnection conn;
            string connectionstring = "Data Source=VIVEK21\\SQLEXPRESS;Initial Catalog=VirtualArtDB;Integrated Security=True;Encrypt=False";
            conn = new SqlConnection();
            conn.ConnectionString = connectionstring;
            return conn;
        }
    }
}
