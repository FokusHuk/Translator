using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Infrastructure
{
    public static class ResultManager
    {
        public static string GetLexerResults(IEnumerable<Token> tokens)
        {
            var result = string.Empty;
            foreach (Token token in tokens)
            {
                result += string.Format("{0}\t<==>\t{1}\n", token.Value, token.Lexem.Name);
            }

            return result;
        }
        
        public static string GetParserMistakes(ParserResults results)
        {
            var result = string.Empty;
            while (results.Mistakes.Count != 0)
                result += results.Mistakes.Pop() + "\n";

            return result;
        }
        
        public static string GetExpressionInPolishNotation(List<Token> POLIS)
        {
            var result = string.Empty;

            var index = 0;
            foreach (var token in POLIS)
            {
                result += $"[{index++}]\t{token.Value}\n";
            }

            return result;
        }
        
        public static string GetTriadsResult(List<Triad> triads)
        {
            var result = string.Empty;
            
            var index = 0;
            foreach (var triad in triads)
            {
                result += $"[{index++}]\t{triad}\n";
            }

            return result;
        }
    }
}