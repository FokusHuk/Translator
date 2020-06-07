using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class SyntacticalAnalyzer
    {
        private List<Token> POLIS;
        private List<Token> tokens;
        private int pointer;
        private Stack<Token> stack;

        private int bracketIndex;

        public SyntacticalAnalyzer()
        {
            POLIS = new List<Token>();
            stack = new Stack<Token>();
        }

        public List<Token> convert(List<Token> tokens)
        {
            this.tokens = tokens;

            POLIS.Clear();
            stack.Clear();        
            pointer = 0;

            expression();

            return POLIS;
        }

        private void expression()
        {
            while (simpleExpression()) { }
            POLIS.Add(new Token("$", Lexem.END));
        }

        private bool simpleExpression()
        {
            Lexem currentLexem = tokens[pointer].lexem;

            if (currentLexem == Lexem.DIGIT || currentLexem == Lexem.VAR)
            {
                POLIS.Add(tokens[pointer]);
                pointer++;
            }
            else if (currentLexem == Lexem.OP || currentLexem == Lexem.ASSIGN_OP || currentLexem == Lexem.COMP_OP)
            {
                while (stack.Count != 0 && compareOperators(tokens[pointer], stack.Peek()))
                {
                    POLIS.Add(stack.Pop());
                }

                stack.Push(tokens[pointer]);
                pointer++;
            }
            else if (currentLexem == Lexem.LB)
            {
                bracketIndex++;
                stack.Push(tokens[pointer]);
                pointer++;
            }
            else if (currentLexem == Lexem.RB)
            {
                bracketIndex--;

                while (stack.Count != 0 && stack.Peek().lexem != Lexem.LB)
                {
                    POLIS.Add(stack.Pop());
                }

                stack.Pop();
                pointer++;
            }
            else if (currentLexem == Lexem.END)
            {
                freeStack();
                return false;
            }
            else if (currentLexem == Lexem.IF_KW)
            {
                ifExpression();
            }
            else if (currentLexem == Lexem.WHILE_KW)
            {
                whileExpression();
            }
            else if (currentLexem == Lexem.OUT_KW)
            {
                pointer ++;
                stack.Push(tokens[pointer]);
                pointer++;
                while (tokens[pointer].lexem != Lexem.EOL)
                {
                    simpleExpression();
                }
                POLIS.Add(new Token("&", Lexem.FUNC));
                POLIS.Add(new Token("out", Lexem.OUT_KW));
                pointer++;
            }
            else if (currentLexem == Lexem.EOL)
            {
                freeStack();
                pointer++;
            }
            else if (currentLexem == Lexem.POINT)
            {
                functionExpression();
            }
            else if (currentLexem == Lexem.LIST_KW)
            {
                POLIS.Add(tokens[pointer]);
                while (tokens[pointer].lexem != Lexem.VAR)
                {
                    pointer++;
                }
                POLIS.Add(tokens[pointer]);
                pointer++;                
            }
            else if (currentLexem == Lexem.SPC || currentLexem == Lexem.LSB)
            {
                pointer++;
            }

            return true;
        }

        private void functionExpression()
        {
            Token var = tokens[pointer - 1];
            Token func = tokens[pointer + 1];
            POLIS.RemoveAt(POLIS.Count - 1);
            pointer += 2;
            stack.Push(tokens[pointer]);
            pointer++;

            if (func.lexem == Lexem.INSERT_KW)
            {
                while (tokens[pointer].lexem != Lexem.COMMA)
                {
                    simpleExpression();
                }
                while (stack.Peek().lexem != Lexem.LB)
                {
                    POLIS.Add(stack.Pop());
                }
                pointer++;

                while (tokens[pointer].lexem != Lexem.EOL)
                {
                    simpleExpression();
                }
                pointer++;
            }
            else
            {
                bracketIndex = 1;

                while (bracketIndex != 0)
                {
                    simpleExpression();
                }
            }

            POLIS.Add(var);
            POLIS.Add(new Token("&", Lexem.FUNC));
            POLIS.Add(func);
        }

        private void freeStack()
        {
            while (stack.Count != 0)
            {
                POLIS.Add(stack.Pop());
            }
        }

        private void ifExpression()
        {
            conditionalExpression();

            int ifStartPosition = POLIS.Count;
            POLIS.Add(new Token("", Lexem.TRANS_LBL));
            POLIS.Add(new Token("!F", Lexem.F_TRANS));

            innerExpression();

            while (tokens[pointer].lexem == Lexem.SPC)
            {
                pointer++;
            }

            if (tokens[pointer].lexem == Lexem.ELSE_KW)
            {
                pointer++;
                int elseStartPosition = POLIS.Count;
                POLIS.Add(new Token("", Lexem.TRANS_LBL));
                POLIS.Add(new Token("!", Lexem.UNC_TRANS));

                POLIS[ifStartPosition].value = POLIS.Count.ToString();

                innerExpression();

                POLIS[elseStartPosition].value = POLIS.Count.ToString();
            }
            else
            {
                POLIS[ifStartPosition].value = POLIS.Count.ToString();
            }
        }

        private void whileExpression()
        {
            int startingPosition = POLIS.Count;

            conditionalExpression();

            int endPosition = POLIS.Count;
            POLIS.Add(new Token("", Lexem.TRANS_LBL));
            POLIS.Add(new Token("!F", Lexem.F_TRANS));

            innerExpression();

            POLIS.Add(new Token(startingPosition.ToString(), Lexem.TRANS_LBL));
            POLIS.Add(new Token("!", Lexem.UNC_TRANS));

            POLIS[endPosition].value = POLIS.Count.ToString();
        }

        private void conditionalExpression()
        {
            pointer++;
            while (tokens[pointer].lexem != Lexem.LSB)
            {
                simpleExpression();
            }
        }

        private void innerExpression()
        {
            pointer++;
            while (tokens[pointer].lexem != Lexem.RSB)
            {
                simpleExpression();
            }
            freeStack();
            pointer++;
        }        

        private bool compareOperators(Token op1, Token op2)
        {
            int op1Weight = 0;
            int op2Weight = 0;

            switch (op1.value)
            {
                case "=": op1Weight = 1; break;
                case ">": op1Weight = 1; break;
                case "<": op1Weight = 1; break;
                case ">=": op1Weight = 1; break;
                case "<=": op1Weight = 1; break;
                case "!=": op1Weight = 1; break;
                case "==": op1Weight = 1; break;
                case "+": op1Weight = 2; break;
                case "-": op1Weight = 2; break;
                case "*": op1Weight = 5; break;
                case "/": op1Weight = 4; break;
            }

            switch (op2.value)
            {
                case "=": op2Weight = 1; break;
                case ">": op1Weight = 1; break;
                case "<": op1Weight = 1; break;
                case ">=": op1Weight = 1; break;
                case "<=": op1Weight = 1; break;
                case "!=": op1Weight = 1; break;
                case "==": op1Weight = 1; break;
                case "+": op2Weight = 2; break;
                case "-": op2Weight = 2; break;
                case "*": op2Weight = 5; break;
                case "/": op2Weight = 4; break;
            }

            return op1Weight <= op2Weight;
        }
    }
}
