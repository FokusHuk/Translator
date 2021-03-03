﻿using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using Translator.Core.FunctionResultParameters;
using Translator.Core.Lexer;
using Translator.Core.ProgramContext;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core
{
    public class FunctionContext
    {
        public string Name { get; set; }
        public string[] Arguments { get; set; }
        public List<Token> Tokens { get; set; }
        public List<Token> POLIS { get; set; }
        public List<Triad> Triads { get; set; }
        public List<Triad> OptimizedTriads { get; set; }
        
        public ExecutingFunctionContext ExecutingContext { get; set; }

        public FunctionContext(string name, string[] arguments, List<Token> tokens)
        {
            Name = name;
            Arguments = arguments;
            Tokens = tokens;
            Initialize();
        }

        private void Initialize()
        {
            ExecutingContext = new ExecutingFunctionContext();
        }

        public FunctionContext GetNewFunctionContext(ExecutingFunctionContext executingContext)
        {
            var newContext = new FunctionContext(Name, Arguments, Tokens);
            
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
