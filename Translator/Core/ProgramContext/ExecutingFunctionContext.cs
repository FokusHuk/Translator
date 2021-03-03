using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.ProgramContext
{
    public class ExecutingFunctionContext
    {
        public List<TriadsStackMachine.TriadWithResult> TriadResults { get; set; }
        public List<TriadsStackMachine.Variable> Variables { get; set; }
        public int CurrentIndex { get; set; }
        public FunctionResultParameters Parameters { get; set; }

        public ExecutingFunctionContext(List<TriadsStackMachine.TriadWithResult> triadResults, List<TriadsStackMachine.Variable> variables, int currentIndex, FunctionResultParameters parameters)
        {
            TriadResults = triadResults;
            Variables = variables;
            CurrentIndex = currentIndex;
            Parameters = parameters;
        }

        public ExecutingFunctionContext()
        {
            TriadResults = new List<TriadsStackMachine.TriadWithResult>();
            Variables = new List<TriadsStackMachine.Variable>();
            CurrentIndex = 0;
        }

        public ExecutingFunctionContext GetNewExecutingContext()
        {
            var newContext = new ExecutingFunctionContext();
            
            var newTriadResults = new List<TriadsStackMachine.TriadWithResult>();
            foreach (var triadResult in TriadResults)
            {
                newTriadResults.Add(new TriadsStackMachine.TriadWithResult(triadResult.TriadIndex, triadResult.Value));
            }
            newContext.TriadResults = newTriadResults;
            
            var newVariables = new List<TriadsStackMachine.Variable>();
            foreach (var variable in Variables)
            {
                newVariables.Add(new TriadsStackMachine.Variable(variable.Name, variable.Value));
            }
            newContext.Variables = newVariables;
            
            
            newContext.CurrentIndex = CurrentIndex;

            newContext.Parameters = Parameters?.GetNewParameters();

            return newContext;
        }
    }
}