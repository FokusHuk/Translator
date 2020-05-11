using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class Parser
    {
        public Stack<GrammarMistake> Mistakes { get; set; }

        private List<Token> tokens;
        private int iteration;

        public Parser()
        {
            Mistakes = new Stack<GrammarMistake>();
        }

        public bool check(List<Token> tokens)
        {
            this.tokens = tokens;
            iteration = 0;

            if (!expr())
            {
                return false;
            }

            while (iteration < tokens.Count)
            {
                if (!expr())
                {
                    return false;
                }
            }

            return true;
        }

        private bool expr()
        {
            int currentIteration = iteration;

            if (assignExpr() || condition_expr() || whileExpr())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool assignExpr()
        {
            int currentIteration = iteration;

            if (!var() || !assignOp() || !valueExpr() || !eol())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool condition_expr()
        {
            int currentIteration = iteration;

            if (!if_kw() || !lb() || !logicExpr() || !rb() || !lsb())
            {
                reset(currentIteration);
                return false;
            }

            while (expr()) { }

            if (!rsb())
            {
                reset(currentIteration);
                return false;
            }

            while (true)
            {
                if (!else_kw()) return true;
                if (!lsb())
                {
                    reset(currentIteration);
                    return false;
                }

                while (expr()) { }

                if (!rsb())
                {
                    reset(currentIteration);
                    return false;
                }
            }
        }

        private bool whileExpr()
        {
            int currentIteration = iteration;

            if (!while_kw() || !lb() || !logicExpr() || !rb() || !lsb())
            {
                reset(currentIteration);
                return false;
            }

            while (expr()) { }

            if (!rsb())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool valueExpr()
        {
            int currentIteration = iteration;

            if (!value())
            {
                return false;
            }

            while (true)
            {
                if (!op()) return true;
                if (!value())
                {
                    reset(currentIteration);
                    return false;
                }
            }
        }

        private bool value()
        {
            int currentIteration = iteration;

            if (var() || digit() || bracketExpr())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool bracketExpr()
        {
            int currentIteration = iteration;

            if (!lb() || !valueExpr() || !rb())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool logicExpr()
        {
            int currentIteration = iteration;

            if (!valueExpr() || !comp_op() || !valueExpr())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        // Конечные грамматики

        private bool var()
        {
            return match(Lexem.VAR);
        }

        private bool digit()
        {
            return match(Lexem.DIGIT);
        }

        private bool assignOp()
        {
            return match(Lexem.ASSIGN_OP);
        }

        private bool op()
        {
            return match(Lexem.OP);
        }

        private bool lb()
        {
            return match(Lexem.LB);
        }

        private bool rb()
        {
            return match(Lexem.RB);
        }

        private bool if_kw()
        {
            return match(Lexem.IF_KW);
        }

        private bool else_kw()
        {
            return match(Lexem.ELSE_KW);
        }

        private bool lsb()
        {
            return match(Lexem.LSB);
        }

        private bool rsb()
        {
            return match(Lexem.RSB);
        }

        private bool eol()
        {
            return match(Lexem.EOL);
        }

        private bool comp_op()
        {
            return match(Lexem.COMP_OP);
        }

        private bool while_kw()
        {
            return match(Lexem.WHILE_KW);
        }

        // проверка токенов
        private bool match(Lexem requiredLexem)
        {
            if (iteration >= tokens.Count)
            {
                return false;
            }

            if (tokens[iteration].lexem == requiredLexem)
            {
                iteration++;
                return true;
            }
            else
            {
                Mistakes.Push(new GrammarMistake(iteration, tokens[iteration], requiredLexem));
                return false;
            }
        }

        private void reset(int currentIteration)
        {
            iteration = currentIteration;
        }
    }
}
