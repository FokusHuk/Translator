using System;

namespace Translator.Exceptions
{
    class LexemNotFoundException: Exception
    {
        public string Value { get; set; }

        public LexemNotFoundException(string value)
        {
            Value = value;
        }
    }
}
