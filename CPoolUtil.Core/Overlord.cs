using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CPoolUtil.Core
{
    public class Overlord
    {
        private List<Template> _templates = new List<Template>();
        private List<Template> _choices = new List<Template>();
        private List<Palette> _palettes = new List<Palette>();
        private List<HelmetInfo> _helmetInfos = new List<HelmetInfo>();
        private List<ArmInfo> _armInfos = new List<ArmInfo>();

        public CPool CharacterPool { get; private set; }
        public IReadOnlyList<Template> Choices => _choices;
        public IReadOnlyList<Palette> Palettes => _palettes;
        public IReadOnlyList<Template> Templates => _templates;
        public IEnumerable<Template> FilteredTemplates => _templates.Where(t => t.Origin == "Vanilla" || DlcAndModOptions.Contains(t.Origin));
        public IReadOnlyList<HelmetInfo> HelmetInfos => _helmetInfos;
        public IReadOnlyList<ArmInfo> ArmInfos => _armInfos;
        public List<string> DlcAndModOptions { get; set; } = new List<string>();

        public void AppendPool(CPool newPool)
        {
            if (CharacterPool == null)
                CharacterPool = newPool;
            else
                CharacterPool.AppendCharacters(newPool.Characters.ToArray());
        }

        public void LoadChoicesTemplates()
        {
            if (_choices.Count > 0) return;

            var choicesTemplate = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Persistent Choices.txt"));
            var choicesLoc = GetLocalizationFile(choicesTemplate.Name);
            foreach (var line in File.ReadAllLines(choicesTemplate.FullName)) _choices.Add(Template.Parse(line, choicesLoc, "Vanilla"));
        }

        public void LoadOrReloadPalettes()
        {
            // Only read unchanging palettes once
            if (_palettes.Count == 0)
            {
                var caucasianSkins = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Palettes/CaucasianSkins.txt")).FullName);
                var africanSkins = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Palettes/AfricanSkins.txt")).FullName);
                var asianSkins = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Palettes/AsianSkins.txt")).FullName);
                var hispanicSkins = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Palettes/HispanicSkins.txt")).FullName);
                var eyeColors = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Palettes/EyeColors.txt")).FullName);

                // Load the skin colors in the same order as race indexes (Caucasian, African, Asian, Hispanic) so the paletteType
                for (int i = 0; i < caucasianSkins.Length; i++) _palettes.Add(Palette.Parse(caucasianSkins[i], i, Palette.ePaletteType.SkinColor, 0));
                for (int i = 0; i < africanSkins.Length; i++) _palettes.Add(Palette.Parse(africanSkins[i], i, Palette.ePaletteType.SkinColor, 1));
                for (int i = 0; i < asianSkins.Length; i++) _palettes.Add(Palette.Parse(asianSkins[i], i, Palette.ePaletteType.SkinColor, 2));
                for (int i = 0; i < hispanicSkins.Length; i++) _palettes.Add(Palette.Parse(hispanicSkins[i], i, Palette.ePaletteType.SkinColor, 3));
                for (int i = 0; i < eyeColors.Length; i++) _palettes.Add(Palette.Parse(eyeColors[i], i, Palette.ePaletteType.EyeColor));
            }

            // Always reload overridable palettes based on which mods are enabled
            _palettes.RemoveAll(p => p.PaletteType == Palette.ePaletteType.HairColor || p.PaletteType == Palette.ePaletteType.ArmorColor);
            bool moreHair = DlcAndModOptions.Contains("More Hair Colors");
            bool moreArmor = DlcAndModOptions.Contains("More Armor Colors");

            var hairColors = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, $"Files/Palettes/{(moreHair ? "More" : "")}HairColors.txt")).FullName);
            var armorColors = File.ReadAllLines(new FileInfo(Path.Combine(Environment.CurrentDirectory, $"Files/Palettes/{(moreArmor ? "More" : "")}ArmorColors.txt")).FullName);

            for (int i = 0; i < hairColors.Length; i++) _palettes.Add(Palette.Parse(hairColors[i], i, Palette.ePaletteType.HairColor));
            for (int i = 0; i < armorColors.Length; i++) _palettes.Add(Palette.Parse(armorColors[i], i, Palette.ePaletteType.ArmorColor));
        }

        public void LoadCustomizationTemplates()
        {
            if (_templates.Count > 0) return;

            // Template files
            var vanillaTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Vanilla.txt"));
            var wotcTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/WotC.txt"));
            var tleTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Tactical Legacy Pack.txt"));
            var anarchyTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Anarchy's Children.txt"));
            var alienHuntersTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Alien Hunters.txt"));
            var shensTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Shens Last Gift.txt"));
            var resistanceTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Resistance Warrior Pack.txt"));
            var capnBubsTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/CapnBubs.txt"));
            var femaleHairTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Female Hair Pack.txt"));
            var maleHairTemplates = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Files/Templates/Male Hair Pack.txt"));

            // Localization files
            var vanillaLoc = GetLocalizationFile(vanillaTemplates.Name);
            var wotcLoc = GetLocalizationFile(wotcTemplates.Name);
            var tleLoc = GetLocalizationFile(tleTemplates.Name);
            var anarchyLoc = GetLocalizationFile(anarchyTemplates.Name);
            var alientHuntersLoc = GetLocalizationFile(alienHuntersTemplates.Name);
            var shensLoc = GetLocalizationFile(shensTemplates.Name);
            var resistanceLoc = GetLocalizationFile(resistanceTemplates.Name);
            var capnBubsLoc = GetLocalizationFile(capnBubsTemplates.Name);
            var femaleHairLoc = GetLocalizationFile(femaleHairTemplates.Name);
            var maleHairLoc = GetLocalizationFile(maleHairTemplates.Name);

            // Template list parsing. Begin with a "None" template
            _templates.Add(Template.CreateNone(vanillaLoc["Pat_Nothing"]));
            foreach (var line in File.ReadAllLines(vanillaTemplates.FullName)) _templates.Add(Template.Parse(line, vanillaLoc, "Vanilla"));
            foreach (var line in File.ReadAllLines(wotcTemplates.FullName)) _templates.Add(Template.Parse(line, wotcLoc, "WotC"));
            foreach (var line in File.ReadAllLines(tleTemplates.FullName)) _templates.Add(Template.Parse(line, tleLoc, "Tactical Legacy Pack"));
            foreach (var line in File.ReadAllLines(anarchyTemplates.FullName)) _templates.Add(Template.Parse(line, anarchyLoc, "Anarchy's Children"));
            foreach (var line in File.ReadAllLines(alienHuntersTemplates.FullName)) _templates.Add(Template.Parse(line, alientHuntersLoc, "Alien Hunters"));
            foreach (var line in File.ReadAllLines(shensTemplates.FullName)) _templates.Add(Template.Parse(line, shensLoc, "Shen's Last Gift"));
            foreach (var line in File.ReadAllLines(resistanceTemplates.FullName)) _templates.Add(Template.Parse(line, resistanceLoc, "Resistance Warrior Pack"));
            foreach (var line in File.ReadAllLines(capnBubsTemplates.FullName)) _templates.Add(Template.Parse(line, capnBubsLoc, "CapnBubs Accessories"));
            foreach (var line in File.ReadAllLines(femaleHairTemplates.FullName)) _templates.Add(Template.Parse(line, femaleHairLoc, "Female Hair Pack"));
            foreach (var line in File.ReadAllLines(maleHairTemplates.FullName)) _templates.Add(Template.Parse(line, maleHairLoc, "Male Hair Pack"));

            // Always load helmet/arm information if this function was called
            var helmetInfoLines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Files/Helmet Info.txt"));
            var armInfoLines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Files/Arm Info.txt"));
            foreach (var line in helmetInfoLines) _helmetInfos.Add(HelmetInfo.Parse(line));
            foreach (var line in armInfoLines) _armInfos.Add(ArmInfo.Parse(line));

            // Then get localization info for things like arms and torso selection (To display Arms 0 - Arms 6 or whatever, instead of just the index)
            var partsLoc = GetLocalizationFile("Part Types");
            foreach (var partLoc in partsLoc)
                _templates.Where(t => t.PartType == partLoc.Key).ToList().ForEach(t => t.PartTypeLocalized = partLoc.Value);
        }

        private Dictionary<string, string> GetLocalizationFile(string fileName)
        {
            string fileNoExt = Path.GetFileNameWithoutExtension(fileName);
            
            // See if our current culture (or its parent) exists as a localization file in the same directory. Fallback to English
            var currentLanguage = CultureInfo.CurrentCulture;
            string locFileName = Path.Combine(Environment.CurrentDirectory, "Files/Localization", $"{fileNoExt}_{currentLanguage.Name}.txt");
            if (!File.Exists(locFileName) && !string.IsNullOrWhiteSpace(currentLanguage.Parent?.Name))
                locFileName = Path.Combine(Environment.CurrentDirectory, "Files/Localization", $"{fileNoExt}_{currentLanguage.Parent.Name}.txt");
            if (!File.Exists(locFileName))
                locFileName = Path.Combine(Environment.CurrentDirectory, "Files/Localization", $"{fileNoExt}_en.txt");

            return File.Exists(locFileName) ? File.ReadAllLines(locFileName).ToDictionary(l => l.Split('=')[0], l => l.Split('=')[1]) : new Dictionary<string, string>();
        }
    }
}