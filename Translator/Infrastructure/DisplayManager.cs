using System;
using System.Collections.Generic;
using Translator.Core;

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
                Console.WriteLine("{0}\t<==>\t{1}", token.value , token.lexem.Name);
            }
        }
    }
}