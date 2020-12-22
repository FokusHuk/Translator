using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Translator.Core.Triads_Representation
{
    public class TriadsHandler
    {
        private readonly struct TriadWithUnprocessedLabel
        {
            public int TriadIndex { get; }
            public bool IsLeftOperandWithLabel { get; }
            public bool IsRightOperandWithLabel { get; }

            public TriadWithUnprocessedLabel(int triadIndex, bool isLeftOperandWithLabel, bool isRightOperandWithLabel)
            {
                TriadIndex = triadIndex;
                IsLeftOperandWithLabel = isLeftOperandWithLabel;
                IsRightOperandWithLabel = isRightOperandWithLabel;
            }
        }

        private readonly struct TriadWithPolisIndex
        {
            public int TriadIndex { get; }
            public int PolisIndex { get; }

            public TriadWithPolisIndex(int triadIndex, int polisIndex)
            {
                TriadIndex = triadIndex;
                PolisIndex = polisIndex;
            }
        }

        private List<Triad> Triads;
        private List<TriadWithPolisIndex> TriadsIndexesInPolis;
        private List<TriadWithUnprocessedLabel> TriadsWithUnprocessedLabel;
        private List<Token> Polis;
        private Stack<TriadOperand> Stack;
        private int LastTriadIndex => Triads.Count - 1;

        public TriadsHandler()
        {
            Triads = new List<Triad>();
            TriadsIndexesInPolis = new List<TriadWithPolisIndex>();
            TriadsWithUnprocessedLabel = new List<TriadWithUnprocessedLabel>();
            Stack = new Stack<TriadOperand>();
        }
        
        public IEnumerable<Triad> GetTriadsFromPolis(List<Token> Polis)
        {
            Initialize();
            this.Polis = Polis;

            foreach (var token in Polis)
            {
                ProcessPolisToken(token);
            }

            PatchUnprocessedLabels();

            return Triads;
        }

        private void Initialize()
        {
            Triads.Clear();
            TriadsIndexesInPolis.Clear();
            TriadsWithUnprocessedLabel.Clear();
            Stack.Clear();
        }

        private void PatchUnprocessedLabels()
        {
            foreach (var triadWithUnpatchedLabel in TriadsWithUnprocessedLabel)
            {
                if (triadWithUnpatchedLabel.IsLeftOperandWithLabel)
                {
                    var labelIndexInPolis = int.Parse(Triads[triadWithUnpatchedLabel.TriadIndex].LeftOperand.Value);
                    var labelIndexInTriads = TriadsIndexesInPolis
                        .First(triad => triad.PolisIndex >= labelIndexInPolis)
                        .TriadIndex;
                    Triads[triadWithUnpatchedLabel.TriadIndex].LeftOperand.Value = labelIndexInTriads.ToString();
                }

                
                if (triadWithUnpatchedLabel.IsRightOperandWithLabel)
                {
                    var labelIndexInPolis = int.Parse(Triads[triadWithUnpatchedLabel.TriadIndex].RightOperand.Value);
                    var labelIndexInTriads = TriadsIndexesInPolis
                        .First(triad => triad.PolisIndex >= labelIndexInPolis)
                        .TriadIndex;
                    Triads[triadWithUnpatchedLabel.TriadIndex].RightOperand.Value = labelIndexInTriads.ToString();
                }
            }
        }

        private void ProcessPolisToken(Token token)
        {
            if (token.Lexem == Lexem.VAR || token.Lexem == Lexem.DIGIT || token.Lexem == Lexem.TRANS_LBL)
            {
                Stack.Push(new TriadOperand(token.Value, false));
            }
            else if (token.Lexem == Lexem.OP || token.Lexem == Lexem.COMP_OP)
            {
                CreateTriadAndSaveIndex(2, token);
                Stack.Push(new TriadOperand(LastTriadIndex.ToString(), true));
            }
            else if (token.Lexem == Lexem.ASSIGN_OP)
            {
                CreateTriadAndSaveIndex(2, token);
            }
            else if (token.Lexem == Lexem.F_TRANS)
            {
                CreateTriadAndSaveIndex(2, token);
                TriadsWithUnprocessedLabel.Add(new TriadWithUnprocessedLabel(LastTriadIndex, false, true));
            }
            else if (token.Lexem == Lexem.UNC_TRANS)
            {
                CreateTriadAndSaveIndex(1, token);
                TriadsWithUnprocessedLabel.Add(new TriadWithUnprocessedLabel(LastTriadIndex, false, true));
            }
            else if (token.Lexem == Lexem.END)
            {
                CreateTriadAndSaveIndex(0, token);
            }
        }

        private void CreateTriadAndSaveIndex(int operandsCount, Token token)
        {
            var newTriad = CreateTriadByOperandsCount(operandsCount, token);
            Triads.Add(newTriad);
            TriadsIndexesInPolis.Add(new TriadWithPolisIndex(LastTriadIndex, GetPolisIndexByToken(token)));
        }

        private Triad CreateTriadByOperandsCount(int operandsCount, Token token)
        {
            var polisIndex = GetPolisIndexByToken(token);
            switch (operandsCount)
            {
                case 2:
                    var rightOperand = Stack.Pop();
                    var leftOperand = Stack.Pop();
                    return new Triad(leftOperand, rightOperand, token, GetTriadType(polisIndex));
                case 1:
                    var operand = Stack.Pop();
                    return new Triad(null, operand, token, GetTriadType(polisIndex));
                default:
                    return new Triad(null, null, token, GetTriadType(polisIndex));
            }
        }

        private int GetPolisIndexByToken(Token token) => Polis.IndexOf(token);

        private TriadType GetTriadType(int polisIndex)
        {
            if (polisIndex == 0)
                return TriadType.Start;
            if (polisIndex == Polis.Count - 1)
                return TriadType.End;
            
            return TriadType.Process;
        }
    }
}