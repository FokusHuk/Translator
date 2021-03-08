using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Tests.Infrastracture
{
    class TestProgram
    {
        public TestSourceKey Key { get; }
        
        public string Source { get; set; }
        
        public List<Token> Tokens { get; }

        public TestProgram(TestSourceKey key, string source, List<Token> tokens)
        {
            Key = key;
            Source = source;
            Tokens = tokens;
        }

        public TestProgram Copy()
        {
            var tokens = Tokens.Select(token => new Token(token.Value, token.Lexem)).ToList();

            return new TestProgram(Key, Source, tokens);
        }
    }
}
