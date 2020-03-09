using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Yarn.Unity;
using Yarn;

namespace Eachb.YarnExtensions {

    /*Extended text parser which can deal with the rich text tags that TextMeshPro can use e.g. different coloured text.
     This script makes it possible to have scrolling text which still makes use of those tags.*/
    public class YarnTextMeshParser {
        const string HTML_TAG_PATTERN = "<.*?>";
        private CharacterInfo[] _stringCharacters;
        private TokenChecker _tokenChecker;
        private VariableStorageBehaviour _variableStorage;
        public int StringLength
        {
            get;
            private set;
        }

        public YarnTextMeshParser(string yarnString, VariableStorageBehaviour variableStore)
        {
            _variableStorage = variableStore;
            yarnString = CheckVars(yarnString);
            StringLength = 0;
            _stringCharacters = new CharacterInfo[yarnString.Length];
            var matches = Regex.Matches(yarnString, HTML_TAG_PATTERN);
            _tokenChecker = new TokenChecker(matches);
            for (int i = 0; i < yarnString.Length; i++)
            {
                if (matches.Count == 0)
                {
                    _stringCharacters[i] = new CharacterInfo()
                    {
                        character = yarnString[i],
                        isToken = false
                    };
                    StringLength++;
                    continue;
                }
                _stringCharacters[i] = new CharacterInfo()
                {
                    character = yarnString[i],
                    isToken = _tokenChecker.IsToken(i)
                };
                if (!_stringCharacters[i].isToken)
                {
                    StringLength++;
                }
            }
        }
        //Returns the yarn string with length number of *visible* characters.
        public string GetYarnString(int index)
        {
            if (index > StringLength)
            {
                index = StringLength;
            }
            int runningLength = index + 1;
            var characters = new StringBuilder();
            foreach (CharacterInfo ci in _stringCharacters)
            {
                if (runningLength > 0 && !ci.isToken)
                {
                    runningLength--;
                    characters.Append(ci.character);
                } else if (ci.isToken)
                {
                    characters.Append(ci.character);
                }
            }
            return characters.ToString();
        }
        //We have a list of these
        private class CharacterInfo {
            internal char character;
            internal bool isToken;
        }
        private class TokenChecker {
            private List<Tuple<int, int>> _matchRanges;
            internal TokenChecker(MatchCollection matches)
            {
                _matchRanges = new List<Tuple<int, int>>();
                foreach (Match m in matches)
                {
                    _matchRanges.Add(new Tuple<int, int>(m.Index, m.Index + m.Length-1));
                }
            }
            internal bool IsToken(int index)
            {
                bool isToken = false;
                foreach (Tuple<int, int> matchRange in _matchRanges)
                {
                    if (index >= matchRange.Item1 && index <= matchRange.Item2)
                    {
                        isToken = true;
                    }
                }
                return isToken;
            }
        }
        string CheckVars(string input)
        {
            string output = string.Empty;
            bool checkingVar = false;
            string currentVar = string.Empty;

            int index = 0;
            while (index < input.Length)
            {
                if (input[index] == '[')
                {
                    checkingVar = true;
                    currentVar = string.Empty;
                } else if (input[index] == ']')
                {
                    checkingVar = false;
                    output += ParseVariable(currentVar);
                    currentVar = string.Empty;
                } else if (checkingVar)
                {
                    currentVar += input[index];
                } else
                {
                    output += input[index];
                }
                index += 1;
            }

            return output;
        }

        string ParseVariable(string varName)
        {
            //Check YarnSpinner's variable storage first
            if (_variableStorage.GetValue(varName) != Yarn.Value.NULL)
            {
                return _variableStorage.GetValue(varName).AsString;
            }

            //Handle other variables here
            if (varName == "$time")
            {
                return Time.time.ToString();
            }

            //If no variables are found, return the variable name
            return varName;
        }
    }
}
