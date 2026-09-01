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

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void formlogin_Load(object sender, EventArgs e)
        {

        }
        public void loadData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                "SELECT SettingID , CompanyLogo from tbl_setting where SettingID = @SettingID", setting.conn);
                cmd.Parameters.AddWithValue("@SettingID", 1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["CompanyLogo"] != DBNull.Value)
                    {
                        byte[] img = (byte[])dr["CompanyLogo"];
                        using (MemoryStream ms = new MemoryStream(img))
                        {
                            pblogo.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pblogo.Image = null;
                    }
                }
                dr.Close();
            }
            catch { }
            }
        }
    }
