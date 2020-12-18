using System;
using System.Collections.Generic;
using Translator.Core;
using Translator.Core.Parser;

namespace Translator.Infrastructure
{
    public static class DisplayManager
    {
        public static void DisplayProgramCode(string programCode)
        {
            Console.WriteLine("Program code:");
            Console.WriteLine(programCode);
            Console.WriteLine("\n");
        }
        
        public static void DisplayLexerResults(IEnumerable<Token> tokens)
        {
            Console.WriteLine("Lexer results:");
            foreach (Token token in tokens)
            {
                Console.WriteLine("{0}\t<==>\t{1}", token.Value , token.Lexem.Name);
            }
        }

        public static void DisplayParserResults(ParserResults results)
        {
            Console.WriteLine("\nParser results:");
            Console.WriteLine(results.IsValid);
            if (!results.IsValid)
            {
                Console.WriteLine("\nСтек ошибок");
                while (results.Mistakes.Count != 0)
                    Console.WriteLine(results.Mistakes.Pop());
            }
        }
    }
}