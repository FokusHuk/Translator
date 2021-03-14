using Translator.Core.Lexer;

namespace Translator.Core.TriadsRepresentation.Entities
{
    public class TriadOperand
    {
        public Token Token { get; set; }
        public bool IsLinkToAnotherTriad { get; set; }

        public TriadOperand(Token token, bool isLinkToAnotherTriad)
        {
            Token = token;
            IsLinkToAnotherTriad = isLinkToAnotherTriad;
        }

        public TriadOperand GetCopy()
        {
            return new TriadOperand(new Token(Token.Value, Token.Lexem), IsLinkToAnotherTriad);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TriadOperand))
                return false;

            var operand = (TriadOperand) obj;

            return Equals(operand.Token, Token) && operand.IsLinkToAnotherTriad == IsLinkToAnotherTriad;
        }
    }
}
