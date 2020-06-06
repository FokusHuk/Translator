using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class DLLNode
    {
        public double Value { get; set; }
        public DLLNode Next { get; set; }
        public DLLNode Previous { get; set; }
        
        public DLLNode(double value)
        {
            Value = value;
        }
    }
}
