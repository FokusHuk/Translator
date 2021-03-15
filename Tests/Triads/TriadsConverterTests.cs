using NUnit.Framework;
using Tests.Infrastructure;
using Translator.Core.TriadsRepresentation;

namespace Tests.Triads
{
    public class TriadsConverterTests
    {
        [Test]
        public void GetTriadsFromPolis_ArithmeticExpression_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.Simple)
                .WithTriads(TestSourceKey.Simple)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithConditionIfOnly_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.If)
                .WithTriads(TestSourceKey.If)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithConditionIfElse_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.IfElse)
                .WithTriads(TestSourceKey.IfElse)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithNestedConditions_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.NestedConditions)
                .WithTriads(TestSourceKey.NestedConditions)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleWhile_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleWhile)
                .WithTriads(TestSourceKey.CycleWhile)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleWhileAndConditions_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleWhileWithConditions)
                .WithTriads(TestSourceKey.CycleWhileWithConditions)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleWhileInCondition_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleWhileInCondition)
                .WithTriads(TestSourceKey.CycleWhileInCondition)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleFor_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleFor)
                .WithTriads(TestSourceKey.CycleFor)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleForAndCondition_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleForWithCondition)
                .WithTriads(TestSourceKey.CycleForWithCondition)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithCycleForInCondition_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.CycleForInCondition)
                .WithTriads(TestSourceKey.CycleForInCondition)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithOutFunction_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.Out)
                .WithTriads(TestSourceKey.Out)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithOutFunctionInCycles_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.OutInCycles)
                .WithTriads(TestSourceKey.OutInCycles)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithReturn_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.Return)
                .WithTriads(TestSourceKey.Return)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
        
        [Test]
        public void GetTriadsFromPolis_ExpressionWithSeveralReturns_CorrectTriads()
        {
            var triadsConverter = new TriadsConverter();
            var program = new TestProgramBuilder()
                .WithPolis(TestSourceKey.SeveralReturnsWithFirstWorking)
                .WithTriads(TestSourceKey.SeveralReturnsWithFirstWorking)
                .Build();

            var actual = triadsConverter.GetTriadsFromPolis(program.Polis, program.PolisConditionIndexes, null);
            
            Assert.AreEqual(program.Triads, actual);
            Assert.AreEqual(program.TriadsConditionIndexes, triadsConverter.TriadsConditionIndexes);
        }
    }
}
