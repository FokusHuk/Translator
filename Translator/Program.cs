using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Translator.Core;
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
            
            DisplayManager.DisplayLexerResults(tokens);
            DisplayManager.DisplayParserResults(parserResults);
            
            if (!parserResults.IsValid)
            {
                Environment.Exit(-1);
            }

            var functionContexts = ContextManager.GetFunctionContexts(tokens);

            var POLIS = syntacticalAnalyzer.Convert(tokens);

            DisplayManager.DisplayExpressionInPolishNotation(POLIS);
            
            stackMachine.calculate(POLIS);
            DisplayManager.DisplayVariablesAfterCalculations(stackMachine.Variables);
            
            var triads = triadsConverter
                .GetTriadsFromPolis(POLIS, syntacticalAnalyzer.PolisConditionsIndexes)
                .ToList();
            DisplayManager.DisplayTriads(triads, "Triads");
            
            var optimizedTriads = triadsOptimizer.Optimize(triads, triadsConverter.TriadsConditionIndexes);
            DisplayManager.DisplayTriads(triads, "Optimized triads");
            
            triadStackMachine.Calculate(optimizedTriads);

            Console.WriteLine("Triads stack machine result:");
            foreach (var variable in triadStackMachine.Variables)
            {
                Console.WriteLine(variable.Name + " " + variable.Value);
            }
        }
    }
}
