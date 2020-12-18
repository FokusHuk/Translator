using System;
using Translator.Core.Lexer;

namespace Translator.Core
{
    public class Token
    {
        public string value { get; set; }
        public Lexem lexem { get; set; }       

        public Token(string value, Lexem lexem)
        {
            this.lexem = lexem;
            this.value = value;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", value, lexem.Name);
        }
    }
}
