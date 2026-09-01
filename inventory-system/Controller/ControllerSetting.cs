using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace inventory_system.Controller
{
    internal class ControllerSetting : Models.ModelsSetting
    {
        public void InsertSetting()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "InsertSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName;
                cmd.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value = CompanyLogo;
                cmd.ExecuteNonQuery();

            }
            catch { }
        }
        public void UpdateSetting()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UpdateSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName;
                cmd.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value = CompanyLogo;
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
        public void DeleteSetting()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DeleteSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                cmd.ExecuteNonQuery();
            }
            catch { }
        }
    }
}
