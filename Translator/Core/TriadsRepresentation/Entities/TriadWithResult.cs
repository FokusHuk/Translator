namespace Translator.Core.TriadsRepresentation.Entities
{
    public class TriadWithResult
    {
        public int TriadIndex { get; }
        public string Value { get; set; }

        public TriadWithResult(int triadIndex, string value)
        {
            TriadIndex = triadIndex;
            Value = value;
        }
    }
}
