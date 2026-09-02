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


namespace inventory_system
{
    public partial class formlogin : Form
    {
        public formlogin()
        {
            InitializeComponent();
        }
        Controller.ControllerSetting setting = new Controller.ControllerSetting();
        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void formlogin_Load(object sender, EventArgs e)
        {
            LoadLogo();
        }
        private void LoadLogo()
        {
            try
            {
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
                            pblogo.SizeMode = PictureBoxSizeMode.StretchImage; // ឬ Zoom តាមចង់
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
                if (setting.conn.State == ConnectionState.Open)
                {
                    setting.conn.Close();
                }
            }
        }
    }       
} 