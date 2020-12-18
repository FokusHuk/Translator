using System;
using System.IO;
using Newtonsoft.Json;
using Translator.Core.Analyzer;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.Stack_Machine;
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
        }
    }
}
