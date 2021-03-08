using NUnit.Framework;
using Tests.Infrastructure;
using Translator.Core.Parser;

namespace Tests.Compiler
{
    public class ParserTests
    {
        [Test]
        public void Check_ArithmeticExpression_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Simple)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithConditionOnlyIf_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.If)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithConditionIfElse_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.IfElse)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithConditionNestedConditions_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.NestedConditions)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleWhile_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleWhileAndConditions_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileWithConditions)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }

        [Test]
        public void Check_ExpressionWithCycleWhileInCondition_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhileInCondition)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }

        [Test]
        public void Check_ExpressionWithCycleFor_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleFor)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleForWithCondition_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForWithCondition)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithCycleForInCondition_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleForInCondition)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithOutFunction_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Out)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithOutFunctionInCycles_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.OutInCycles)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithReturn_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.Return)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ExpressionWithSeveralReturns_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.SeveralReturnsWithFirstWorking)
                .WithMainFunction()
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }

        [Test]
        public void Check_TwoFunctions_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.SeveralReturnsWithFirstWorking)
                .WithMainFunction()
                .WithAnotherFunction(new TestProgramBuilder()
                    .WithSource(TestSourceKey.Simple)
                    .WithFunction("func", "test", new [] {"a", "b"}))
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }

        [Test]
        public void Check_ThreeFunctionsTwoAreAsync_Valid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.SeveralReturnsWithFirstWorking)
                .WithMainFunction()
                .WithAnotherFunction(new TestProgramBuilder()
                    .WithSource(TestSourceKey.Simple)
                    .WithFunction("func", "first", new [] {"a", "b"}, true))
                .WithAnotherFunction(new TestProgramBuilder()
                    .WithSource(TestSourceKey.Simple)
                    .WithFunction("func", "second", new [] {"d"}, true))
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsTrue(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoBracket_Invalid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .WithMainFunction()
                .WithGrammarMistake(TestGrammarMistakeType.NoBracket)
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoEol_Invalid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .WithMainFunction()
                .WithGrammarMistake(TestGrammarMistakeType.NoEol)
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoAssign_Invalid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .WithMainFunction()
                .WithGrammarMistake(TestGrammarMistakeType.NoAssign)
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
        
        [Test]
        public void Check_ProgramCodeWithGrammarMistake_NoOperation_Invalid()
        {
            var parser = new Parser();
            var program = new TestProgramBuilder()
                .WithSource(TestSourceKey.CycleWhile)
                .WithMainFunction()
                .WithGrammarMistake(TestGrammarMistakeType.NoOperation)
                .Build();
            
            var actual = parser.Check(program.Tokens);
            
            Assert.IsFalse(actual.IsValid);
        }
    }
}
