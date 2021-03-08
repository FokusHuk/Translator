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
        
        public List<Token> Polis { get; }

        public TestProgram(TestSourceKey key, string source, List<Token> tokens, List<Token> polis = null)
        {
            Key = key;
            Source = source;
            Tokens = tokens;
            Polis = polis;
        }

        public TestProgram Copy()
        {
            var tokens = Tokens.Select(token => new Token(token.Value, token.Lexem)).ToList();
            //var polis = Polis.Select(token => new Token(token.Value, token.Lexem)).ToList();

            return new TestProgram(Key, Source, tokens);
        }
    }
}
