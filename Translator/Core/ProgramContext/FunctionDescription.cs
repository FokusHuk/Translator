using System.Collections.Generic;

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
        public List<string> Args { get; }
    }
}