using System;
using System.Collections.Generic;
using Translator.Core;
using Translator.Core.Analyzer;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.Stack_Machine;
using Translator.Exceptions;
using Translator.Infrastructure;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            var programCode = FileManager.ReadAllFile(Environment.CurrentDirectory + "\\Templates\\Code.txt");

            DisplayManager.DisplayProgramCode(programCode);
            
            Parser parser = new Parser();
            SyntacticalAnalyzer syntAnalyzer = new SyntacticalAnalyzer();
            StackMachine stackMachine = new StackMachine();

            // Получение списка токенов лексером
            var tokens = Lexer.GetTokensFromExpression(programCode);
            
            DisplayManager.DisplayLexerResults(tokens);

            // Проверка корректности выражения парсером
            var parserResults = parser.Check(tokens);
            
            DisplayManager.DisplayParserResults(parserResults);
            
            if (!parserResults.IsValid)
            {
                Environment.Exit(-1);
            }

            // Перевод в польскую запись
            Console.WriteLine("\nSyntactical analyzer results:");
            tokens.Add(new Token("$", Lexem.END));
            List<Token> POLIS = syntAnalyzer.convert(tokens);
            Console.WriteLine("POLIS: ");
            int i = 0;
            int delta = 25;
            while (i < POLIS.Count)
            {
                int i_pos = i + delta;
                if (i_pos > POLIS.Count)
                {
                    delta -= i_pos - POLIS.Count;
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
                i -= delta;
                for (; i < i_pos; i++)
                {
                    Console.Write("{0}   ", POLIS[i].Value);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            // Вычисление выражения в польской нотации
            Console.WriteLine("\nStack machine results:");
            try
            {
                stackMachine.calculate(POLIS);
            }
            catch (VariableNotFoundException excp)
            {
                Console.WriteLine("Отсутствует объявление для переменной \"{0}\".", excp.Value);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            catch (TypeNotRecognizedException excp)
            {
                Console.WriteLine("Тип переменной \"{0}\" не был распознан (\"{1}\").", excp.Value, excp.Value.GetType());
                Console.ReadKey();
                Environment.Exit(-1);
            }
            catch (Exception excp)
            {
                Console.WriteLine("Ошибка вычисления.\nMessage:\n{0}.", excp.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            Console.WriteLine("\nVariables:");
            foreach (KeyValuePair<string, object> var in stackMachine.Variables)
            {
                Console.WriteLine("{0}: {1}", var.Key, var.Value);
            }

            Console.ReadKey();
        }
    }
}
