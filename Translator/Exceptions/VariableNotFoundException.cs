using System;

namespace Translator.Exceptions
{
    class VariableNotFoundException: Exception
    {
        public string Value { get; set; }

        public VariableNotFoundException(string value)
        {
            Value = value;
        }
    }
}
