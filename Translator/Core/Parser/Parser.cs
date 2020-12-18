using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Translator.Core.Parser
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

            spc();

            if (assignExpr() || condition_expr() || whileExpr() || function() || listExpr() || htExpr() || forExpr())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool assignExpr()
        {
            int currentIteration = iteration;

            if (!var() || !spc() || !assignOp() || !spc() || !valueExpr() || !spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool condition_expr()
        {
            int currentIteration = iteration;

            if (!if_kw() || !spc() || !lb() || !spc() || !logicExpr() || !spc() || !rb() || !spc() || !lsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            while (expr()) { }

            if (!spc() || !rsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            while (true)
            {
                if (!else_kw() || !spc()) return true;
                if (!lsb() || !spc())
                {
                    reset(currentIteration);
                    return false;
                }

                while (expr()) { }

                if (!spc() || !rsb() || !spc())
                {
                    reset(currentIteration);
                    return false;
                }
            }
        }

        private bool whileExpr()
        {
            int currentIteration = iteration;

            if (!while_kw() || !spc() || !lb() || !spc() || !logicExpr() || !spc() || !rb() || !spc() || !lsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            while (expr()) { }

            if (!spc() || !rsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool forExpr()
        {
            int currentIteration = iteration;

            if (!for_kw() || !spc() || !lb() || !spc() || !assignExpr() || !logicExpr() || !eol() || !spc() || !var() || !spc() || !assignOp() || !spc() || !valueExpr() || !spc() || !rb() || !spc() || !lsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            while (expr()) { }

            if (!spc() || !rsb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool function()
        {
            int currentIteration = iteration;

            if (!out_func())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        // Хеш-таблица

        private bool htExpr()
        {
            int currentIteration = iteration;

            if (htInit() || htInsert() || htDelete() || htDisplay())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htInit()
        {
            int currentIteration = iteration;

            if (ht_kw() && space_kw() && spc() && var() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htInsert()
        {
            int currentIteration = iteration;

            if (var() && point() && insert_kw() && lb() && spc() && valueExpr() && spc() && comma() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htDelete()
        {
            int currentIteration = iteration;

            if (var() && point() && delete_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htSearch()
        {
            int currentIteration = iteration;

            if (var() && point() && search_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htDisplay()
        {
            int currentIteration = iteration;

            if (var() && point() && display_kw() && lb() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        // Двусвязный список

        private bool listExpr()
        {
            int currentIteration = iteration;

            if (listInit() || listInsert() || listDelete() || listSimpleFunc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listInit()
        {
            int currentIteration = iteration;

            if (list_kw() && space_kw() && spc() && var() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listInsert()
        {
            int currentIteration = iteration;

            if (var() && point() && insert_kw() && lb() && spc() && valueExpr() && spc() && comma() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGet()
        {
            int currentIteration = iteration;

            if (listGetValue() || listGetIndex() || listSize())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGetValue()
        {
            int currentIteration = iteration;

            if (var() && point() && get_value_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGetIndex()
        {
            int currentIteration = iteration;

            if (var() && point() && get_index_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listDelete()
        {
            int currentIteration = iteration;

            if (var() && point() && delete_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listSize()
        {
            int currentIteration = iteration;

            if (var() && point() && size_kw() && lb() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listSimpleFunc()
        {
            int currentIteration = iteration;

            if (var() && point() && funcName() && lb() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool funcName()
        {
            int currentIteration = iteration;

            if (clear_kw() || display_kw())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        // Базовые грамматики

        private bool out_func()
        {
            int currentIteration = iteration;

            if (!out_kw() || !lb() || !spc() || !valueExpr() || !spc() || !rb() || !spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool valueExpr()
        {
            int currentIteration = iteration;

            if (!value() || !spc())
            {
                return false;
            }

            while (true)
            {
                if (!op() || !spc()) return true;
                if (!value() || !spc())
                {
                    reset(currentIteration);
                    return false;
                }
            }
        }

        private bool value()
        {
            int currentIteration = iteration;

            if (htSearch() || listGet() || var() || digit() || bracketExpr())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool bracketExpr()
        {
            int currentIteration = iteration;

            if (!lb() || !spc() || !valueExpr() || !spc() || !rb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool logicExpr()
        {
            int currentIteration = iteration;

            if (!valueExpr() || !spc() || !comp_op() || !spc() || !valueExpr() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool spc()
        {
            while (space_kw())
            {

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

        private bool for_kw()
        {
            return match(Lexem.FOR_KW);
        }

        private bool out_kw()
        {
            return match(Lexem.OUT_KW);
        }

        private bool list_kw()
        {
            return match(Lexem.LIST_KW);
        }

        private bool point()
        {
            return match(Lexem.POINT);
        }

        private bool comma()
        {
            return match(Lexem.COMMA);
        }

        private bool insert_kw()
        {
            return match(Lexem.INSERT_KW);
        }

        private bool get_value_kw()
        {
            return match(Lexem.GET_VALUE_KW);
        }

        private bool get_index_kw()
        {
            return match(Lexem.GET_INDEX_KW);
        }

        private bool delete_kw()
        {
            return match(Lexem.DELETE_KW);
        }

        private bool clear_kw()
        {
            return match(Lexem.CLEAR_KW);
        }

        private bool display_kw()
        {
            return match(Lexem.DISPLAY_KW);
        }

        private bool size_kw()
        {
            return match(Lexem.SIZE_KW);
        }

        private bool space_kw()
        {
            return match(Lexem.SPC);
        }

        private bool ht_kw()
        {
            return match(Lexem.HT_KW);
        }

        private bool search_kw()
        {
            return match(Lexem.SEARCH_KW);
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
