using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Tests.Infrastructure
{
    class TestProgram
    {
        public string Source { get; set; }
        
        public List<Token> Tokens { get; set; }

        public List<Token> Polis { get; set; }

        public List<bool> PolisConditionIndexes { get; set; }
        
        public List<Triad> Triads { get; set; }
        
        public List<bool> TriadsConditionIndexes { get; set; }
        
        public List<Triad> OptimizedTriads { get; set; }
    }
}
