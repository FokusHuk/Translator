﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Translator;
using Translator.Core;
using Translator.Core.FunctionResultParameters;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.TriadsRepresentation;
using Translator.Infrastructure;

namespace ConsoleClient
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

            var (functionContexts, functionDescriptions) = ContextManager.GetFunctionContexts(tokens);

            var t = functionContexts[0].ExecutingContext == functionContexts[1].ExecutingContext;
            
            foreach (var context in functionContexts)
            {
                compiler.Compile(context, functionDescriptions);
            }

            var callStack = new Stack<FunctionContext>();
            var currentFunctionContext = functionContexts.First(c => c.Name == "main");

            while (true)
            {
                var executingContext = triadStackMachine.Calculate(currentFunctionContext);

                if (executingContext.Parameters.ResultType == ResultType.Complete)
                {
                    break;
                }
                else if (executingContext.Parameters.ResultType == ResultType.Call)
                {
                    var functionContext = functionContexts
                        .First(c => c.Name == executingContext.Parameters.FunctionName);

                    if (functionContext.IsAsync)
                    {
                        var threadFunctionContext = functionContext
                            .GetNewFunctionContext(functionContext.ExecutingContext);

                        var threadStackMachine = new TriadsStackMachine();

                        var thread = new Thread(() => threadStackMachine.Calculate(threadFunctionContext));
                        thread.Start();

                        currentFunctionContext.ExecutingContext.CurrentIndex++;
                    }
                    else
                    {
                        callStack.Push(currentFunctionContext.GetNewFunctionContext(executingContext));

                        currentFunctionContext =
                            functionContext.GetNewFunctionContext(functionContext.ExecutingContext);

                        // описание функции
                        var funcDescription = functionDescriptions
                            .First(f => f.Name == executingContext.Parameters.FunctionName);

                        // получить числовые аргументы
                        var functionArgs = executingContext.Parameters.FunctionArgs
                            .Split(' ')
                            .Select(arg => arg.Replace(" ", ""))
                            .ToList();

                        // добавить переменные из аргументов в variables для функции
                        for (int i = 0; i < funcDescription.ArgsCount; i++)
                        {
                            currentFunctionContext.ExecutingContext.Variables
                                .Add(new TriadsStackMachine.Variable(currentFunctionContext.Arguments[i],
                                    functionArgs[i]));
                        }
                    }
                }
                else if (executingContext.Parameters.ResultType == ResultType.Return)
                {
                    // загрузка предыдущего контекста из стека
                    if (callStack.Count == 0)
                    {
                        break;
                    }
                    var previousContext = callStack.Pop();

                    if (executingContext.Parameters.Value != "")
                    {
                        var triadResult = previousContext.ExecutingContext.TriadResults
                            .FirstOrDefault(tr => tr.TriadIndex == previousContext.ExecutingContext.CurrentIndex);
                        if (triadResult == null)
                            previousContext.ExecutingContext.TriadResults.Add(new TriadsStackMachine.TriadWithResult(
                            previousContext.ExecutingContext.CurrentIndex, executingContext.Parameters.Value));
                        else
                        {
                            triadResult.Value = executingContext.Parameters.Value;
                        }
                    }

                    previousContext.ExecutingContext.CurrentIndex++;
                    currentFunctionContext = previousContext;
                }
            }

            Console.WriteLine("Triads stack machine result:");
            foreach (var variable in functionContexts[0].ExecutingContext.Variables)
            {
                Console.WriteLine(variable.Name + " " + variable.Value);
            }
            Console.WriteLine($"Return result = {functionContexts[0].ExecutingContext.Parameters?.Value}");
        }
    }
}