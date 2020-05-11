using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    struct GrammarMistake
    {
        public int Position { get; set; }
        public Token FixedToken { get; set; }
        public Lexem RequiredLexem { get; set; }

        public GrammarMistake(int position, Token fixedToken, Lexem requiredLexem)
        {
            Position = position;
            FixedToken = fixedToken;
            RequiredLexem = requiredLexem;
        }

        public override string ToString()
        {
            return String.Format("Mistake: in position {0} ({1}). Lexem {2} expected, but {3} founded", Position, FixedToken.value, RequiredLexem.name, FixedToken.lexem.name);
        }
    }
}
