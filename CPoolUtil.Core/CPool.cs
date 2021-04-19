using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CPoolUtil.Core
{
    public class CPool : PropertyBag
    {
        private List<Character> _characters = new List<Character>();
        private List<Character> _unchangedCharacterList = new List<Character>(); // Allows resets without reloading the entire pool

        public ArrayProperty CharacterPoolProp => Properties.FirstOrDefault(p => p.Name == "CharacterPool") as ArrayProperty; // Won't exist if file has no characters
        public int NumCharacters => CharacterPoolProp?.DataVal ?? 0;
        public StrProperty PoolFileName => Properties.FirstOrDefault(p => p.Name == "PoolFileName") as StrProperty;
        public IReadOnlyList<Character> Characters => _characters;
        public bool IsDirty { get; set; }

        public event Action<int> SendProgressUpdateEvent;
        public event Action<List<Character>> DuplicateCharactersIgnoredEvent;

        private CPool(IOutputter outputter) : this(null, outputter)
        {
            properties.Add(ArrayProperty.Create("CharacterPool", 0));
            properties.Add(StrProperty.Create("PoolFileName", "Unsaved Pool"));
            IsDirty = true;
        }

        public CPool(Parser parser, IOutputter outputter) : base(parser, outputter) { }

        public static CPool Create(IOutputter outputter)
        {
            return new CPool(outputter);
        }

        public void Load()
        {
            ReadHeader();
            Outputter.WriteLine();
            WriteDebug(0);

            if (_characters.Count > 0) return;

            for (int i = 0; i < NumCharacters; i++)
            {
                Outputter.WriteLine();
                Outputter.WriteLine($"Character {i + 1}");
                var newChar = new Character(_parser, Outputter);
                _characters.Add(newChar);
                _unchangedCharacterList.Add(newChar.Clone());

                // Send progress update event
                int progress = (int)Math.Round(((i + 1f) / NumCharacters) * 100);
                SendProgressUpdateEvent?.Invoke(progress);
            }
        }

        public void Save(string filePath)
        {
            Outputter.WriteLine($"Saving character pool to {filePath}...");

            var fileBytes = WriteHeader().AsEnumerable();
            foreach (var character in Characters)
            {
                Outputter.WriteLine();
                Outputter.WriteLine($"Writing {character.FullName}...");
                fileBytes = fileBytes.Concat(character.WriteProperties());
            }

            File.WriteAllBytes(filePath, fileBytes.ToArray());
        }

        private void ReadHeader()
        {
            // Header always consists of 4 0xFF bytes
            var header = _parser.GetBytes(4);
            if (!Enumerable.SequenceEqual(header, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }))
                throw new Exception("Invalid file header.");

            // Get header properties (CharacterPool [unless empty] and PoolFileName)
            // NOTE: Occasionally the header has a "CharacterPoolSerializeHelper" StructProperty containing 1(?) character
            // This may cause problems if there is ever more than 1, but for now it looks like we can simply throw it away (the character also shows up in the list)
            GetProperties();

            // Read one more int and compare it against the number of characters
            int validatedCharacterCount = _parser.GetInt();

            if (validatedCharacterCount != NumCharacters)
                throw new Exception("Character count mismatch found in header.");
        }

        private byte[] WriteHeader()
        {
            // Write 4 0xFF bytes to begin
            var bytes = new List<byte>() { 0xFF, 0xFF, 0xFF, 0xFF };

            // Write CharacterPool and PoolFileName (and "None" finalizer) properties only
            bytes.AddRange(Parser.WriteProperty(CharacterPoolProp));
            bytes.AddRange(Parser.WriteProperty(PoolFileName));
            bytes.AddRange(Parser.WriteProperty(null));

            // Write number of characters again as a standalone int
            bytes.AddRange(Parser.WriteInt(NumCharacters));

            return bytes.ToArray();
        }

        public void AppendCharacters(params Character[] characters)
        {
            if (characters == null)
                return;

            var duplicates = new List<Character>();
            foreach (var newCharacter in characters)
            {
                // Don't append duplicates
                if (Characters.Any(c => c.IsDuplicate(newCharacter)))
                    duplicates.Add(newCharacter);
                else
                {
                    _characters.Add(newCharacter);
                    _unchangedCharacterList.Add(newCharacter.Clone());
                    IsDirty = true;
                }
            }

            if (duplicates.Any())
                DuplicateCharactersIgnoredEvent?.Invoke(duplicates);

            UpdateNumCharactersProperty();
        }

        public void RemoveCharacters(params Character[] characters)
        {
            if (characters == null)
                return;

            foreach (var character in characters)
            {
                _characters.Remove(character);
                _unchangedCharacterList.Remove(_unchangedCharacterList.First(uc => uc.ID == character.ID));
            }

            IsDirty = true;
            UpdateNumCharactersProperty();
        }

        public void ResetCharacters(params Character[] characters)
        {
            if (characters == null)
                return;

            foreach (var character in characters)
            {
                // Clone from the clone to keep them separated and reload original property values. Don't reset "new" characters
                var originalCharacter = _unchangedCharacterList.FirstOrDefault(uc => uc.ID == character.ID).Clone();
                if (!originalCharacter.IsDirty)
                {
                    _characters.Remove(character);
                    _characters.Add(originalCharacter);
                }
            }
        }

        private void UpdateNumCharactersProperty()
        {
            CharacterPoolProp.DataVal = _characters.Count;
        }
    }
}