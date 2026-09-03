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
            bool closeConnection = false;
            try
            {
                closeConnection = EnsureConnectionIsOpen();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "InsertSetting";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName;
                    cmd.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value = CompanyLogo;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Insert setting failed: " + ex.Message, ex);
            }
            finally
            {
                CloseConnectionIfOpened(closeConnection);
            }
        }
        public void UpdateSetting()
        {
            bool closeConnection = false;
            try
            {
                closeConnection = EnsureConnectionIsOpen();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UpdateSetting";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName;
                    cmd.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value = CompanyLogo;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Update setting failed: " + ex.Message, ex);
            }
            finally
            {
                CloseConnectionIfOpened(closeConnection);
            }
        }
        public void DeleteSetting()
        {
            bool closeConnection = false;
            try
            {
                closeConnection = EnsureConnectionIsOpen();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DeleteSetting";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Delete setting failed: " + ex.Message, ex);
            }
            finally
            {
                CloseConnectionIfOpened(closeConnection);
            }
        }

        public System.Drawing.Image GetCompanyLogo()
        {
            return GetCompanyLogo("SELECT TOP 1 CompanyLogo FROM tblSetting ORDER BY CompanyId DESC", null);
        }

        public System.Drawing.Image GetCompanyLogo(int companyId)
        {
            return GetCompanyLogo("SELECT CompanyLogo FROM tblSetting WHERE CompanyId = @CompanyId", companyId);
        }

        private System.Drawing.Image GetCompanyLogo(string sql, int? companyId)
        {
            bool closeConnection = false;
            try
            {
                closeConnection = EnsureConnectionIsOpen();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (companyId.HasValue)
                    {
                        cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId.Value;
                    }

                    object value = cmd.ExecuteScalar();
                    if (value == null || value == DBNull.Value)
                    {
                        return null;
                    }

                    byte[] img = (byte[])value;
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(img))
                    using (System.Drawing.Image logo = System.Drawing.Image.FromStream(ms))
                    {
                        return new System.Drawing.Bitmap(logo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Load company logo failed: " + ex.Message, ex);
            }
            finally
            {
                CloseConnectionIfOpened(closeConnection);
            }
        }

        private bool EnsureConnectionIsOpen()
        {
            if (conn == null)
            {
                throw new InvalidOperationException("Database connection failed to initialize. Check your server configuration.");
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                return true;
            }

            return false;
        }

        private void CloseConnectionIfOpened(bool closeConnection)
        {
            if (closeConnection && conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
