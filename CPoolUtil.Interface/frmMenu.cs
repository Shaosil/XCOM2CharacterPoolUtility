using CPoolUtil.Core;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public partial class frmMenu : Form
    {
        private static readonly HttpClient updateCheckerClient = new HttpClient(new HttpClientHandler { Proxy = null, UseProxy = false });
        private IOutputter _outputter = new NullOutputter();

        public frmMenu()
        {
            InitializeComponent();

            lblVersion.Text = $"v{Application.ProductVersion}";

            // Silently check for updates on load
            updateCheckerClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            CheckForUpdatesAsync();
        }

        private void menuItemAboutUpdates_Click(object sender, EventArgs e)
        {
            CheckForUpdatesAsync(true);
        }

        private void btnCreatePool_Click(object sender, EventArgs e)
        {
            new frmPoolEditor(_outputter).ShowDialog();
        }

        private void btnCharacterPoolBrowser_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "bin files (*.bin)|*.bin", Multiselect = true, RestoreDirectory = true };
            if (ofd.ShowDialog() == DialogResult.OK)
                new frmPoolEditor(ofd.FileNames, _outputter).ShowDialog();
        }

        private void btnTemplateBrowser_Click(object sender, EventArgs e)
        {
            new frmTemplateBrowser().ShowDialog();
        }

        private void CheckForUpdatesAsync(bool displayInfoMessages = false)
        {
            menuItemAboutUpdates.Enabled = false;
            lblUpdateStatus.Text = "Checking for updates...";
            lblUpdateStatus.Cursor = Cursors.Default;

            var bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += (s, e) =>
            {
                try
                {
                    // Ping the github releases and just pull back the latest tag name
                    var latestTag = JsonConvert.DeserializeObject<UpdateCheckResponse>(updateCheckerClient.GetStringAsync("https://api.github.com/repos/Shaosil/XCOM2CharacterPoolUtility/releases/latest").Result).tag_name;

                    if (string.IsNullOrEmpty(latestTag))
                        throw new Exception("Current version info not found.");

                    // Parse it out and compare it to our current version
                    var ourVersionInfo = ParseVersion(Application.ProductVersion);
                    var latestVersionInfo = ParseVersion(latestTag);

                    for (int i = 0; i < ourVersionInfo.Length; i++)
                        if (latestVersionInfo[i] > ourVersionInfo[i])
                        {
                            // 100 will be interpreted as a new version is available
                            bw.ReportProgress(100);
                            return;
                        }

                    // 0 will be interpreted as no new versions available
                    bw.ReportProgress(displayInfoMessages ? 0 : -1);
                }
                catch (Exception ex)
                {
                    bw.ReportProgress(-1, displayInfoMessages ? ex : null);
                }
            };
            bw.ProgressChanged += (s, e) =>
            {
                if (e.UserState is Exception ex)
                    MessageBox.Show($"Error checking for updates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (e.ProgressPercentage == 0)
                    MessageBox.Show($"No new version available", "Update Check Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (e.ProgressPercentage == 100 && MessageBox.Show("A newer version has been found online. Go to project page?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    GoToDownloadPage();

                lblUpdateStatus.Text = e.ProgressPercentage == 100 ? "New version available! Click here to download." : string.Empty;
                lblUpdateStatus.Cursor = e.ProgressPercentage == 100 ? Cursors.Hand : Cursors.Default;
                menuItemAboutUpdates.Enabled = true;
            };
            bw.RunWorkerAsync();
        }

        private void lblUpdateStatus_Click(object sender, EventArgs e)
        {
            if (lblUpdateStatus.Cursor == Cursors.Hand)
                GoToDownloadPage();
        }

        /// <summary>
        /// Always returns an array of 3 ints (major, minor, revision)
        /// </summary>
        /// <param name="version">The version number string. E.g. "1.0.0"</param>
        /// <returns></returns>
        private int[] ParseVersion(string version)
        {
            var vParts = version?.Split('.') ?? Array.Empty<string>();
            int[] viParts = new int[3];

            for (int i = 0; i < 3; i++)
                int.TryParse(vParts.Length > i ? vParts[i] : "0", out viParts[i]);

            return viParts;
        }

        private void GoToDownloadPage()
        {
            Process.Start(new ProcessStartInfo { FileName = "https://github.com/Shaosil/XCOM2CharacterPoolUtility/releases/latest", UseShellExecute = true });
        }
    }

    public class UpdateCheckResponse
    {
        public string tag_name { get; set; }
    }
}