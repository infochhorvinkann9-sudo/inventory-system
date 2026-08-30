using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace inventory_system
{
    class connection_db
    {
        public SqlConnection conn;
        public connection_db()
        {
            try
            {
                string connectionString =
                      @"Data Source=DESKTOP-O31OODG;
                      Initial Catalog=inventory_db;
                      User ID=sa;
                      Password=12";
                conn = new SqlConnection(connectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
