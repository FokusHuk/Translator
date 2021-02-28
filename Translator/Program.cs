using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Translator.Core.Analyzer;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.StackMachine;
using Translator.Core.TriadsRepresentation;
using Translator.Infrastructure;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            var settingInJson = File.ReadAllText("appsettings.json");
            var settings =  JsonConvert.DeserializeObject<TranslatorSettings>(settingInJson);
            
            var programCode = FileManager.ReadAllFile(settings.SourceFilePath);
            
            var parser = new Parser();
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var stackMachine = new StackMachine();
            var triadStackMachine = new TriadsStackMachine();
            
            var tokens = Lexer.GetTokensFromExpression(programCode);
            var parserResults = parser.Check(tokens);
            
            if (!parserResults.IsValid)
            {
                Environment.Exit(-1);
            }
            
            var POLIS = syntacticalAnalyzer.Convert(tokens);

            DisplayManager.DisplayLexerResults(tokens);
            DisplayManager.DisplayParserResults(parserResults);
            DisplayManager.DisplayExpressionInPolishNotation(POLIS);
            
            stackMachine.calculate(POLIS);
            DisplayManager.DisplayVariablesAfterCalculations(stackMachine.Variables);
            
            
            var triadsHandler = new TriadsHandler();
            var triads = triadsHandler.GetTriadsFromPolis(POLIS);
            Console.WriteLine("\nTriads:");
            var i = 0;
            foreach (var triad in triads)
            {
                Console.WriteLine(i++ + " " + triad);
            }

            Console.WriteLine();
            
            triadStackMachine.Calculate(triads.ToList());
            foreach (var variable in triadStackMachine.Variables)
            {
                Console.WriteLine(variable.Name + " " + variable.Value);
            }
        }
    }
}
