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
<<<<<<< HEAD

=======
        //public DataSet ds = new DataSet();
>>>>>>> d93c21be10aa2b46b7f57d77e3e1e524154171af
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
