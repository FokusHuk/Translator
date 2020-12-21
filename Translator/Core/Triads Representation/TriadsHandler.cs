using System;
using System.Collections.Generic;
using Translator.Core.Lexer;

namespace Translator.Core.Triads_Representation
{
    public class TriadsHandler
    {
        private struct Label
        {
            public string PolisIndex { get; set; }
            public int TriadIndex { get; set; }

            public Label(string polisIndex, int triadIndex)
            {
                PolisIndex = polisIndex;
                TriadIndex = triadIndex;
            }
        }

        private List<Triad> Triads;
        private Dictionary<string, string> Labels;
        private List<int> TriadIndexesInPolis;
        private Dictionary<string, bool> TriadOperandsWithLabel;
        private List<Token> Polis;
        private Stack<TriadOperand> Stack;
        private string LastTriadIndex => (Triads.Count - 1).ToString();

        public TriadsHandler()
        {
            Triads = new List<Triad>();
            Labels = new Dictionary<string, string>();
            TriadIndexesInPolis = new List<int>();
            TriadOperandsWithLabel = new Dictionary<string, bool>();
            Stack = new Stack<TriadOperand>();
        }
        
        public IEnumerable<Triad> GetTriadsFromPolis(List<Token> Polis)
        {
            Initialize();
            this.Polis = Polis;

            for (int i = 0; i < Polis.Count; i++)
            {
                ProcessPolisSymbol(i);
            }
// 1 5 9 16
// 14
            foreach (var key in TriadOperandsWithLabel.Keys)
            {
                if (TriadOperandsWithLabel[key])
                {
                    foreach (var triadIndexInPolis in TriadIndexesInPolis)
                    {
                        if (triadIndexInPolis >= Convert.ToInt32(Triads[Convert.ToInt32(key)].RightOperand.Value))
                        {
                            Triads[Convert.ToInt32(key)].RightOperand.Value =
                                TriadIndexesInPolis.IndexOf(triadIndexInPolis).ToString();
                            break;
                        }
                    }
                }
            }

            return Triads;
        }

        private void Initialize()
        {
            Triads.Clear();
            Labels.Clear();
            Stack.Clear();
        }

        private void ProcessPolisSymbol(int index)
        {
            var currentToken = Polis[index];

            if (currentToken.Lexem == Lexem.VAR || currentToken.Lexem == Lexem.DIGIT || currentToken.Lexem == Lexem.TRANS_LBL)
            {
                Stack.Push(new TriadOperand(currentToken.Value, false));
            }
            else if (currentToken.Lexem == Lexem.OP || currentToken.Lexem == Lexem.COMP_OP)
            {
                TriadIndexesInPolis.Add(index);
                Triads.Add(CreateTriad(2, index, currentToken));
                Stack.Push(new TriadOperand(LastTriadIndex, true));
            }
            else if (currentToken.Lexem == Lexem.ASSIGN_OP)
            {
                TriadIndexesInPolis.Add(index);
                Triads.Add(CreateTriad(2, index, currentToken));
            }
            else if (currentToken.Lexem == Lexem.F_TRANS)
            {
                TriadIndexesInPolis.Add(index);
                var rightOperand = Stack.Pop();
                var leftOperand = Stack.Pop();
                var triad = new Triad(leftOperand, rightOperand, currentToken, GetTriadType(index));
                Triads.Add(triad);
                TriadOperandsWithLabel[LastTriadIndex] = true;
            }
            else if (currentToken.Lexem == Lexem.UNC_TRANS)
            {
                TriadIndexesInPolis.Add(index);
                var operand = Stack.Pop();
                var triad = new Triad(null, operand, currentToken, GetTriadType(index));
                Triads.Add(triad);
                TriadOperandsWithLabel[LastTriadIndex] = true;
            }
            else if (currentToken.Lexem == Lexem.END)
            {
                TriadIndexesInPolis.Add(index);
                var triad = new Triad(null, null, currentToken, GetTriadType(index));
                Triads.Add(triad);
            }
        }

        private TriadType GetTriadType(int tokenIndex)
        {
            if (tokenIndex == 0)
                return TriadType.Start;
            if (tokenIndex == Polis.Count - 1)
                return TriadType.End;
            
            return TriadType.Process;
        }

        private Triad CreateTriad(int operandsCount, int index, Token currentToken)
        {
            if (operandsCount == 2)
            {
                var rightOperand = Stack.Pop();
                var leftOperand = Stack.Pop();
                return new Triad(leftOperand, rightOperand, currentToken, GetTriadType(index));
            }

            var operand = Stack.Pop();
            return new Triad(null, operand, currentToken, GetTriadType(index));
        }
    }
}