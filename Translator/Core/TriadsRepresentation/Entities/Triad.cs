using Translator.Core.Lexer;

namespace Translator.Core.TriadsRepresentation.Entities
{
    public class Triad
    {
        public TriadOperand LeftOperand { get; set; }
        public TriadOperand RightOperand { get; set; }
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


        public static Triad Empty => EmptyTriad;

        public override string ToString()
        {
            var left = LeftOperand != null && LeftOperand.IsLinkToAnotherTriad ? "." + LeftOperand?.Token.Value : LeftOperand?.Token.Value;
            var right = RightOperand != null && RightOperand.IsLinkToAnotherTriad ? "." + RightOperand?.Token.Value : RightOperand?.Token.Value;
            return $"({left}, {right}, {Operation.Value})";
        }

        private static readonly Triad EmptyTriad = new Triad(
            new TriadOperand(new Token("", Lexem.VAR), false),
            new TriadOperand(new Token("", Lexem.VAR), false),
            new Token("", Lexem.OP),
            TriadType.Process);
    }
}