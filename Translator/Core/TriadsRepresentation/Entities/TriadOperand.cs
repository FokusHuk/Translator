namespace Translator.Core.TriadsRepresentation.Entities
{
    public class TriadOperand
    {
        public string Value { get; set; }
        public bool IsLinkToAnotherTriad { get; }

        public TriadOperand(string value, bool isLinkToAnotherTriad)
        {
            Value = value;
            IsLinkToAnotherTriad = isLinkToAnotherTriad;
        }
    }
}