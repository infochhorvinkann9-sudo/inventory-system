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
    public partial class uc_Supplier : UserControl
    {
        public uc_Supplier()
        {
            InitializeComponent();
        }
        Controller.Controllersupplier supplier = new Controller.Controllersupplier();

        private void uc_Supplier_Load(object sender, EventArgs e)
        {
            btnClear.Enabled = false;
            btndelete.Enabled = false;
            viewSupplier();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (btnsave.Text == "Add Supplier")
            {
                ClearSupplierTextBoxes();
                txtsupplierId.Enabled = false;
                btnsave.Text = "Insert Supplier";
                btnClear.Enabled = true;
                btnClear.Text = "Clear Supplier";
            }
            else if (btnsave.Text == "Insert Supplier")
            {
                try
                {
                    supplier.SupplierName = txtsuppliername.Text;
                    supplier.SupplierPhone = txtnumber.Text;
                    supplier.SupplierEmail = txtsupplieremail.Text;
                    supplier.SupplierAddress = txtadress.Text;
                    supplier.InsertSupplier();
                    viewSupplier();
                    MessageBox.Show("Supplier Has Been Inserted", "Insert Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetSupplierButtons();
                    ClearSupplierTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insert Supplier Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void viewSupplier()
        {
            supplier.viewSupplier();
            dgsupplier.DataSource = supplier.dt;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this supplier?", "Delete Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    supplier.SupplierId = Convert.ToInt32(txtsupplierId.Text);
                    supplier.DeleteSupplier();
                    viewSupplier();
                    MessageBox.Show("Supplier Has Been Deleted", "Delete Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetSupplierButtons();
                    ClearSupplierTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Supplier Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(btnClear.Text == "Clear Supplier")
            {
                ClearSupplierTextBoxes();
                ResetSupplierButtons();
            }else if(btnClear.Text == "Update Supplier")
            {
                try
                {
                    supplier.SupplierId = Convert.ToInt32(txtsupplierId.Text);
                    supplier.SupplierName = txtsuppliername.Text;
                    supplier.SupplierPhone = txtnumber.Text;
                    supplier.SupplierEmail = txtsupplieremail.Text;
                    supplier.SupplierAddress = txtadress.Text;
                    supplier.UpdateSupplier();
                    viewSupplier();
                    MessageBox.Show("Supplier Has Been Updated", "Update Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetSupplierButtons();
                    ClearSupplierTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update Supplier Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgsupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectSupplierRow(e.RowIndex);
        }

        private void SelectSupplierRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgsupplier.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgsupplier.Rows[rowIndex];
            DataRowView supplierRow = row.DataBoundItem as DataRowView;
            if (supplierRow == null)
            {
                return;
            }

            txtsupplierId.Text = GetSupplierValue(supplierRow, "SupplierId");
            txtsuppliername.Text = GetSupplierValue(supplierRow, "SupplierName");
            txtnumber.Text = GetSupplierValue(supplierRow, "SupplierPhone");
            txtsupplieremail.Text = GetSupplierValue(supplierRow, "SupplierEmail");
            txtadress.Text = GetSupplierValue(supplierRow, "SupplierAddress");

            btnsave.Text = "Add Supplier";
            btnClear.Text = "Update Supplier";
            btnClear.Enabled = true;
            btndelete.Enabled = true;
            txtsupplierId.Enabled = false;
        }

        private string GetSupplierValue(DataRowView row, string columnName)
        {
            if (!row.DataView.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
            {
                return "";
            }

            return row[columnName].ToString();
        }

        private void ClearSupplierTextBoxes()
        {
            txtsupplierId.Clear();
            txtsuppliername.Clear();
            txtnumber.Clear();
            txtsupplieremail.Clear();
            txtadress.Clear();
        }

        private void ResetSupplierButtons()
        {
            btnsave.Text = "Add Supplier";
            btnClear.Text = "Update Supplier";
            btnClear.Enabled = false;
            btndelete.Enabled = false;
            txtsupplierId.Enabled = false;
        }
    }
}
