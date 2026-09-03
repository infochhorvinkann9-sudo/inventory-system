using inventory_system.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_system.View
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // Controllers for settings and database connection
        Controller.ControllerSetting setting = new Controller.ControllerSetting();
        connection_db newConn = new connection_db();

        private void FormLogin_Load(object sender, EventArgs e)
        {
            LoadLogo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoadLogo()
        {
            try
            {
                if (setting.conn == null) return;

                SqlCommand cmd = new SqlCommand(
                    "SELECT CompanyLogo FROM tblSetting WHERE CompanyId = @CompanyId", setting.conn);
                cmd.Parameters.AddWithValue("@CompanyId", 1);

                if (setting.conn.State != ConnectionState.Open)
                {
                    setting.conn.Open();
                }

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["CompanyLogo"] != DBNull.Value)
                    {
                        byte[] img = (byte[])dr["CompanyLogo"];
                        using (MemoryStream ms = new MemoryStream(img))
                        {
                            pblogo.Image = Image.FromStream(ms);
                            pblogo.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Logo Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (setting.conn != null && setting.conn.State == ConnectionState.Open)
                {
                    setting.conn.Close();
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Check Login Info!", "Don't forget!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (newConn.conn == null)
                    {
                        MessageBox.Show("Database connection failed to initialize. Check your server configuration.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (newConn.conn.State != ConnectionState.Open)
                    {
                        newConn.conn.Open();
                    }

                    SqlCommand cmd = newConn.conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM tblUsers WHERE UserName = @UserName AND Password = @Password";

                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        this.Hide();
                        MainForm mainForm = new MainForm();
                        mainForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login Info!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (newConn.conn != null && newConn.conn.State == ConnectionState.Open)
                    {
                        newConn.conn.Close();
                    }
                }
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}