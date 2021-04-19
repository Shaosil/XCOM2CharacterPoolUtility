
namespace CPoolUtil.Interface
{
    partial class frmPoolDlcModOptions
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpOfficial = new System.Windows.Forms.GroupBox();
            this.chkWotC = new System.Windows.Forms.CheckBox();
            this.chkTLE = new System.Windows.Forms.CheckBox();
            this.chkAnarchy = new System.Windows.Forms.CheckBox();
            this.chkAlienHunters = new System.Windows.Forms.CheckBox();
            this.chkShensLastGift = new System.Windows.Forms.CheckBox();
            this.chkResistancePack = new System.Windows.Forms.CheckBox();
            this.grpMods = new System.Windows.Forms.GroupBox();
            this.lblMoreComing = new System.Windows.Forms.Label();
            this.chkFemaleClothing = new System.Windows.Forms.CheckBox();
            this.chkMoreArmorColors = new System.Windows.Forms.CheckBox();
            this.chkMoreHairColors = new System.Windows.Forms.CheckBox();
            this.chkMaleHair = new System.Windows.Forms.CheckBox();
            this.chkCapnBubs = new System.Windows.Forms.CheckBox();
            this.btnToggleAll = new System.Windows.Forms.Button();
            this.chkFemaleHair = new System.Windows.Forms.CheckBox();
            this.grpOfficial.SuspendLayout();
            this.grpMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDescription.Location = new System.Drawing.Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(287, 84);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Checking these boxes will make their respective options available when choosing c" +
    "haracter customizations.\r\n\r\nIf an option is disabled, it is currently in use.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // grpOfficial
            // 
            this.grpOfficial.Controls.Add(this.chkWotC);
            this.grpOfficial.Controls.Add(this.chkTLE);
            this.grpOfficial.Controls.Add(this.chkAnarchy);
            this.grpOfficial.Controls.Add(this.chkAlienHunters);
            this.grpOfficial.Controls.Add(this.chkShensLastGift);
            this.grpOfficial.Controls.Add(this.chkResistancePack);
            this.grpOfficial.Location = new System.Drawing.Point(12, 96);
            this.grpOfficial.Name = "grpOfficial";
            this.grpOfficial.Size = new System.Drawing.Size(287, 99);
            this.grpOfficial.TabIndex = 2;
            this.grpOfficial.TabStop = false;
            this.grpOfficial.Text = "Official DLCs";
            // 
            // chkWotC
            // 
            this.chkWotC.AutoSize = true;
            this.chkWotC.Location = new System.Drawing.Point(7, 23);
            this.chkWotC.Name = "chkWotC";
            this.chkWotC.Size = new System.Drawing.Size(124, 19);
            this.chkWotC.TabIndex = 0;
            this.chkWotC.Tag = "WotC";
            this.chkWotC.Text = "War of the Chosen";
            this.chkWotC.UseVisualStyleBackColor = true;
            this.chkWotC.CheckedChanged += new System.EventHandler(this.chkWotC_CheckedChanged);
            // 
            // chkTLE
            // 
            this.chkTLE.AutoSize = true;
            this.chkTLE.Enabled = false;
            this.chkTLE.Location = new System.Drawing.Point(148, 23);
            this.chkTLE.Name = "chkTLE";
            this.chkTLE.Size = new System.Drawing.Size(133, 19);
            this.chkTLE.TabIndex = 0;
            this.chkTLE.Tag = "Tactical Legacy Pack";
            this.chkTLE.Text = "Tactical Legacy Pack";
            this.chkTLE.UseVisualStyleBackColor = true;
            this.chkTLE.CheckedChanged += new System.EventHandler(this.chkTLE_CheckedChanged);
            // 
            // chkAnarchy
            // 
            this.chkAnarchy.AutoSize = true;
            this.chkAnarchy.Location = new System.Drawing.Point(7, 48);
            this.chkAnarchy.Name = "chkAnarchy";
            this.chkAnarchy.Size = new System.Drawing.Size(126, 19);
            this.chkAnarchy.TabIndex = 0;
            this.chkAnarchy.Tag = "Anarchy\'s Children";
            this.chkAnarchy.Text = "Anarchy\'s Children";
            this.chkAnarchy.UseVisualStyleBackColor = true;
            // 
            // chkAlienHunters
            // 
            this.chkAlienHunters.AutoSize = true;
            this.chkAlienHunters.Location = new System.Drawing.Point(148, 48);
            this.chkAlienHunters.Name = "chkAlienHunters";
            this.chkAlienHunters.Size = new System.Drawing.Size(98, 19);
            this.chkAlienHunters.TabIndex = 0;
            this.chkAlienHunters.Tag = "Alien Hunters";
            this.chkAlienHunters.Text = "Alien Hunters";
            this.chkAlienHunters.UseVisualStyleBackColor = true;
            // 
            // chkShensLastGift
            // 
            this.chkShensLastGift.AutoSize = true;
            this.chkShensLastGift.Location = new System.Drawing.Point(7, 73);
            this.chkShensLastGift.Name = "chkShensLastGift";
            this.chkShensLastGift.Size = new System.Drawing.Size(106, 19);
            this.chkShensLastGift.TabIndex = 0;
            this.chkShensLastGift.Tag = "Shen\'s Last Gift";
            this.chkShensLastGift.Text = "Shen\'s Last Gift";
            this.chkShensLastGift.UseVisualStyleBackColor = true;
            // 
            // chkResistancePack
            // 
            this.chkResistancePack.AutoSize = true;
            this.chkResistancePack.Location = new System.Drawing.Point(130, 73);
            this.chkResistancePack.Name = "chkResistancePack";
            this.chkResistancePack.Size = new System.Drawing.Size(151, 19);
            this.chkResistancePack.TabIndex = 0;
            this.chkResistancePack.Tag = "Resistance Warrior Pack";
            this.chkResistancePack.Text = "Resistance Warrior Pack";
            this.chkResistancePack.UseVisualStyleBackColor = true;
            // 
            // grpMods
            // 
            this.grpMods.Controls.Add(this.lblMoreComing);
            this.grpMods.Controls.Add(this.chkFemaleHair);
            this.grpMods.Controls.Add(this.chkFemaleClothing);
            this.grpMods.Controls.Add(this.chkMoreArmorColors);
            this.grpMods.Controls.Add(this.chkMoreHairColors);
            this.grpMods.Controls.Add(this.chkMaleHair);
            this.grpMods.Controls.Add(this.chkCapnBubs);
            this.grpMods.Location = new System.Drawing.Point(13, 201);
            this.grpMods.Name = "grpMods";
            this.grpMods.Size = new System.Drawing.Size(286, 117);
            this.grpMods.TabIndex = 3;
            this.grpMods.TabStop = false;
            this.grpMods.Text = "Unofficial Mods";
            // 
            // lblMoreComing
            // 
            this.lblMoreComing.AutoSize = true;
            this.lblMoreComing.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblMoreComing.Location = new System.Drawing.Point(73, 99);
            this.lblMoreComing.Name = "lblMoreComing";
            this.lblMoreComing.Size = new System.Drawing.Size(136, 15);
            this.lblMoreComing.TabIndex = 1;
            this.lblMoreComing.Text = "More mod support TBD...";
            // 
            // chkFemaleClothing
            // 
            this.chkFemaleClothing.AutoSize = true;
            this.chkFemaleClothing.Location = new System.Drawing.Point(6, 47);
            this.chkFemaleClothing.Name = "chkFemaleClothing";
            this.chkFemaleClothing.Size = new System.Drawing.Size(141, 19);
            this.chkFemaleClothing.TabIndex = 0;
            this.chkFemaleClothing.Tag = "Female Clothing Pack";
            this.chkFemaleClothing.Text = "Female Clothing Pack";
            this.chkFemaleClothing.UseVisualStyleBackColor = true;
            // 
            // chkMoreArmorColors
            // 
            this.chkMoreArmorColors.AutoSize = true;
            this.chkMoreArmorColors.Location = new System.Drawing.Point(6, 72);
            this.chkMoreArmorColors.Name = "chkMoreArmorColors";
            this.chkMoreArmorColors.Size = new System.Drawing.Size(128, 19);
            this.chkMoreArmorColors.TabIndex = 0;
            this.chkMoreArmorColors.Tag = "More Armor Colors";
            this.chkMoreArmorColors.Text = "More Armor Colors";
            this.chkMoreArmorColors.UseVisualStyleBackColor = true;
            // 
            // chkMoreHairColors
            // 
            this.chkMoreHairColors.AutoSize = true;
            this.chkMoreHairColors.Location = new System.Drawing.Point(163, 72);
            this.chkMoreHairColors.Name = "chkMoreHairColors";
            this.chkMoreHairColors.Size = new System.Drawing.Size(116, 19);
            this.chkMoreHairColors.TabIndex = 0;
            this.chkMoreHairColors.Tag = "More Hair Colors";
            this.chkMoreHairColors.Text = "More Hair Colors";
            this.chkMoreHairColors.UseVisualStyleBackColor = true;
            // 
            // chkMaleHair
            // 
            this.chkMaleHair.AutoSize = true;
            this.chkMaleHair.Location = new System.Drawing.Point(163, 22);
            this.chkMaleHair.Name = "chkMaleHair";
            this.chkMaleHair.Size = new System.Drawing.Size(105, 19);
            this.chkMaleHair.TabIndex = 0;
            this.chkMaleHair.Tag = "Male Hair Pack";
            this.chkMaleHair.Text = "Male Hair Pack";
            this.chkMaleHair.UseVisualStyleBackColor = true;
            // 
            // chkCapnBubs
            // 
            this.chkCapnBubs.AutoSize = true;
            this.chkCapnBubs.Location = new System.Drawing.Point(6, 22);
            this.chkCapnBubs.Name = "chkCapnBubs";
            this.chkCapnBubs.Size = new System.Drawing.Size(144, 19);
            this.chkCapnBubs.TabIndex = 0;
            this.chkCapnBubs.Tag = "CapnBubs Accessories";
            this.chkCapnBubs.Text = "CapnBubs Accessories";
            this.chkCapnBubs.UseVisualStyleBackColor = true;
            // 
            // btnToggleAll
            // 
            this.btnToggleAll.Location = new System.Drawing.Point(118, 324);
            this.btnToggleAll.Name = "btnToggleAll";
            this.btnToggleAll.Size = new System.Drawing.Size(75, 23);
            this.btnToggleAll.TabIndex = 4;
            this.btnToggleAll.Text = "Toggle All";
            this.btnToggleAll.UseVisualStyleBackColor = true;
            this.btnToggleAll.Click += new System.EventHandler(this.btnToggleAll_Click);
            // 
            // chkFemaleHair
            // 
            this.chkFemaleHair.AutoSize = true;
            this.chkFemaleHair.Location = new System.Drawing.Point(163, 47);
            this.chkFemaleHair.Name = "chkFemaleHair";
            this.chkFemaleHair.Size = new System.Drawing.Size(117, 19);
            this.chkFemaleHair.TabIndex = 0;
            this.chkFemaleHair.Tag = "Female Hair Pack";
            this.chkFemaleHair.Text = "Female Hair Pack";
            this.chkFemaleHair.UseVisualStyleBackColor = true;
            // 
            // frmPoolDlcModOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 359);
            this.Controls.Add(this.btnToggleAll);
            this.Controls.Add(this.grpMods);
            this.Controls.Add(this.grpOfficial);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblDescription);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(327, 398);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(327, 398);
            this.Name = "frmPoolDlcModOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DLC/Mod Options";
            this.grpOfficial.ResumeLayout(false);
            this.grpOfficial.PerformLayout();
            this.grpMods.ResumeLayout(false);
            this.grpMods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnToggleAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpOfficial;
        private System.Windows.Forms.CheckBox chkWotC;
        private System.Windows.Forms.CheckBox chkTLE;
        private System.Windows.Forms.CheckBox chkAnarchy;
        private System.Windows.Forms.CheckBox chkAlienHunters;
        private System.Windows.Forms.CheckBox chkShensLastGift;
        private System.Windows.Forms.CheckBox chkResistancePack;
        private System.Windows.Forms.GroupBox grpMods;
        private System.Windows.Forms.CheckBox chkCapnBubs;
        private System.Windows.Forms.CheckBox chkFemaleClothing;
        private System.Windows.Forms.CheckBox chkMaleHair;
        private System.Windows.Forms.Label lblMoreComing;
        private System.Windows.Forms.CheckBox chkMoreHairColors;
        private System.Windows.Forms.CheckBox chkMoreArmorColors;
        private System.Windows.Forms.CheckBox chkFemaleHair;
    }
}