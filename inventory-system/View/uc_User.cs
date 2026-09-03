using inventory_system.Controller;
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
    public partial class uc_User : UserControl
    {
        public uc_User()
        {
            InitializeComponent();
        }

        // Call Controller to Insert User
        Controller.UserController userCtrl = new Controller.UserController();

        // Disable and Enable Functions
        Functions DE_Functions = new Functions();

        private void uc_User_Load(object sender, EventArgs e)
        {
            DE_Functions.DisableTxtAndCbox(this);
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            GetUserData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                DE_Functions.EnableTxtAndCbox(this);
                txtUserId.Enabled = false;
                btnAdd.Text = "Insert"; 
                btnUpdate.Enabled = true;
                btnUpdate.Text = "Clear";
                cboxUserRole.Enabled = true;
                btnAdd.Enabled = false;
            }
            else if (btnAdd.Text == "Insert") 
            {
                if (cboxUserRole.SelectedIndex == -1 || cboxUserStatus.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Check user Info", "Don't forget!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    userCtrl.UserName = txtUserName.Text;
                    userCtrl.Password = txtPassword.Text;
                    userCtrl.UserRole = cboxUserRole.SelectedItem.ToString();
                    userCtrl.UserStatus = cboxUserStatus.SelectedIndex;
                    userCtrl.InsertUser();
                    ckShowPass.Enabled = false;

                    MessageBox.Show("User Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetUserData();
                    DE_Functions.ClearTxtAndCbox(this);
                    DE_Functions.DisableTxtAndCbox(this);
                    ckShowPass.Checked = false;
                    ckShowPass.Enabled = true;
                    btnAdd.Text = "Add";
                    btnAdd.Enabled = true;
                    btnUpdate.Text = "Update";
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else if (btnAdd.Text == "Clear") 
            {
                DE_Functions.ClearTxtAndCbox(this);
                DE_Functions.DisableTxtAndCbox(this);
                ckShowPass.Checked = false;
                ckShowPass.Enabled = true;
                btnAdd.Text = "Add";
                btnAdd.Enabled = true;
                btnUpdate.Text = "Update";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Text == "Clear")
            {
                DE_Functions.ClearTxtAndCbox(this);
                DE_Functions.DisableTxtAndCbox(this);
                btnUpdate.Text = "Update";
                btnUpdate.Enabled = false;
                btnAdd.Text = "Add";
                ckShowPass.Checked = false;
                ckShowPass.Enabled = true;
                btnAdd.Enabled = true;
            }
            else if (btnUpdate.Text == "Update")
            {
                if (cboxUserRole.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Check user Info",
                        "Don't forgot!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    userCtrl.UserId = Convert.ToInt32(txtUserId.Text);
                    userCtrl.UserName = txtUserName.Text;
                    userCtrl.Password = txtPassword.Text;
                    userCtrl.UserRole = cboxUserRole.SelectedItem.ToString();
                    userCtrl.UserStatus = cboxUserStatus.SelectedIndex;
                    userCtrl.UpdateUser();
                    MessageBox.Show("User Updated", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetUserData();
                    DE_Functions.ClearTxtAndCbox(this);
                    DE_Functions.DisableTxtAndCbox(this);
                    ckShowPass.Checked = false;
                    ckShowPass.Enabled = true;
                    btnAdd.Text = "Add";
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure?",
                "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                if (int.TryParse(txtUserId.Text, out int userId))
                {
                    userCtrl.UserId = userId;
                    userCtrl.DeleteUser();

                    MessageBox.Show("User Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetUserData();
                    DE_Functions.ClearTxtAndCbox(this);
                    DE_Functions.DisableTxtAndCbox(this);
                    ckShowPass.Checked = false;
                    ckShowPass.Enabled = true;
                    btnAdd.Text = "Add";
                    btnUpdate.Text = "Update";
                    btnUpdate.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please select a valid user to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }

        // Check Box Show Password
        private void ckShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '#')
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '#';
            }
        }

        // Check required Password and confirm password
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length != 8)
            {
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }

        // Call User Data to display data in the datagridview
        public void GetUserData()
        {
            userCtrl.GetUserData();
            dgUser.DataSource = userCtrl.dt;
        }

        private void dgUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgUser.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    txtUserId.Text = dgUser.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtUserName.Text = dgUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtPassword.Text = dgUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cboxUserRole.SelectedItem = dgUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cboxUserStatus.SelectedIndex = Convert.ToInt32(dgUser.Rows[e.RowIndex].Cells[4].Value.ToString());
                }

                DE_Functions.EnableTxtAndCbox(this);
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;
                ckShowPass.Enabled = true;
                cboxUserRole.Enabled = true;
                txtUserId.Enabled = false;
                btnAdd.Text = "Clear";

                if (btnAdd.Text == "Clear")
                {
                    btnUpdate.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgUser.Columns[e.ColumnIndex].Index == 2 && e.Value != null)
            {
                int statusValue;
                if (int.TryParse(e.Value.ToString(), out statusValue))
                {
                    dgUser.Rows[e.RowIndex].Tag = e.Value;
                    e.Value = new string('#', e.Value.ToString().Length);
                }
            }
        }
    }
}
