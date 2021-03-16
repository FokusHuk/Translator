using NUnit.Framework;
using Tests.Infrastructure;
using Translator.Core.TriadsRepresentation;

namespace Tests.Triads
{
    public class TriadsOptimizerTests
    {
        [Test]
        public void Optimize_ArithmeticExpression_CorrectOptimization()
        {
            var triadsOptimizer = new TriadsOptimizer();
            var program = new TestProgramBuilder()
                .WithTriads(TestSourceKey.Simple)
                .WithOptimizedTriads(TestSourceKey.Simple)
                .Build();

            var actual = triadsOptimizer.Optimize(program.Triads, program.TriadsConditionIndexes);
            
            Assert.AreEqual(program.OptimizedTriads, actual);
        }
        
        [Test]
        public void Optimize_ExpressionWithOutFunction_CorrectOptimization()
        {
            var triadsOptimizer = new TriadsOptimizer();
            var program = new TestProgramBuilder()
                .WithTriads(TestSourceKey.Out)
                .WithOptimizedTriads(TestSourceKey.Out)
                .Build();

            var actual = triadsOptimizer.Optimize(program.Triads, program.TriadsConditionIndexes);
            
            Assert.AreEqual(program.OptimizedTriads, actual);
        }
        
        [Test]
        public void Optimize_ExpressionWithReturn_CorrectOptimization()
        {
            var triadsOptimizer = new TriadsOptimizer();
            var program = new TestProgramBuilder()
                .WithTriads(TestSourceKey.Return)
                .WithOptimizedTriads(TestSourceKey.Return)
                .Build();

            var actual = triadsOptimizer.Optimize(program.Triads, program.TriadsConditionIndexes);
            
            Assert.AreEqual(program.OptimizedTriads, actual);
        }
    }
}
