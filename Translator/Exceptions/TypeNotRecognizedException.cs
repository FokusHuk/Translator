using System;

namespace Translator.Exceptions
{
    class TypeNotRecognizedException: Exception
    {
        public TypeNotRecognizedException(object value)
        : base($"Тип переменной \"{value}\" не был распознан ({value.GetType()}).")
        {
        }
    }
}
