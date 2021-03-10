using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Tests.Infrastructure
{
    class TestProgram
    {
        public TestSourceKey Key { get; }
        
        public string Source { get; set; }
        
        public List<Token> Tokens { get; }
        
        public List<Token> Polis { get; }
        
        public List<bool> PolisConditionIndexes { get; }

        public TestProgram(
            TestSourceKey key, 
            string source, 
            List<Token> tokens, 
            List<Token> polis, 
            List<bool> polisConditionIndexes = null)
        {
            Key = key;
            Source = source;
            Tokens = tokens;
            Polis = polis;
            PolisConditionIndexes = polisConditionIndexes;
        }
    }
}
