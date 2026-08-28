namespace inventory_system.View
{
    partial class uc_Accessory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Accessory = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Accessory
            // 
            this.Accessory.AutoSize = true;
            this.Accessory.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Accessory.ForeColor = System.Drawing.Color.IndianRed;
            this.Accessory.Location = new System.Drawing.Point(395, 301);
            this.Accessory.Name = "Accessory";
            this.Accessory.Size = new System.Drawing.Size(224, 52);
            this.Accessory.TabIndex = 0;
            this.Accessory.Text = "Accessory";
            // 
            // uc_Accessory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Accessory);
            this.Name = "uc_Accessory";
            this.Size = new System.Drawing.Size(1021, 682);
            this.Load += new System.EventHandler(this.uc_Accessory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Accessory;
    }
}
