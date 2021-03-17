using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Translator.Core;
using Translator.Core.FunctionResultParameters;
using Translator.Core.Lexer;
using Translator.Core.ProgramContext;
using Translator.Core.TriadsRepresentation;
using Translator.Core.TriadsRepresentation.Entities;

namespace Tests.ComplexTests
{
    public class ComplexTests
    {
        [Test]
        public void Complex_ExpressionWithConditions_CorrectResult()
        {
            var programCode = @"
            void main() {
            a = 5;
            if(a > 3)
            {
                if(a < 8)
                {
                b = a / 2;
                }
            }
            c = a + b;}";
            
            var result = Execute(programCode);
            var variables = result[0].ExecutingContext.Variables;
            
            Assert.AreEqual("a", variables[0].Name);
            Assert.AreEqual("5", variables[0].Value);
            Assert.AreEqual("b", variables[1].Name);
            Assert.AreEqual("2,5", variables[1].Value);
            Assert.AreEqual("c", variables[2].Name);
            Assert.AreEqual("7,5", variables[2].Value);
        }

        [Test]
        public void Complex_ExpressionWithManyVariables_CorrectResult()
        {
            var programCode = @"
            void main() {
                first = 0;
                second = 1;
                n = 7;
                i = 2;
                
                while (i < n)
                {
                    second = first + second;
                    first = second - first;
                    i = i + 1;
                }
            }";
            
            var result = Execute(programCode);
            var variables = result[0].ExecutingContext.Variables;
            
            Assert.AreEqual("first", variables[0].Name);
            Assert.AreEqual("5", variables[0].Value);
            Assert.AreEqual("second", variables[1].Name);
            Assert.AreEqual("8", variables[1].Value);
            Assert.AreEqual("n", variables[2].Name);
            Assert.AreEqual("7", variables[2].Value);
            Assert.AreEqual("i", variables[3].Name);
            Assert.AreEqual("7", variables[3].Value);
        }

        private List<FunctionContext> Execute(string programCode)
        {
            var lexer = new Lexer();
            var compiler = new Translator.Core.Compiler();
            var triadStackMachine = new TriadsCalculator();
            
            var tokens = lexer.GetTokensFromExpression(programCode);
            var (functionContexts, functionDescriptions) = ContextManager.GetFunctionContexts(tokens);
            
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

                if (executingContext.Parameters.ResultType == ResultType.Call)
                {
                    var functionContext = functionContexts
                        .First(c => c.Name == executingContext.Parameters.FunctionName);

                    if (functionContext.IsAsync)
                    {
                        var threadFunctionContext = functionContext
                            .GetNewFunctionContext(functionContext.ExecutingContext);

                        var threadStackMachine = new TriadsCalculator();

                        var thread = new Thread(() => threadStackMachine.Calculate(threadFunctionContext));
                        thread.Start();

                        currentFunctionContext.ExecutingContext.CurrentIndex++;
                    }
                    else
                    {
                        callStack.Push(currentFunctionContext.GetNewFunctionContext(executingContext));

                        currentFunctionContext =
                            functionContext.GetNewFunctionContext(functionContext.ExecutingContext);
                        
                        var funcDescription = functionDescriptions
                            .First(f => f.Name == executingContext.Parameters.FunctionName);
                        
                        var functionArgs = executingContext.Parameters.FunctionArgs
                            .Split(' ')
                            .Select(arg => arg.Replace(" ", ""))
                            .ToList();
                        
                        for (int i = 0; i < funcDescription.ArgsCount; i++)
                        {
                            currentFunctionContext.ExecutingContext.Variables
                                .Add(new Variable(currentFunctionContext.Arguments[i],
                                    functionArgs[i]));
                        }
                    }
                }
                else if (executingContext.Parameters.ResultType == ResultType.Return)
                {
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
                            previousContext.ExecutingContext.TriadResults.Add(new TriadWithResult(
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

            return functionContexts;
        }
    }
}