using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Tests.Compiler
{
    public static class TestLexer
    {
        public static List<Token> GetPolisTokensFromExpression(string expression)
        {
            var tokens = Lexer.GetTokensFromExpression(expression, Lexem.GetAll())
                .Where(t => t.Lexem != Lexem.SPC)
                .ToList();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Lexem == Lexem.F_TRANS || tokens[i].Lexem == Lexem.UNC_TRANS)
                    tokens[i - 1].Lexem = Lexem.TRANS_LBL;
            }

            return tokens;
        }
    }
}