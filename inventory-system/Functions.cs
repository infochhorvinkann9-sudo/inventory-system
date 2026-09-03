using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_system
{
    internal class Functions
    {
        // Enable functions in the UC

        public void EnableTxtAndCbox(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.Enabled = true;
                }
                if (control.HasChildren)
                {
                    EnableTxtAndCbox(control);
                }
            }
        }

        //  Disable functions in the UC

        public void DisableTxtAndCbox(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.Enabled = false;
                }
                if (control.HasChildren)
                {
                    DisableTxtAndCbox(control);
                }
            }
        }

        // Clear Data from txt and Cbox in 
        public void ClearTxtAndCbox(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
                if (control.HasChildren)
                {
                    ClearTxtAndCbox(control);
                }
            }
        }
    }
}