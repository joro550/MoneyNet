using System.Collections;
using System.Collections.Generic;

namespace Monkey.Core.Lexing
{
    public class Script
    {
        private readonly string _script;
        private int _currentPosition;


        public Script(string script)
        {
            _script = script;
        }

        public char Peek()
        {
            var index = _currentPosition+1;
            return index >= _script.Length ? ' ' : _script[index];
        }

        public string Current() 
            => _script[_currentPosition].ToString();

        public string Next()
        {
            _currentPosition++;
            return _currentPosition >= _script.Length 
                ? null 
                : _script[_currentPosition].ToString();
        }
    }
}