using System;
using Translator.Core.Lexer;

namespace Translator.Core.Parser
{
    public struct GrammarMistake
    {
        public int Position { get; }
        public Token FixedToken { get; }
        public Lexem RequiredLexem { get; }

        public GrammarMistake(int position, Token fixedToken, Lexem requiredLexem)
        {
            Position = position;
            FixedToken = fixedToken;
            RequiredLexem = requiredLexem;
        }

        public override string ToString()
        {
            return String.Format("Mistake: in position {0} ({1}). Lexem {2} expected, but {3} founded", Position, FixedToken.Value, RequiredLexem.Name, FixedToken.Lexem.Name);
        }
    }
}
