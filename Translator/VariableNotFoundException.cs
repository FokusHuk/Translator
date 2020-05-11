using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
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
