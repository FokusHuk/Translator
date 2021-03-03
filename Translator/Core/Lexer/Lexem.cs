using System.Collections.Generic;

namespace Translator.Core.Lexer
{
    public class Lexem
    {
        public string Name { get; }
        public string Value { get; }

        public Lexem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public static readonly Lexem VAR = new Lexem("VAR", @"^([a-zA-Z]+)$");
        public static readonly Lexem DIGIT = new Lexem("DIGIT", @"^(0|[1-9][0-9]*)$");
        public static readonly Lexem ASSIGN_OP = new Lexem("ASSIGN_OP", @"^=$");
        public static readonly Lexem OP = new Lexem("OP", @"^(\+|-|\*|\/)$");
        public static readonly Lexem LB = new Lexem("LB", @"^\($");
        public static readonly Lexem RB = new Lexem("RB", @"^\)$");
        public static readonly Lexem IF_KW = new Lexem("IF_KW", @"^if$");
        public static readonly Lexem ELSE_KW = new Lexem("ELSE_KW", @"^else$");
        public static readonly Lexem LSB = new Lexem("LSB", @"^\{$");
        public static readonly Lexem RSB = new Lexem("RSB", @"^\}$");
        public static readonly Lexem COMP_OP = new Lexem("COMP_OP", @"^(>=|<=|>|<|!=|==)$");
        public static readonly Lexem WHILE_KW = new Lexem("WHILE_KW", @"^while$");
        public static readonly Lexem FOR_KW = new Lexem("FOR_KW", @"^for$");
        public static readonly Lexem EOL = new Lexem("EOL", @"^;$");
        public static readonly Lexem SPC = new Lexem("SPC", @"^ $");
        public static readonly Lexem OUT_KW = new Lexem("OUT_KW", @"^out$");

        public static readonly Lexem LIST_KW = new Lexem("LIST_KW", @"^list$");
        public static readonly Lexem POINT = new Lexem("POINT", @"^\.$");
        public static readonly Lexem COMMA = new Lexem("COMMA", @"^,$");
        public static readonly Lexem INSERT_KW = new Lexem("INSERT_KW", @"^insert$");
        public static readonly Lexem GET_VALUE_KW = new Lexem("GET_VALUE_KW", @"^getValue$");
        public static readonly Lexem GET_INDEX_KW = new Lexem("GET_INDEX_KW", @"^getIndex$");
        public static readonly Lexem DELETE_KW = new Lexem("DELETE_KW", @"^delete$");
        public static readonly Lexem CLEAR_KW = new Lexem("CLEAR_KW", @"^clear$");
        public static readonly Lexem DISPLAY_KW = new Lexem("DISPLAY_KW", @"^display$");
        public static readonly Lexem SIZE_KW = new Lexem("SIZE_KW", @"^size$");
        public static readonly Lexem HT_KW = new Lexem("HT_KW", @"^htable$");
        public static readonly Lexem SEARCH_KW = new Lexem("SEARCH_KW", @"^search$");

        public static readonly Lexem END = new Lexem("END", "");
        public static readonly Lexem F_TRANS = new Lexem("F_TRANS", "");
        public static readonly Lexem T_TRANS = new Lexem("T_TRANS", "");
        public static readonly Lexem UNC_TRANS = new Lexem("UNC_TRANS", "");
        public static readonly Lexem TRANS_LBL = new Lexem("TRANS_LBL", "");
        public static readonly Lexem FUNC = new Lexem("FUNC", "");
        
        public static readonly Lexem VOID_T = new Lexem("VOID_T", @"^void$");
        public static readonly Lexem FUNC_T = new Lexem("FUNC_T", @"^func$");
        public static readonly Lexem RETURN_KW = new Lexem("RETURN_KW", @"^return$");
        public static readonly Lexem EF_NAME = new Lexem("EXT_FUNC_NAME", @"^([a-zA-Z]+)$");
        public static readonly Lexem ASYNC_KW = new Lexem("ASYNC_KW", @"^async$");

        public static List<Lexem> GetAll()
        {
            return new List<Lexem>
            {
                VAR, DIGIT, ASSIGN_OP, OP, LB, RB, IF_KW, ELSE_KW, LSB, RSB, COMP_OP, WHILE_KW, FOR_KW, EOL, 
                SPC, OUT_KW, LIST_KW, POINT, COMMA, SPC, INSERT_KW, GET_VALUE_KW, GET_VALUE_KW, GET_INDEX_KW,
                DELETE_KW, CLEAR_KW, DISPLAY_KW, SIZE_KW, HT_KW, SEARCH_KW, VOID_T, FUNC_T, RETURN_KW, ASYNC_KW
            };
        }
    }
}
