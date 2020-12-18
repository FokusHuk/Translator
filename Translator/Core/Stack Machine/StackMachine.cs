﻿using System;
using System.Collections.Generic;
using Translator.Core.Lexer;
using Translator.DataStructures;
using Translator.Exceptions;

namespace Translator.Core.Stack_Machine
{
    class StackMachine
    {
        public List<Token> POLIS { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public Dictionary<string, DoublyLinkedList<double>> Lists { get; set; }
        public Dictionary<string, HashTable> HTables { get; set; }
        private Stack<object> stack;
        private int pointer;

        public StackMachine()
        {
            stack = new Stack<object>();
            Variables = new Dictionary<string, object>();
            Lists = new Dictionary<string, DoublyLinkedList<double>>();
            HTables = new Dictionary<string, HashTable>();
        }

        public void calculate(List<Token> POLIS)
        {
            this.POLIS = POLIS;
            stack.Clear();
            Variables.Clear();

            POLIS.Add(new Token("$", Lexem.END));
            pointer = 0;

            while (POLIS[pointer].lexem != Lexem.END)
            {
                Lexem currentLexem = POLIS[pointer].lexem;

                if (currentLexem == Lexem.VAR)
                {
                    stack.Push(POLIS[pointer].value);
                }
                else if (currentLexem == Lexem.DIGIT)
                {
                    stack.Push(Convert.ToDouble(POLIS[pointer].value));
                }
                else if (currentLexem == Lexem.OP)
                {
                    double param2 = getStackParam();
                    double param1 = getStackParam();
                    stack.Push(executeArithmeticOperation(param1, param2, POLIS[pointer].value));
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
                    stack.Push(executeLogicOperation(param1, param2, POLIS[pointer].value));
                }
                else if (currentLexem == Lexem.TRANS_LBL)
                {
                    stack.Push(Convert.ToInt32(POLIS[pointer].value));
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
                    
                    if (POLIS[pointer].lexem == Lexem.OUT_KW)
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
                    Lists.Add(POLIS[pointer + 1].value, new DoublyLinkedList<double>());
                    pointer++;
                }
                else if (currentLexem == Lexem.HT_KW)
                {
                    HTables.Add(POLIS[pointer + 1].value, new HashTable());
                    pointer++;
                }

                pointer++;
            }
        }

        private void collectionFunction()
        {
            if (POLIS[pointer].lexem == Lexem.INSERT_KW)
            {
                string objectName = Convert.ToString(stack.Pop());               
                if (Lists.ContainsKey(objectName))
                {
                    int index = (int)getStackParam();
                    double value = getStackParam();
                    Lists[objectName].insertAt(value, index);
                }
                else if (HTables.ContainsKey(objectName))
                {
                    double value = getStackParam();
                    int key = (int)getStackParam();
                    HTables[objectName].insert(key, value);
                }
            }
            else if (POLIS[pointer].lexem == Lexem.DISPLAY_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                if (Lists.ContainsKey(objectName))
                {
                    Lists[objectName].display();
                }
                else if (HTables.ContainsKey(objectName))
                {
                    HTables[objectName].display();
                }
            }
            else if (POLIS[pointer].lexem == Lexem.CLEAR_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                Lists[objectName].clear();
            }
            else if (POLIS[pointer].lexem == Lexem.DELETE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int param = (int)getStackParam();
                if (Lists.ContainsKey(objectName))
                {
                    Lists[objectName].deleteAt(param);
                }
                else if (HTables.ContainsKey(objectName))
                {
                    HTables[objectName].delete(param);
                }
            }
            else if (POLIS[pointer].lexem == Lexem.GET_VALUE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int index = (int)getStackParam();
                double value = Lists[objectName].getValue(index);
                stack.Push(value);
            }
            else if (POLIS[pointer].lexem == Lexem.GET_INDEX_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                double value = getStackParam();
                int index = Lists[objectName].getIndex(value);
                stack.Push(index);
            }
            else if (POLIS[pointer].lexem == Lexem.SIZE_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int size = Lists[objectName].Size;
                stack.Push(size);
            }
            else if (POLIS[pointer].lexem == Lexem.SEARCH_KW)
            {
                string objectName = Convert.ToString(stack.Pop());
                int key = (int)getStackParam();
                double value = HTables[objectName].search(key);
                stack.Push(value);
            }
        }

        private double getStackParam()
        {
            if (stack.Peek() is double || stack.Peek() is int)
            {
                return Convert.ToDouble(stack.Pop());
            }
            else if (stack.Peek() is string)
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
