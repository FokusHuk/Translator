using Translator.Core.Analyzer;
using Translator.Core.TriadsRepresentation;

namespace Translator.Core
{
    public class Compiler
    {
        private readonly SyntacticalAnalyzer SyntacticalAnalyzer;
        private readonly TriadsConverter TriadsConverter;
        private readonly TriadsOptimizer TriadsOptimizer;

        public Compiler()
        {
            SyntacticalAnalyzer = new SyntacticalAnalyzer();
            TriadsConverter = new TriadsConverter();
            TriadsOptimizer = new TriadsOptimizer();
        }
        
        public void Compile(FunctionContext context)
        {
            var POLIS = SyntacticalAnalyzer.Convert(context.Tokens);
            var triads = TriadsConverter.GetTriadsFromPolis(POLIS, SyntacticalAnalyzer.PolisConditionsIndexes);
            var optimizedTriads = TriadsOptimizer.Optimize(triads, TriadsConverter.TriadsConditionIndexes);
            
            context.POLIS = POLIS;
            context.Triads = triads;
            context.OptimizedTriads = optimizedTriads;
        }
    }
}