namespace Translator.Core.Triads_Representation
{
    public class TriadOperand
    {
        public string Value { get; set; }
        public bool IsLink { get; }

        public TriadOperand(string value, bool isLink)
        {
            Value = value;
            IsLink = isLink;
        }
    }
}