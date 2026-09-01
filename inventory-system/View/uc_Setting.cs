using inventory_system.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_system.View
{
    public partial class uc_Setting : UserControl
    {
        public uc_Setting()
        {
            InitializeComponent();
        }
        Controller.ControllerSetting setting = new Controller.ControllerSetting();


        private void uc_Setting_Load(object sender, EventArgs e)
        {
            loadData();
            if(txtcompanyname.Text != "")
            {
                btnSave.Text="Update Setting";
            }
        }
        public void loadData()
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(
                "SELECT CompanyId , CompanyName, CompanyLogo from tblSetting where CompanyId = @CompanyId", controllerSetting.conn);
                cmd.Parameters.AddWithValue("@CompanyId", 1);
                System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["CompanyLogo"] != DBNull.Value)
                    {
                        byte[] img = (byte[])dr["CompanyLogo"];
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(img))
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

        private void addLogo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Your Logo(*.jpg;*.png;*.gif)|*.jpg; *.png; *.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pblogo.Image = System.Drawing.Image.FromFile(ofd.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Insert Setting")
            {
            }
            if (txtcompanyname.Text == "")
            {
                MessageBox.Show("Invalid Company Name", "Valid Company Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcompanyname.Focus();
            }
            else
            {
                if (btnSave.Text == "Add Setting")
                {
                    System.Drawing.Image img = pblogo.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    setting.CompanyName = txtcompanyname.Text;
                    setting.CompanyLogo = arr;
                    setting.InsertSetting();
                    MessageBox.Show("Setting Has Been Inserted",
                    "Insert Setting", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    System.Drawing.Image img = pblogo.Image; byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    setting.CompanyId = int.Parse(txtcompanyid.Text);
                    setting.CompanyName = txtcompanyname.Text;
                    setting.CompanyLogo = arr;
                    setting.UpdateSetting();
                    MessageBox.Show("Setting Has Been Updated", "Update Setting", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }
        }
    }
}
