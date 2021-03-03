using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Translator.Core
{
    public static class ContextManager
    {
        public static (List<FunctionContext>, List<FunctionDescription>) GetFunctionContexts(List<Token> tokens)
        {
            var functionDescriptions = new List<FunctionDescription>();
            var contexts = new List<FunctionContext>();

            var startIndex = 0;
            var lastContextIndex = 0;
            
            while (lastContextIndex != tokens.Count - 1)
            {
                lastContextIndex = GetLastContextIndex(startIndex, tokens);
                var context = GetContext(tokens.Skip(startIndex).Take(lastContextIndex - startIndex + 1).ToList());
                contexts.Add(context);
                startIndex = lastContextIndex + 1;
                
                functionDescriptions.Add(new FunctionDescription(context.Name, context.Arguments.Length, context.IsAsync));
            }

            return (contexts, functionDescriptions);
        }

        private static int GetLastContextIndex(int startIndex, List<Token> tokens)
        {
            var bias = 1;

            for (int i = tokens.FindIndex(startIndex, t => t.Lexem == Lexem.LSB) + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == Lexem.LSB) bias++;
                else if (tokens[i].Lexem == Lexem.RSB) bias--;
                
                if (bias == 0)
                    return i;
            }

            throw new InvalidOperationException();
        }

        private static FunctionContext GetContext(List<Token> tokens)
        {
            var tokensWithoutSpaces = tokens.Where(t => t.Lexem != Lexem.SPC).ToList();

            string name;
            int index;
            var arguments = new List<string>();
            bool isAsync;

            if (tokensWithoutSpaces[0].Lexem == Lexem.ASYNC_KW)
            {
                name = tokensWithoutSpaces[2].Value;
                index = 4;
                isAsync = true;
            }
            else
            {
                name = tokensWithoutSpaces[1].Value;
                index = 3;
                isAsync = false;
            }

            while (tokensWithoutSpaces[index].Lexem != Lexem.RB)
            {
                if (tokensWithoutSpaces[index].Lexem == Lexem.VAR)
                    arguments.Add(tokensWithoutSpaces[index].Value);
                index++;
            }

            tokens = tokens.Skip(tokens.FindIndex(t => t.Lexem == Lexem.LSB) + 1).ToList();
            tokens = tokens.Skip(tokens.FindIndex(t => t.Lexem != Lexem.SPC)).ToList();
            tokens.RemoveAt(tokens.Count - 1);
            
            return new FunctionContext(name, arguments.ToArray(), tokens, isAsync);
        }
    }
}
