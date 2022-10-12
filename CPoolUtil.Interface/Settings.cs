using CPoolUtil.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CPoolUtil.Interface
{
    [Serializable]
    public class Settings : ICloneable
    {
        public List<string> SelectedDLCsAndMods { get; set; } = new List<string>();
        public bool ShowWarnings { get; set; } = true;

        public static void Save()
        {
            // Always set DLCs from Overlord
            Program.Settings.SelectedDLCsAndMods = Overlord.DlcAndModOptions;

            try
            {
                var serializer = new XmlSerializer(typeof(Settings));
                var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Files/Settings.ini"));
                serializer.Serialize(writer, Program.Settings);
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings file: {ex.Message}");
            }
}

        public static void Load()
        {
            var existingSettings = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Settings.ini"));

            if (existingSettings.Exists)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Files/Settings.ini"));
                    Program.Settings = serializer.Deserialize(reader) as Settings;
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading settings file: {ex.Message}");
                }

                // Set Overlord DLCs from loaded settings IF it's empty
                if (Overlord.DlcAndModOptions.Count == 0)
                    Overlord.DlcAndModOptions = Program.Settings.SelectedDLCsAndMods;
            }
        }

        public object Clone()
        {
            return new Settings
            {
                SelectedDLCsAndMods = this.SelectedDLCsAndMods.ToList(),
                ShowWarnings = this.ShowWarnings
            };
        }
    }
}