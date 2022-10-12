using CPoolUtil.Core;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public static class Program
    {
        public static Settings Settings { get; set; } = new Settings();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Settings.Load();
            Application.Run(new frmMenu());
        }

        /// <summary>
        /// Keeping this around in case I support more mods that have helmets. It parses all the extracted XComHelmetContent files in a directory and generates a
        /// text file that has custom information on which helmets block things like hair and facial props
        /// </summary>
        /// <param name="rawHelmetFilesRootDir">The top level directory of the extracted files from the XCOM Editor</param>
        /// <param name="outputFilePath">The consolidated output text file location</param>
        static void ParseHelmetInfo(string rawHelmetFilesRootDir, string outputFilePath)
        {
            Overlord.LoadCustomizationTemplates();
            var files = new DirectoryInfo(rawHelmetFilesRootDir).GetFiles("*", SearchOption.AllDirectories);

            var sb = new StringBuilder();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file.FullName).Where(l => !string.IsNullOrEmpty(l)).Select(l => l.Trim().Replace("\"", "").Split('=')).Where(l => l.Length > 1).ToList();

                string arcName = $"{file.Directory.Name}.{lines.First(l => l[0] == "Name")[1]}";
                bool hideHair = !lines.Any(l => l[0] == "FallbackHairIndex");
                bool hideUpper = lines.Any(l => l[0] == "bHideUpperFacialProps");
                bool hideLower = lines.Any(l => l[0] == "bHideLowerFacialProps");
                bool hideFacialHair = lines.Any(l => l[0] == "bHideFacialHair");

                var matchingTemplate = Overlord.Templates.First(t => t.ArchetypeName?.Equals(arcName, StringComparison.OrdinalIgnoreCase) ?? false);

                sb.AppendLine($"{matchingTemplate.Name}={(hideHair ? "" : "!")}HideHair,{(hideUpper ? "" : "!")}HideUpperFaceProps,{(hideLower ? "" : "!")}HideLowerFaceProps,{(hideFacialHair ? "" : "!")}HideFacialHair");
            }

            File.WriteAllText(outputFilePath, sb.ToString());
        }
    }
}