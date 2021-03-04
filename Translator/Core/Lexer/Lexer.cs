using System.Collections.Generic;
using System.Text.RegularExpressions;
using Translator.Exceptions;

namespace Translator.Core.Lexer
{
    public static class Lexer
    {
        public static List<Token> GetTokensFromExpression(string expression)
        {
            expression = expression
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("\t", " ")
                .TrimStart(' ');
            var tokens = new List<Token>();
            var matches = new Stack<Token>();
            var lexems = Lexem.GetAll();
            var subexpression = "";
            var endOfLexem = true;
            var maxSearchRange = 2;
            var searchIndex = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                endOfLexem = true;
                subexpression += expression[i];

                foreach (Lexem lexem in lexems)
                {
                    if (Regex.IsMatch(subexpression, lexem.Value))
                    {
                        matches.Push(new Token(subexpression, lexem));
                        endOfLexem = false;
                    }
                }

                if (endOfLexem || i == expression.Length - 1)
                {
                    if (matches.Count != 0)
                    {
                        if (matches.Peek().Lexem == Lexem.LB)
                        {
                            var tempIndex = tokens.Count - 1;
                            while (tokens[tempIndex].Lexem == Lexem.SPC && tempIndex > 0) tempIndex--;
                            if (tokens[tempIndex].Lexem == Lexem.VAR)
                            {
                                tokens[tempIndex].Lexem = Lexem.EF_NAME;
                            }
                        }
                        tokens.Add(matches.Peek());
                        subexpression = "";
                        matches.Clear();
                        searchIndex = 0;
                    }
                    else
                    {
                        searchIndex++;
                        if (searchIndex == maxSearchRange)
                        {
                            throw new LexemNotFoundException(subexpression);
                        }

                        continue;
                    }

                    if (endOfLexem)
                    {
                        i--;
                    }
                }
            }

            return tokens;
        }
    }
}
