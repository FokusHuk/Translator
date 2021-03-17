﻿namespace Translator.Core.TriadsRepresentation.Entities
{
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
}
