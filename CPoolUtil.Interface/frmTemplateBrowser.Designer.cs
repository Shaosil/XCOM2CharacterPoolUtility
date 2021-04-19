
using System.Collections.Generic;

namespace CPoolUtil.Interface
{
    partial class frmTemplateBrowser
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTemplates = new System.Windows.Forms.DataGridView();
            this.displayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.raceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.archetypeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.characterTemplateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.armorTemplateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.veteranDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canUseOnCivilianDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.specializedTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.setNamesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.techDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.lblRace = new System.Windows.Forms.Label();
            this.txtRace = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.lblArchetype = new System.Windows.Forms.Label();
            this.txtArchetype = new System.Windows.Forms.TextBox();
            this.lblCharTemplate = new System.Windows.Forms.Label();
            this.txtCharTemplate = new System.Windows.Forms.TextBox();
            this.txtArmorTemplate = new System.Windows.Forms.TextBox();
            this.lblArmorTemplate = new System.Windows.Forms.Label();
            this.txtVeteran = new System.Windows.Forms.TextBox();
            this.lblVeteran = new System.Windows.Forms.Label();
            this.lblCanUseOnCiv = new System.Windows.Forms.Label();
            this.lblSpecializedType = new System.Windows.Forms.Label();
            this.txtSetNames = new System.Windows.Forms.TextBox();
            this.lblSetNames = new System.Windows.Forms.Label();
            this.txtTech = new System.Windows.Forms.TextBox();
            this.lblTech = new System.Windows.Forms.Label();
            this.txtPartType = new System.Windows.Forms.TextBox();
            this.lblPartType = new System.Windows.Forms.Label();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnChooseDlcsMods = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkIncludeVanilla = new System.Windows.Forms.CheckBox();
            this.chkCanUserOnCiv = new System.Windows.Forms.CheckBox();
            this.chkSpecializedType = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemplates)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTemplates
            // 
            this.dgvTemplates.AllowUserToAddRows = false;
            this.dgvTemplates.AllowUserToDeleteRows = false;
            this.dgvTemplates.AllowUserToResizeRows = false;
            this.dgvTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTemplates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTemplates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTemplates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.genderDataGridViewTextBoxColumn,
            this.raceDataGridViewTextBoxColumn,
            this.languageDataGridViewTextBoxColumn,
            this.archetypeNameDataGridViewTextBoxColumn,
            this.characterTemplateDataGridViewTextBoxColumn,
            this.armorTemplateDataGridViewTextBoxColumn,
            this.veteranDataGridViewTextBoxColumn,
            this.canUseOnCivilianDataGridViewTextBoxColumn,
            this.specializedTypeDataGridViewTextBoxColumn,
            this.setNamesDataGridViewTextBoxColumn,
            this.techDataGridViewTextBoxColumn,
            this.partTypeDataGridViewTextBoxColumn,
            this.originDataGridViewTextBoxColumn});
            this.dgvTemplates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTemplates.Location = new System.Drawing.Point(13, 64);
            this.dgvTemplates.Name = "dgvTemplates";
            this.dgvTemplates.ReadOnly = true;
            this.dgvTemplates.RowHeadersVisible = false;
            this.dgvTemplates.RowTemplate.Height = 25;
            this.dgvTemplates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTemplates.ShowEditingIcon = false;
            this.dgvTemplates.Size = new System.Drawing.Size(1761, 859);
            this.dgvTemplates.TabIndex = 3;
            this.dgvTemplates.TabStop = false;
            // 
            // displayDataGridViewTextBoxColumn
            // 
            this.displayDataGridViewTextBoxColumn.DataPropertyName = "Display";
            this.displayDataGridViewTextBoxColumn.HeaderText = "Display";
            this.displayDataGridViewTextBoxColumn.Name = "displayDataGridViewTextBoxColumn";
            this.displayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // genderDataGridViewTextBoxColumn
            // 
            this.genderDataGridViewTextBoxColumn.DataPropertyName = "Gender";
            this.genderDataGridViewTextBoxColumn.HeaderText = "Gender";
            this.genderDataGridViewTextBoxColumn.Name = "genderDataGridViewTextBoxColumn";
            this.genderDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // raceDataGridViewTextBoxColumn
            // 
            this.raceDataGridViewTextBoxColumn.DataPropertyName = "Race";
            this.raceDataGridViewTextBoxColumn.HeaderText = "Race";
            this.raceDataGridViewTextBoxColumn.Name = "raceDataGridViewTextBoxColumn";
            this.raceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // archetypeNameDataGridViewTextBoxColumn
            // 
            this.archetypeNameDataGridViewTextBoxColumn.DataPropertyName = "ArchetypeName";
            this.archetypeNameDataGridViewTextBoxColumn.HeaderText = "ArchetypeName";
            this.archetypeNameDataGridViewTextBoxColumn.Name = "archetypeNameDataGridViewTextBoxColumn";
            this.archetypeNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // characterTemplateDataGridViewTextBoxColumn
            // 
            this.characterTemplateDataGridViewTextBoxColumn.DataPropertyName = "CharacterTemplate";
            this.characterTemplateDataGridViewTextBoxColumn.HeaderText = "CharacterTemplate";
            this.characterTemplateDataGridViewTextBoxColumn.Name = "characterTemplateDataGridViewTextBoxColumn";
            this.characterTemplateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // armorTemplateDataGridViewTextBoxColumn
            // 
            this.armorTemplateDataGridViewTextBoxColumn.DataPropertyName = "ArmorTemplate";
            this.armorTemplateDataGridViewTextBoxColumn.HeaderText = "ArmorTemplate";
            this.armorTemplateDataGridViewTextBoxColumn.Name = "armorTemplateDataGridViewTextBoxColumn";
            this.armorTemplateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // veteranDataGridViewTextBoxColumn
            // 
            this.veteranDataGridViewTextBoxColumn.DataPropertyName = "Veteran";
            this.veteranDataGridViewTextBoxColumn.HeaderText = "Veteran";
            this.veteranDataGridViewTextBoxColumn.Name = "veteranDataGridViewTextBoxColumn";
            this.veteranDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // canUseOnCivilianDataGridViewTextBoxColumn
            // 
            this.canUseOnCivilianDataGridViewTextBoxColumn.DataPropertyName = "CanUseOnCivilian";
            this.canUseOnCivilianDataGridViewTextBoxColumn.HeaderText = "CanUseOnCivilian";
            this.canUseOnCivilianDataGridViewTextBoxColumn.Name = "canUseOnCivilianDataGridViewTextBoxColumn";
            this.canUseOnCivilianDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // specializedTypeDataGridViewTextBoxColumn
            // 
            this.specializedTypeDataGridViewTextBoxColumn.DataPropertyName = "SpecializedType";
            this.specializedTypeDataGridViewTextBoxColumn.HeaderText = "SpecializedType";
            this.specializedTypeDataGridViewTextBoxColumn.Name = "specializedTypeDataGridViewTextBoxColumn";
            this.specializedTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // setNamesDataGridViewTextBoxColumn
            // 
            this.setNamesDataGridViewTextBoxColumn.DataPropertyName = "SetNames";
            this.setNamesDataGridViewTextBoxColumn.HeaderText = "SetNames";
            this.setNamesDataGridViewTextBoxColumn.Name = "setNamesDataGridViewTextBoxColumn";
            this.setNamesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // techDataGridViewTextBoxColumn
            // 
            this.techDataGridViewTextBoxColumn.DataPropertyName = "Tech";
            this.techDataGridViewTextBoxColumn.HeaderText = "Tech";
            this.techDataGridViewTextBoxColumn.Name = "techDataGridViewTextBoxColumn";
            this.techDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // partTypeDataGridViewTextBoxColumn
            // 
            this.partTypeDataGridViewTextBoxColumn.DataPropertyName = "PartType";
            this.partTypeDataGridViewTextBoxColumn.HeaderText = "PartType";
            this.partTypeDataGridViewTextBoxColumn.Name = "partTypeDataGridViewTextBoxColumn";
            this.partTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // originDataGridViewTextBoxColumn
            // 
            this.originDataGridViewTextBoxColumn.DataPropertyName = "Origin";
            this.originDataGridViewTextBoxColumn.HeaderText = "Origin";
            this.originDataGridViewTextBoxColumn.Name = "originDataGridViewTextBoxColumn";
            this.originDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDisplayName.Location = new System.Drawing.Point(25, 35);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(108, 23);
            this.txtDisplayName.TabIndex = 0;
            this.txtDisplayName.Tag = "Name";
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDisplayName.Location = new System.Drawing.Point(25, 9);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(108, 23);
            this.lblDisplayName.TabIndex = 5;
            this.lblDisplayName.Text = "Display/Name";
            this.lblDisplayName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblGender
            // 
            this.lblGender.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGender.Location = new System.Drawing.Point(161, 9);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(89, 23);
            this.lblGender.TabIndex = 7;
            this.lblGender.Text = "Gender";
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtGender
            // 
            this.txtGender.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtGender.Location = new System.Drawing.Point(153, 35);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(104, 23);
            this.txtGender.TabIndex = 1;
            this.txtGender.Tag = "Gender";
            this.txtGender.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblRace
            // 
            this.lblRace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRace.Location = new System.Drawing.Point(286, 9);
            this.lblRace.Name = "lblRace";
            this.lblRace.Size = new System.Drawing.Size(89, 23);
            this.lblRace.TabIndex = 9;
            this.lblRace.Text = "Race";
            this.lblRace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtRace
            // 
            this.txtRace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtRace.Location = new System.Drawing.Point(278, 35);
            this.txtRace.Name = "txtRace";
            this.txtRace.Size = new System.Drawing.Size(104, 23);
            this.txtRace.TabIndex = 2;
            this.txtRace.Tag = "Race";
            this.txtRace.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLanguage.Location = new System.Drawing.Point(412, 9);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(89, 23);
            this.lblLanguage.TabIndex = 11;
            this.lblLanguage.Text = "Language";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtLanguage
            // 
            this.txtLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtLanguage.Location = new System.Drawing.Point(400, 35);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(112, 23);
            this.txtLanguage.TabIndex = 3;
            this.txtLanguage.Tag = "Language";
            this.txtLanguage.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblArchetype
            // 
            this.lblArchetype.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblArchetype.Location = new System.Drawing.Point(537, 9);
            this.lblArchetype.Name = "lblArchetype";
            this.lblArchetype.Size = new System.Drawing.Size(89, 23);
            this.lblArchetype.TabIndex = 13;
            this.lblArchetype.Text = "Archetype";
            this.lblArchetype.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtArchetype
            // 
            this.txtArchetype.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtArchetype.Location = new System.Drawing.Point(518, 35);
            this.txtArchetype.Name = "txtArchetype";
            this.txtArchetype.Size = new System.Drawing.Size(126, 23);
            this.txtArchetype.TabIndex = 4;
            this.txtArchetype.Tag = "ArchetypeName";
            this.txtArchetype.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblCharTemplate
            // 
            this.lblCharTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCharTemplate.Location = new System.Drawing.Point(652, 9);
            this.lblCharTemplate.Name = "lblCharTemplate";
            this.lblCharTemplate.Size = new System.Drawing.Size(115, 23);
            this.lblCharTemplate.TabIndex = 15;
            this.lblCharTemplate.Text = "Character Template";
            this.lblCharTemplate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtCharTemplate
            // 
            this.txtCharTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCharTemplate.Location = new System.Drawing.Point(650, 35);
            this.txtCharTemplate.Name = "txtCharTemplate";
            this.txtCharTemplate.Size = new System.Drawing.Size(119, 23);
            this.txtCharTemplate.TabIndex = 5;
            this.txtCharTemplate.Tag = "CharacterTemplate";
            this.txtCharTemplate.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // txtArmorTemplate
            // 
            this.txtArmorTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtArmorTemplate.Location = new System.Drawing.Point(775, 35);
            this.txtArmorTemplate.Name = "txtArmorTemplate";
            this.txtArmorTemplate.Size = new System.Drawing.Size(119, 23);
            this.txtArmorTemplate.TabIndex = 6;
            this.txtArmorTemplate.Tag = "ArmorTemplate";
            this.txtArmorTemplate.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblArmorTemplate
            // 
            this.lblArmorTemplate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblArmorTemplate.Location = new System.Drawing.Point(777, 9);
            this.lblArmorTemplate.Name = "lblArmorTemplate";
            this.lblArmorTemplate.Size = new System.Drawing.Size(115, 23);
            this.lblArmorTemplate.TabIndex = 15;
            this.lblArmorTemplate.Text = "Armor Template";
            this.lblArmorTemplate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtVeteran
            // 
            this.txtVeteran.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtVeteran.Location = new System.Drawing.Point(900, 35);
            this.txtVeteran.Name = "txtVeteran";
            this.txtVeteran.Size = new System.Drawing.Size(119, 23);
            this.txtVeteran.TabIndex = 7;
            this.txtVeteran.Tag = "Veteran";
            this.txtVeteran.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblVeteran
            // 
            this.lblVeteran.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblVeteran.Location = new System.Drawing.Point(902, 9);
            this.lblVeteran.Name = "lblVeteran";
            this.lblVeteran.Size = new System.Drawing.Size(115, 23);
            this.lblVeteran.TabIndex = 15;
            this.lblVeteran.Text = "Veteran";
            this.lblVeteran.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCanUseOnCiv
            // 
            this.lblCanUseOnCiv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCanUseOnCiv.Location = new System.Drawing.Point(1027, 9);
            this.lblCanUseOnCiv.Name = "lblCanUseOnCiv";
            this.lblCanUseOnCiv.Size = new System.Drawing.Size(115, 23);
            this.lblCanUseOnCiv.TabIndex = 15;
            this.lblCanUseOnCiv.Text = "Can Use On Civ";
            this.lblCanUseOnCiv.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSpecializedType
            // 
            this.lblSpecializedType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSpecializedType.Location = new System.Drawing.Point(1152, 9);
            this.lblSpecializedType.Name = "lblSpecializedType";
            this.lblSpecializedType.Size = new System.Drawing.Size(115, 23);
            this.lblSpecializedType.TabIndex = 15;
            this.lblSpecializedType.Text = "Specialized Type";
            this.lblSpecializedType.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtSetNames
            // 
            this.txtSetNames.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSetNames.Location = new System.Drawing.Point(1275, 35);
            this.txtSetNames.Name = "txtSetNames";
            this.txtSetNames.Size = new System.Drawing.Size(119, 23);
            this.txtSetNames.TabIndex = 10;
            this.txtSetNames.Tag = "SetNames";
            this.txtSetNames.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblSetNames
            // 
            this.lblSetNames.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSetNames.Location = new System.Drawing.Point(1277, 9);
            this.lblSetNames.Name = "lblSetNames";
            this.lblSetNames.Size = new System.Drawing.Size(115, 23);
            this.lblSetNames.TabIndex = 15;
            this.lblSetNames.Text = "Set Names";
            this.lblSetNames.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtTech
            // 
            this.txtTech.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTech.Location = new System.Drawing.Point(1400, 35);
            this.txtTech.Name = "txtTech";
            this.txtTech.Size = new System.Drawing.Size(119, 23);
            this.txtTech.TabIndex = 11;
            this.txtTech.Tag = "Tech";
            this.txtTech.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblTech
            // 
            this.lblTech.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTech.Location = new System.Drawing.Point(1402, 9);
            this.lblTech.Name = "lblTech";
            this.lblTech.Size = new System.Drawing.Size(115, 23);
            this.lblTech.TabIndex = 15;
            this.lblTech.Text = "Tech";
            this.lblTech.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtPartType
            // 
            this.txtPartType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPartType.Location = new System.Drawing.Point(1525, 35);
            this.txtPartType.Name = "txtPartType";
            this.txtPartType.Size = new System.Drawing.Size(119, 23);
            this.txtPartType.TabIndex = 12;
            this.txtPartType.Tag = "PartType";
            this.txtPartType.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblPartType
            // 
            this.lblPartType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPartType.Location = new System.Drawing.Point(1527, 9);
            this.lblPartType.Name = "lblPartType";
            this.lblPartType.Size = new System.Drawing.Size(115, 23);
            this.lblPartType.TabIndex = 15;
            this.lblPartType.Text = "Part Type";
            this.lblPartType.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtOrigin
            // 
            this.txtOrigin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtOrigin.Location = new System.Drawing.Point(1650, 35);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(119, 23);
            this.txtOrigin.TabIndex = 12;
            this.txtOrigin.Tag = "Origin";
            this.txtOrigin.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // lblOrigin
            // 
            this.lblOrigin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOrigin.Location = new System.Drawing.Point(1652, 9);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(115, 23);
            this.lblOrigin.TabIndex = 15;
            this.lblOrigin.Text = "Origin";
            this.lblOrigin.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(13, 929);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(104, 34);
            this.btnClearFilters.TabIndex = 16;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // btnChooseDlcsMods
            // 
            this.btnChooseDlcsMods.Location = new System.Drawing.Point(123, 929);
            this.btnChooseDlcsMods.Name = "btnChooseDlcsMods";
            this.btnChooseDlcsMods.Size = new System.Drawing.Size(163, 34);
            this.btnChooseDlcsMods.TabIndex = 16;
            this.btnChooseDlcsMods.Text = "Choose DLCs and Mods...";
            this.btnChooseDlcsMods.UseVisualStyleBackColor = true;
            this.btnChooseDlcsMods.Click += new System.EventHandler(this.btnChooseDlcsMods_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1650, 929);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 34);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkIncludeVanilla
            // 
            this.chkIncludeVanilla.AutoSize = true;
            this.chkIncludeVanilla.Checked = true;
            this.chkIncludeVanilla.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeVanilla.Location = new System.Drawing.Point(292, 938);
            this.chkIncludeVanilla.Name = "chkIncludeVanilla";
            this.chkIncludeVanilla.Size = new System.Drawing.Size(102, 19);
            this.chkIncludeVanilla.TabIndex = 17;
            this.chkIncludeVanilla.Text = "Include Vanilla";
            this.chkIncludeVanilla.UseVisualStyleBackColor = true;
            this.chkIncludeVanilla.CheckedChanged += new System.EventHandler(this.chkIncludeVanilla_CheckedChanged);
            // 
            // chkCanUserOnCiv
            // 
            this.chkCanUserOnCiv.AutoSize = true;
            this.chkCanUserOnCiv.Checked = true;
            this.chkCanUserOnCiv.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkCanUserOnCiv.Location = new System.Drawing.Point(1077, 39);
            this.chkCanUserOnCiv.Name = "chkCanUserOnCiv";
            this.chkCanUserOnCiv.Size = new System.Drawing.Size(15, 14);
            this.chkCanUserOnCiv.TabIndex = 18;
            this.chkCanUserOnCiv.ThreeState = true;
            this.chkCanUserOnCiv.UseVisualStyleBackColor = true;
            this.chkCanUserOnCiv.CheckStateChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // chkSpecializedType
            // 
            this.chkSpecializedType.AutoSize = true;
            this.chkSpecializedType.Checked = true;
            this.chkSpecializedType.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkSpecializedType.Location = new System.Drawing.Point(1202, 39);
            this.chkSpecializedType.Name = "chkSpecializedType";
            this.chkSpecializedType.Size = new System.Drawing.Size(15, 14);
            this.chkSpecializedType.TabIndex = 19;
            this.chkSpecializedType.ThreeState = true;
            this.chkSpecializedType.UseVisualStyleBackColor = true;
            this.chkSpecializedType.CheckStateChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // frmTemplateBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1786, 975);
            this.Controls.Add(this.chkSpecializedType);
            this.Controls.Add(this.chkCanUserOnCiv);
            this.Controls.Add(this.chkIncludeVanilla);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnChooseDlcsMods);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.lblOrigin);
            this.Controls.Add(this.lblPartType);
            this.Controls.Add(this.txtOrigin);
            this.Controls.Add(this.txtPartType);
            this.Controls.Add(this.lblTech);
            this.Controls.Add(this.txtTech);
            this.Controls.Add(this.lblSetNames);
            this.Controls.Add(this.txtSetNames);
            this.Controls.Add(this.lblSpecializedType);
            this.Controls.Add(this.lblCanUseOnCiv);
            this.Controls.Add(this.lblVeteran);
            this.Controls.Add(this.txtVeteran);
            this.Controls.Add(this.lblArmorTemplate);
            this.Controls.Add(this.txtArmorTemplate);
            this.Controls.Add(this.lblCharTemplate);
            this.Controls.Add(this.txtCharTemplate);
            this.Controls.Add(this.lblArchetype);
            this.Controls.Add(this.txtArchetype);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.txtLanguage);
            this.Controls.Add(this.lblRace);
            this.Controls.Add(this.txtRace);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.lblDisplayName);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.dgvTemplates);
            this.Name = "frmTemplateBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Template Browser";
            this.Load += new System.EventHandler(this.frmTemplateBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemplates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTemplates;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblRace;
        private System.Windows.Forms.TextBox txtRace;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.Label lblArchetype;
        private System.Windows.Forms.TextBox txtArchetype;
        private System.Windows.Forms.Label lblCharTemplate;
        private System.Windows.Forms.TextBox txtCharTemplate;
        private System.Windows.Forms.TextBox txtArmorTemplate;
        private System.Windows.Forms.Label lblArmorTemplate;
        private System.Windows.Forms.TextBox txtVeteran;
        private System.Windows.Forms.Label lblVeteran;
        private System.Windows.Forms.Label lblCanUseOnCiv;
        private System.Windows.Forms.Label lblSpecializedType;
        private System.Windows.Forms.TextBox txtSetNames;
        private System.Windows.Forms.Label lblSetNames;
        private System.Windows.Forms.TextBox txtTech;
        private System.Windows.Forms.Label lblTech;
        private System.Windows.Forms.TextBox txtPartType;
        private System.Windows.Forms.Label lblPartType;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn genderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn raceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn archetypeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn characterTemplateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn armorTemplateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn veteranDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn canUseOnCivilianDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn specializedTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn setNamesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn techDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnChooseDlcsMods;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkIncludeVanilla;
        private System.Windows.Forms.CheckBox chkCanUserOnCiv;
        private System.Windows.Forms.CheckBox chkSpecializedType;
    }
}

