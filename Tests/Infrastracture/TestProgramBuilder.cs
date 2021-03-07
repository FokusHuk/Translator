using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Infrastracture
{
    class TestProgramBuilder
    {
        private TestProgram Program;

        private TestProgramBuilder(TestProgram program)
        {
            Program = program;
        }

        public TestProgramBuilder WithSource(TestSourceKey sourceKey)
        {
            Program = CodeExamples.First(e => e.Key == sourceKey);

            return new TestProgramBuilder(Program);
        }

        public TestProgramBuilder WithMainFunction()
        {
            Program.Source = $"void main ()" + "{" + Program.Source + "}";
            
            return new TestProgramBuilder(Program);
        }

        public TestProgramBuilder WithFunction(string type, string name, string parameters = "")
        {
            Program.Source = $"{type} {name} ({parameters})" + "{" + Program.Source + "}";

            return new TestProgramBuilder(Program);
        }
        
        public TestProgramBuilder WithGrammarMistake(TestGrammarMistakeType type)
        {
            Program.Source = type switch
            {
                TestGrammarMistakeType.NoBracket =>
                    Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '(' || c == '{')), 1),
                TestGrammarMistakeType.NoEol =>
                    Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == ';')), 1),
                TestGrammarMistakeType.NoAssign =>
                    Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '=')), 1),
                TestGrammarMistakeType.NoOperation =>
                    Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '+' || c == '-' || c == '*' || c == '/')), 1),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return new TestProgramBuilder(Program);
        }

        public TestProgram Build()
        {
            return Program;
        }

        private static readonly List<TestProgram> CodeExamples = new List<TestProgram>()
        {
            new TestProgram(
                key: TestSourceKey.Simple,
                source:
                @"
                a = 2;
                b = 1;
                c = a + b - 2 / 2;"),
            
            new TestProgram(
                key: TestSourceKey.If,
                source:
                @"
                a = 5;
                b = 0;
                if (a < 10)
                {
                b = 2;
                }"),

            new TestProgram(
                key: TestSourceKey.IfElse,
                source:
                @"
                a = 0;
                if (a >= 0)
                {
                b = 2;
                }
                else
                {
                b = 3;
                }"),

            new TestProgram(
                key: TestSourceKey.NestedConditions,
                source:
                @"
                a = 5;
                if(a > 3)
                {
                    if(a < 8)
                    {
                        b = a / 2;
                    }
                    else
                    {
                        b = 0;
                    }
                }
                else
                {
                    b = 1;
                }
                c = a + b;"),

            new TestProgram(
                key: TestSourceKey.CycleWhile,
                source:
                @"
                a = 6;
                b = 0;
                while(a >= 2)
                {
                    b = b + a * 2;
                    a = a - 1;
                }"),
            
            new TestProgram(
                key: TestSourceKey.CycleWhileWithConditions,
                source:
                @"
                a = 0;
                b = 0;
                while(a < 10)
                {
                    a = a + 3;
                    if(a < 7)
                    {
                        b = b + 2;
                    }
                    else
                    {
                        b = b - 1;
                    } 
                } 
                b = b / 2;
                if(b >= 5)
                {
                    a = b / 2;
                }"),
            
            new TestProgram(
                key: TestSourceKey.CycleWhileInCondition,
                source:
                @"
                a = 7;
                if(a > 5)
                {
                    b = a - 2;
                    while(a < 60)
                    {
                        a = a * 2;
                    }
                    a = a - b;
                }
                else
                {
                    b = 0;
                }"),
            
            new TestProgram(
                key: TestSourceKey.CycleFor,
                source:
                @"
                a = 5;
                for (i = 0; i < 5; i = i + 1)
                {
                    a = a + 10;
                }"),
            
            new TestProgram(
                key: TestSourceKey.CycleForWithCondition,
                source:
                @"
                a = 5;
                b = 0;
                c = 0;
                for (i = 0; i < 10; i = i + 1)
                {
                    a = a + 10;
                    if (a > 40)
                    {
                        b = b + 1;
                    }
                    else
                    {
                        c = c - 1;
                    }
                }"),
            
            new TestProgram(
                key: TestSourceKey.CycleForInCondition,
                source:
                @"
                a = 5;
                if (a <= 5)
                {
                    for (i = 0; i <= 3; i = i + 1)
                    {
                        a = a + 10;
                    }
                }
                else
                {
                    a = 0;
                }"),
            
            new TestProgram(
                key: TestSourceKey.Out,
                source:
                @"
                a = 5;
                b = 4;
                c = a + b;
                out (a);
                out (b);
                out (c);"),
            
            new TestProgram(
                key: TestSourceKey.OutInCycles,
                source:
                @"
                a = 5;
                for (i = 0; i < 5; i = i + 1)
                {
                    a = a + 10;
                    out(a);
                }
                b = 0;
                while(a > 0)
                {
                    b = b + a * 2;
                    a = a - 20;
                    out(b);
                }
                out(a);"),
            
            new TestProgram(
                key: TestSourceKey.Return,
                source:
                @"
                a = 5;
                b = 4;
                c = a + b;
                return c;"),
            
            new TestProgram(
                key: TestSourceKey.SeveralReturnsWithFirstWorking,
                source:
                @"
                a = 5;
                if (a > 0)
                {
                    return a;
                }
                return 0;"),
            
            new TestProgram(
                key: TestSourceKey.SeveralReturnsWithSecondWorking,
                source:
                @"
                a = 5;
                if (a < 0)
                {
                    return a;
                }
                return 0;")
        };
    }
}
