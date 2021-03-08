using NUnit.Framework;
using Tests.Infrastructure;
using Translator.Core.Lexer;

namespace Tests.Compiler
{
    public class LexerTests
    {
        [Test]
        public void GetTokensFromExpression_ArithmeticExpression_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Simple)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithConditionOnlyIf_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.If)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithConditionIfElse_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.IfElse)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithNestedConditions_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.NestedConditions)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleWhile_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleWhileAndConditions_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileWithConditions)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleWhileInCondition_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileInCondition)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleFor_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleFor)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleForAndCondition_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForWithCondition)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithCycleForInCondition_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForInCondition)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithOutFunction_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Out)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithOutFunctionInCycles_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.OutInCycles)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithReturn_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Return)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
        
        [Test]
        public void GetTokensFromExpression_ExpressionWithTwoReturns_CorrectTokens()
        {
            var lexer = new Lexer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.SeveralReturnsWithFirstWorking)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
    }
}
