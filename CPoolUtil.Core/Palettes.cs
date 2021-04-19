using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace CPoolUtil.Core
{
    public class Palette
    {
        public enum ePaletteType { SkinColor, EyeColor, HairColor, ArmorColor }

        public int Index { get; private set; }
        public Color Primary { get; private set; }
        public Color Secondary { get; private set; }
        public ePaletteType PaletteType { get; private set; }
        public int PaletteSubType { get; private set; } // For example, there are 4 subtypes of skinColor

        public static Palette Parse(string line, int index, ePaletteType paletteType, int subType = -1)
        {
            var newPalette = new Palette
            {
                Index = index,
                PaletteType = paletteType,
                PaletteSubType = subType
            };

            // Capture both primary and secondary definitions if they exist
            var colorTypes = Regex.Matches(line, @"(Primary|Secondary)=\(((\w=[-\d\.]+),?)+\)");

            foreach (Match colorType in colorTypes)
            {
                // The fourth group is key, and should contain a capture for each R=X, G=X, etc. If we found none of those, skip to the next definition
                if (colorType.Groups.Count < 4) break;

                // Build a dictionary of Letter = Value
                var valueStringsDict = colorType.Groups[3].Captures.Select(c => new { k = c.Value.Split('=')[0].ToUpper(), v = c.Value.Split('=')[1] }).ToDictionary(c => c.k, c => c.v);

                // Build a color based on what we found. Since input values are decimals ranging from 0-1, convert them to 0-255
                float.TryParse(valueStringsDict.ContainsKey("R") ? valueStringsDict["R"] : "1", out var rFloat);
                float.TryParse(valueStringsDict.ContainsKey("G") ? valueStringsDict["G"] : "1", out var gFloat);
                float.TryParse(valueStringsDict.ContainsKey("B") ? valueStringsDict["B"] : "1", out var bFloat);
                float.TryParse(valueStringsDict.ContainsKey("A") ? valueStringsDict["A"] : "1", out var aFloat);
                int R = (int)Math.Round(255 * Math.Clamp(rFloat, 0, 1));
                int G = (int)Math.Round(255 * Math.Clamp(gFloat, 0, 1));
                int B = (int)Math.Round(255 * Math.Clamp(bFloat, 0, 1));
                int A = (int)Math.Round(255 * Math.Clamp(aFloat, 0, 1));
                var resultingColor = Color.FromArgb(A, R, G, B);

                if (colorType.Groups[1].Value.Equals("Primary", StringComparison.OrdinalIgnoreCase))
                    newPalette.Primary = resultingColor;
                else
                    newPalette.Secondary = resultingColor;
            }

            return newPalette;
        }
    }
}