using System;

namespace Tests.Infrastructure
{
    static partial class TestProgramsGenerator
    {
        public static string GetSource(TestSourceKey key) =>
            key switch
            {
                TestSourceKey.Simple =>
                    @"
                    a = 2;
                    b = 1;
                    c = a + b - 2 / 2;",
                
                TestSourceKey.If => 
                    @"
                    a = 5;
                    b = 0;
                    if (a < 10)
                    {
                    b = 2;
                    }",
                
                TestSourceKey.IfElse => 
                    @"
                    a = 0;
                    if (a >= 0)
                    {
                    b = 2;
                    }
                    else
                    {
                    b = 3;
                    }",
                
                TestSourceKey.NestedConditions => 
                    @"
                    a = 5;
                    if(a > 3)
                    {
                        if(a < 8)
                        {
                            b = a / 2;
                        }
                        else
                        {
                            b = 0;
                        }
                    }
                    else
                    {
                        b = 1;
                    }
                    c = a + b;",
                
                TestSourceKey.CycleWhile => 
                    @"
                    a = 6;
                    b = 0;
                    while(a >= 2)
                    {
                        b = b + a * 2;
                        a = a - 1;
                    }",
                
                TestSourceKey.CycleWhileWithConditions => 
                    @"
                    a = 0;
                    b = 0;
                    while(a < 10)
                    {
                        a = a + 3;
                        if(a < 7)
                        {
                            b = b + 2;
                        }
                        else
                        {
                            b = b - 1;
                        } 
                    } 
                    b = b / 2;
                    if(b >= 5)
                    {
                        a = b / 2;
                    }",
                
                TestSourceKey.CycleWhileInCondition => 
                    @"
                    a = 7;
                    if(a > 5)
                    {
                        b = a - 2;
                        while(a < 60)
                        {
                            a = a * 2;
                        }
                        a = a - b;
                    }
                    else
                    {
                        b = 0;
                    }",
                
                TestSourceKey.CycleFor => 
                    @"
                    a = 5;
                    for (i = 0; i < 5; i = i + 1)
                    {
                        a = a + 10;
                    }",
                
                TestSourceKey.CycleForWithCondition => 
                    @"
                    a = 5;
                    b = 0;
                    c = 0;
                    for (i = 0; i < 10; i = i + 1)
                    {
                        a = a + 10;
                        if (a > 40)
                        {
                            b = b + 1;
                        }
                        else
                        {
                            c = c - 1;
                        }
                    }",
                
                TestSourceKey.CycleForInCondition => 
                    @"
                    a = 5;
                    if (a <= 5)
                    {
                        for (i = 0; i <= 3; i = i + 1)
                        {
                            a = a + 10;
                        }
                    }
                    else
                    {
                        a = 0;
                    }",
                
                TestSourceKey.Out => 
                    @"
                    a = 5;
                    b = 4;
                    c = a + b;
                    out (a);
                    out (b);
                    out (c);",
                
                TestSourceKey.OutInCycles => 
                    @"
                    a = 5;
                    for (i = 0; i < 5; i = i + 1)
                    {
                        a = a + 10;
                        out(a);
                    }
                    b = 0;
                    while(a > 0)
                    {
                        b = b + a * 2;
                        a = a - 20;
                        out(b);
                    }
                    out(a);",
                
                TestSourceKey.Return => 
                    @"
                    a = 5;
                    b = 4;
                    c = a + b;
                    return c;",
                
                TestSourceKey.SeveralReturnsWithFirstWorking => 
                    @"
                    a = 5;
                    if (a > 0)
                    {
                        return a;
                    }
                    return 0;",
                
                TestSourceKey.SeveralReturnsWithSecondWorking => 
                    @"
                    a = 5;
                    if (a < 0)
                    {
                        return a;
                    }
                    return 0;",
                
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
            };
    }
}
