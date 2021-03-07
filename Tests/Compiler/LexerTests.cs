using System.Collections.Generic;
using NUnit.Framework;
using Translator.Core.Lexer;

namespace Tests.Compiler
{
    public class LexerTests
    {
        [Test]
        public void GetTokensFromExpression_ArithmeticExpression_CorrectTokens()
        {
            var lexer = new Lexer();
            var expression = "a + b - 2 / 2";

            var expected = new List<Token>()
            {
                new Token("a", Lexem.VAR),
                new Token(" ", Lexem.SPC),
                new Token("+", Lexem.OP),
                new Token(" ", Lexem.SPC),
                new Token("b", Lexem.VAR),
                new Token(" ", Lexem.SPC),
                new Token("-", Lexem.OP),
                new Token(" ", Lexem.SPC),
                new Token("2", Lexem.DIGIT),
                new Token(" ", Lexem.SPC),
                new Token("/", Lexem.OP),
                new Token(" ", Lexem.SPC),
                new Token("2", Lexem.DIGIT)
            };

            var actual = lexer.GetTokensFromExpression(expression);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithConditions_CorrectTokens()
        {
            var lexer = new Lexer();
            var expression = "if(a>5){b=2}else{b=0}";
            
            var expected = new List<Token>()
            {
                new Token("if", Lexem.IF_KW),
                new Token("(", Lexem.LB),
                new Token("a", Lexem.VAR),
                new Token(">", Lexem.COMP_OP),
                new Token("5", Lexem.DIGIT),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("b", Lexem.VAR),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("2", Lexem.DIGIT),
                new Token("}", Lexem.RSB),
                new Token("else", Lexem.ELSE_KW),
                new Token("{", Lexem.LSB),
                new Token("b", Lexem.VAR),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("0", Lexem.DIGIT),
                new Token("}", Lexem.RSB)
            };

            var actual = lexer.GetTokensFromExpression(expression);
            
            Assert.AreEqual(expected, actual);
        }
        
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithFunction_CorrectTokens()
        {
            var lexer = new Lexer();
            var expression = "async void function(){return a;}";
            
            var expected = new List<Token>()
            {
                new Token("async", Lexem.ASYNC_KW),
                new Token(" ", Lexem.SPC),
                new Token("void", Lexem.VOID_T),
                new Token(" ", Lexem.SPC),
                new Token("function", Lexem.EF_NAME),
                new Token("(", Lexem.LB),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("return", Lexem.RETURN_KW),
                new Token(" ", Lexem.SPC),
                new Token("a", Lexem.VAR),
                new Token(";", Lexem.EOL),
                new Token("}", Lexem.RSB)
            };

            var actual = lexer.GetTokensFromExpression(expression);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycles_CorrectTokens()
        {
            var lexer = new Lexer();
            var expression = "for(i=1;i<5;i=i+1)while(a>0){}";
            
            var expected = new List<Token>()
            {
                new Token("for", Lexem.FOR_KW),
                new Token("(", Lexem.LB),
                new Token("i", Lexem.VAR),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("1", Lexem.DIGIT),
                new Token(";", Lexem.EOL),
                new Token("i", Lexem.VAR),
                new Token("<", Lexem.COMP_OP),
                new Token("5", Lexem.DIGIT),
                new Token(";", Lexem.EOL),
                new Token("i", Lexem.VAR),
                new Token("=", Lexem.ASSIGN_OP),
                new Token("i", Lexem.VAR),
                new Token("+", Lexem.OP),
                new Token("1", Lexem.DIGIT),
                new Token(")", Lexem.RB),
                new Token("while", Lexem.WHILE_KW),
                new Token("(", Lexem.LB),
                new Token("a", Lexem.VAR),
                new Token(">", Lexem.COMP_OP),
                new Token("0", Lexem.DIGIT),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("}", Lexem.RSB)
            };

            var actual = lexer.GetTokensFromExpression(expression);
            
            Assert.AreEqual(expected, actual);
        }
    }
}
