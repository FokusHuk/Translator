namespace Tests.Infrastracture
{
    class TestProgram
    {
        public TestSourceKey Key { get; }
        
        public string Source { get; set; }

        public TestProgram(TestSourceKey key, string source)
        {
            Key = key;
            Source = source;
        }
    }
}
