using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Translator.Core.Analyzer
{
    class SyntacticalAnalyzer
    {
        private List<Token> POLIS;
        private List<Token> tokens;
        private int pointer;
        private Stack<Token> stack;
        private int bracketIndex;

        public List<bool> PolisConditionsIndexes;

        public SyntacticalAnalyzer()
        {
            POLIS = new List<Token>();
            stack = new Stack<Token>();
            PolisConditionsIndexes = new List<bool>();
        }

        public List<Token> Convert(List<Token> tokens)
        {
            tokens.Add(new Token("$", Lexem.END));
            this.tokens = tokens;

            Initialize();

            POLIS.Clear();
            stack.Clear();
            pointer = 0;

            expression();

            return POLIS;
        }

        private void expression()
        {
            while (simpleExpression()) { }
        }

        private bool simpleExpression()
        {
            Lexem currentLexem = tokens[pointer].Lexem;

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

                while (stack.Count != 0 && stack.Peek().Lexem != Lexem.LB)
                {
                    POLIS.Add(stack.Pop());
                }

                stack.Pop();
                pointer++;
            }
            else if (currentLexem == Lexem.END)
            {
                freeStack();
                POLIS.Add(tokens[pointer]);
                return false;
            }
            else if (currentLexem == Lexem.IF_KW)
            {
                var pointerBeforeCondition = POLIS.Count;
                ifExpression();
                MarkConditionIndexes(pointerBeforeCondition, POLIS.Count - 1);
            }
            else if (currentLexem == Lexem.WHILE_KW)
            {
                var pointerBeforeCondition = POLIS.Count;
                whileExpression();
                MarkConditionIndexes(pointerBeforeCondition, POLIS.Count - 1);
            }
            else if (currentLexem == Lexem.FOR_KW)
            {
                var pointerBeforeCondition = POLIS.Count;
                forExpression();
                MarkConditionIndexes(pointerBeforeCondition, POLIS.Count - 1);
            }
            else if (currentLexem == Lexem.OUT_KW)
            {
                pointer ++;
                stack.Push(tokens[pointer]);
                pointer++;
                while (tokens[pointer].Lexem != Lexem.EOL)
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
            else if (currentLexem == Lexem.LIST_KW || currentLexem == Lexem.HT_KW)
            {
                POLIS.Add(tokens[pointer]);
                while (tokens[pointer].Lexem != Lexem.VAR)
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
            // return;     return a + b;
            else if (currentLexem == Lexem.RETURN_KW)
            {
                var tempPointer = pointer + 1;
                while (tokens[tempPointer].Lexem == Lexem.SPC) tempPointer++;
                
                if (tokens[tempPointer].Lexem == Lexem.EOL)
                {
                    POLIS.Add(new Token("RET", currentLexem));
                    pointer = tempPointer + 1;
                }
                else
                {
                    pointer++;
                    while (tokens[pointer].Lexem != Lexem.EOL)
                    {
                        simpleExpression();
                    }
                    freeStack();
                    POLIS.Add(new Token("RET", currentLexem));
                    pointer++;
                }
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

            if (func.Lexem == Lexem.INSERT_KW)
            {
                while (tokens[pointer].Lexem != Lexem.COMMA)
                {
                    simpleExpression();
                }
                while (stack.Peek().Lexem != Lexem.LB)
                {
                    POLIS.Add(stack.Pop());
                }
                pointer++;

                while (tokens[pointer].Lexem != Lexem.EOL)
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

            while (tokens[pointer].Lexem == Lexem.SPC)
            {
                pointer++;
            }

            if (tokens[pointer].Lexem == Lexem.ELSE_KW)
            {
                pointer++;
                int elseStartPosition = POLIS.Count;
                POLIS.Add(new Token("", Lexem.TRANS_LBL));
                POLIS.Add(new Token("!", Lexem.UNC_TRANS));

                POLIS[ifStartPosition].Value = POLIS.Count.ToString();

                innerExpression();

                POLIS[elseStartPosition].Value = POLIS.Count.ToString();
            }
            else
            {
                POLIS[ifStartPosition].Value = POLIS.Count.ToString();
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

            POLIS[endPosition].Value = POLIS.Count.ToString();
        }

        private void forExpression()
        {
            int startPosition;
            int conditionTransition;
            int bodyTransition;
            int iterationPosition;

            while(tokens[pointer].Lexem != Lexem.LB)
            {
                pointer++;
            }
            pointer++;

            // for (i = 0;
            while (tokens[pointer].Lexem != Lexem.EOL)
            {
                simpleExpression();
            }
            freeStack();
            pointer++;

            startPosition = POLIS.Count;

            // i < n;
            while (tokens[pointer].Lexem != Lexem.EOL)
            {
                simpleExpression();
            }
            freeStack();
            pointer++;

            conditionTransition = POLIS.Count;
            POLIS.Add(new Token("", Lexem.TRANS_LBL));
            POLIS.Add(new Token("!F", Lexem.F_TRANS));

            bodyTransition = POLIS.Count;
            POLIS.Add(new Token("", Lexem.TRANS_LBL));
            POLIS.Add(new Token("!", Lexem.UNC_TRANS));

            // i = i + 1)
            iterationPosition = POLIS.Count;
            stack.Push(new Token("(", Lexem.LB));
            bracketIndex = 1;
            while (bracketIndex != 0)
            {
                simpleExpression();
            }

            POLIS.Add(new Token(startPosition.ToString(), Lexem.TRANS_LBL));
            POLIS.Add(new Token("!", Lexem.UNC_TRANS));
            POLIS[bodyTransition].Value = POLIS.Count.ToString();

            // { body }
            while (tokens[pointer].Lexem != Lexem.RSB)
            {
                simpleExpression();
            }
            pointer++;

            POLIS.Add(new Token(iterationPosition.ToString(), Lexem.TRANS_LBL));
            POLIS.Add(new Token("!", Lexem.UNC_TRANS));
            POLIS[conditionTransition].Value = POLIS.Count.ToString();
        }

        private void conditionalExpression()
        {
            pointer++;
            while (tokens[pointer].Lexem != Lexem.LSB)
            {
                simpleExpression();
            }
        }

        private void innerExpression()
        {
            pointer++;
            while (tokens[pointer].Lexem != Lexem.RSB)
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

            switch (op1.Value)
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

            switch (op2.Value)
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

        private void Initialize()
        {
            PolisConditionsIndexes = new List<bool>();
            
            for (int i = 0; i < tokens.Count * 2; i++)
            {
                PolisConditionsIndexes.Add(false);
            }
        }

        private void MarkConditionIndexes(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                PolisConditionsIndexes[i] = true;
            }
        }
    }
}
