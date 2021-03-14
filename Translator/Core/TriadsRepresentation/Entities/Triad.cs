using Translator.Core.Lexer;

namespace Translator.Core.TriadsRepresentation.Entities
{
    public class Triad
    {
        public TriadOperand LeftOperand { get; set; }
        public TriadOperand RightOperand { get; set; }
        public Token Operation { get; }
        public TriadType Type { get; }

        public Triad(TriadOperand leftOperand, TriadOperand rightOperand, Token operation, TriadType type)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Operation = operation;
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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Triad))
                return false;

            var triad = (Triad) obj;

            return Equals(triad.LeftOperand, LeftOperand) &&
                   Equals(triad.RightOperand, RightOperand) &&
                   Equals(triad.Operation, Operation) &&
                   Equals(triad.Type, Type);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (LeftOperand != null ? LeftOperand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RightOperand != null ? RightOperand.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Operation != null ? Operation.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Type;
                return hashCode;
            }
        }
    }
}
