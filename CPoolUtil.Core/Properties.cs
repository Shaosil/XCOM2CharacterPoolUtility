using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CPoolUtil.Core
{
    #region Property types

    [DebuggerDisplay("{Name} = {Data}")]
    public abstract class CProperty
    {
        protected Parser Parser { get; set; }

        public string Name { get; protected set; }
        public string FriendlyName { get; private set; }
        public string Data { get; set; }

        public abstract void ParseData();

        public CProperty(string name, Parser parser)
        {
            Parser = parser;
            Name = name;

            // Top level properties
            FriendlyName = Name == "CharacterPool" ? "Character Count"
                : Name == "PoolFileName" ? "File Name"

            // Character properties
                : Name == "strFirstName" ? "First Name"
                : Name == "strLastName" ? "Last Name"
                : Name == "strNickName" ? "Nickname"
                : Name == "m_SoldierClassTemplateName" ? "Soldier Class"
                : Name == "CharacterTemplateName" ? "Character Template"
                : Name == "kAppearance" ? "Appearance"
                : Name == "Country" ? "Country"
                : Name == "AllowedTypeSoldier" ? "Can Be Soldier"
                : Name == "AllowedTypeVIP" ? "Can Be VIP"
                : Name == "AllowedTypeDarkVIP" ? "Can Be Dark VIP"
                : Name == "PoolTimestamp" ? "Created On"
                : Name == "BackgroundText" ? "Biography"

            // Appearance properties
                : Name == "nmHead" ? "Head"
                : Name == "iGender" ? "Gender"
                : Name == "iRace" ? "Race"
                : Name == "nmHaircut" ? "Haircut"
                : Name == "iHairColor" ? "Hair Color"
                : Name == "iFacialHair" ? "Facial Hair"
                : Name == "nmBeard" ? "Beard"
                : Name == "iSkinColor" ? "Skin Color"
                : Name == "iEyeColor" ? "Eye Color"
                : Name == "nmFlag" ? "Flag"
                : Name == "iVoice" ? "Voice"
                : Name == "iAttitude" ? "Attitude"
                : Name == "iArmorDeco" ? "Armor Decor"
                : Name == "iArmorTint" ? "Armor Color"
                : Name == "iArmorTintSecondary" ? "Secondary Armor Color"
                : Name == "iWeaponTint" ? "Weapon Color"
                : Name == "iTattooTint" ? "Tattoo Color"
                : Name == "nmWeaponPattern" ? "Weapon Pattern"
                : Name == "nmPawn" ? "Pawn"
                : Name == "nmTorso" ? "Torso"
                : Name == "nmArms" ? "Arms"
                : Name == "nmLegs" ? "Legs"
                : Name == "nmHelmet" ? "Helmet"
                : Name == "nmEye" ? "Eye"
                : Name == "nmTeeth" ? "Teeth"
                : Name == "nmFacePropLower" ? "Lower Face Prop"
                : Name == "nmFacePropUpper" ? "Upper Face Prop"
                : Name == "nmPatterns" ? "Patterns"
                : Name == "nmVoice" ? "Voice Model"
                : Name == "nmLanguage" ? "Language"
                : Name == "nmTattoo_LeftArm" ? "Left Arm Tattoo"
                : Name == "nmTattoo_RightArm" ? "Right Arm Tattoo"
                : Name == "nmScars" ? "Scars"
                : Name == "nmTorso_Underlay" ? "Torso Underlay"
                : Name == "nmArms_Underlay" ? "Arms Underlay"
                : Name == "nmLegs_Underlay" ? "Legs Underlay"
                : Name == "nmFacePaint" ? "Face Paint"
                : Name == "nmLeftArm" ? "Left Arm"
                : Name == "nmRightArm" ? "Right Arm"
                : Name == "nmLeftArmDeco" ? "Left Arm Decor"
                : Name == "nmRightArmDeco" ? "Right Arm Decor"
                : Name == "nmLeftForearm" ? "Left Forearm"
                : Name == "nmRightForearm" ? "Right Forearm"
                : Name == "nmThighs" ? "Thighs"
                : Name == "nmShins" ? "Shins"
                : Name == "nmTorsoDeco" ? "Torso Decor"
                : Name == "bGhostPawn" ? "Ghost Pawn"

            // Fallback
                : Name + " (Missing Friendly Name)";
        }

        public virtual CProperty Clone()
        {
            var cloned = MemberwiseClone() as CProperty;
            cloned.Name = Name;
            cloned.FriendlyName = FriendlyName;
            cloned.Data = Data;
            return cloned;
        }

        public bool IsDuplicate(CProperty otherProperty)
        {
            return otherProperty.Data == Data;
        }

        public abstract byte[] WriteSizeAndData();
    }

    // Data = Simple 4 byte number
    public class IntProperty : CProperty
    {
        private IntProperty(string name, int newValue) : base(name, null)
        {
            DataVal = newValue;
        }

        public int DataVal
        {
            get => int.Parse(Data);
            set => Data = value.ToString();
        }

        public IntProperty(string name, Parser parser) : base(name, parser) { }

        public override void ParseData()
        {
            Data = Parser.GetInt().ToString(); // We know the size is always 4, so just call Getint()
        }

        public static IntProperty Create(string name, int value = 0)
        {
            return new IntProperty(name, value);
        }

        public override byte[] WriteSizeAndData()
        {
            // Size (4), Padding, Value
            return Parser.WriteInt(4).Concat(Parser.WritePadding()).Concat(Parser.WriteInt(DataVal)).ToArray();
        }
    }

    // Data = Number of elements in an array. Same format as IntProperty
    public class ArrayProperty : IntProperty
    {
        private ArrayProperty(string name, int newValue) : base(name, null)
        {
            DataVal = newValue;
        }

        public ArrayProperty(string name, Parser parser) : base(name, parser) { }

        public static ArrayProperty Create(string name, int value = 0)
        {
            return new ArrayProperty(name, value);
        }
    }

    // Data = ASCII(?) encoded text block
    public class StrProperty : CProperty
    {
        protected int DataLength;

        protected StrProperty(string name, string newValue) : base(name, null)
        {
            Data = newValue;
        }

        public StrProperty(int dataLength, string name, Parser parser) : base(name, parser)
        {
            DataLength = dataLength;
        }

        public override void ParseData()
        {
            // The actual size is the data length - 4, to account for the extra potential size definition
            int actualSize = DataLength - 4;
            Parser.GetInt();
            Data = Parser.GetString(actualSize);
        }

        public static StrProperty Create(string name, string value = null)
        {
            return new StrProperty(name, value);
        }

        public override byte[] WriteSizeAndData()
        {
            var formatted = Parser.WriteString(Data);

            // Size + 4, Padding, Size, Value
            return Parser.WriteInt(formatted.Length + 4).Concat(Parser.WritePadding()).Concat(Parser.WriteInt(formatted.Length)).Concat(formatted).ToArray();
        }
    }

    // Data = Like StrProperty, but with end padding
    public class NameProperty : StrProperty
    {
        private NameProperty(string name, string newValue) : base(name, null)
        {
            Data = newValue;
        }

        public NameProperty(int dataLength, string name, Parser parser) : base(dataLength, name, parser) { }

        public new static NameProperty Create(string name, string value = null)
        {
            return new NameProperty(name, value);
        }

        public override byte[] WriteSizeAndData()
        {
            var formatted = Parser.WriteString(Data);

            // Size + 8, Padding, Size, Value, Padding
            return Parser.WriteInt(formatted.Length + 8).Concat(Parser.WritePadding()).Concat(Parser.WriteInt(formatted.Length)).Concat(formatted).Concat(Parser.WritePadding()).ToArray();
        }
    }

    // Data = 1 or 0
    public class BoolProperty : CProperty
    {
        private BoolProperty(string name, bool newValue) : base(name, null)
        {
            DataVal = newValue;
        }

        public BoolProperty(string name, Parser parser) : base(name, parser) { }

        public bool DataVal
        {
            get => Data == "1";
            set => Data = value ? "1" : "0";
        }

        public override void ParseData()
        {
            // One byte of data
            Data = Parser.GetBytes(1)[0].ToString();
        }

        public static BoolProperty Create(string name, bool value = false)
        {
            return new BoolProperty(name, value);
        }

        public override byte[] WriteSizeAndData()
        {
            // Size (always 0), Padding, Value
            return Parser.WriteInt(0).Concat(Parser.WritePadding()).Concat(BitConverter.GetBytes(DataVal)).ToArray();
        }
    }

    // Data = A subcollection of properties
    public class StructProperty : CProperty
    {
        public PropertyBag Properties { get; private set; }

        private StructProperty(string name, params CProperty[] properties) : base(name, null)
        {
            Properties = PropertyBag.Create(properties);
        }

        public StructProperty(string name, Parser parser, IOutputter outputter) : base(name, parser)
        {
            Properties = new PropertyBag(parser, outputter);
        }

        public override void ParseData()
        {
            // Read the "TAppearance" string and padding
            int headerLength = Parser.GetInt();
            Parser.GetString(headerLength);
            Parser.SkipPadding();

            // Parse the properties of this struct and store them in its own list
            Properties.GetProperties();

            // For comparison purposes, serialize all properties name and value into Data
            Data = string.Join("", Properties.Properties.Select(p => $"{p.Name}{p.Data}"));
        }

        public static StructProperty Create(string name, params CProperty[] properties)
        {
            return new StructProperty(name, properties);
        }

        public override StructProperty Clone()
        {
            var cloned = base.Clone() as StructProperty;
            cloned.Properties = Properties.Clone();
            return cloned;
        }

        public override byte[] WriteSizeAndData()
        {
            // Get the data of all sub properties
            var bytes = Properties.WriteProperties();

            // Size is the combined length of all the properties AFTER the TAppearance string, plus our own ending "None"
            var tAppearanceBytes = Parser.WriteInt(bytes.Count()).Concat(Parser.WritePadding()).Concat(Parser.WriteInt(12)).Concat(Parser.WriteString("TAppearance")).Concat(Parser.WritePadding());

            // Size (combined length of all the properties AFTER the TAppearance string, plus our own ending "None"), Padding, TApperance Length & String, All Property Values, "None"
            return tAppearanceBytes.Concat(bytes).ToArray();
        }
    }

    #endregion

    public class PropertyBag
    {
        protected Parser _parser;
        protected IOutputter Outputter;

        protected List<CProperty> properties = new List<CProperty>();
        public IReadOnlyList<CProperty> Properties => properties;

        private PropertyBag(params CProperty[] pProperties) : this((Parser)null, null)
        {
            properties = pProperties.ToList();
        }

        public PropertyBag(Parser parser, IOutputter outputter)
        {
            _parser = parser;
            Outputter = outputter;
        }

        public static PropertyBag Create(params CProperty[] properties)
        {
            return new PropertyBag(properties);
        }

        public void GetProperties()
        {
            // Keep reading until we hit a "None" property
            CProperty curProp;
            do
            {
                curProp = _parser.GetProperty();
                if (curProp != null) properties.Add(curProp);
            } while (curProp != null);
        }

        public byte[] WriteProperties()
        {
            var bytes = new List<byte>();

            // Write each property, then a "None"
            foreach (var prop in properties)
                bytes.AddRange(Parser.WriteProperty(prop));
            bytes.AddRange(Parser.WriteProperty(null));

            return bytes.ToArray();
        }

        public PropertyBag Clone()
        {
            var cloned = MemberwiseClone() as PropertyBag;
            var oldList = properties;
            var newList = new List<CProperty>();
            foreach (var p in oldList)
                newList.Add(p.Clone());
            cloned.properties = newList;
            return cloned;
        }

        public void WriteDebug(int curTabLevel)
        {
            string tabs = string.Join("", Enumerable.Repeat("\t", curTabLevel));

            foreach (var prop in Properties)
            {
                Outputter.WriteLine($"{tabs}{prop.FriendlyName}: {prop.Data}");

                if (prop is StructProperty sProp)
                    sProp.Properties.WriteDebug(++curTabLevel);
            }
        }
    }
}