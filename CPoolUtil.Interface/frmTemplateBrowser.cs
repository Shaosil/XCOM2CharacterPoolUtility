using CPoolUtil.Core;
using FastMember;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public partial class frmTemplateBrowser : Form
    {
        private BindingSource templateBs = new BindingSource();
        private Overlord _overlord;

        public frmTemplateBrowser()
        {
            InitializeComponent();
        }

        private void frmTemplateBrowser_Load(object sender, EventArgs e)
        {
            _overlord = new Overlord();
            _overlord.LoadCustomizationTemplates();

            var dt = new DataTable();
            using (var reader = ObjectReader.Create(_overlord.Templates))
                dt.Load(reader);
            dt.Columns["Display"].SetOrdinal(0);
            dt.Columns["Name"].SetOrdinal(1);
            dt.Columns["Gender"].SetOrdinal(2);
            dt.Columns["Race"].SetOrdinal(3);
            dt.Columns["Language"].SetOrdinal(4);
            dt.Columns["ArchetypeName"].SetOrdinal(5);
            dt.Columns["CharacterTemplate"].SetOrdinal(6);
            dt.Columns["ArmorTemplate"].SetOrdinal(7);
            dt.Columns["Veteran"].SetOrdinal(8);
            dt.Columns["CanUseOnCivilian"].SetOrdinal(9);
            dt.Columns["SpecializedType"].SetOrdinal(10);
            dt.Columns["SetNames"].SetOrdinal(11);
            dt.Columns["Tech"].SetOrdinal(12);
            dt.Columns["PartType"].SetOrdinal(13);
            dt.Columns["Origin"].SetOrdinal(14);

            templateBs.DataSource = dt;
            dgvTemplates.DataSource = templateBs;
            dgvTemplates.Columns["Index"].Visible = false;
            dgvTemplates.Columns["PartTypeLocalized"].Visible = false;
            ApplyFilter();
        }

        private void txtFilters_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            // Textbox and origin checkbox filters
            var str = @$"('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtDisplayName.Text)}' OR Display LIKE '%{txtDisplayName.Text}%' OR Name LIKE '%{txtDisplayName.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtGender.Text)}' OR Gender LIKE '%{txtGender.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtRace.Text)}' OR Race LIKE '%{txtRace.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtLanguage.Text)}' OR Language LIKE '%{txtLanguage.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtArchetype.Text)}' OR ArchetypeName LIKE '%{txtArchetype.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtCharTemplate.Text)}' OR CharacterTemplate LIKE '%{txtCharTemplate.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtArmorTemplate.Text)}' OR ArmorTemplate LIKE '%{txtArmorTemplate.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtVeteran.Text)}' OR Veteran LIKE '%{txtVeteran.Text}%')
                AND ('{bool.TrueString}' = '{chkCanUserOnCiv.CheckState == CheckState.Indeterminate}' OR CanUseOnCivilian = {(chkCanUserOnCiv.Checked ? '1' : '0')})
                AND ('{bool.TrueString}' = '{chkSpecializedType.CheckState == CheckState.Indeterminate}' OR SpecializedType = {(chkSpecializedType.Checked ? '1' : '0')})
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtSetNames.Text)}' OR SetNames LIKE '%{txtSetNames.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtTech.Text)}' OR Tech LIKE '%{txtTech.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtPartType.Text)}' OR PartType LIKE '%{txtPartType.Text}%')
                AND ('{bool.TrueString}' = '{string.IsNullOrWhiteSpace(txtOrigin.Text)}' OR Origin LIKE '%{txtOrigin.Text}%')

                AND ('{bool.TrueString}' = '{chkIncludeVanilla.Checked}' OR Origin <> 'Vanilla')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("WotC")}' OR Origin <> 'WotC')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Tactical Legacy Pack")}' OR Origin <> 'Tactical Legacy Pack')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Anarchy's Children")}' OR Origin <> 'Anarchy''s Children')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Alien Hunters")}' OR Origin <> 'Alien Hunters')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Shen's Last Gift")}' OR Origin <> 'Shen''s Last Gift')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Resistance Warrior Pack")}' OR Origin <> 'Resistance Warrior Pack')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("CapnBubs Accessories")}' OR Origin <> 'CapnBubs Accessories')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Female Hair Pack")}' OR Origin <> 'Female Hair Pack')
                AND ('{bool.TrueString}' = '{_overlord.DlcAndModOptions.Contains("Male Hair Pack")}' OR Origin <> 'Male Hair Pack')";

            templateBs.Filter = str;
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            foreach (var txt in Controls.OfType<TextBox>())
                txt.Text = string.Empty;
        }

        private void btnChooseDlcsMods_Click(object sender, EventArgs e)
        {
            var optionsForm = new frmPoolDlcModOptions(_overlord.DlcAndModOptions, null);
            if (optionsForm.ShowDialog() == DialogResult.OK)
            {
                _overlord.DlcAndModOptions = optionsForm.SelectedOptions;
                ApplyFilter();
            }
        }

        private void chkIncludeVanilla_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class ConsoleOutputter : IOutputter
    {
        public void Write(string message)
        {
            Debug.Write(message);
        }

        public void WriteLine(string message = null)
        {
            Debug.WriteLine(message);
        }
    }

    public class NullOutputter : IOutputter
    {
        public void Write(string message)
        {
            // Do nothing
        }

        public void WriteLine(string message = null)
        {
            // Do nothing
        }
    }
}
