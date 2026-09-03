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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "InsertUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = UserRole;
                cmd.Parameters.Add("@UserStatus", SqlDbType.Int).Value = UserStatus;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Inserted Failed: " + ex.Message, 
                    "Cannot Insert User", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            }
        }

        public void UpdateUser()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UpdateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = UserRole;
                cmd.Parameters.Add("@UserStatus", SqlDbType.Int).Value = UserStatus;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Update Failed: " + ex.Message,
                    "Cannot Update User", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            }
        }

        public void DeleteUser()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DeleteUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("User Delete Failed: " + ex.Message,
                    "Cannot Delete User", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
            }
        }



































        // Get data from Users

        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public SqlDataAdapter adapter = new SqlDataAdapter();

        // Get obj
        public void GetUserData()
        {
            string sql = "SELECT * FROM tblUsers";
            SqlCommand cmd = new SqlCommand(sql, conn);
            adapter.SelectCommand = cmd;
            ds.Clear();
            adapter.Fill(ds);
            dt = ds.Tables[0];
        }
    }
}