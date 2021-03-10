using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Tests.Infrastructure
{
    class TestProgram
    {
        public string Source { get; set; }
        
        public List<Token> Tokens { get; set; }

        public List<Token> Polis { get; set; }

        public List<bool> PolisConditionIndexes { get; set; }

        public TestProgram()
        {
            
        }
    }
}
