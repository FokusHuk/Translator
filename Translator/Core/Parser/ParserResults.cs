using System.Collections.Generic;

namespace Translator.Core.Parser
{
    public class ParserResults
    {
        public ParserResults(bool isValid, Stack<GrammarMistake> mistakes)
        {
            IsValid = isValid;
            Mistakes = mistakes;
        }

        public bool IsValid { get; }

        public Stack<GrammarMistake> Mistakes { get; }
    }
}