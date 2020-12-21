﻿namespace Translator.Core.Triads_Representation
{
    public class Triad
    {
        public TriadOperand LeftOperand { get; }
        public TriadOperand RightOperand { get; }
        public Token Operation { get; }
        public int? Previous { get; set; }
        public int? Next { get; set; }
        public TriadType Type { get; }

        public Triad(TriadOperand leftOperand, TriadOperand rightOperand, Token operation, TriadType type)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Operation = operation;
            Type = type;
        }

        public Triad(TriadOperand leftOperand, TriadOperand rightOperand, Token operation, int previous, int next, TriadType type)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Operation = operation;
            Previous = previous;
            Next = next;
            Type = type;
        }

        public override string ToString()
        {
            return $"({LeftOperand?.Value}, {RightOperand?.Value}, {Operation.Value})";
        }
    }
}