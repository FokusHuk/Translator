namespace Translator.Core
{
    public class FunctionDescription
    {
        public FunctionDescription(string name, int argsCount)
        {
            Name = name;
            ArgsCount = argsCount;
        }

        public string Name { get; }
        public int ArgsCount { get; }
    }
}