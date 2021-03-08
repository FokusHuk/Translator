using NUnit.Framework;
using Tests.Infrastructure;
using Translator.Core.Analyzer;

namespace Tests.Compiler
{
    public class SyntacticalAnalyzerTests
    {
        [Test]
        public void Convert_ArithmeticExpression_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Simple)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithConditionOnlyIf_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.If)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithConditionIfElse_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.IfElse)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithNestedConditions_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.NestedConditions)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhile_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhileAndConditions_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileWithConditions)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhileInCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileInCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleFor_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleFor)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleForAndCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForWithCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleForInCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForInCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithOutFunction_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Out)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithOutFunctionInCycles_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.OutInCycles)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithReturn_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Return)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
        
        [Test]
        public void Convert_ExpressionWithSeveralReturns_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.SeveralReturnsWithFirstWorking)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
        }
    }
}
