using System;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public partial class frmSettings : Form
    {
        private Settings _oldSettings;

        public frmSettings()
        {
            InitializeComponent();

            // Initialize databindings
            chkShowWarnings.DataBindings.Add("Checked", Program.Settings, "ShowWarnings");

            _oldSettings = Program.Settings.Clone() as Settings;
        }

        private void btnDlcMods_Click(object sender, EventArgs e)
        {
            new frmPoolDlcModOptions(null).ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Revert to old settings and close form
            Program.Settings = _oldSettings;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Save and close
            Settings.Save();
            Close();
        }
    }
}
