
namespace CPoolUtil.Interface
{
    partial class frmSettings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkShowWarnings = new System.Windows.Forms.CheckBox();
            this.lblMoreComing = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnDlcMods = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(224, 324);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkShowWarnings
            // 
            this.chkShowWarnings.AutoSize = true;
            this.chkShowWarnings.Location = new System.Drawing.Point(61, 43);
            this.chkShowWarnings.Name = "chkShowWarnings";
            this.chkShowWarnings.Size = new System.Drawing.Size(189, 19);
            this.chkShowWarnings.TabIndex = 0;
            this.chkShowWarnings.Text = "Show Character Pool Warnings";
            this.toolTip.SetToolTip(this.chkShowWarnings, "Show or hide the info boxes that display when loading character pools");
            this.chkShowWarnings.UseVisualStyleBackColor = true;
            // 
            // lblMoreComing
            // 
            this.lblMoreComing.AutoSize = true;
            this.lblMoreComing.Location = new System.Drawing.Point(46, 286);
            this.lblMoreComing.Name = "lblMoreComing";
            this.lblMoreComing.Size = new System.Drawing.Size(218, 15);
            this.lblMoreComing.TabIndex = 3;
            this.lblMoreComing.Text = "More Settings Coming Soon, Probably...";
            // 
            // btnDlcMods
            // 
            this.btnDlcMods.BackColor = System.Drawing.SystemColors.Control;
            this.btnDlcMods.Location = new System.Drawing.Point(84, 165);
            this.btnDlcMods.Name = "btnDlcMods";
            this.btnDlcMods.Size = new System.Drawing.Size(143, 29);
            this.btnDlcMods.TabIndex = 7;
            this.btnDlcMods.Text = "DLC/Mod Availability";
            this.btnDlcMods.UseVisualStyleBackColor = false;
            this.btnDlcMods.Click += new System.EventHandler(this.btnDlcMods_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 359);
            this.Controls.Add(this.btnDlcMods);
            this.Controls.Add(this.lblMoreComing);
            this.Controls.Add(this.chkShowWarnings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(327, 398);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(327, 398);
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkShowWarnings;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblMoreComing;
        private System.Windows.Forms.Button btnDlcMods;
    }
}