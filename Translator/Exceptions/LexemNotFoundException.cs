using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
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
