using System;

namespace Translator.Exceptions
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
