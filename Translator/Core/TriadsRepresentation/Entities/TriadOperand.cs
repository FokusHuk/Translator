using Translator.Core.Lexer;

namespace Translator.Core.TriadsRepresentation.Entities
{
    public class TriadOperand
    {
        public Token Token { get; set; }
        public bool IsLinkToAnotherTriad { get; }

        public TriadOperand(Token token, bool isLinkToAnotherTriad)
        {
            Token = token;
            IsLinkToAnotherTriad = isLinkToAnotherTriad;
        }
    }
}