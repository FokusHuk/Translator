using System.Collections.Generic;
using System.Linq;
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
            var expression = @"
            a = 5; 
            if(a > 3)
            { 
                if(a < 8)
                {
                    b = a / 2;
                } 
            }
            c = a + b";
            var polisExpression = "a 5 = a 3 > 18 !F a 8 < 18 !F b a 2 / = c a b + = $";
            var tokens = Lexer.GetTokensFromExpression(expression);
            var expected = TestLexer.GetPolisTokensFromExpression(polisExpression);

            var actual = syntacticalAnalyzer.Convert(tokens);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Check_ExpressionWithCyclesAndConditions_Valid()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var expression = @"
            a = 0;
            while(a < 10)
            {
                a = a + 3;
                if(a < 7)
                {
                    b = 4;
                }
                else
                {
                    b = 10;
                } 
            } 
            b = b / 2;
            s = 0;
            for (i = 1; i <= b; i = i + 1)
            {
                s = s + i;
            }
            out(a);
            out(b);
            out(s);";
            var polisExpression =
                "a 0 = a 10 < 28 !F a a 3 + = a 7 < 23 !F b 4 = 26 ! b 10 = 3 ! b b 2 / = s 0 = i 1 = i b <= 60 !F 53" +
                " ! i i 1 + = 39 ! s s i + = 46 ! a & out b & out s & out $";
            var tokens = Lexer.GetTokensFromExpression(expression);
            var expected = TestLexer.GetPolisTokensFromExpression(polisExpression);
            
            var actual = syntacticalAnalyzer.Convert(tokens);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Check_ExpressionWithFunctions_Valid()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var expression = @"
            a = 5;
            b = 3;
            c = sum(a, b);
            d = upd(c);
            noArgs();
    
            a = 10;
            out(a);";
            var polisExpression = "a 5 = b 3 = c a b sum = d c upd = noArgs a 10 = a & out $";
            var tokens = Lexer.GetTokensFromExpression(expression);
            var expected = TestLexer
                .GetPolisTokensFromExpression(polisExpression)
                .Select(token =>
                {
                    if (token.Value == "sum" || token.Value == "upd" || token.Value == "noArgs")
                        return new Token(token.Value, Lexem.EF_NAME);
                    return token;
                })
                .ToList();

            var actual = syntacticalAnalyzer.Convert(tokens);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void Check_ExpressionWithReturn_Valid()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var expression = @"
            a = 5;
            b = 3;
            return a + b;";
            var polisExpression = "a 5 = b 3 = a b + return $";
            var tokens = Lexer.GetTokensFromExpression(expression);
            var expected = TestLexer.GetPolisTokensFromExpression(polisExpression);

            var actual = syntacticalAnalyzer.Convert(tokens);
            
            ForTestCreations(actual);

            Assert.AreEqual(expected, actual);
        }

        // TODO: delete this
        void ForTestCreations(List<Token> tokens)
        {
            var s = "";
            foreach (var token in tokens)
            {
                s += token.Value + " ";
            }
            return;
        }
    }
}