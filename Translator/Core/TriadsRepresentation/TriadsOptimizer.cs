using System;
using System.Collections.Generic;
using System.Linq;
using Translator.Core.Lexer;
using Translator.Core.TriadsRepresentation.Entities;

namespace Translator.Core.TriadsRepresentation
{
    public class TriadsOptimizer
    {
        private class Variable
        {
            public string Name { get; }
            public string Value { get; set; }
            public bool IsSafe { get; set; }

            public Variable(string name, string value)
            {
                Name = name;
                Value = value;
                IsSafe = true;
            }
        }
        
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
        
        private List<Triad> OptimizedTriads;
        private List<Variable> Variables;
        private List<TriadWithResult> CalculatedTriads;

        public List<Triad> Optimize(List<Triad> triads, List<bool> triadsConditionIndexes)
        {
            Initialize();

            for (int i = 0; i < triads.Count; i++)
            {
                var triad = triads[i];

                if (triadsConditionIndexes[i])
                {
                    if (triad.Operation.Lexem == Lexem.ASSIGN_OP)
                    {
                        if (triad.LeftOperand?.Token.Lexem == Lexem.VAR)
                        {
                            AddOrUpdateVariable(triad.LeftOperand.Token.Value, "");
                            var variable = Variables.First(v => v.Name == triad.LeftOperand.Token.Value);
                            variable.IsSafe = false;
                        }
                    }
                    OptimizedTriads.Add(triad);
                }
                else if (triad.Operation.Lexem == Lexem.ASSIGN_OP)
                {
                    if (triad.RightOperand.IsLinkToAnotherTriad)
                    {
                        var calculatedTriad = CalculatedTriads.FirstOrDefault(t => t.TriadIndex == int.Parse(triad.RightOperand.Token.Value));
                        if (calculatedTriad == null)
                        {
                            AddOrUpdateVariable(triad.LeftOperand.Token.Value, "");
                            var variable = Variables.First(v => v.Name == triad.LeftOperand.Token.Value);
                            variable.IsSafe = false;
                            OptimizedTriads.Add(triad);
                        }
                        else
                        {
                            AddOrUpdateVariable(triad.LeftOperand.Token.Value, calculatedTriad.Value);
                            triad.RightOperand.Token.Value = calculatedTriad.Value;
                            triad.RightOperand.Token.Lexem = Lexem.DIGIT;
                            triad.RightOperand.IsLinkToAnotherTriad = false;
                            OptimizedTriads.Add(triad);
                        }
                    }
                    else if (triad.RightOperand.Token.Lexem == Lexem.DIGIT)
                    {
                        AddOrUpdateVariable(triad.LeftOperand.Token.Value, triad.RightOperand.Token.Value);
                        OptimizedTriads.Add(triad);
                    }
                    else if (triad.RightOperand.Token.Lexem == Lexem.VAR)
                    {
                        var variable = Variables.First(v => v.Name == triad.RightOperand.Token.Value);
                        if (variable.IsSafe)
                        {
                            AddOrUpdateVariable(triad.LeftOperand.Token.Value, variable.Value);
                            triad.RightOperand.Token.Value = variable.Value;
                            triad.RightOperand.Token.Lexem = Lexem.DIGIT;
                            OptimizedTriads.Add(triad);
                        }
                        else
                        {
                            AddOrUpdateVariable(triad.LeftOperand.Token.Value, String.Empty);
                            Variables.First(v => v.Name == triad.LeftOperand.Token.Value).IsSafe = false;
                            OptimizedTriads.Add(triad);
                        }
                    }
                }
                else if (triad.Operation.Lexem == Lexem.OP)
                {
                    if (CheckTriadSafity(triad))
                    {
                        var leftOperand = GetTriadOperandValue(triad.LeftOperand);
                        var rightOperand = GetTriadOperandValue(triad.RightOperand);
                        var result = ExecuteArithmeticOperation(
                            double.Parse(leftOperand), 
                            double.Parse(rightOperand), 
                            triad.Operation.Value);
                        CalculatedTriads.Add(new TriadWithResult(triads.IndexOf(triad), result.ToString()));
                        OptimizedTriads.Add(Triad.Empty);
                    }
                    else
                    {
                        OptimizedTriads.Add(triad);
                    }
                }
                else if (triad.Operation.Lexem == Lexem.RETURN_KW)
                {
                    if (triad.RightOperand.IsLinkToAnotherTriad)
                    {
                        var calculatedTriad = CalculatedTriads.FirstOrDefault(t => t.TriadIndex == int.Parse(triad.RightOperand.Token.Value));
                        if (calculatedTriad == null)
                        {
                            OptimizedTriads.Add(triad);
                        }
                        else
                        {
                            triad.RightOperand.Token.Value = calculatedTriad.Value;
                            triad.RightOperand.Token.Lexem = Lexem.DIGIT;
                            triad.RightOperand.IsLinkToAnotherTriad = false;
                            OptimizedTriads.Add(triad);
                        }
                    }
                    else if (triad.RightOperand.Token.Lexem == Lexem.VAR)
                    {
                        var variable = Variables.First(v => v.Name == triad.RightOperand.Token.Value);
                        if (variable.IsSafe)
                        {
                            triad.RightOperand.Token.Value = variable.Value;
                            triad.RightOperand.Token.Lexem = Lexem.DIGIT;
                            OptimizedTriads.Add(triad);
                        }
                        else
                        {
                            OptimizedTriads.Add(triad);
                        }
                    }
                    else
                    {
                        OptimizedTriads.Add(triad);
                    }
                }
                else
                {
                    OptimizedTriads.Add(triad);
                }
            }

            PatchLabels();
            return OptimizedTriads;
        }

        private string GetTriadOperandValue(TriadOperand triadOperand)
        {
            if (triadOperand.IsLinkToAnotherTriad)
            {
                return CalculatedTriads
                    .FirstOrDefault(t => t.TriadIndex == int.Parse(triadOperand.Token.Value))?
                    .Value;
            }
            
            if (triadOperand.Token.Lexem == Lexem.DIGIT)
            {
                return triadOperand.Token.Value;
            }
            
            if (triadOperand.Token.Lexem == Lexem.VAR)
            {
                return Variables.First(v => v.Name == triadOperand.Token.Value).Value;
            }

            throw new InvalidOperationException();
        }

        private void AddOrUpdateVariable(string name, string value)
        {
            var variable = Variables.FirstOrDefault(v => v.Name == name);
            if (variable != null)
                variable.Value = value;
            else
                Variables.Add(new Variable(name, value));
        }

        private bool CheckTriadSafity(Triad triad)
        {
            if (!CheckTriadOperandSafity(triad.LeftOperand)) return false;
            if (!CheckTriadOperandSafity(triad.RightOperand)) return false;
            return true;
        }

        private bool CheckTriadOperandSafity(TriadOperand triadOperand)
        {
            if (triadOperand.IsLinkToAnotherTriad)
            {
                var calculatedTriad = CalculatedTriads
                    .FirstOrDefault(t => t.TriadIndex == int.Parse(triadOperand.Token.Value));
                if (calculatedTriad == null)
                    return false;
                return true;
            }

            if (triadOperand.Token.Lexem == Lexem.DIGIT)
                return true;

            if (triadOperand.Token.Lexem == Lexem.VAR)
            {
                var variable = Variables.First(v => v.Name == triadOperand.Token.Value);
                if (variable.IsSafe)
                    return true;
                return false;
            }

            throw new InvalidOperationException();
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

        private void PatchLabels()
        {
            for (int i = 0; i < OptimizedTriads.Count; i++)
            {
                if (OptimizedTriads[i] == Triad.Empty)
                {
                    for (int j = i + 1; j < OptimizedTriads.Count; j++)
                    {
                        var triadOperand = OptimizedTriads[j].LeftOperand;
                        var rightOperand = OptimizedTriads[j].RightOperand;

                        if (triadOperand != null && triadOperand.IsLinkToAnotherTriad)
                            OptimizedTriads[j].LeftOperand.Token.Value =
                                (int.Parse(OptimizedTriads[j].LeftOperand.Token.Value) - 1).ToString();


                        if (rightOperand != null && rightOperand.IsLinkToAnotherTriad)
                            OptimizedTriads[j].RightOperand.Token.Value =
                                (int.Parse(OptimizedTriads[j].RightOperand.Token.Value) - 1).ToString();
                        else if (OptimizedTriads[j].Operation.Lexem == Lexem.F_TRANS
                                 || OptimizedTriads[j].Operation.Lexem == Lexem.UNC_TRANS)
                            OptimizedTriads[j].RightOperand.Token.Value =
                                (int.Parse(OptimizedTriads[j].RightOperand.Token.Value) - 1).ToString();
                    }
                }
            }

            OptimizedTriads = OptimizedTriads.Where(t => t != Triad.Empty).ToList();
        }
        
        private void Initialize()
        {
            OptimizedTriads = new List<Triad>();
            Variables = new List<Variable>();
            CalculatedTriads = new List<TriadWithResult>();
        }
    }
}
