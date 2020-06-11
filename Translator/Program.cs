using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            Console.WriteLine("Program source:");
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\Code.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string currentLine = reader.ReadLine();
                    input += currentLine + " ";
                    Console.WriteLine(currentLine);
                }
            }
            Console.WriteLine("\n");
            
            List<Token> tokens = null;

            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            SyntacticalAnalyzer syntAnalyzer = new SyntacticalAnalyzer();
            StackMachine stackMachine = new StackMachine();

            // Получение списка токенов лексером
            try
            {
                tokens = lexer.execute(input);
            }
            catch (LexemNotFoundException excp)
            {
                Console.WriteLine("Лексема \"{0}\" не была распознана.", excp.Value);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            // Вывод списка токенов
            Console.WriteLine("Lexer results:");
            foreach (Token token in tokens)
            {
                Console.WriteLine("{0}\t<==>\t{1}", token.value , token.lexem.name);
            }

            // Проверка корректности выражения парсером
            Console.WriteLine("\nParser results:");
            bool parserResult = parser.check(tokens);
            Console.WriteLine(parserResult);
            if (!parserResult)
            {
                Console.WriteLine("\nСтек ошибок");
                while (parser.Mistakes.Count != 0)
                    Console.WriteLine(parser.Mistakes.Pop());
                Console.ReadKey();
                Environment.Exit(-1);
            }

            // Перевод в польскую запись
            Console.WriteLine("\nSyntactical analyzer results:");
            tokens.Add(new Token("$", Lexem.END));
            List<Token> POLIS = syntAnalyzer.convert(tokens);
            Console.WriteLine("POLIS: ");
            int i = 0;
            int delta = 25;
            while (i < POLIS.Count)
            {
                int i_pos = i + delta;
                if (i_pos > POLIS.Count)
                {
                    delta -= i_pos - POLIS.Count;
                    i_pos = POLIS.Count;                   
                }
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                for (; i < i_pos; i++)
                {
                    Console.Write("{0}", i);
                    for (int j = 0; j < POLIS[i].value.Length + 3 - i.ToString().Length; j++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                i -= delta;
                for (; i < i_pos; i++)
                {
                    Console.Write("{0}   ", POLIS[i].value);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            // Вычисление выражения в польской нотации
            Console.WriteLine("\nStack machine results:");
            try
            {
                stackMachine.calculate(POLIS);
            }
            catch (VariableNotFoundException excp)
            {
                Console.WriteLine("Отсутствует объявление для переменной \"{0}\".", excp.Value);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            catch (TypeNotRecognizedException excp)
            {
                Console.WriteLine("Тип переменной \"{0}\" не был распознан (\"{1}\").", excp.Value, excp.Value.GetType());
                Console.ReadKey();
                Environment.Exit(-1);
            }
            catch (Exception excp)
            {
                Console.WriteLine("Ошибка вычисления.\nMessage:\n{0}.", excp.Message);
                Console.ReadKey();
                Environment.Exit(-1);
            }
            Console.WriteLine("\nVariables:");
            foreach (KeyValuePair<string, object> var in stackMachine.Variables)
            {
                Console.WriteLine("{0}: {1}", var.Key, var.Value);
            }

            Console.ReadKey();
        }
    }
}
