using System;
using System.Collections.Generic;
using System.Linq;

namespace CPoolUtil.Core
{
    public class Character : PropertyBag
    {
        public Guid ID { get; private set; } // For easier lookup
        public StrProperty FirstName => Properties.FirstOrDefault(p => p.Name == "strFirstName") as StrProperty;
        public StrProperty LastName => Properties.FirstOrDefault(p => p.Name == "strLastName") as StrProperty;
        public StrProperty NickName => Properties.FirstOrDefault(p => p.Name == "strNickName") as StrProperty;
        public string FullName => $"{FirstName?.Data} {NickName?.Data} {LastName?.Data}".Trim().Replace("  ", " ") + (IsDirty ? "*" : "");
        public NameProperty SoldierType => Properties.FirstOrDefault(p => p.Name == "CharacterTemplateName") as NameProperty;
        public NameProperty PreferredClass => Properties.FirstOrDefault(p => p.Name == "m_SoldierClassTemplateName") as NameProperty;
        public NameProperty Country => Properties.FirstOrDefault(p => p.Name == "Country") as NameProperty;
        private StructProperty _appearance => Properties.FirstOrDefault(p => p.Name == "kAppearance") as StructProperty;
        public Appearance Appearance { get; private set; }
        public BoolProperty CanBeSoldier => Properties.FirstOrDefault(p => p.Name == "AllowedTypeSoldier") as BoolProperty;
        public BoolProperty CanBeVIP => Properties.FirstOrDefault(p => p.Name == "AllowedTypeVIP") as BoolProperty;
        public BoolProperty CanBeDarkVIP => Properties.FirstOrDefault(p => p.Name == "AllowedTypeDarkVIP") as BoolProperty;
        public StrProperty CreatedOn => Properties.FirstOrDefault(p => p.Name == "PoolTimestamp") as StrProperty;
        public StrProperty Biography => Properties.FirstOrDefault(p => p.Name == "BackgroundText") as StrProperty;

        public bool IsDirty { get; set; }

        private Character(IOutputter outputter, string nickname) : base(null, outputter)
        {
            properties.Add(StrProperty.Create("strFirstName", "New"));
            properties.Add(StrProperty.Create("strLastName", "Character"));
            properties.Add(StrProperty.Create("strNickName"));
            properties.Add(NameProperty.Create("CharacterTemplateName", "Soldier"));
            properties.Add(NameProperty.Create("m_SoldierClassTemplateName", "Rookie"));
            properties.Add(NameProperty.Create("Country", "Country_USA"));
            var appearance = Appearance.CreateAppearanceProperty();
            properties.Add(appearance);
            Appearance = new Appearance(outputter, appearance.Properties.Properties);
            properties.Add(BoolProperty.Create("AllowedTypeSoldier", true));
            properties.Add(BoolProperty.Create("AllowedTypeVIP"));
            properties.Add(BoolProperty.Create("AllowedTypeDarkVIP"));
            properties.Add(StrProperty.Create("PoolTimestamp", DateTime.Now.ToString("MMMM d, yyyy - h:m tt")));
            properties.Add(StrProperty.Create("BackgroundText"));
            NickName.Data = $"'{nickname}'";
            IsDirty = true;
        }

        // Private constructor used for cloning
        private Character(IOutputter outputter, Character fromCharacter) : base(null, outputter)
        {
            ID = fromCharacter.ID;
            properties = fromCharacter.Properties.Select(p => p.Clone()).ToList();
            Appearance = new Appearance(outputter, _appearance.Properties.Properties);
            IsDirty = fromCharacter.IsDirty;
        }

        public Character(Parser parser, IOutputter outputter) : base(parser, outputter)
        {
            ID = Guid.NewGuid();
            GetProperties();
            Appearance = new Appearance(outputter, _appearance.Properties.Properties);
            WriteDebug(1);
        }

        public bool IsDuplicate(Character otherCharacter)
        {
            // Return false as soon as any discrepency is detected
            if (otherCharacter.Properties.Count != Properties.Count)
                return false;

            foreach (var ourProp in Properties)
                if (otherCharacter.Properties.FirstOrDefault(p => p.Name == ourProp.Name)?.Data != ourProp.Data)
                    return false;

            return true;
        }

        public static Character Create(IOutputter outputter, string nickName = "")
        {
            return new Character(outputter, nickName);
        }

        public new Character Clone()
        {
            return new Character(Outputter, this);
        }
    }

    public class Appearance : PropertyBag
    {
        #region Properties
        public NameProperty Head => properties.FirstOrDefault(p => p.Name == "nmHead") as NameProperty;
        public IntProperty Gender => properties.FirstOrDefault(p => p.Name == "iGender") as IntProperty;
        public IntProperty Race => properties.FirstOrDefault(p => p.Name == "iRace") as IntProperty;
        public NameProperty Haircut => properties.FirstOrDefault(p => p.Name == "nmHaircut") as NameProperty;
        public IntProperty HairColor => properties.FirstOrDefault(p => p.Name == "iHairColor") as IntProperty;
        public IntProperty FacialHair => properties.FirstOrDefault(p => p.Name == "iFacialHair") as IntProperty;
        public NameProperty Beard => properties.FirstOrDefault(p => p.Name == "nmBeard") as NameProperty;
        public IntProperty SkinColor => properties.FirstOrDefault(p => p.Name == "iSkinColor") as IntProperty;
        public IntProperty EyeColor => properties.FirstOrDefault(p => p.Name == "iEyeColor") as IntProperty;
        public NameProperty Flag => properties.FirstOrDefault(p => p.Name == "nmFlag") as NameProperty;
        public IntProperty iVoice => properties.FirstOrDefault(p => p.Name == "iVoice") as IntProperty;
        public IntProperty Attitude => properties.FirstOrDefault(p => p.Name == "iAttitude") as IntProperty;
        public IntProperty ArmorDeco => properties.FirstOrDefault(p => p.Name == "iArmorDeco") as IntProperty;
        public IntProperty ArmorTint1 => properties.FirstOrDefault(p => p.Name == "iArmorTint") as IntProperty;
        public IntProperty ArmorTint2 => properties.FirstOrDefault(p => p.Name == "iArmorTintSecondary") as IntProperty;
        public IntProperty WeaponTint => properties.FirstOrDefault(p => p.Name == "iWeaponTint") as IntProperty;
        public IntProperty TattooTint => properties.FirstOrDefault(p => p.Name == "iTattooTint") as IntProperty;
        public NameProperty WeaponPattern => properties.FirstOrDefault(p => p.Name == "nmWeaponPattern") as NameProperty;
        public NameProperty Pawn => properties.FirstOrDefault(p => p.Name == "nmPawn") as NameProperty;
        public NameProperty Torso => properties.FirstOrDefault(p => p.Name == "nmTorso") as NameProperty;
        public NameProperty Arms => properties.FirstOrDefault(p => p.Name == "nmArms") as NameProperty;
        public NameProperty Legs => properties.FirstOrDefault(p => p.Name == "nmLegs") as NameProperty;
        public NameProperty Helmet => properties.FirstOrDefault(p => p.Name == "nmHelmet") as NameProperty;
        public NameProperty Eye => properties.FirstOrDefault(p => p.Name == "nmEye") as NameProperty;
        public NameProperty Teeth => properties.FirstOrDefault(p => p.Name == "nmTeeth") as NameProperty;
        public NameProperty FacePropLower => properties.FirstOrDefault(p => p.Name == "nmFacePropLower") as NameProperty;
        public NameProperty FacePropUpper => properties.FirstOrDefault(p => p.Name == "nmFacePropUpper") as NameProperty;
        public NameProperty ArmorPatterns => properties.FirstOrDefault(p => p.Name == "nmPatterns") as NameProperty;
        public NameProperty Voice => properties.FirstOrDefault(p => p.Name == "nmVoice") as NameProperty;
        public NameProperty Language => properties.FirstOrDefault(p => p.Name == "nmLanguage") as NameProperty;
        public NameProperty Tattoo_LeftArm => properties.FirstOrDefault(p => p.Name == "nmTattoo_LeftArm") as NameProperty;
        public NameProperty Tattoo_RightArm => properties.FirstOrDefault(p => p.Name == "nmTattoo_RightArm") as NameProperty;
        public NameProperty Scars => properties.FirstOrDefault(p => p.Name == "nmScars") as NameProperty;
        public NameProperty TorsoUnderlay => properties.FirstOrDefault(p => p.Name == "nmTorso_Underlay") as NameProperty;
        public NameProperty ArmsUnderlay => properties.FirstOrDefault(p => p.Name == "nmArms_Underlay") as NameProperty;
        public NameProperty LegsUnderlay => properties.FirstOrDefault(p => p.Name == "nmLegs_Underlay") as NameProperty;
        public NameProperty FacePaint => properties.FirstOrDefault(p => p.Name == "nmFacePaint") as NameProperty;
        public NameProperty LeftArm => properties.FirstOrDefault(p => p.Name == "nmLeftArm") as NameProperty;
        public NameProperty RightArm => properties.FirstOrDefault(p => p.Name == "nmRightArm") as NameProperty;
        public NameProperty LeftArmDeco => properties.FirstOrDefault(p => p.Name == "nmLeftArmDeco") as NameProperty;
        public NameProperty RightArmDeco => properties.FirstOrDefault(p => p.Name == "nmRightArmDeco") as NameProperty;
        public NameProperty LeftForearm => properties.FirstOrDefault(p => p.Name == "nmLeftForearm") as NameProperty;
        public NameProperty RightForearm => properties.FirstOrDefault(p => p.Name == "nmRightForearm") as NameProperty;
        public NameProperty Thighs => properties.FirstOrDefault(p => p.Name == "nmThighs") as NameProperty;
        public NameProperty Shins => properties.FirstOrDefault(p => p.Name == "nmShins") as NameProperty;
        public NameProperty TorsoDeco => properties.FirstOrDefault(p => p.Name == "nmTorsoDeco") as NameProperty;
        public BoolProperty GhostPawn => properties.FirstOrDefault(p => p.Name == "bGhostPawn") as BoolProperty;
        #endregion

        public Appearance(IOutputter outputter, IReadOnlyList<CProperty> appearanceProps) : base(null, outputter)
        {
            properties = appearanceProps.ToList();

            // Create any properties that aren't guaranteed to exist (some DLCs added more properties)
            if (TorsoDeco == null) properties.Add(NameProperty.Create("nmTorsoDeco"));
            if (LeftForearm == null) properties.Add(NameProperty.Create("nmLeftForearm"));
            if (RightForearm == null) properties.Add(NameProperty.Create("nmRightForearm"));
            if (LeftArmDeco == null) properties.Add(NameProperty.Create("nmLeftArmDeco"));
            if (RightArmDeco == null) properties.Add(NameProperty.Create("nmRightArmDeco"));
            if (Thighs == null) properties.Add(NameProperty.Create("nmThighs"));
        }

        public static StructProperty CreateAppearanceProperty()
        {
            var props = new CProperty[]
            {
                NameProperty.Create("nmHead"),
                IntProperty.Create("iGender", 1),
                IntProperty.Create("iRace"),
                NameProperty.Create("nmHaircut"),
                IntProperty.Create("iHairColor"),
                IntProperty.Create("iFacialHair"),
                NameProperty.Create("nmBeard"),
                IntProperty.Create("iSkinColor"),
                IntProperty.Create("iEyeColor"),
                NameProperty.Create("nmFlag"),
                IntProperty.Create("iVoice"),
                IntProperty.Create("iAttitude"),
                IntProperty.Create("iArmorDeco"),
                IntProperty.Create("iArmorTint"),
                IntProperty.Create("iArmorTintSecondary"),
                IntProperty.Create("iWeaponTint"),
                IntProperty.Create("iTattooTint"),
                NameProperty.Create("nmWeaponPattern"),
                NameProperty.Create("nmPawn", "None"),
                NameProperty.Create("nmTorso"),
                NameProperty.Create("nmArms"),
                NameProperty.Create("nmLegs"),
                NameProperty.Create("nmHelmet"),
                NameProperty.Create("nmEye", "DefaultEyes"),
                NameProperty.Create("nmTeeth", "DefaultTeeth"),
                NameProperty.Create("nmFacePropLower"),
                NameProperty.Create("nmFacePropUpper"),
                NameProperty.Create("nmPatterns"),
                NameProperty.Create("nmVoice"),
                NameProperty.Create("nmLanguage", "None"),
                NameProperty.Create("nmTattoo_LeftArm"),
                NameProperty.Create("nmTattoo_RightArm"),
                NameProperty.Create("nmScars"),
                NameProperty.Create("nmTorso_Underlay"),
                NameProperty.Create("nmArms_Underlay"),
                NameProperty.Create("nmLegs_Underlay"),
                NameProperty.Create("nmFacePaint"),
                NameProperty.Create("nmLeftArm"),
                NameProperty.Create("nmRightArm"),
                NameProperty.Create("nmLeftArmDeco"),
                NameProperty.Create("nmRightArmDeco"),
                NameProperty.Create("nmLeftForearm"),
                NameProperty.Create("nmRightForearm"),
                NameProperty.Create("nmThighs"),
                NameProperty.Create("nmShins", "None"),
                NameProperty.Create("nmTorsoDeco")
            };
            var structProp = StructProperty.Create("kAppearance", props);

            return structProp;
        }
    }
}