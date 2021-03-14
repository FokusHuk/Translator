namespace Translator.Core.Lexer
{
    public class Token
    {
        public string Value { get; set; }
        public Lexem Lexem { get; set; }

        public Token(string value, Lexem lexem)
        {
            Lexem = lexem;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} - {Lexem.Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Token))
                return false;

            var token = (Token) obj;

            return token.Value == Value && token.Lexem == Lexem;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() + Lexem.GetHashCode();
        }
    }
}
