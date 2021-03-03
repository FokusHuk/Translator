using System.Collections.Generic;

namespace Translator.Core
{
    public class FunctionDescription
    {
        public FunctionDescription(string name, int argsCount, bool isAsync)
        {
            Name = name;
            ArgsCount = argsCount;
            IsAsync = isAsync;
        }

        public string Name { get; }
        public int ArgsCount { get; }
        public bool IsAsync { get; }
    }
}