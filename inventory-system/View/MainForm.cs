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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Container.Dock = DockStyle.Fill;
            uc_Dashboard dashboardControl = new uc_Dashboard();
            uc_Container.Controls.Add(dashboardControl);
            dashboardControl.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Container.Dock = DockStyle.Fill;
            uc_Accessory accessoryControl = new uc_Accessory();
            uc_Container.Controls.Add(accessoryControl);
            accessoryControl.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Container.Dock = DockStyle.Fill;
            uc_Category categoryControl = new uc_Category();
            uc_Container.Controls.Add(categoryControl);
            categoryControl.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Container.Dock = DockStyle.Fill;
            uc_Supplier supplierControl = new uc_Supplier();
            uc_Container.Controls.Add(supplierControl);
            supplierControl.BringToFront();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Container.Dock = DockStyle.Fill;
            uc_User userControl = new uc_User();
            uc_Container.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            uc_Container.Controls.Clear();
            uc_Setting settingControl = new uc_Setting();
            uc_Container.Controls.Add(settingControl);
            settingControl.BringToFront();
        }

        private void uc_Container_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
