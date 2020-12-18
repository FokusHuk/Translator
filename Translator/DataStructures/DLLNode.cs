namespace Translator.DataStructures
{
    class DLLNode<T>
    {
        public T Value { get; set; }
        public DLLNode<T> Next { get; set; }
        public DLLNode<T> Previous { get; set; }
        
        public DLLNode(T value)
        {
            Value = value;
        }
    }
}
