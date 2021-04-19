using System.Collections.Generic;
using System.Linq;

namespace CPoolUtil.Core
{
    public class Template
    {
        public int Index { get; private set; }
        public string PartTypeLocalized;

        public string Name { get; private set; }
        public string Display { get; private set; }
        public string Gender { get; private set; }
        public string Race { get; private set; }
        public string Language { get; private set; }
        public string ArchetypeName { get; private set; }
        public string CharacterTemplate { get; private set; }
        public string ArmorTemplate { get; private set; }
        public string Veteran { get; private set; }
        public bool CanUseOnCivilian { get; private set; }
        public bool SpecializedType { get; private set; }
        public string SetNames { get; private set; }
        public string Tech { get; private set; }
        public string PartType { get; private set; }
        public string Origin { get; private set; }

        private Template() { } // Only allow specific parsing

        public static Template CreateNone(string display)
        {
            return new Template { Name = "None", Display = display, Origin = "Vanilla" };
        }

        public static Template Parse(string line, Dictionary<string, string> localization, string origin)
        {
            var parts = line.Split(',').ToDictionary(l => l.Split('=')[0], l => l.Split('=')[1]);
            string templateName = parts["TemplateName"];
            string partType = parts.ContainsKey("PartType") ? parts["PartType"] : null;

            return new Template
            {
                Name = templateName,
                Display = localization.ContainsKey(templateName) ? localization[templateName] : string.Empty,
                Gender = parts.ContainsKey("Gender") ? parts["Gender"] : null,
                Race = parts.ContainsKey("Race") ? parts["Race"] : null,
                Language = parts.ContainsKey("Language") ? parts["Language"] : null,
                ArchetypeName = parts.ContainsKey("ArchetypeName") ? parts["ArchetypeName"] : null,
                CharacterTemplate = parts.ContainsKey("CharacterTemplate") ? parts["CharacterTemplate"] : null,
                ArmorTemplate = parts.ContainsKey("ArmorTemplate") ? parts["ArmorTemplate"] : null,
                Veteran = parts.ContainsKey("bVeteran") ? parts["bVeteran"] : null,
                CanUseOnCivilian = parts.ContainsKey("bCanUseOnCivilian") ? parts["bCanUseOnCivilian"].ToUpper().Contains("TRUE") : false,
                SpecializedType = parts.ContainsKey("SpecializedType") ? parts["SpecializedType"].ToUpper().Contains("TRUE") : false,
                SetNames = parts.ContainsKey("SetNames") ? parts["SetNames"] : null,
                Tech = parts.ContainsKey("Tech") ? parts["Tech"] : null,
                PartType = partType,
                Origin = origin
            };
        }

        public static List<Template> GetIndexes(List<Template> templateList)
        {
            int curIndex = 0;

            if (templateList != null)
                foreach (var template in templateList)
                {
                    template.Index = curIndex++;

                    // Populate the display with the simple index if it has none
                    if (string.IsNullOrWhiteSpace(template.Display))
                        template.Display = $"{template.PartTypeLocalized} {template.Index}";
                }

            return templateList;
        }

        public bool IsGender(string partialGenderString, bool ignoreNulls = false)
        {
            return (ignoreNulls && string.IsNullOrWhiteSpace(Gender)) || (Gender?.Contains(partialGenderString) ?? false);
        }

        public bool IsRace(string partialRaceString, bool ignoreNulls = false)
        {
            return (ignoreNulls && string.IsNullOrWhiteSpace(Race)) || (Race?.Contains(partialRaceString) ?? false);
        }
    }
}