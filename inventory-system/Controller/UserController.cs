    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_system.Controller
{
    internal class UserController : Models.UserModel
    {
        // Insert User 
        public void InsertUser()
        {
            try
            {
                connection_db db = new connection_db();
                if (db.conn.State != ConnectionState.Open)
                {
                    db.conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand("InsertUser", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                    cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = UserRole;
                    cmd.Parameters.Add("@UserStatus", SqlDbType.Int).Value = UserStatus;
                    cmd.ExecuteNonQuery();
                }

                if (db.conn.State == ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Inserted Failed: " + ex.Message,
                    "Cannot Insert User", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        public void UpdateUser()
        {
            try
            {
                connection_db db = new connection_db();
                if (db.conn.State != ConnectionState.Open)
                {
                    db.conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand("UpdateUser", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                    cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = UserRole;
                    cmd.Parameters.Add("@UserStatus", SqlDbType.Int).Value = UserStatus;

                    cmd.ExecuteNonQuery();
                }

                if (db.conn.State == ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User Update Failed: " + ex.Message);
            }
        }

        public void DeleteUser()
        {
            try
            {
                connection_db db = new connection_db();
                if (db.conn.State != ConnectionState.Open)
                {
                    db.conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand("DeleteUser", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.ExecuteNonQuery();
                }

                if (db.conn.State == ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Delete Failed: " + ex.Message,
                    "Cannot Delete User", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        public static bool IsUsernameToken(string username)
        {
            string sql = "SELECT COUNT(*) FROM tblUsers WHERE UserName = @UserName";
            using (SqlConnection conn = connection_db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Get data from Users
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public SqlDataAdapter adapter = new SqlDataAdapter();

        public void GetUserData()
        {
            try
            {
                connection_db db = new connection_db();
                if (db.conn.State != ConnectionState.Open)
                {
                    db.conn.Open();
                }

                string sql = "SELECT * FROM tblUsers";
                SqlCommand cmd = new SqlCommand(sql, db.conn);
                adapter.SelectCommand = cmd;
                ds.Clear();
                adapter.Fill(ds);
                dt = ds.Tables[0];

                if (db.conn.State == ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get User Data Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}