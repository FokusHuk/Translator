using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Translator.Exceptions;

namespace Translator.Core.Lexer
{
    public class Lexer
    {
        private string Expression;
        private List<Lexem> Lexems;
        
        private readonly List<Token> Tokens;
        private readonly Stack<Token> Matches;

        private string Subexpression;
        private bool EndOfLexem;
        private int MaxSearchRange;
        private int SearchIndex;

        public Lexer()
        {
            Tokens = new List<Token>();
            Matches = new Stack<Token>();
        }

        private void Initialize(string expression, List<Lexem> lexems)
        {
            Expression = expression
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("\t", " ")
                .TrimStart(' ');
            
            Lexems = lexems ?? Lexem.GetForInitialAnalysis();

            Tokens.Clear();
            Matches.Clear();

            Subexpression = String.Empty;
            EndOfLexem = true;
            MaxSearchRange = 2;
            SearchIndex = 0;
        }
        
        public List<Token> GetTokensFromExpression(string expression, List<Lexem> lexems = null)
        {
            Initialize(expression, lexems);

            for (int i = 0; i < Expression.Length; i++)
            {
                EndOfLexem = true;
                Subexpression += Expression[i];

                foreach (Lexem lexem in Lexems)
                {
                    if (Regex.IsMatch(Subexpression, lexem.Value))
                    {
                        Matches.Push(new Token(Subexpression, lexem));
                        EndOfLexem = false;
                    }
                }

                if (EndOfLexem || i == Expression.Length - 1)
                {
                    if (Matches.Count != 0)
                    {
                        if (Matches.Peek().Lexem == Lexem.LB)
                        {
                            var innerSearchIndex = Tokens.Count - 1;
                            
                            while (Tokens[innerSearchIndex].Lexem == Lexem.SPC && innerSearchIndex > 0) innerSearchIndex--;
                            
                            if (Tokens[innerSearchIndex].Lexem == Lexem.VAR)
                            {
                                Tokens[innerSearchIndex].Lexem = Lexem.EF_NAME;
                            }
                        }
                        
                        Tokens.Add(Matches.Peek());
                        Subexpression = "";
                        Matches.Clear();
                        SearchIndex = 0;
                    }
                    else
                    {
                        SearchIndex++;
                        if (SearchIndex == MaxSearchRange)
                        {
                            throw new LexemNotFoundException(Subexpression);
                        }

                        continue;
                    }

                    if (EndOfLexem)
                    {
                        i--;
                    }
                }
            }

            return Tokens.Where(t => t.Lexem != Lexem.SPC).ToList();
        }
    }
}
