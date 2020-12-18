using System;

namespace Translator.Exceptions
{
    class LexemNotFoundException: Exception
    {
        public LexemNotFoundException(string value)
        :base($"Lexem \"{value}\" not recognized")
        {
        }
    }
}
