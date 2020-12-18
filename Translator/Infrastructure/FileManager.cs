using System.IO;

namespace Translator.Infrastructure
{
    public static class FileManager
    {
        public static string ReadAllFile(string filePath)
        {
            var content = "";
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    content += reader.ReadLine();
                }
            }

            return content;
        }
    }
}