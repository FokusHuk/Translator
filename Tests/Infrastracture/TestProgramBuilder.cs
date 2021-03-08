using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;

namespace Tests.Infrastracture
{
    class TestProgramBuilder
    {
        private TestProgram Program;

        public TestProgramBuilder()
        {
            
        }

        private TestProgramBuilder(TestProgram program)
        {
            Program = program;
        }

        public TestProgramBuilder WithSource(TestSourceKey sourceKey)
        {
            Program = TestProgramsRepository.GetProgramByKey(sourceKey);

            return new TestProgramBuilder(Program);
        }

        public TestProgramBuilder WithMainFunction()
        {
            Program.Source = $"void main ()" + "{" + Program.Source + "}";
            Program.Tokens.Insert(0, new Token("{", Lexem.LSB));
            Program.Tokens.Insert(0, new Token(")", Lexem.RB));
            Program.Tokens.Insert(0, new Token("(", Lexem.LB));
            Program.Tokens.Insert(0, new Token("main", Lexem.EF_NAME));
            Program.Tokens.Insert(0, new Token("void", Lexem.VOID_T));
            Program.Tokens.Add(new Token("}", Lexem.RSB));
            
            return new TestProgramBuilder(Program);
        }

        public TestProgramBuilder WithFunction(string type, string name, string[] parameters = null, bool isAsync = false)
        {
            var parametersForSource = "";
            var asyncParameter = "";
            
            Program.Tokens.Insert(0, new Token("{", Lexem.LSB));
            Program.Tokens.Insert(0, new Token(")", Lexem.RB));
            
            if (parameters != null)
            {
                var functionArguments = new List<Token>();
                
                foreach (var parameter in parameters)
                {
                    functionArguments.Insert(0, new Token(parameter, Lexem.VAR));
                    functionArguments.Insert(0, new Token(",", Lexem.COMMA));
                    parametersForSource += parameter + ",";
                }

                functionArguments.RemoveAt(0);
                functionArguments.Reverse();
                Program.Tokens.InsertRange(0, functionArguments);
                parametersForSource = parametersForSource.Remove(parametersForSource.Length - 1);
            }

            Program.Tokens.Insert(0, new Token("(", Lexem.LB));
            Program.Tokens.Insert(0, new Token(name, Lexem.EF_NAME));
            
            if (type == "void")
                Program.Tokens.Insert(0, new Token(type, Lexem.VOID_T));
            else
                Program.Tokens.Insert(0, new Token(type, Lexem.FUNC_T));
            
            if (isAsync)
            {
                Program.Tokens.Insert(0, new Token("async", Lexem.ASYNC_KW));
                asyncParameter = "async ";
            }
            
            Program.Tokens.Add(new Token("}", Lexem.RSB));
            Program.Source = $"{asyncParameter}{type} {name} ({parametersForSource})" + "{" + Program.Source + "}";

            return new TestProgramBuilder(Program);
        }
        
        public TestProgramBuilder WithGrammarMistake(TestGrammarMistakeType type)
        {
            switch (type)
            {
                case TestGrammarMistakeType.NoBracket:
                    Program.Source = Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '(' || c == '{')), 1);
                    Program.Tokens.Remove(Program.Tokens.First(t => t.Lexem == Lexem.LB || t.Lexem == Lexem.LSB));
                    break;
                case TestGrammarMistakeType.NoEol:
                    Program.Source = Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == ';')), 1);
                    Program.Tokens.Remove(Program.Tokens.First(t => t.Lexem == Lexem.EOL));
                    break;
                case TestGrammarMistakeType.NoAssign:
                    Program.Source = Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '=')), 1);
                    Program.Tokens.Remove(Program.Tokens.First(t => t.Lexem == Lexem.ASSIGN_OP));
                    break;
                case TestGrammarMistakeType.NoOperation:
                    Program.Source = Program.Source.Remove(
                        Program.Source.IndexOf(
                            Program.Source.First(c => c == '+' || c == '-' || c == '*' || c == '/')), 1);
                    Program.Tokens.Remove(Program.Tokens.First(t => t.Lexem == Lexem.OP));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return new TestProgramBuilder(Program);
        }

        public TestProgramBuilder WithAnotherFunction(TestProgramBuilder builder)
        {
            Program.Source += "\n" + builder.Program.Source;
            Program.Tokens.AddRange(builder.Program.Tokens);

            return new TestProgramBuilder(Program);
        }

        public TestProgram Build()
        {
            return Program;
        }
    }
}
