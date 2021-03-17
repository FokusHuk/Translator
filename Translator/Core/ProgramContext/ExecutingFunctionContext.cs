using System.Collections.Generic;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.ProgramContext
{
    public class ExecutingFunctionContext
    {
        public List<TriadWithResult> TriadResults { get; set; }
        public List<Variable> Variables { get; set; }
        public int CurrentIndex { get; set; }
        public FunctionResultParameters Parameters { get; set; }

        public ExecutingFunctionContext(List<TriadWithResult> triadResults, List<Variable> variables, int currentIndex, FunctionResultParameters parameters)
        {
            TriadResults = triadResults;
            Variables = variables;
            CurrentIndex = currentIndex;
            Parameters = parameters;
        }

        public ExecutingFunctionContext()
        {
            TriadResults = new List<TriadWithResult>();
            Variables = new List<Variable>();
            CurrentIndex = 0;
        }

        public ExecutingFunctionContext GetNewExecutingContext()
        {
            var newContext = new ExecutingFunctionContext();
            
            var newTriadResults = new List<TriadWithResult>();
            foreach (var triadResult in TriadResults)
            {
                newTriadResults.Add(new TriadWithResult(triadResult.TriadIndex, triadResult.Value));
            }
            newContext.TriadResults = newTriadResults;
            
            var newVariables = new List<Variable>();
            foreach (var variable in Variables)
            {
                newVariables.Add(new Variable(variable.Name, variable.Value));
            }
            newContext.Variables = newVariables;
            
            
            newContext.CurrentIndex = CurrentIndex;

            newContext.Parameters = Parameters?.GetNewParameters();

            return newContext;
        }
    }
}