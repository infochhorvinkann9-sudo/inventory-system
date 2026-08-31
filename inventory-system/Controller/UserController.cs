using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace inventory_system.Controller
{
    internal class UserController : Models.UserModel
    {
        // Insert Data to Table
        public void InsertUser()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

        }

        // Get and View Data User
        public DataTable dt = new DataTable();

        public SqlDataAdapter adapter = new SqlDataAdapter();

        public DataTable ShowData()
        {
            string sql = "SELECT * FROM tblUser;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }
    }
}
