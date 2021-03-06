using System.Collections.Generic;
using NUnit.Framework;
using Translator.Core.Analyzer;
using Translator.Core.Lexer;

namespace Tests.Compiler
{
    public class SyntacticalAnalyzerTests
    {
        [Test]
        public void Check_ArithmeticExpressionWithConditions_Valid()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var expression = "a = 5; if(a > 3){ if(a < 8){b = a / 2;} }c = a + b";
            var tokens = Lexer.GetTokensFromExpression(expression);

            var expected = new List<Token>()
            {
                new Token("a", Lexem.VAR),
                new Token("5", Lexem.DIGIT),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("a", Lexem.VAR),
                new Token("3", Lexem.DIGIT),
                new Token(">", Lexem.COMP_OP),
                new Token("18", Lexem.TRANS_LBL),
                new Token("!F", Lexem.F_TRANS),
                new Token("a", Lexem.VAR),
                new Token("8", Lexem.DIGIT),
                new Token("<", Lexem.COMP_OP),
                new Token("18", Lexem.TRANS_LBL),
                new Token("!F", Lexem.F_TRANS),
                new Token("b", Lexem.VAR),
                new Token("a", Lexem.VAR),
                new Token("2", Lexem.DIGIT),
                new Token("/", Lexem.OP),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("c", Lexem.VAR),
                new Token("a", Lexem.VAR),
                new Token("b", Lexem.VAR),
                new Token("+", Lexem.OP),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("$", Lexem.END)
            };

            var actual = syntacticalAnalyzer.Convert(tokens);
            
            Assert.AreEqual(expected, actual);
        }

        // TODO: delete this
        void ForTestCreations()
        {
            var expected = new List<Token>()
            {
                new Token("a", Lexem.VAR),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
                new Token("5", Lexem.DIGIT),
            };
        }
    }
}