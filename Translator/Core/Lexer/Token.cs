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
    }
}
