using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.TriadsRepresentation
{
    public class TriadsConverter
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
        private List<FunctionDescription> ProgramFucntions;
        private int LastTriadIndex => Triads.Count - 1;

        private List<bool> PolisConditionIndexes;
        public List<bool> TriadsConditionIndexes;

        public TriadsConverter()
        {
            Triads = new List<Triad>();
            TriadsIndexesInPolis = new List<TriadWithPolisIndex>();
            TriadsWithUnprocessedLabel = new List<TriadWithUnprocessedLabel>();
            Stack = new Stack<TriadOperand>();
            TriadsConditionIndexes = new List<bool>();
        }
        
        public List<Triad> GetTriadsFromPolis(
            List<Token> Polis, 
            List<bool> PolisConditionIndexes, 
            List<FunctionDescription> ProgramFucntions)
        {
            this.Polis = Polis;
            this.PolisConditionIndexes = PolisConditionIndexes;
            this.ProgramFucntions = ProgramFucntions;
            Initialize();

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

            TriadsConditionIndexes = new List<bool>();
            for (int i = 0; i < PolisConditionIndexes.Count; i++)
            {
                TriadsConditionIndexes.Add(false);
            }
        }

        private void ProcessPolisToken(Token token)
        {
            if (token.Lexem == Lexem.VAR || token.Lexem == Lexem.DIGIT || token.Lexem == Lexem.TRANS_LBL)
            {
                Stack.Push(new TriadOperand(token, false));
            }
            else if (token.Lexem == Lexem.OP || token.Lexem == Lexem.COMP_OP)
            {
                CreateTriadAndSaveIndex(2, token);
                Stack.Push(new TriadOperand(new Token(LastTriadIndex.ToString(), Lexem.DIGIT), true));
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
            else if (token.Lexem == Lexem.RETURN_KW)
            {
                CreateTriadAndSaveIndex(1, token);
            }
            else if (token.Lexem == Lexem.EF_NAME)
            {
                var functionDescription = ProgramFucntions.FirstOrDefault(f => f.Name == token.Value);
                
                if (functionDescription == null)
                    throw new InvalidOperationException();

                var args = GetFunctionArguments(functionDescription.ArgsCount);
                
                Stack.Push(new TriadOperand(
                    new Token(
                        args, 
                        Lexem.VAR), 
                    false));
                
                CreateTriadAndSaveIndex(1, token);
                Stack.Push(new TriadOperand(new Token(LastTriadIndex.ToString(), Lexem.DIGIT), true));
            }
        }

        private string GetFunctionArguments(int argsCount)
        {
            var args = string.Empty;

            for (int i = 0; i < argsCount; i++)
            {
                args += Stack.Pop().Token.Value + " ";
            }

            return args;
        }

        private void CheckCurrentTokenIndexForConditionIndexes(int tokenIndex)
        {
            if (PolisConditionIndexes[tokenIndex])
                TriadsConditionIndexes[Triads.Count - 1] = true;
        }

        private void CreateTriadAndSaveIndex(int operandsCount, Token token)
        {
            var newTriad = CreateTriadByOperandsCount(operandsCount, token);
            Triads.Add(newTriad);
            TriadsIndexesInPolis.Add(new TriadWithPolisIndex(LastTriadIndex, GetPolisIndexByToken(token)));
            CheckCurrentTokenIndexForConditionIndexes(GetPolisIndexByToken(token));
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

        private void PatchUnprocessedLabels()
        {
            foreach (var triadWithUnpatchedLabel in TriadsWithUnprocessedLabel)
            {
                if (triadWithUnpatchedLabel.IsLeftOperandWithLabel)
                {
                    var labelIndexInPolis = int.Parse(Triads[triadWithUnpatchedLabel.TriadIndex].LeftOperand.Token.Value);
                    var labelIndexInTriads = TriadsIndexesInPolis
                        .First(triad => triad.PolisIndex >= labelIndexInPolis)
                        .TriadIndex;
                    Triads[triadWithUnpatchedLabel.TriadIndex].LeftOperand.Token.Value = labelIndexInTriads.ToString();
                }

                
                if (triadWithUnpatchedLabel.IsRightOperandWithLabel)
                {
                    var labelIndexInPolis = int.Parse(Triads[triadWithUnpatchedLabel.TriadIndex].RightOperand.Token.Value);
                    var labelIndexInTriads = TriadsIndexesInPolis
                        .First(triad => triad.PolisIndex >= labelIndexInPolis)
                        .TriadIndex;
                    Triads[triadWithUnpatchedLabel.TriadIndex].RightOperand.Token.Value = labelIndexInTriads.ToString();
                }
            }
        }
    }
}