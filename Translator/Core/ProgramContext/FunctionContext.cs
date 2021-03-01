using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Translator.Core
{
    public class FunctionContext
    {
        public string Name { get; set; }
        public string[] Arguments { get; set; }
        public List<Token> Tokens { get; set; }

        public FunctionContext(string name, string[] arguments, List<Token> tokens)
        {
            Name = name;
            Arguments = arguments;
            Tokens = tokens;
        }
    }
}
