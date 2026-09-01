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
                Console.WriteLine("Unknown computer! No matching server config.");
                return;
            }

            try
            {
                string connectionString =
                    $@"Data Source={dataSource};
                    Initial Catalog=inventory_db;
                    User ID=sa;
                    Password={password};";

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