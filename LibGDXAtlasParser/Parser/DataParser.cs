using LibGDXAtlasParser.Exceptions;
using LibGDXAtlasParser.Model;
using LibGDXAtlasParser.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Parser
{
    public class DataParser
    {
        private string _curToken;
        private SectionTreeCollection _dataTree;
        private List<SectionTreeCollection> _curSectionDepth;
        private int _indentLevel;
        private int _indentPos;
        private int _charCount;
        private int _lineCount;
        private int _newLinePos;
        private SectionData _curSection;
        private KeyData _curKey;
        private ParseState _curState = ParseState.BEGINLINE;
        private AtlasParserConfiguration _config;
        
        enum ParseState
        {
            BEGINLINE,
            INDENTATIONCONTROL,
            TOKEN,
            VALUES,
            NEWLINE
        }

        public DataParser()
        {
            SectionData root = new SectionData("root", -1);
            _config = new AtlasParserConfiguration();
            _curToken = "";
            _curState = ParseState.BEGINLINE;
            _dataTree = new SectionTreeCollection(root);
            _curSectionDepth = new List<SectionTreeCollection>();
            _curSectionDepth.Add(_dataTree);
            _curSection = null;
            _indentLevel = 0;
            _indentPos = 0;
            _lineCount = 0;
            _charCount = 0;
            _newLinePos = 0;
        }

        public void Parse(string atlasString)
        {
            if (string.IsNullOrEmpty(atlasString))
            {
                return;
            }

            foreach (char curChar in atlasString)
            {
                Console.Write(curChar);
                switch(_curState)
                {
                    case ParseState.BEGINLINE:
                        if(_indentPos < _config.SectionIndentation.Length && curChar == _config.SectionIndentation[_indentPos])
                        {
                            _curState = ParseState.INDENTATIONCONTROL;
                            _indentPos++;
                            _charCount++;

                            if(_indentPos >= _config.SectionIndentation.Length)
                            {
                                _indentPos = 0;
                                _indentLevel++;
                            }
                        }
                        else if(_newLinePos < _config.NewLineChar.Length && curChar == _config.NewLineChar[_newLinePos])
                        {
                            _curState = ParseState.NEWLINE;
                            _newLinePos++;
                            _charCount++;
                            if (_newLinePos >= _config.NewLineChar.Length)
                            {
                                _curState = ParseState.BEGINLINE;
                                _newLinePos = 0;
                                _lineCount++;
                                _indentLevel = 0;
                                _indentPos = 0;
                                _charCount = 0;
                            }
                        }
                        else if(_config.IsValidToken(curChar)) // && !IsControlCharacter(isValid)
                        {
                            _curState = ParseState.TOKEN;
                            _curToken += curChar;
                            _charCount++;
                            Console.WriteLine("TOKEN");
                        }
                        else
                        {
                            //throw Syntax error
                            throw new ParseException("Invalid symbol ["+curChar+"] line: "+_lineCount+" col: "+_charCount);
                        }
                        break;
                    case ParseState.INDENTATIONCONTROL:
                        if (_indentPos < _config.SectionIndentation.Length && curChar == _config.SectionIndentation[_indentPos])
                        {
                            _indentPos++;
                            _charCount++;
                            if (_indentPos >= _config.SectionIndentation.Length)
                            {
                                _indentPos = 0;
                                _indentLevel++;
                            }
                        }
                        else if (_newLinePos < _config.NewLineChar.Length && curChar == _config.NewLineChar[_newLinePos])
                        {
                            _curState = ParseState.NEWLINE;
                            _newLinePos++;
                            _charCount++;
                            if (_newLinePos >= _config.NewLineChar.Length)
                            {
                                _curState = ParseState.BEGINLINE;
                                _newLinePos = 0;
                                _lineCount++;
                                _indentLevel = 0;
                                _indentPos = 0;
                                _charCount = 0;
                            }
                        }
                        else if (_config.IsValidToken(curChar))
                        {
                            _curState = ParseState.TOKEN;
                            _curToken += curChar;
                            _charCount++;
                            Console.WriteLine("TOKEN");
                        }
                        else
                        {
                            //throw Syntax error
                            throw new ParseException("Invalid symbol [" + curChar + "] line: " + _lineCount + " col: " + _charCount);
                        }
                        break;
                    case ParseState.TOKEN:
                        if (_newLinePos < _config.NewLineChar.Length && curChar == _config.NewLineChar[_newLinePos])
                        {
                            _curState = ParseState.NEWLINE;
                            _newLinePos++;
                            _charCount++;

                            Console.WriteLine("NEWLINE");

                            if (_curToken.Trim() != string.Empty)
                            {
                                _curToken += curChar;
                                if (_curSection == null)
                                {
                                    _curSection = new SectionData(_curToken.Trim());
                                    Console.WriteLine("New Section:"+ _curToken.Trim());
                                    _curToken = "";
                                }
                                else
                                {
                                    //Error
                                    throw new ParseException("Invalid token [" + _curToken + "]");
                                }
                            }
                            else
                            {
                                //throw syntax error
                                throw new ParseException("Invalid symbol [" + curChar + "] line: " + _lineCount + " col: " + _charCount);
                            }

                            if (_newLinePos >= _config.NewLineChar.Length)
                            {
                                _curState = ParseState.BEGINLINE;
                                _newLinePos = 0;
                                _lineCount++;
                                _charCount = 0;
                            }
                        }
                        else if(curChar == _config.KeyValueSeparator)
                        {
                            _curState = ParseState.VALUES;
                            string keyName;
                            keyName = _curToken.Trim();

                            Console.WriteLine("VALUE["+curChar+"]");

                            if(keyName != string.Empty)
                            {
                                _charCount++;
                                _curKey = new KeyData(keyName);

                                _curToken = "";
                            }
                            else
                            {
                                //throw syntax error
                                throw new ParseException("Invalid token [" + _curToken + "]");
                            }
                            
                        }
                        else if (_config.IsValidToken(curChar))
                        {
                            _curState = ParseState.TOKEN;
                            _curToken += curChar;
                        }
                        else
                        {
                            //throw Syntax error
                            throw new ParseException("Invalid symbol [" + curChar + "] line: " + _lineCount + " col: " + _charCount);
                        }
                        break;
                    case ParseState.VALUES:
                        if (_newLinePos < _config.NewLineChar.Length && curChar == _config.NewLineChar[_newLinePos])
                        {
                            _curState = ParseState.NEWLINE;
                            _newLinePos++;
                            _charCount++;

                            string value;
                            value = _curToken.Trim();

                            if (_curToken.Trim() != string.Empty)
                            {
                                _charCount++;
                                if (_curKey != null)
                                {
                                    _curKey.Values.Add(value);
                                    if (_curSection != null)
                                    {
                                        if (!_curSection.IsLevelSet())
                                        {
                                            if (_curSectionDepth.First().IsDataRoot() || _curSectionDepth.Count() == 1)
                                            {
                                                //Simply Add to List Root of tree
                                                SectionTreeCollection temp;
                                                temp = new SectionTreeCollection(_curSection);
                                                _curSectionDepth.First().Add(temp);
                                                _curSectionDepth.Insert(0, temp);
                                                Console.WriteLine("New Section:" + _curSection.SectionName);
                                                Console.WriteLine("Section Count:" + _curSectionDepth.Count());
                                                Console.WriteLine("Indent Count:" + _indentLevel);
                                                Console.WriteLine("Section Simply ADDED");
                                            }
                                            else if (_curSectionDepth.Count() > 1)
                                            {
                                                if (_curSectionDepth.First().Level + 1 == _indentLevel)
                                                {
                                                    SectionTreeCollection temp;
                                                    temp = new SectionTreeCollection(_curSection);
                                                    _curSectionDepth.First().Add(temp);
                                                    _curSectionDepth.Insert(0, temp);
                                                    Console.WriteLine("New Section:"+_curSection.SectionName);
                                                    Console.WriteLine("Section Count:" + _curSectionDepth.Count());
                                                    Console.WriteLine("Indent Count:" + _indentLevel);
                                                    Console.WriteLine("Section Added");
                                                }
                                                else if (_indentLevel >= 0 && _curSectionDepth.First().Level >= _indentLevel)
                                                {
                                                    while (!_curSectionDepth.First().IsDataRoot() &&
                                                        _curSectionDepth.First().Level >= _indentLevel &&
                                                        _curSectionDepth.Count() > 1)
                                                    {
                                                        _curSectionDepth.RemoveAt(0);
                                                    }
                                                    SectionTreeCollection temp;
                                                    temp = new SectionTreeCollection(_curSection);
                                                    _curSectionDepth.First().Add(temp);
                                                    _curSectionDepth.Insert(0, temp);
                                                    Console.WriteLine("New Section:" + _curSection.SectionName);
                                                    Console.WriteLine("Section Count:" + _curSectionDepth.Count());
                                                    Console.WriteLine("Indent Count:" + _indentLevel);
                                                    Console.WriteLine("Section Removed and Added");
                                                }
                                                else
                                                {
                                                    // Error key belongs to unknown section
                                                    throw new ParseException("Invalid indentation of key, does not belong to an existing section. Indent:"+_indentLevel+" Key:"+_curKey.KeyName+" line: " + _lineCount + " col: " + _charCount);
                                                }
                                            }
                                            _curSection.Keys.Add(_curKey);
                                            PrintKey();
                                            _curSection = null;
                                        }
                                        else
                                        {
                                            while (!_curSectionDepth.First().IsDataRoot() &&
                                                    _curSectionDepth.First().Level > _indentLevel &&
                                                    _curSectionDepth.Count() > 1)
                                            {
                                                _curSectionDepth.RemoveAt(0);
                                            }
                                            _curSectionDepth.First().Keys.Add(_curKey);
                                            PrintKey();
                                        }
                                    }
                                    else
                                    {
                                        _curSectionDepth.First().Keys.Add(_curKey);
                                        PrintKey();
                                    }
                                    _curKey = null;
                                }
                                else
                                {
                                    //Error
                                    throw new ParseException("Invalid symbol [" + curChar + "] Reached values state without a valid Key. line: " + _lineCount + " col: " + _charCount);
                                }

                                _curToken = "";
                            }
                            else
                            {
                                //throw syntax error
                                throw new ParseException("Invalid symbol [" + curChar + "] line: " + _lineCount + " col: " + _charCount);
                            }

                            if (_newLinePos >= _config.NewLineChar.Length)
                            {
                                _curState = ParseState.BEGINLINE;
                                _newLinePos = 0;
                                _lineCount++;
                                _indentLevel = 0;
                                _indentPos = 0;
                                _charCount = 0;
                            }
                        }
                        else if (curChar == _config.MultiValueSeparator)
                        {
                            string value;
                            value = _curToken.Trim();
                            if (_curToken.Trim() != string.Empty)
                            {
                                _charCount++;
                                if (_curKey != null)
                                {
                                    _curKey.Values.Add(value);
                                }

                                _curToken = "";
                            }
                            else
                            {
                                //throw syntax error
                                throw new ParseException("Read ["+_config.MultiValueSeparator+"] expected a non-empty string length: " + _newLinePos + " line: " + _lineCount + " col: " + _charCount);
                            }

                        }
                        else if (_config.IsValidToken(curChar))
                        {
                            _curToken += curChar;
                        }
                        else
                        {
                            //throw Syntax error
                            throw new ParseException("Invalid symbol [" + curChar + "] line: " + _lineCount + " col: " + _charCount);
                        }
                        break;
                    case ParseState.NEWLINE:
                        
                        if (_newLinePos < _config.NewLineChar.Length && curChar == _config.NewLineChar[_newLinePos])
                        {
                            _newLinePos++;
                            _charCount++;
                            if (_newLinePos >= _config.NewLineChar.Length)
                            {
                                _curState = ParseState.BEGINLINE;
                                _newLinePos = 0;
                                _lineCount++;
                                _indentLevel = 0;
                                _indentPos = 0;
                                _charCount = 0;
                            }
                        }
                        else if(_newLinePos < _config.NewLineChar.Length && curChar != _config.NewLineChar[_newLinePos])
                        {
                            // throw expected next newline char
                            throw new ParseException("Invalid symbol [" + curChar + "] Expected Next newline char:["+ _config.NewLineChar[_newLinePos] + "] line: " + _lineCount + " col: " + _charCount);

                        }
                        else if(_newLinePos > _config.NewLineChar.Length)
                        {
                            throw new ParseException("Invalid symbol [" + curChar + "] Recieved more characters than newline characters newline length: " + _newLinePos + " line: " + _lineCount + " col: " + _charCount);
                        }
                        else
                        {
                            throw new ParseException("Invalid symbol [" + curChar + "] Uknown exit from newline parse. Newline length: " + _newLinePos + " line: " + _lineCount + " col: " + _charCount);
                        }
                        break;
                }
            }
        }

        public SectionTreeCollection DataTree
        {
            get
            {
                return _dataTree;
            }
        }

        private void PrintKey()
        {
            Console.Write("New Key Added:" + _curKey.KeyName + " ");
            foreach (string val in _curKey.Values)
            {
                Console.Write("Value:" + val + " ");
            }
            Console.WriteLine(" ");
        }

        
    }
}
