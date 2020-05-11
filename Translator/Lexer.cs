using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Translator
{
    class Lexer
    {
        public Lexer()
        {
            
        }

        public List<Token> execute(string expression)
        {
            expression = expression.Replace(" ", "");

            List<Token> tokens = new List<Token>();
            Stack<Token> matches = new Stack<Token>();
            List<Lexem> lexems = Lexem.getList();
            string substring = "";
            bool endOfLexem;
            int maxSearchRange = 2;
            int searchIndex = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                substring += expression[i];
                endOfLexem = true;

                foreach (Lexem lexem in lexems)
                {
                    if (Regex.IsMatch(substring, lexem.value))
                    {
                        matches.Push(new Token(substring, lexem));
                        endOfLexem = false;
                    }
                }

                if (endOfLexem || i == expression.Length - 1)
                {
                    if (matches.Count != 0)
                    {
                        tokens.Add(matches.Peek());
                        substring = "";
                        matches.Clear();
                        searchIndex = 0;
                    }
                    else
                    {
                        searchIndex++;
                        if (searchIndex == maxSearchRange)
                        {
                            throw new LexemNotFoundException(substring);
                        }
                        else
                        {
                            continue;
                        }
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
