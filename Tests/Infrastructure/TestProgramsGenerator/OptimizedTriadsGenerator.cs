using System;
using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static List<Triad> GetOptimizedTriads(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.If => GetTriads(key),

                TestSourceKey.IfElse => GetTriads(key),

                TestSourceKey.NestedConditions => GetTriads(key),

                TestSourceKey.CycleWhile => GetTriads(key),

                TestSourceKey.CycleWhileWithConditions => GetTriads(key),

                TestSourceKey.CycleWhileInCondition => GetTriads(key),

                TestSourceKey.CycleFor => GetTriads(key),

                TestSourceKey.CycleForWithCondition => GetTriads(key),

                TestSourceKey.CycleForInCondition => GetTriads(key),

                TestSourceKey.Out =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("4", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("9", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("4", Lexem.DIGIT), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("9", Lexem.DIGIT), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.OutInCycles => GetTriads(key),

                TestSourceKey.Return =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("4", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("9", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("9", Lexem.DIGIT), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.SeveralReturnsWithFirstWorking => GetTriads(key),

                TestSourceKey.SeveralReturnsWithSecondWorking => GetTriads(key),

                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}
