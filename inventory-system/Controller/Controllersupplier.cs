using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_system.Controller
{
    internal class Controllersupplier : Models.Modelsupplier
    {
        public void InsertSupplier()
        {
            connection_db db = new connection_db();
            try
            {
                OpenConnection(db);

                using (SqlCommand cmd = new SqlCommand("InsertSupplier", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = SupplierName;
                    cmd.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = SupplierPhone;
                    cmd.Parameters.Add("@SupplierEmail", SqlDbType.NVarChar).Value = SupplierEmail;
                    cmd.Parameters.Add("@SupplierAddress", SqlDbType.NVarChar).Value = SupplierAddress;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting supplier: " + ex.Message);
            }
            finally
            {
                CloseConnection(db);
            }
        }
        public void UpdateSupplier()
        {
            connection_db db = new connection_db();
            try
            {
                OpenConnection(db);
                using (SqlCommand cmd = new SqlCommand("UpdateSupplier", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = SupplierId;
                    cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar).Value = SupplierName;
                    cmd.Parameters.Add("@SupplierPhone", SqlDbType.NVarChar).Value = SupplierPhone;
                    cmd.Parameters.Add("@SupplierEmail", SqlDbType.NVarChar).Value = SupplierEmail;
                    cmd.Parameters.Add("@SupplierAddress", SqlDbType.NVarChar).Value = SupplierAddress;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating supplier: " + ex.Message);
            }
            finally
            {
                CloseConnection(db);
            }
        }
        public void DeleteSupplier()
        {
            connection_db db = new connection_db();
            try
            {
                OpenConnection(db);
                using (SqlCommand cmd = new SqlCommand("DeleteSupplier", db.conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = SupplierId;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting supplier: " + ex.Message);
            }
            finally
            {
                CloseConnection(db);
            }
        }
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public SqlDataAdapter da = new SqlDataAdapter();
        public void viewSupplier()
        {
            connection_db db = new connection_db();
            try
            {
                string query = "SELECT * FROM V_Supplier";
                OpenConnection(db);
                using (SqlCommand cmd = new SqlCommand(query, db.conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error viewing suppliers: " + ex.Message);
            }
            finally
            {
                CloseConnection(db);
            }
        }

        private void OpenConnection(connection_db db)
        {
            if (db.conn == null)
            {
                throw new InvalidOperationException("Database connection failed to initialize. Check your server configuration.");
            }

            if (db.conn.State != ConnectionState.Open)
            {
                db.conn.Open();
            }
        }

        private void CloseConnection(connection_db db)
        {
            if (db.conn != null && db.conn.State == ConnectionState.Open)
            {
                db.conn.Close();
            }
        }
    }
}
