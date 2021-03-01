using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.TriadsRepresentation
{
    public class TriadsStackMachine
    {
        private class TriadWithResult
        {
            public int TriadIndex { get; }
            public string Value { get; set; }

            public TriadWithResult(int triadIndex, string value)
            {
                TriadIndex = triadIndex;
                Value = value;
            }
        }

        public class Variable
        {
            public string Name { get; }
            public string Value { get; set; }

            public Variable(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }
        
        private List<Triad> Triads;
        private List<TriadWithResult> TriadResults;

        private int CurrentIndex;

        public List<Variable> Variables { get; private set; }
        public string ReturnResult { get; private set; }
        public string Output { get; private set; }

        public void Calculate(List<Triad> triads)
        {
            Initialize();
            Triads = triads;

            while (triads[CurrentIndex].Type != TriadType.End)
            {
                var triad = triads[CurrentIndex];
                
                if (triad.Operation.Lexem == Lexem.OP || triad.Operation.Lexem == Lexem.COMP_OP)
                {
                    CalculateTriad(triad);
                }
                else if (triad.Operation.Lexem == Lexem.ASSIGN_OP)
                {
                    ExecuteAssigning(triad);
                }
                else if (triad.Operation.Lexem == Lexem.UNC_TRANS)
                {
                    CurrentIndex = int.Parse(triad.RightOperand.Token.Value);
                    continue;
                }
                else if (triad.Operation.Lexem == Lexem.F_TRANS)
                {
                    var condition = Boolean.Parse(TriadResults
                        .First(result => result.TriadIndex == int.Parse(triad.LeftOperand.Token.Value))
                        .Value);

                    if (!condition)
                    {
                        CurrentIndex = int.Parse(triad.RightOperand.Token.Value);
                        continue;
                    }
                }
                else if (triad.Operation.Lexem == Lexem.RETURN_KW)
                {
                    ReturnResult = GetTriadOperandValue(triad.RightOperand);
                    break;
                }
                
                CurrentIndex++;
            }
        }

        private void CalculateTriad(Triad triad)
        {
            var leftOperand = GetTriadOperandValue(triad.LeftOperand);
            var rightOperand = GetTriadOperandValue(triad.RightOperand);

            if (triad.Operation.Lexem == Lexem.OP)
            {
                var result = ExecuteArithmeticOperation(
                    double.Parse(leftOperand),
                    double.Parse(rightOperand),
                    triad.Operation.Value);
                
                AddOrUpdateTriadWithResult(triad, result);

            }
            else
            {
                var result = ExecuteLogicOperation(
                    double.Parse(leftOperand),
                    double.Parse(rightOperand),
                    triad.Operation.Value);
                
                AddOrUpdateTriadWithResult(triad, result);
            }
        }

        private double ExecuteArithmeticOperation(double param1, double param2, string op)
        {
            switch (op)
            {
                case "+": return param1 + param2;
                case "-": return param1 - param2;
                case "*": return param1 * param2;
                case "/": return param1 / param2;
                default: return 0;
            }
        }

        private bool ExecuteLogicOperation(double param1, double param2, string op)
        {
            switch (op)
            {
                case ">": return param1 > param2;
                case "<": return param1 < param2;
                case ">=": return param1 >= param2;
                case "<=": return param1 <= param2;
                case "!=": return param1 != param2;
                case "==": return param1 == param2;
                default: return false;
            }
        }

        private void ExecuteAssigning(Triad triad)
        {
            var rightOperand = GetTriadOperandValue(triad.RightOperand);

            var variable = Variables.FirstOrDefault(variable => variable.Name == triad.LeftOperand.Token.Value);
            if (variable != null)
                variable.Value = rightOperand;
            else
                Variables.Add(new Variable(triad.LeftOperand.Token.Value, rightOperand));
        }
        
        private string GetTriadOperandValue(TriadOperand operand)
        {
            if (operand.IsLinkToAnotherTriad)
            {
                return TriadResults
                    .First(result => result.TriadIndex == int.Parse(operand.Token.Value))
                    .Value;
            }

            if (operand.Token.Lexem == Lexem.VAR)
            {
                return Variables
                    .First(result => result.Name == operand.Token.Value)
                    .Value;
            }

            return operand.Token.Value;
        }

        private int GetTriadIndex(Triad triad) => Triads.IndexOf(triad);

        private void AddOrUpdateTriadWithResult(Triad triad, double result)
        {
            var triadWithResult = TriadResults.FirstOrDefault(t => t.TriadIndex == GetTriadIndex(triad));
            if (triadWithResult != null)
                triadWithResult.Value = result.ToString();
            else
                TriadResults.Add(new TriadWithResult(GetTriadIndex(triad), result.ToString()));
        }
        
        private void AddOrUpdateTriadWithResult(Triad triad, bool result)
        {
            var triadWithResult = TriadResults.FirstOrDefault(t => t.TriadIndex == GetTriadIndex(triad));
            if (triadWithResult != null)
                triadWithResult.Value = result.ToString();
            else
                TriadResults.Add(new TriadWithResult(GetTriadIndex(triad), result.ToString()));
        }

        private void Initialize()
        {
            CurrentIndex = 0;
            Output = String.Empty;
            TriadResults = new List<TriadWithResult>();
            Variables = new List<Variable>();
        }
    }
}
