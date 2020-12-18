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
            
            var parser = new Parser();
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var stackMachine = new StackMachine();

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
            var POLIS = syntacticalAnalyzer.Convert(tokens);
            
            DisplayManager.DisplayExpressionInPolishNotation(POLIS);

            // Вычисление выражения в польской нотации
            Console.WriteLine("\nStack machine results:");
            stackMachine.calculate(POLIS);

            DisplayManager.DisplayVariablesAfterCalculations(stackMachine.Variables);

            Console.ReadKey();
        }
    }
}
