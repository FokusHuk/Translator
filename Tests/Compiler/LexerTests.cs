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
                .WithTokens(TestSourceKey.Simple)
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
                .WithTokens(TestSourceKey.If)
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
                .WithTokens(TestSourceKey.IfElse)
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
                .WithTokens(TestSourceKey.NestedConditions)
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
                .WithTokens(TestSourceKey.CycleWhile)
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
                .WithTokens(TestSourceKey.CycleWhileWithConditions)
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
                .WithTokens(TestSourceKey.CycleWhileInCondition)
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
                .WithTokens(TestSourceKey.CycleFor)
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
                .WithTokens(TestSourceKey.CycleForWithCondition)
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
                .WithTokens(TestSourceKey.CycleForInCondition)
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
                .WithTokens(TestSourceKey.Out)
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
                .WithTokens(TestSourceKey.OutInCycles)
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
                .WithTokens(TestSourceKey.Return)
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
                .WithTokens(TestSourceKey.SeveralReturnsWithFirstWorking)
                .Build();
            
            var actual = lexer.GetTokensFromExpression(program.Source);
            
            Assert.AreEqual(program.Tokens, actual);
        }
    }
}
