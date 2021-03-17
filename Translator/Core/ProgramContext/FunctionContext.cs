using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.ProgramContext
{
    public class FunctionContext
    {
        public string Name { get; set; }
        public string[] Arguments { get; set; }
        public bool IsAsync { get; }
        public List<Token> Tokens { get; set; }
        public List<Token> POLIS { get; set; }
        public List<Triad> Triads { get; set; }
        public List<Triad> OptimizedTriads { get; set; }
        
        public ExecutingFunctionContext ExecutingContext { get; set; }

        public FunctionContext(string name, string[] arguments, List<Token> tokens, bool isAsync)
        {
            Name = name;
            Arguments = arguments;
            Tokens = tokens;
            IsAsync = isAsync;
            Initialize();
        }

        private void Initialize()
        {
            ExecutingContext = new ExecutingFunctionContext();
        }

        public FunctionContext GetNewFunctionContext(ExecutingFunctionContext executingContext)
        {
            var newContext = new FunctionContext(Name, Arguments, Tokens, IsAsync);
            
            var newPolis = new List<Token>();
            foreach (var token in POLIS)
            {
                newPolis.Add(new Token(token.Value, token.Lexem));
            }
            newContext.POLIS = newPolis;

            var newTriads = new List<Triad>();
            foreach (var triad in Triads)
            {
                newTriads.Add(new Triad(triad.LeftOperand?.GetCopy(), triad.RightOperand?.GetCopy(), new Token(triad.Operation.Value, triad.Operation.Lexem), triad.Type));
            }
            newContext.Triads = newTriads;
            
            var newOptimizedTriads = new List<Triad>();
            foreach (var triad in OptimizedTriads)
            {
                newOptimizedTriads.Add(new Triad(triad.LeftOperand?.GetCopy(), triad.RightOperand?.GetCopy(), new Token(triad.Operation.Value, triad.Operation.Lexem), triad.Type));
            }
            newContext.OptimizedTriads = newOptimizedTriads;
            
            newContext.ExecutingContext = executingContext.GetNewExecutingContext();
            return newContext;
        }
    }
}
