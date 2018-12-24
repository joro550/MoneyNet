using System.Collections;
using System.Collections.Generic;

namespace Monkey.Core.Lexing
{
    public class Script
    {
        private readonly Queue<char> _scriptLetters;
        private string _currentLetter = string.Empty;

        public Script(string script) 
            => _scriptLetters = new Queue<char>(script);

        public char Peek()
        {
            _scriptLetters.TryPeek(out var r);
            return r;
        }

        public string Current() 
            => _currentLetter;

        public string Next()
        {
            var result = _scriptLetters.TryDequeue(out var currentLetter);
            _currentLetter = result ? currentLetter.ToString() : null;
            return _currentLetter;
        }
    }
}