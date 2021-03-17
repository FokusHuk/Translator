using System;
using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Infrastructure
{
    public static class DisplayManager
    {
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

        public static void DisplayExpressionInPolishNotation(List<Token> POLIS)
        {
            Console.WriteLine("\nSyntactical analyzer results:");
            Console.WriteLine("POLIS: ");
            int i = 0;
            int widthOfDisplayingText = 25;
            while (i < POLIS.Count)
            {
                int i_pos = i + widthOfDisplayingText;
                if (i_pos > POLIS.Count)
                {
                    widthOfDisplayingText -= i_pos - POLIS.Count;
                    i_pos = POLIS.Count;                   
                }
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                for (; i < i_pos; i++)
                {
                    Console.Write("{0}", i);
                    for (int j = 0; j < POLIS[i].Value.Length + 3 - i.ToString().Length; j++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                i -= widthOfDisplayingText;
                for (; i < i_pos; i++)
                {
                    Console.Write("{0}   ", POLIS[i].Value);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void DisplayVariablesAfterCalculations(Dictionary<string, object> variables)
        {
            Console.WriteLine("\nVariables:");
            foreach (var variable in variables)
            {
                Console.WriteLine($"{variable.Key}: {variable.Value}");
            }
        }

        public static void DisplayTriads(List<Triad> triads, string header)
        {
            Console.WriteLine($"\n{header}:");
            var i = 0;
            foreach (var triad in triads)
            {
                Console.WriteLine($"[{i++}]" + " " + triad);
            }
            Console.WriteLine();
        }
    }
}