﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using Translator;
using Translator.Core;
using Translator.Core.FunctionResultParameters;
using Translator.Core.Lexer;
using Translator.Core.Parser;
using Translator.Core.TriadsRepresentation;
using Translator.Infrastructure;

namespace IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Parser = new Parser();
            Compiler = new Compiler();
            TriadsStackMachine = new TriadsStackMachine();
        }

        private Parser Parser;
        private Compiler Compiler;
        private TriadsStackMachine TriadsStackMachine;

        private void Run_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var source = Input.Text.Replace("\n", "").Replace("\r", " ");
            var tokens = Lexer.GetTokensFromExpression(source);
            Lexems.Text = ResultManager.GetLexerResults(tokens);
            var parserResults = Parser.Check(tokens);

            if (!parserResults.IsValid)
            {
                Build.Content = "Failed";
                Output.Text = ResultManager.GetParserMistakes(parserResults);
                WorkPanel.SelectedIndex = 4;
                return;
            }

            Build.Content = "In progress";

            var (functionContexts, functionDescriptions) = ContextManager.GetFunctionContexts(tokens);
            try
            {
                foreach (var context in functionContexts)
                {
                    Compiler.Compile(context, functionDescriptions);
                    Polis.Text = ResultManager.GetExpressionInPolishNotation(context.POLIS);
                    Triads.Text = ResultManager.GetTriadsResult(context.Triads);
                    Optimization.Text = ResultManager.GetTriadsResult(context.OptimizedTriads);
                }
            }
            catch (Exception exception)
            {
                Build.Content = "Failed";
                Output.Text = exception.Message;
                WorkPanel.SelectedIndex = 4;
                return;
            }

            Build.Content = "Success";

            var callStack = new Stack<FunctionContext>();
            var currentFunctionContext = functionContexts.First(c => c.Name == "main");

            while (true)
            {
                var executingContext = TriadsStackMachine.Calculate(currentFunctionContext);

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

            WorkPanel.SelectedIndex = 4;

            var mainContext = functionContexts.First(c => c.Name == "main");
            foreach (var variable in mainContext.ExecutingContext.Variables)
            {
                Variables.Text += variable.Name + "\t" + variable.Value + "\n";
            }
            
            Thread.Sleep(2000);

            Output.Text = GlobalOutput.Output;
            
            if (mainContext.ExecutingContext.Parameters != null)
                Output.Text += $"\nReturn result: {mainContext.ExecutingContext.Parameters.Value}";
        }
    }
}