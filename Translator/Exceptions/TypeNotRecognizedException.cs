using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class TypeNotRecognizedException: Exception
    {
        public object Value { get; set; }

        public TypeNotRecognizedException(object value)
        {
            Value = value;
        }
    }
}
