﻿using System;
using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static List<Token> GetPolis(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("+", Lexem.OP),
                        new Token("2", Lexem.DIGIT),
                        new Token("2", Lexem.DIGIT),
                        new Token("/", Lexem.OP),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.If =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("14", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.IfElse =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token(">=", Lexem.COMP_OP),
                        new Token("13", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("16", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("3", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.NestedConditions =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("3", Lexem.DIGIT),
                        new Token(">", Lexem.COMP_OP),
                        new Token("25", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("8", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("20", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("/", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("23", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("28", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleWhile =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("6", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token(">=", Lexem.COMP_OP),
                        new Token("25", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("*", Lexem.OP),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("6", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleWhileWithConditions =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("35", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("3", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("7", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("28", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("33", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("6", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("/", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token(">=", Lexem.COMP_OP),
                        new Token("50", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("/", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleWhileInCondition =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("7", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token(">", Lexem.COMP_OP),
                        new Token("32", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("60", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("25", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("*", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("13", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("35", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleFor =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("27", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("20", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("i", Lexem.VAR),
                        new Token("i", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("6", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("13", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleForWithCondition =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("50", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("26", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("i", Lexem.VAR),
                        new Token("i", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("12", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("40", Lexem.DIGIT),
                        new Token(">", Lexem.COMP_OP),
                        new Token("43", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("48", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("c", Lexem.VAR),
                        new Token("c", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("19", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.CycleForInCondition =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("<=", Lexem.COMP_OP),
                        new Token("34", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("i", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("3", Lexem.DIGIT),
                        new Token("<=", Lexem.COMP_OP),
                        new Token("32", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("25", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("i", Lexem.VAR),
                        new Token("i", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("11", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("18", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("37", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.Out =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("4", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("b", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("c", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.OutInCycles =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("i", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("30", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("20", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("i", Lexem.VAR),
                        new Token("i", Lexem.VAR),
                        new Token("1", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("6", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("10", Lexem.DIGIT),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("13", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token(">", Lexem.COMP_OP),
                        new Token("55", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("b", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("2", Lexem.DIGIT),
                        new Token("*", Lexem.OP),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("20", Lexem.DIGIT),
                        new Token("-", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("33", Lexem.TRANS_LBL),
                        new Token("!", Lexem.UNC_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("&", Lexem.FUNC),
                        new Token("out", Lexem.OUT_KW),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.Return =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("b", Lexem.VAR),
                        new Token("4", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("a", Lexem.VAR),
                        new Token("b", Lexem.VAR),
                        new Token("+", Lexem.OP),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("c", Lexem.VAR),
                        new Token("RET", Lexem.RETURN_KW),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.SeveralReturnsWithFirstWorking =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token(">", Lexem.COMP_OP),
                        new Token("10", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("RET", Lexem.RETURN_KW),
                        new Token("0", Lexem.DIGIT),
                        new Token("RET", Lexem.RETURN_KW),
                        new Token("$", Lexem.END)
                    },

                TestSourceKey.SeveralReturnsWithSecondWorking =>
                    new List<Token>()
                    {
                        new Token("a", Lexem.VAR),
                        new Token("5", Lexem.DIGIT),
                        new Token("=", Lexem.ASSIGN_OP),
                        new Token("a", Lexem.VAR),
                        new Token("0", Lexem.DIGIT),
                        new Token("<", Lexem.COMP_OP),
                        new Token("10", Lexem.TRANS_LBL),
                        new Token("!F", Lexem.F_TRANS),
                        new Token("a", Lexem.VAR),
                        new Token("RET", Lexem.RETURN_KW),
                        new Token("0", Lexem.DIGIT),
                        new Token("RET", Lexem.RETURN_KW),
                        new Token("$", Lexem.END)
                    },

                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}
