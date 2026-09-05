using inventory_system.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
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
            this.Dock = DockStyle.Fill;
        }

        Controller.ControllerSetting setting = new Controller.ControllerSetting();

        private void uc_Setting_Load(object sender, EventArgs e)
        {
            loadData(1);
            LoadGrid();
        }

        public void loadData(int companyId)
        {
            try
            {
                if (setting.conn.State != ConnectionState.Open)
                {
                    setting.conn.Open();
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(
                    "SELECT CompanyId, CompanyName, CompanyLogo from tblSetting where CompanyId = @CompanyId", setting.conn);
                cmd.Parameters.AddWithValue("@CompanyId", companyId);

                System.Data.SqlClient.SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtcompanyid.Text = dr["CompanyId"].ToString();
                    txtcompanyname.Text = dr["CompanyName"].ToString();

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
                    btnSave.Text = "Update Setting";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Setting Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (setting.conn.State == ConnectionState.Open)
                {
                    setting.conn.Close();
                }
            }
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
            if (txtcompanyname.Text == "")
            {
                MessageBox.Show("Invalid Company Name", "Valid Company Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcompanyname.Focus();
                return;
            }

            if (pblogo.Image == null)
            {
                MessageBox.Show("Please Select a Company Logo", "Valid Logo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(pblogo.Image, typeof(byte[]));

            if (btnSave.Text == "Add Logo")
            {
                try
                {
                    setting.CompanyName = txtcompanyname.Text;
                    setting.CompanyLogo = arr;
                    setting.InsertSetting();
                    MessageBox.Show("Logo Has Been Inserted", "Insert Logo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insert Logo Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                int companyId;
                if (!int.TryParse(txtcompanyid.Text, out companyId))
                {
                    MessageBox.Show("Invalid Company Id", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcompanyid.Focus();
                    return;
                }
                try
                {
                    setting.CompanyId = companyId;
                    setting.CompanyName = txtcompanyname.Text;
                    setting.CompanyLogo = arr;
                    setting.UpdateSetting();
                    MessageBox.Show("Logo Has Been Updated", "Update Logo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update Logo Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            LoadGrid();
            RefreshMainFormLogo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int companyId;
            if (!int.TryParse(txtcompanyid.Text, out companyId))
            {
                MessageBox.Show("Invalid Company Id", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this setting?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    setting.CompanyId = companyId;
                    setting.DeleteSetting();
                    MessageBox.Show("Logo Has Been Deleted", "Delete Logo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Logo Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtcompanyid.Text = "";
                txtcompanyname.Text = "";
                pblogo.Image = null;
                btnSave.Text = "Add Logo";
                LoadGrid();
                RefreshMainFormLogo();
            }
        }

        private void dgsetting_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgsetting.Rows[e.RowIndex];

            if (int.TryParse(row.Cells["CompanyId"].Value?.ToString(), out int companyId))
            {
                loadData(companyId);
            }
        }

        public void LoadGrid()
        {
            try
            {
                if (setting.conn.State != ConnectionState.Open)
                {
                    setting.conn.Open();
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(
                    "SELECT CompanyId, CompanyName, CompanyLogo FROM tblSetting", setting.conn);
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgsetting.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Grid Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (setting.conn.State == ConnectionState.Open)
                {
                    setting.conn.Close();
                }
            }
        }

        private void RefreshMainFormLogo()
        {
            MainForm mainForm = FindForm() as MainForm;
            if (mainForm != null)
            {
                mainForm.RefreshLogo();
            }
        }
    }
}
