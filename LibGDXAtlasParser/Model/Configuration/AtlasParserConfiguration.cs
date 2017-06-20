using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model.Configuration
{
    /*
        <summary>
            AtlasParserConfiguration stores configuration data about the Atlas Parser
        </summary>
    */
    public class AtlasParserConfiguration
    {
        #region Non-public Members
        private string _sectionIndentation;
        private char _keyValueSeparator;
        private char _multiValueSeparator;
        private string _newLineChar;
        private List<char> _listParseChars;
        private Dictionary<char, bool> _validFileSpecialCharDict;

        public readonly char[] ValidFileSpecialCharacters = { '!', '@', '#', '$', '%', '^', '&', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', ';', '\'', ',', '`', '~' };

        public readonly char WinDirectorySeparator = '/';
        #endregion

        #region Initialization
        /*
            <summary>
                AtlasParserConfiguration constructor
            </summary>
        */
        public AtlasParserConfiguration()
        {
            _listParseChars = new List<char>();
            SectionIndentation = "  ";
            KeyValueSeparator = ':';
            MultiValueSeparator = ',';
            NewLineChar = "\n";
            _validFileSpecialCharDict = ValidFileSpecialCharacters.ToDictionary(x => x, x=> true);
        }
        #endregion

        #region Fields
        /*
            <summary>
                string of the Section indentation
            </summary>
        */
        public string SectionIndentation
        {
            get
            {
                return _sectionIndentation;
            }
            set
            {
                _sectionIndentation = value;
            }
        }

        /*
            <summary>
                character Key value separator
            </summary>
        */
        public char KeyValueSeparator
        {
            get
            {
                return _keyValueSeparator;
            }
            set
            {
                if (!_listParseChars.Contains(value))
                {
                    _listParseChars.Add(value);
                    _keyValueSeparator = value;
                }
            }
        }

        /*
            <summary>
                character Multi value separator
            </summary>
        */
        public char MultiValueSeparator
        {
            get
            {
                return _multiValueSeparator;
            }
            set
            {
                if (!_listParseChars.Contains(value))
                {
                    _listParseChars.Add(value);
                    _multiValueSeparator = value;
                }
            }
        }

        /*
            <summary>
                character new line
            </summary>
        */
        public string NewLineChar
        {
            get
            {
                return _newLineChar;
            }
            set
            {
                _newLineChar = value;
            }
        }

        /*
            <summary>
                Gets the valid file special character dictionary<char, bool>.
                Check the dictionary contains.
            </summary>
        */
        public Dictionary<char, bool> ValidFileSpecialCharDict
        {
            get
            {
                return _validFileSpecialCharDict;
            }
        }
        #endregion

        #region Method
        /*
            <summary>
                bool is process character a control character
            </summary>
        */
        public bool IsControlCharacter(char processChar)
        {
            return _listParseChars.Contains(processChar);
        }

        

        /*
            <summary>
                Is the isValid a valid section character.
            </summary>
        */
        public bool IsValidSectionChar(char isValid)
        {
            return ValidFileSpecialCharDict.ContainsKey(isValid) || WinDirectorySeparator.Equals(isValid);
        }

        /*
            <summary>
                Is the isValid a valid Token character
            </summary>
        */
        public bool IsValidToken(char isValid)
        {
            return IsValidSectionChar(isValid) || char.IsLetterOrDigit(isValid) || char.IsWhiteSpace(isValid) || char.IsPunctuation(isValid);
        }
        #endregion
    }
}
