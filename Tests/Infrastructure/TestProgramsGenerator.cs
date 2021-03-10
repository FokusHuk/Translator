﻿using System;
using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Tests.Infrastructure
{
    static class TestProgramsGenerator
    {
        #region Source
        
        public static string GetSource(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple =>
                    @"
                    a = 2;
                    b = 1;
                    c = a + b - 2 / 2;",
                
                TestSourceKey.If => 
                    @"
                    a = 5;
                    b = 0;
                    if (a < 10)
                    {
                    b = 2;
                    }",
                
                TestSourceKey.IfElse => 
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
                
                TestSourceKey.NestedConditions => 
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
                
                TestSourceKey.CycleWhile => 
                    @"
                    a = 6;
                    b = 0;
                    while(a >= 2)
                    {
                        b = b + a * 2;
                        a = a - 1;
                    }",
                
                TestSourceKey.CycleWhileWithConditions => 
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
                
                TestSourceKey.CycleWhileInCondition => 
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
                
                TestSourceKey.CycleFor => 
                    @"
                    a = 5;
                    for (i = 0; i < 5; i = i + 1)
                    {
                        a = a + 10;
                    }",
                
                TestSourceKey.CycleForWithCondition => 
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
                
                TestSourceKey.CycleForInCondition => 
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
                
                TestSourceKey.Out => 
                    @"
                    a = 5;
                    b = 4;
                    c = a + b;
                    out (a);
                    out (b);
                    out (c);",
                
                TestSourceKey.OutInCycles => 
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
                
                TestSourceKey.Return => 
                    @"
                    a = 5;
                    b = 4;
                    c = a + b;
                    return c;",
                
                TestSourceKey.SeveralReturnsWithFirstWorking => 
                    @"
                    a = 5;
                    if (a > 0)
                    {
                        return a;
                    }
                    return 0;",
                
                TestSourceKey.SeveralReturnsWithSecondWorking => 
                    @"
                    a = 5;
                    if (a < 0)
                    {
                        return a;
                    }
                    return 0;",
                
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
        
        #endregion
        
        #region Tokens

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

        #endregion

        #region Polis

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

        #endregion
    }
}