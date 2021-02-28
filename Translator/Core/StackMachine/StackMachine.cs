using System;
using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.DataStructures;
using Translator.Exceptions;

namespace Translator.Core.StackMachine
{
    class StackMachine
    {
        public Dictionary<string, object> Variables { get; }
        
        private List<Token> POLIS;
        private Dictionary<string, DoublyLinkedList<double>> doublyLinkedLists;
        private Dictionary<string, HashTable> hashTables;
        private Stack<object> stack;
        private int pointer;

        public StackMachine()
        {
            stack = new Stack<object>();
            Variables = new Dictionary<string, object>();
            doublyLinkedLists = new Dictionary<string, DoublyLinkedList<double>>();
            hashTables = new Dictionary<string, HashTable>();
        }

        public void calculate(List<Token> POLIS)
        {
            showHeader();
            this.POLIS = POLIS;
            stack.Clear();
            Variables.Clear();

            POLIS.Add(new Token("$", Lexem.END));
            pointer = 0;

            while (POLIS[pointer].Lexem != Lexem.END)
            {
                Lexem currentLexem = POLIS[pointer].Lexem;

                if (currentLexem == Lexem.VAR)
                {
                    stack.Push(POLIS[pointer].Value);
                }
                else if (currentLexem == Lexem.DIGIT)
                {
                    stack.Push(Convert.ToDouble(POLIS[pointer].Value));
                }
                else if (currentLexem == Lexem.OP)
                {
                    double param2 = getStackParam();
                    double param1 = getStackParam();
                    stack.Push(executeArithmeticOperation(param1, param2, POLIS[pointer].Value));
                }
                else if (currentLexem == Lexem.ASSIGN_OP)
                {
                    double param = (double)stack.Pop();
                    Variables[(string)stack.Pop()] = param;
                }
                else if (currentLexem == Lexem.COMP_OP)
                {
                    double param2 = getStackParam();
                    double param1 = getStackParam();
                    stack.Push(executeLogicOperation(param1, param2, POLIS[pointer].Value));
                }
                else if (currentLexem == Lexem.TRANS_LBL)
                {
                    stack.Push(Convert.ToInt32(POLIS[pointer].Value));
                }
                else if (currentLexem == Lexem.F_TRANS)
                {
                    int label = (int)stack.Pop();
                    bool condition = (bool)stack.Pop();

                    if (!condition)
                    {
                        pointer = label;
                        continue;
                    }
                }
                else if (currentLexem == Lexem.UNC_TRANS)
                {
                    int label = (int)stack.Pop();

                    pointer = label;
                    continue;
                }
                else if (currentLexem == Lexem.FUNC)
                {
                    pointer++;
                    
                    if (POLIS[pointer].Lexem == Lexem.OUT_KW)
                    {
                        Console.WriteLine(getStackParam());
                    }
                    else
                    {
                        collectionFunction();
                    }
                    
                }
                else if (currentLexem == Lexem.LIST_KW)
                {
                    doublyLinkedLists.Add(POLIS[pointer + 1].Value, new DoublyLinkedList<double>());
                    pointer++;
                }
                else if (currentLexem == Lexem.HT_KW)
                {
                    hashTables.Add(POLIS[pointer + 1].Value, new HashTable());
                    pointer++;
                }

                pointer++;
            }
        }

        private void showHeader()
        {
            Console.WriteLine("\nStack machine results:");
        }

        private void collectionFunction()
        {
            if (POLIS[pointer].Lexem == Lexem.INSERT_KW)
            {
                string objectName = Convert.ToString(stack.Pop());               
                if (doublyLinkedLists.ContainsKey(objectName))
                {
                    int index = (int)getStackParam();
                    double value = getStackParam();
                    doublyLinkedLists[objectName].insertAt(value, index);
                }
                else if (hashTables.ContainsKey(objectName))
                {
                    double value = getStackParam();
                    int key = (int)getStackParam();
                    hashTables[objectName].insert(key, value);
                }
            }
            else if (POLIS[pointer].Lexem == Lexem.DISPLAY_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                if (doublyLinkedLists.ContainsKey(objectName))
                {
                    doublyLinkedLists[objectName].display();
                }
                else if (hashTables.ContainsKey(objectName))
                {
                    hashTables[objectName].display();
                }
            }
            else if (POLIS[pointer].Lexem == Lexem.CLEAR_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                doublyLinkedLists[objectName].clear();
            }
            else if (POLIS[pointer].Lexem == Lexem.DELETE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int param = (int)getStackParam();
                if (doublyLinkedLists.ContainsKey(objectName))
                {
                    doublyLinkedLists[objectName].deleteAt(param);
                }
                else if (hashTables.ContainsKey(objectName))
                {
                    hashTables[objectName].delete(param);
                }
            }
            else if (POLIS[pointer].Lexem == Lexem.GET_VALUE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int index = (int)getStackParam();
                double value = doublyLinkedLists[objectName].getValue(index);
                stack.Push(value);
            }
            else if (POLIS[pointer].Lexem == Lexem.GET_INDEX_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                double value = getStackParam();
                int index = doublyLinkedLists[objectName].getIndex(value);
                stack.Push(index);
            }
            else if (POLIS[pointer].Lexem == Lexem.SIZE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int size = doublyLinkedLists[objectName].Size;
                stack.Push(size);
            }
            else if (POLIS[pointer].Lexem == Lexem.SEARCH_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int key = (int)getStackParam();
                double value = hashTables[objectName].search(key);
                stack.Push(value);
            }
        }

        private double getStackParam()
        {
            if (stack.Peek() is double || stack.Peek() is int)
            {
                return Convert.ToDouble(stack.Pop());
            }

            if (stack.Peek() is string)
            {
                if (Variables.ContainsKey((string)stack.Peek()))
                {
                    if (Variables[(string)stack.Peek()] is double)
                    {
                        return (double)Variables[(string)stack.Pop()];
                    }
                }

                throw new VariableNotFoundException((string)stack.Peek());
            }

            throw new TypeNotRecognizedException(stack.Peek());
        }

        private double executeArithmeticOperation(double param1, double param2, string op)
        {
            switch (op)
            {
                case "+": return param1 + param2;
                case "-": return param1 - param2;
                case "*": return param1 * param2;
                case "/": return param1 / param2;
                default: return 0;
            }
        }

        private bool executeLogicOperation(double param1, double param2, string op)
        {
            switch (op)
            {
                case ">": return param1 > param2;
                case "<": return param1 < param2;
                case ">=": return param1 >= param2;
                case "<=": return param1 <= param2;
                case "!=": return param1 != param2;
                case "==": return param1 == param2;
                default: return false;
            }
        }
    }
}
