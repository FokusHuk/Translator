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
            var triadsConverter = new TriadsConverter();
            var triadsOptimizer = new TriadsOptimizer();
            
            var tokens = Lexer.GetTokensFromExpression(programCode);
            var parserResults = parser.Check(tokens);
            
            if (!parserResults.IsValid)
            {
                Environment.Exit(-1);
            }
            
            var POLIS = syntacticalAnalyzer.Convert(tokens);
            
            var triads = triadsConverter
                .GetTriadsFromPolis(POLIS, syntacticalAnalyzer.PolisConditionsIndexes)
                .ToList();
            
            var optimizedTriads = triadsOptimizer.Optimize(triads, triadsConverter.TriadsConditionIndexes);

            DisplayManager.DisplayLexerResults(tokens);
            DisplayManager.DisplayParserResults(parserResults);
            DisplayManager.DisplayExpressionInPolishNotation(POLIS);
            
            stackMachine.calculate(POLIS);
            DisplayManager.DisplayVariablesAfterCalculations(stackMachine.Variables);
            DisplayManager.DisplayTriads(triads, "Triads");
            DisplayManager.DisplayTriads(triads, "Optimized triads");
            
            triadStackMachine.Calculate(optimizedTriads);
            Console.WriteLine("Result:");
            foreach (var variable in triadStackMachine.Variables)
            {
                Console.WriteLine(variable.Name + " " + variable.Value);
            }
        }
    }
}
