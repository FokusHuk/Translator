using System;

namespace Translator.Exceptions
{
    class VariableNotFoundException: Exception
    {
        public VariableNotFoundException(string value)
        : base($"Отсутствует объявление переменной \"{value}\"")
        {
        }
    }
}
