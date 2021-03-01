using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
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
            var source = FileManager.ReadAllFile(settings.SourceFilePath);
            
            var parser = new Parser();
            var compiler = new Compiler();
            var triadStackMachine = new TriadsStackMachine();

            var tokens = Lexer.GetTokensFromExpression(source);
            DisplayManager.DisplayLexerResults(tokens);
            
            var parserResults = parser.Check(tokens);
            
            DisplayManager.DisplayParserResults(parserResults);
            
            if (!parserResults.IsValid)
            {
                Environment.Exit(-1);
            }

            var functionContexts = ContextManager.GetFunctionContexts(tokens);

            foreach (var context in functionContexts)
            {
                compiler.Compile(context);
            }

            triadStackMachine.Calculate(functionContexts.First(c => c.Name == "main").OptimizedTriads);

            Console.WriteLine("Triads stack machine result:");
            foreach (var variable in triadStackMachine.Variables)
            {
                Console.WriteLine(variable.Name + " " + variable.Value);
            }
            Console.WriteLine($"Return result = {triadStackMachine.ReturnResult}");
        }
    }
}
