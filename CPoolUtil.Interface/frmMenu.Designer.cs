
namespace CPoolUtil.Interface
{
    partial class frmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.btnTemplateBrowser = new System.Windows.Forms.Button();
            this.btnCharacterPoolEditor = new System.Windows.Forms.Button();
            this.btnCreatePool = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAboutUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTemplateBrowser
            // 
            this.btnTemplateBrowser.Location = new System.Drawing.Point(75, 352);
            this.btnTemplateBrowser.Name = "btnTemplateBrowser";
            this.btnTemplateBrowser.Size = new System.Drawing.Size(245, 72);
            this.btnTemplateBrowser.TabIndex = 0;
            this.btnTemplateBrowser.Text = "Customization Templates Browser";
            this.btnTemplateBrowser.UseVisualStyleBackColor = true;
            this.btnTemplateBrowser.Click += new System.EventHandler(this.btnTemplateBrowser_Click);
            // 
            // btnCharacterPoolEditor
            // 
            this.btnCharacterPoolEditor.Location = new System.Drawing.Point(75, 203);
            this.btnCharacterPoolEditor.Name = "btnCharacterPoolEditor";
            this.btnCharacterPoolEditor.Size = new System.Drawing.Size(245, 72);
            this.btnCharacterPoolEditor.TabIndex = 0;
            this.btnCharacterPoolEditor.Text = "Edit Existing Pools";
            this.btnCharacterPoolEditor.UseVisualStyleBackColor = true;
            this.btnCharacterPoolEditor.Click += new System.EventHandler(this.btnCharacterPoolBrowser_Click);
            // 
            // btnCreatePool
            // 
            this.btnCreatePool.Location = new System.Drawing.Point(75, 54);
            this.btnCreatePool.Name = "btnCreatePool";
            this.btnCreatePool.Size = new System.Drawing.Size(245, 72);
            this.btnCreatePool.TabIndex = 0;
            this.btnCreatePool.Text = "Create New Pool";
            this.btnCreatePool.UseVisualStyleBackColor = true;
            this.btnCreatePool.Click += new System.EventHandler(this.btnCreatePool_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 453);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(13, 15);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "v";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemAbout});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(404, 24);
            this.mainMenuStrip.TabIndex = 2;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // mnuItemAbout
            // 
            this.mnuItemAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAboutUpdates});
            this.mnuItemAbout.Name = "mnuItemAbout";
            this.mnuItemAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuItemAbout.Text = "About";
            // 
            // menuItemAboutUpdates
            // 
            this.menuItemAboutUpdates.Enabled = false;
            this.menuItemAboutUpdates.Name = "menuItemAboutUpdates";
            this.menuItemAboutUpdates.Size = new System.Drawing.Size(179, 22);
            this.menuItemAboutUpdates.Text = "Check for updates...";
            this.menuItemAboutUpdates.Click += new System.EventHandler(this.menuItemAboutUpdates_Click);
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdateStatus.Location = new System.Drawing.Point(75, 453);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(317, 23);
            this.lblUpdateStatus.TabIndex = 3;
            this.lblUpdateStatus.Text = "Checking for updates...";
            this.lblUpdateStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblUpdateStatus.Click += new System.EventHandler(this.lblUpdateStatus_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 480);
            this.Controls.Add(this.lblUpdateStatus);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnCreatePool);
            this.Controls.Add(this.btnCharacterPoolEditor);
            this.Controls.Add(this.btnTemplateBrowser);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 519);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 519);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XCOM 2 Character Pool Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTemplateBrowser;
        private System.Windows.Forms.Button btnCharacterPoolEditor;
        private System.Windows.Forms.Button btnCreatePool;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemAboutUpdates;
        private System.Windows.Forms.Label lblUpdateStatus;
    }
}