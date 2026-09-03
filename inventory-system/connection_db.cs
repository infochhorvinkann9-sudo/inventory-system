using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace inventory_system
{
    class connection_db
    {
        public SqlConnection conn;

        public connection_db()
        {
            string machineName = Environment.MachineName;
            string dataSource;
            string password;

            if (machineName.Equals("DESKTOP-O31OODG", StringComparison.OrdinalIgnoreCase))
            {
                dataSource = "DESKTOP-O31OODG";
                password = "123";
            }
            else if (machineName.Equals("DESKTOP-GLML442", StringComparison.OrdinalIgnoreCase))
            {
                dataSource = @"DESKTOP-GLML442\MSSQLSERVER01";
                password = "2233";
            }
            else
            {
                return;
            }

            string connectionString = $@"Data Source={dataSource};Initial Catalog=inventory_db;User ID=sa;Password={password};";
            conn = new SqlConnection(connectionString);
        }

        // Add this static method so calls to connection_db.GetConnection() work everywhere!
        public static SqlConnection GetConnection()
        {
            connection_db db = new connection_db();
            if (db.conn != null && db.conn.State != System.Data.ConnectionState.Open)
            {
                db.conn.Open();
            }
            return db.conn;
        }
    }
}