﻿using System;
using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static List<Token> GetTokens(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple =>
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
                    },

                TestSourceKey.If =>
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
                    },

                TestSourceKey.IfElse =>
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
                    },

                TestSourceKey.NestedConditions =>
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
                    },

                TestSourceKey.CycleWhile =>
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
                    },

                TestSourceKey.CycleWhileWithConditions =>
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
                    },

                TestSourceKey.CycleWhileInCondition =>
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
                    },

                TestSourceKey.CycleFor =>
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
                    },

                TestSourceKey.CycleForWithCondition =>
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
                    },

                TestSourceKey.CycleForInCondition =>
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
                    },

                TestSourceKey.Out =>
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
                    },

                TestSourceKey.OutInCycles =>
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
                    },

                TestSourceKey.Return =>
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
                    },

                TestSourceKey.SeveralReturnsWithFirstWorking =>
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
                    },

                TestSourceKey.SeveralReturnsWithSecondWorking =>
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
                    },

                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}