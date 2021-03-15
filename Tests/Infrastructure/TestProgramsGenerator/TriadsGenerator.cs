using System;
using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static List<Triad> GetTriads(TestSourceKey key) =>
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
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("/", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("3", Lexem.DIGIT), true),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("4", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.If =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("5", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.IfElse =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token(">=", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("5", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("6", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("3", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.NestedConditions =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("3", Lexem.DIGIT), false),
                            new Token(">", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("10", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("8", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("3", Lexem.DIGIT), true),
                            new TriadOperand(new Token("8", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("/", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("9", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("11", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("11", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleWhile =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("6", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token(">=", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("10", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("*", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("4", Lexem.DIGIT), true),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("2", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleWhileWithConditions =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("14", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("3", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("4", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("6", Lexem.DIGIT), true),
                            new TriadOperand(new Token("11", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("8", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("13", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("11", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("2", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("/", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("14", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token(">=", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("16", Lexem.DIGIT), true),
                            new TriadOperand(new Token("20", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("/", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("18", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleWhileInCondition =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token(">", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("13", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("3", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("60", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("5", Lexem.DIGIT), true),
                            new TriadOperand(new Token("10", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("*", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("5", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("14", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleFor =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("11", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("8", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("2", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("8", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("5", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleForWithCondition =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("4", Lexem.DIGIT), true),
                            new TriadOperand(new Token("20", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("10", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("4", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("40", Lexem.DIGIT), false),
                            new Token(">", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("12", Lexem.DIGIT), true),
                            new TriadOperand(new Token("17", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("14", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("19", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("17", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("7", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.CycleForInCondition =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("<=", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("14", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("3", Lexem.DIGIT), false),
                            new Token("<=", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("4", Lexem.DIGIT), true),
                            new TriadOperand(new Token("13", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("10", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("7", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("4", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("7", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("15", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

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
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.OutInCycles =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new TriadOperand(new Token("12", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("8", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("1", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("i", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("2", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("10", Lexem.DIGIT), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("8", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("5", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token(">", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("13", Lexem.DIGIT), true),
                            new TriadOperand(new Token("22", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), false),
                            new Token("*", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("15", Lexem.DIGIT), true),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new TriadOperand(new Token("16", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("20", Lexem.DIGIT), false),
                            new Token("-", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("18", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("13", Lexem.TRANS_LBL), false),
                            new Token("!", Lexem.UNC_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new Token("out", Lexem.OUT_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

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
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("b", Lexem.VAR), false),
                            new Token("+", Lexem.OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new TriadOperand(new Token("2", Lexem.DIGIT), true),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("c", Lexem.VAR), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.SeveralReturnsWithFirstWorking =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token(">", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("4", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                TestSourceKey.SeveralReturnsWithSecondWorking =>
                    new List<Triad>()
                    {
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("5", Lexem.DIGIT), false),
                            new Token("=", Lexem.ASSIGN_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("<", Lexem.COMP_OP), TriadType.Process),
                        new Triad(
                            new TriadOperand(new Token("1", Lexem.DIGIT), true),
                            new TriadOperand(new Token("4", Lexem.TRANS_LBL), false),
                            new Token("!F", Lexem.F_TRANS), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("a", Lexem.VAR), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            new TriadOperand(new Token("0", Lexem.DIGIT), false),
                            new Token("RET", Lexem.RETURN_KW), TriadType.Process),
                        new Triad(
                            null,
                            null,
                            new Token("$", Lexem.END), TriadType.End),
                    },

                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}
