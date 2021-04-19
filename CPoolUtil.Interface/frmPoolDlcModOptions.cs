using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public partial class frmPoolDlcModOptions : Form
    {
        public List<string> SelectedOptions = new List<string>();
        private List<string> _forcedOptions = new List<string>();

        public frmPoolDlcModOptions(List<string> currentOptions, List<string> forcedOptions)
        {
            InitializeComponent();

            if (forcedOptions != null) _forcedOptions = forcedOptions;
            foreach (var cbx in grpOfficial.Controls.OfType<CheckBox>().Concat(grpMods.Controls.OfType<CheckBox>()))
            {
                var isForced = _forcedOptions.Contains(cbx.Tag.ToString());
                cbx.Checked = currentOptions.Contains(cbx.Tag.ToString()) || isForced;
                cbx.Enabled = !isForced;
            }

            // Refresh the WotC/TLE checkboxes to get proper statuses
            chkWotC_CheckedChanged(null, null);
            chkTLE_CheckedChanged(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnToggleAll_Click(object sender, EventArgs e)
        {
            // "Currently checked" = the more common checked state of all non-forced checkboxes
            var validCheckboxes = grpOfficial.Controls.OfType<CheckBox>().Concat(grpMods.Controls.OfType<CheckBox>()).Where(c => !_forcedOptions.Contains(c.Tag.ToString())).ToList();
            bool currentlyChecked = validCheckboxes.Count(c => c.Checked) >= validCheckboxes.Count(c => !c.Checked);

            foreach (var cbx in validCheckboxes)
                cbx.Checked = !currentlyChecked;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SelectedOptions = grpOfficial.Controls.OfType<CheckBox>().Where(c => c.Checked).Select(c => c.Tag.ToString())
                .Concat(grpMods.Controls.OfType<CheckBox>().Where(c => c.Checked).Select(c => c.Tag.ToString())).ToList();
            Close();
        }

        private void chkWotC_CheckedChanged(object sender, EventArgs e)
        {
            // TLE requires WotC
            if (!chkWotC.Checked) chkTLE.Checked = false;
            chkTLE.Enabled = chkWotC.Checked && !_forcedOptions.Contains(chkTLE.Tag.ToString());
        }

        private void chkTLE_CheckedChanged(object sender, EventArgs e)
        {
            // TLE requires WotC
            if (chkTLE.Checked) chkWotC.Checked = true;
            chkWotC.Enabled = !chkTLE.Checked && !_forcedOptions.Contains(chkWotC.Tag.ToString());
        }
    }
}
