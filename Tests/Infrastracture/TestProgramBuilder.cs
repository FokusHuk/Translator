using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Tests.Infrastracture
{
    class TestProgramBuilder
    {
        private TestProgram Program;

        public TestProgramBuilder()
        {
            
        }

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
                c = a + b - 2 / 2;",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("b", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token("/", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL)
                }),
            
            new TestProgram(
                key: TestSourceKey.If,
                source:
                @"
                a = 5;
                b = 0;
                if (a < 10)
                {
                b = 2;
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),

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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">=", Lexem.COMP_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("3", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),

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
                c = a + b;",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">", Lexem.COMP_OP),
                    new Token("3", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("8", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("/", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("b", Lexem.VAR),
                    new Token(";", Lexem.EOL)
                }),

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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("6", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("while", Lexem.WHILE_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">=", Lexem.COMP_OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("a", Lexem.VAR),
                    new Token("*", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),
            
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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("while", Lexem.WHILE_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("3", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("7", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("}", Lexem.RSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("/", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("b", Lexem.VAR),
                    new Token(">=", Lexem.COMP_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("/", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),
            
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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("7", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">", Lexem.COMP_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("while", Lexem.WHILE_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("60", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("*", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("b", Lexem.VAR),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),
            
            new TestProgram(
                key: TestSourceKey.CycleFor,
                source:
                @"
                a = 5;
                for (i = 0; i < 5; i = i + 1)
                {
                    a = a + 10;
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("for", Lexem.FOR_KW),
                    new Token("(", Lexem.LB),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
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
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),
            
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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("for", Lexem.FOR_KW),
                    new Token("(", Lexem.LB),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("i", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("i", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">", Lexem.COMP_OP),
                    new Token("40", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("c", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("}", Lexem.RSB)
                }),
            
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
                }",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<=", Lexem.COMP_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("for", Lexem.FOR_KW),
                    new Token("(", Lexem.LB),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("i", Lexem.VAR),
                    new Token("<=", Lexem.COMP_OP),
                    new Token("3", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("i", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("1", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("}", Lexem.RSB),
                    new Token("else", Lexem.ELSE_KW),
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB)
                }),
            
            new TestProgram(
                key: TestSourceKey.Out,
                source:
                @"
                a = 5;
                b = 4;
                c = a + b;
                out (a);
                out (b);
                out (c);",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("4", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("b", Lexem.VAR),
                    new Token(";", Lexem.EOL),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("b", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("c", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL)
                }),
            
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
                out(a);",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("for", Lexem.FOR_KW),
                    new Token("(", Lexem.LB),
                    new Token("i", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
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
                    new Token("{", Lexem.LSB),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("10", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("while", Lexem.WHILE_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">", Lexem.COMP_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("b", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("a", Lexem.VAR),
                    new Token("*", Lexem.OP),
                    new Token("2", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("-", Lexem.OP),
                    new Token("20", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("b", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("out", Lexem.OUT_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(")", Lexem.RB),
                    new Token(";", Lexem.EOL)
                }),
            
            new TestProgram(
                key: TestSourceKey.Return,
                source:
                @"
                a = 5;
                b = 4;
                c = a + b;
                return c;",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("b", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("4", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("c", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("a", Lexem.VAR),
                    new Token("+", Lexem.OP),
                    new Token("b", Lexem.VAR),
                    new Token(";", Lexem.EOL),
                    new Token("return", Lexem.RETURN_KW),
                    new Token("c", Lexem.VAR),
                    new Token(";", Lexem.EOL)
                }),
            
            new TestProgram(
                key: TestSourceKey.SeveralReturnsWithFirstWorking,
                source:
                @"
                a = 5;
                if (a > 0)
                {
                    return a;
                }
                return 0;",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token(">", Lexem.COMP_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("return", Lexem.RETURN_KW),
                    new Token("a", Lexem.VAR),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("return", Lexem.RETURN_KW),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL)
                }),
            
            new TestProgram(
                key: TestSourceKey.SeveralReturnsWithSecondWorking,
                source:
                @"
                a = 5;
                if (a < 0)
                {
                    return a;
                }
                return 0;",
                tokens:
                new List<Token>()
                {
                    new Token("a", Lexem.VAR),
                    new Token("=", Lexem.ASSIGN_OP),
                    new Token("5", Lexem.DIGIT),
                    new Token(";", Lexem.EOL),
                    new Token("if", Lexem.IF_KW),
                    new Token("(", Lexem.LB),
                    new Token("a", Lexem.VAR),
                    new Token("<", Lexem.COMP_OP),
                    new Token("0", Lexem.DIGIT),
                    new Token(")", Lexem.RB),
                    new Token("{", Lexem.LSB),
                    new Token("return", Lexem.RETURN_KW),
                    new Token("a", Lexem.VAR),
                    new Token(";", Lexem.EOL),
                    new Token("}", Lexem.RSB),
                    new Token("return", Lexem.RETURN_KW),
                    new Token("0", Lexem.DIGIT),
                    new Token(";", Lexem.EOL)
                })
        };
    }
}
