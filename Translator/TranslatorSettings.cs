namespace Translator
{
    public class TranslatorSettings
    {
        public TranslatorSettings(string sourceFilePath)
        {
            SourceFilePath = sourceFilePath;
        }

        public string SourceFilePath { get; }
    }
}