using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Translator.Core.Parser
{
    public class Parser
    {
        private Stack<GrammarMistake> _mistakes;
        private List<Token> _tokens;
        private int _iteration;

        public Parser()
        {
            _mistakes = new Stack<GrammarMistake>();
        }

        public ParserResults Check(List<Token> tokens)
        {
            _tokens = tokens;
            _iteration = 0;
            _mistakes.Clear();

            if (!function())
            {
                return new ParserResults(false, _mistakes);
            }

            while (_iteration < tokens.Count)
            {
                if (!function())
                {
                    return new ParserResults(false, _mistakes);
                }
            }

            return new ParserResults(true, _mistakes);
        }

        #region Функции
        
        private bool function()
        {
            int currentIteration = _iteration;

            if (!func_type() || !spc() || !var() || !spc() || !lb() || !spc() || !function_args() || !spc() ||
                !rb() || !spc() || !lsb() || !spc())
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
        
        private bool function_args()
        {
            if (!var() || !spc())
                return true;

            while (true)
            {
                if (!comma())
                    return true;
                if (!spc() || !var() || !spc())
                    return false;
            }
        }
        
        private bool func_type()
        {
            if (void_t() || func_t())
                return true;

            return false;
        }

        #endregion
        
        #region Начальные грамматики

        private bool expr()
        {
            int currentIteration = _iteration;

            spc();

            if (assignExpr() || condition_expr() || whileExpr() || lang_func() || listExpr() || htExpr() || forExpr() || return_expr() || inl_ext_func())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool assignExpr()
        {
            int currentIteration = _iteration;

            if (!var() || !spc() || !assignOp() || !spc() || !valueExpr() || !spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool condition_expr()
        {
            int currentIteration = _iteration;

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
            int currentIteration = _iteration;

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
            int currentIteration = _iteration;

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

        private bool lang_func()
        {
            int currentIteration = _iteration;

            if (!out_func())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }
        
        private bool return_expr()
        {
            int currentIteration = _iteration;

            if (!return_kw() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            if (valueExpr())
            {
                if (valueExpr())
                {
                    reset(currentIteration);
                    return false;
                }
            }

            if (!spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }
            
            return true;
        }
        
        private bool inl_ext_func()
        {
            int currentIteration = _iteration;
            
            if (!ext_func() || !spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }
        
        #endregion

        #region Хеш-таблица

        private bool htExpr()
        {
            int currentIteration = _iteration;

            if (htInit() || htInsert() || htDelete() || htDisplay())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htInit()
        {
            int currentIteration = _iteration;

            if (ht_kw() && space_kw() && spc() && var() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htInsert()
        {
            int currentIteration = _iteration;

            if (var() && point() && insert_kw() && lb() && spc() && valueExpr() && spc() && comma() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htDelete()
        {
            int currentIteration = _iteration;

            if (var() && point() && delete_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htSearch()
        {
            int currentIteration = _iteration;

            if (var() && point() && search_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool htDisplay()
        {
            int currentIteration = _iteration;

            if (var() && point() && display_kw() && lb() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }
        
        #endregion

        #region Двусвязный список

        private bool listExpr()
        {
            int currentIteration = _iteration;

            if (listInit() || listInsert() || listDelete() || listSimpleFunc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listInit()
        {
            int currentIteration = _iteration;

            if (list_kw() && space_kw() && spc() && var() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listInsert()
        {
            int currentIteration = _iteration;

            if (var() && point() && insert_kw() && lb() && spc() && valueExpr() && spc() && comma() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGet()
        {
            int currentIteration = _iteration;

            if (listGetValue() || listGetIndex() || listSize())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGetValue()
        {
            int currentIteration = _iteration;

            if (var() && point() && get_value_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listGetIndex()
        {
            int currentIteration = _iteration;

            if (var() && point() && get_index_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listDelete()
        {
            int currentIteration = _iteration;

            if (var() && point() && delete_kw() && lb() && spc() && valueExpr() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listSize()
        {
            int currentIteration = _iteration;

            if (var() && point() && size_kw() && lb() && spc() && rb() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool listSimpleFunc()
        {
            int currentIteration = _iteration;

            if (var() && point() && funcName() && lb() && spc() && rb() && spc() && eol() && spc())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool funcName()
        {
            int currentIteration = _iteration;

            if (clear_kw() || display_kw())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }
        
        #endregion

        #region Базовые грамматики

        private bool out_func()
        {
            int currentIteration = _iteration;

            if (!out_kw() || !lb() || !spc() || !valueExpr() || !spc() || !rb() || !spc() || !eol() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool valueExpr()
        {
            int currentIteration = _iteration;

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
            int currentIteration = _iteration;

            if (ext_func() || htSearch() || listGet() || var() || digit() || bracketExpr())
            {
                return true;
            }

            reset(currentIteration);
            return false;
        }

        private bool bracketExpr()
        {
            int currentIteration = _iteration;

            if (!lb() || !spc() || !valueExpr() || !spc() || !rb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }

        private bool logicExpr()
        {
            int currentIteration = _iteration;

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
        
        private bool ext_func()
        {
            int currentIteration = _iteration;

            if (!var() || !spc() || !lb() || !spc() || !ext_func_args() || !spc() || !rb() || !spc())
            {
                reset(currentIteration);
                return false;
            }

            return true;
        }
        
        private bool ext_func_args()
        {
            if (!valueExpr() || !spc())
                return true;

            while (true)
            {
                if (!comma())
                    return true;
                if (!spc() || !valueExpr() || !spc())
                    return false;
            }
        }
        
        #endregion

        #region Конечные грамматики

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

        private bool return_kw()
        {
            return match(Lexem.RETURN_KW);
        }

        private bool void_t()
        {
            return match(Lexem.VOID_T);
        }

        private bool func_t()
        {
            return match(Lexem.FUNC_T);
        }
        
        #endregion

        #region Проверка токенов
        
        private bool match(Lexem requiredLexem)
        {
            if (_iteration >= _tokens.Count)
            {
                return false;
            }

            if (_tokens[_iteration].Lexem == requiredLexem)
            {
                _iteration++;
                return true;
            }

            _mistakes.Push(new GrammarMistake(_iteration, _tokens[_iteration], requiredLexem));
            return false;
        }

        private void reset(int currentIteration)
        {
            _iteration = currentIteration;
        }
        
        #endregion
    }
}
