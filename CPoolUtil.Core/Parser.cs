using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CPoolUtil.Core
{
    public sealed class Parser
    {
        private IOutputter _outputter;
        private byte[] _rawBytes;
        private int curFilePosition = 0; // Keep track of our own progress each time we read bytes

        public IReadOnlyList<byte> RawBytes => _rawBytes;

        public Parser(IOutputter outputter)
        {
            _outputter = outputter;
        }

        public Parser(string fileLocation, IOutputter outputter) : this(outputter)
        {
            _outputter.Write($"Loading character pool from {fileLocation}... ");
            _rawBytes = File.ReadAllBytes(fileLocation);
            _outputter.WriteLine("Success!");
        }

        #region Read Methods

        public byte[] GetBytes(int length)
        {
            return ReadBytes(length);
        }

        public int GetInt()
        {
            var subArray = ReadBytes(4);

            // If our current environment is not Little Endian, reverse the array before converting
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(subArray);

            return BitConverter.ToInt32(subArray);
        }

        public string GetString(int length)
        {
            // Strings seem to be encoded in Windows 1252 (superset of Western European (ISO))
            var subArray = ReadBytes(length);
            var encoding = CodePagesEncodingProvider.Instance.GetEncoding(1252);
            return encoding.GetString(subArray.Take(subArray.Length - 1).ToArray()).Replace("\r", Environment.NewLine); // Strip out the null terminating byte and encode line feeds
        }

        public CProperty GetProperty()
        {
            // First, read the length and value of the following property name string
            int propNameLength = GetInt();
            string propName = GetString(propNameLength);
            SkipPadding();

            // If this is a "None" ending type property, just return - nothing else will follow this
            if (propName == "None") return null;

            // Get the property type string
            int typeNameLength = GetInt();
            string typeName = GetString(typeNameLength);
            SkipPadding();

            // Skip past the size of the following data and its padding
            GetInt();
            SkipPadding();

            // Finally, read the data based on the property type
            CProperty returnProperty;
            switch (typeName)
            {
                case "ArrayProperty":
                    returnProperty = new ArrayProperty(propName, this);
                    break;

                case "IntProperty":
                    returnProperty = new IntProperty(propName, this);
                    break;

                case "StrProperty":
                    returnProperty = new StrProperty(propName, this);
                    break;

                case "NameProperty":
                    returnProperty = new NameProperty(propName, this);
                    break;

                case "StructProperty":
                    returnProperty = new StructProperty(propName, this, _outputter);
                    break;

                case "BoolProperty":
                    returnProperty = new BoolProperty(propName, this);
                    break;

                default:
                    throw new Exception($"Unexpected property type: {typeName}");
            }

            returnProperty.ParseData();
            return returnProperty;
        }

        private byte[] ReadBytes(int length)
        {
            // Copy a section of our raw bytes to a subarray, then increment out current file position
            byte[] subArray = new byte[length];
            Array.Copy(_rawBytes, curFilePosition, subArray, 0, length);
            curFilePosition += length;

            return subArray;
        }

        public void SkipPadding()
        {
            curFilePosition += 4;
        }

        #endregion

        #region Static Write Methods

        public static byte[] WriteInt(int value)
        {
            var bytes = BitConverter.GetBytes(value);

            // Correct endianness if necessary
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        public static byte[] WriteString(string value)
        {
            if (value == null)
                return new byte[0];

            // Replace CRLF with CR, add a null terminating byte, and encode from Windows 1252
            var formatted = value.Replace(Environment.NewLine, "\r").Append('\0').ToArray();
            return CodePagesEncodingProvider.Instance.GetEncoding(1252).GetBytes(formatted);
        }

        public static byte[] WriteProperty(CProperty property)
        {
            var bytes = new List<byte>();

            // Write length and name of property
            var formattedName = WriteString(property?.Name ?? "None");
            bytes.AddRange(WriteInt(formattedName.Length));
            bytes.AddRange(formattedName);
            bytes.AddRange(WritePadding());

            // If this was a "None" property, nothing else is needed
            if (property == null) return bytes.ToArray();

            // Write property type string
            formattedName = WriteString(property.GetType().Name);
            bytes.AddRange(WriteInt(formattedName.Length));
            bytes.AddRange(formattedName);
            bytes.AddRange(WritePadding());

            // Size of data and data itself will be written by the derived types
            bytes.AddRange(property.WriteSizeAndData());

            return bytes.ToArray();
        }

        public static byte[] WritePadding()
        {
            return new byte[4];
        }

        #endregion
    }
}
