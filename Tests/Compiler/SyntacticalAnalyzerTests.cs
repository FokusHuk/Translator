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
                .WithTokens(TestSourceKey.Simple)
                .WithPolis(TestSourceKey.Simple)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithConditionOnlyIf_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.If)
                .WithPolis(TestSourceKey.If)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithConditionIfElse_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.IfElse)
                .WithPolis(TestSourceKey.IfElse)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithNestedConditions_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.NestedConditions)
                .WithPolis(TestSourceKey.NestedConditions)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhile_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleWhile)
                .WithPolis(TestSourceKey.CycleWhile)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhileAndConditions_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleWhileWithConditions)
                .WithPolis(TestSourceKey.CycleWhileWithConditions)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleWhileInCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleWhileInCondition)
                .WithPolis(TestSourceKey.CycleWhileInCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleFor_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleFor)
                .WithPolis(TestSourceKey.CycleFor)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleForAndCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleForWithCondition)
                .WithPolis(TestSourceKey.CycleForWithCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithCycleForInCondition_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.CycleForInCondition)
                .WithPolis(TestSourceKey.CycleForInCondition)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithOutFunction_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.Out)
                .WithPolis(TestSourceKey.Out)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithOutFunctionInCycles_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.OutInCycles)
                .WithPolis(TestSourceKey.OutInCycles)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithReturn_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.Return)
                .WithPolis(TestSourceKey.Return)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
        
        [Test]
        public void Convert_ExpressionWithSeveralReturns_CorrectPolis()
        {
            var syntacticalAnalyzer = new SyntacticalAnalyzer();
            var program = new TestProgramBuilder()
                .WithTokens(TestSourceKey.SeveralReturnsWithFirstWorking)
                .WithPolis(TestSourceKey.SeveralReturnsWithFirstWorking)
                .Build();

            var actual = syntacticalAnalyzer.Convert(program.Tokens);
            
            Assert.AreEqual(program.Polis, actual);
            Assert.AreEqual(program.PolisConditionIndexes, syntacticalAnalyzer.PolisConditionsIndexes);
        }
    }
}
