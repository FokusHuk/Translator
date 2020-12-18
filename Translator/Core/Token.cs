﻿using Translator.Core.Lexer;

namespace Translator.Core
{
    public class Token
    {
        public string Value { get; set; }
        public Lexem Lexem { get; }       

        public Token(string value, Lexem lexem)
        {
            Lexem = lexem;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} - {Lexem.Name}";
        }
    }
}
