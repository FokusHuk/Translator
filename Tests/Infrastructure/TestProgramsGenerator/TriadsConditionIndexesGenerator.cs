using System;
using System.Collections.Generic;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static List<bool> GetTriadsConditionIndexes(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple => new List<bool>(){ false, false, false, false, false, false, false },
                TestSourceKey.If => new List<bool>(){ false, false, true, true, true, false },
                TestSourceKey.IfElse => new List<bool>(){ false, true, true, true, true, true, false },
                TestSourceKey.NestedConditions => 
                    new List<bool>(){ false, true, true, true, true, true, true, true, true, true, true, false, false,
                        false },
                TestSourceKey.CycleWhile => 
                    new List<bool>(){ false, false, true, true, true, true, true, true, true, true, false },
                TestSourceKey.CycleWhileWithConditions => new List<bool>(){ false, false, true, true, true, true, true,
                    true, true, true, true, true, true, true, false, false, true, true, true, true, false },
                TestSourceKey.CycleWhileInCondition => 
                    new List<bool>(){ false, true, true, true, true, true, true, true, true, true, true, true, true, 
                        true, false },
                TestSourceKey.CycleFor => new List<bool>(){ false, true, true, true, true, true, true, true, true, 
                    true, true, false },
                TestSourceKey.CycleForWithCondition => new List<bool>(){ false, false, false, true, true, true, true, 
                    true, true, true, true, true, true, true, true, true, true, true, true, true, false },
                TestSourceKey.CycleForInCondition => new List<bool>(){ false, true, true, true, true, true, true, true,
                    true, true, true, true, true, true, true, false },
                TestSourceKey.Out => new List<bool>(){ false, false, false, false, false, false, false, false },
                TestSourceKey.OutInCycles => new List<bool>(){ false, true, true, true, true, true, true, true, true, 
                    true, true, true, false, true, true, true, true, true, true, true, true, true, false, false },
                TestSourceKey.Return => new List<bool>(){ false, false, false, false, false, false },
                TestSourceKey.SeveralReturnsWithFirstWorking => 
                    new List<bool>(){ false, true, true, true, false, false },
                TestSourceKey.SeveralReturnsWithSecondWorking => 
                    new List<bool>(){ false, true, true, true, false, false },
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}
