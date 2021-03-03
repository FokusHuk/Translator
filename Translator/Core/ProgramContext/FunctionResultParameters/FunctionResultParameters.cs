using Translator.Core.FunctionResultParameters;

namespace Translator.Core.ProgramContext
{
    public class FunctionResultParameters
    {
        public ResultType ResultType { get; set; }
        public string FunctionName { get; set; }
        public string FunctionArgs { get; set; }
        public string Value { get; set; }

        public FunctionResultParameters(ResultType type)
        {
            ResultType = type;
        }

        public FunctionResultParameters(ResultType type, string value)
        {
            ResultType = type;
            Value = value;
        }

        public FunctionResultParameters(ResultType type, string functionName, string functionArgs)
        {
            ResultType = type;
            FunctionName = functionName;
            FunctionArgs = functionArgs;
        }

        public FunctionResultParameters()
        {
            
        }

        public FunctionResultParameters GetNewParameters()
        {
            var newParameters = new FunctionResultParameters();

            newParameters.Value = Value;
            newParameters.FunctionArgs = FunctionArgs;
            newParameters.FunctionName = FunctionName;
            newParameters.ResultType = ResultType;

            return newParameters;
        }
    }
}