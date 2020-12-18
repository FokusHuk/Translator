using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class Token
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
            return String.Format("{0} - {1}", value, lexem.name);
        }
    }
}
