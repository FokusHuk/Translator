using System.Collections.Generic;
using NUnit.Framework;
using Translator.Core.Lexer;
using Translator.Core.Parser;

namespace Tests.Compiler
{
    public class ParserTests
    {
        [Test]
        public void Check_ArithmeticExpression_Valid()
        {
            var parser = new Parser();
            
            // expression: void main(){a = 2 + 2; out(a);}
            var tokens = new List<Token>()
            {
                new Token("void", Lexem.VOID_T),
                new Token(" ", Lexem.SPC),
                new Token("main", Lexem.EF_NAME),
                new Token("(", Lexem.LB),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("a", Lexem.VAR),
                new Token(" ", Lexem.SPC),
                new Token("=", Lexem.ASSIGN_OP),
                new Token(" ", Lexem.SPC),
                new Token("2", Lexem.DIGIT),
                new Token(" ", Lexem.SPC),
                new Token("+", Lexem.OP),
                new Token(" ", Lexem.SPC),
                new Token("2", Lexem.DIGIT),
                new Token(";", Lexem.EOL),
                new Token(" ", Lexem.SPC),
                new Token("out", Lexem.OUT_KW),
                new Token("(", Lexem.LB),
                new Token("a", Lexem.VAR),
                new Token(")", Lexem.RB),
                new Token(";", Lexem.EOL),
                new Token("}", Lexem.RSB)
            };

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_TwoFunctions_Valid()
        {
            var parser = new Parser();
            
            // expression: void main(){} func test(){}}
            var tokens = new List<Token>()
            {
                new Token("void", Lexem.VOID_T),
                new Token(" ", Lexem.SPC),
                new Token("main", Lexem.EF_NAME),
                new Token("(", Lexem.LB),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("}", Lexem.RSB),
                new Token(" ", Lexem.SPC),
                new Token("func", Lexem.FUNC_T),
                new Token(" ", Lexem.SPC),
                new Token("test", Lexem.EF_NAME),
                new Token("(", Lexem.LB),
                new Token(")", Lexem.RB),
                new Token("{", Lexem.LSB),
                new Token("}", Lexem.RSB)
            };

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }

        [Test]
        public void Check_ArithmeticExpressionWithConditions_Valid()
        {
            var parser = new Parser();
            var expression = "void main(){ a = 5; if(a > 3){ if(a < 8){b = a / 2;} }c = a + b;}";
            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleAndConditions_Valid()
        {
            var parser = new Parser();
            var expression = @"
            void main() {
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
            if(b >= 5)
            {
                a = b / 2;
            }
            out(a);
            out(b);
            }";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleWhile_Valid()
        {
            var parser = new Parser();
            var expression = @"
            void main(){
            a = 7;
            if(a > 5)
            {
                b = a - 2;
                while(a < 60)
                {
                    a = a * 2;
                }
                a = a - b;
            }}";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleFor_Valid()
        {
            var parser = new Parser();
            var expression = @"
            void main(){
            a = 5;

            for (i = 0; i < 5; i = i + 1)
            {
                out(a);
                a = a + 1;
            }
            }";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ThreeFunctionsTwoAreAsync_Valid()
        {
            var parser = new Parser();
            var expression = @"
            void main()
            {
                first();
                second();
                
                a = 9999999;
                out(a);
            }
            
            async func first()
            {
                for (i = 0; i < 100; i = i + 1)
                {
                    out(i);
                }
            }
            
            async func second()
            {
                for (i = 101; i < 200; i = i + 1)
                {
                    out(i);
                }
            }";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoSemicolon_Invalid()
        {
            var parser = new Parser();
            var expression = @"
            void main()
            {
                first();
                second();
                
                a = 9999999;
                out(a);
            }
            
            async func first()
            {
                for (i = 0; i < 100; i = i + 1)
                {
                    out(i)
                }
            }
            
            async func second()
            {
                for (i = 101; i < 200; i = i + 1)
                {
                    out(i);
                }
            }";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoSquareBracket_Invalid()
        {
            var parser = new Parser();
            var expression = @"
            void main(){
            a = 5;

            for (i = 0; i < 5; i = i + 1)
            {
                out(a);
                a = a + 1;
            }
            ";

            var tokens = Lexer.GetTokensFromExpression(expression);

            var actual = parser.Check(tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
    }
}