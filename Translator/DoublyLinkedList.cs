using System;
using System.Collections.Generic;
using System.Text;

namespace Translator
{
    class DoublyLinkedList
    {
        private DLLNode First;
        private DLLNode Current;
        private DLLNode Last;
        private int Size;

        public DoublyLinkedList()
        {
            Size = 0;
            First = Current = Last = null;
        }

        public bool isEmpty() => Size == 0;

        public void insertAt(double newElement, int index)
        {
            if (index < 0 || index > Size && Size != 0)
            {
                throw new InvalidOperationException();
            }
            else if (index == 0)
            {
                DLLNode newNode = new DLLNode(newElement);

                if (First == null)
                {
                    First = Last = newNode;
                }
                else
                {
                    newNode.Next = First;
                    First = newNode;
                    newNode.Next.Previous = First;
                }
                Size++;
            }
            else if (index == Size)
            {
                DLLNode newNode = new DLLNode(newElement);

                if (First == null)
                {
                    First = Last = newNode;
                }
                else
                {
                    Last.Next = newNode;
                    newNode.Previous = Last;
                    Last = newNode;
                }
                Size++;
            }
            else
            {
                int count = 0;
                Current = First;
                while (Current != null && count != index)
                {
                    Current = Current.Next;
                    count++;
                }
                DLLNode newNode = new DLLNode(newElement);
                Current.Previous.Next = newNode;
                newNode.Previous = Current.Previous;
                Current.Previous = newNode;
                newNode.Next = Current;
                Size++;
            }
        }

        public void clear()
        {
            while (!isEmpty())
            {
                DLLNode temp = First;
                if (First.Next != null)
                {
                    First.Next.Previous = null;
                }
                First = First.Next;
                temp = null;
                Size--;
            }
        }

        public void display()
        {
            if (First == null)
            {
                Console.WriteLine("empty");
                return;
            }
            Current = First;
            int count = 1;
            while (Current != null)
            {
                Console.WriteLine("Element {0}: {1}", count, Current.Value);
                count++;
                Current = Current.Next;
            }
        }

        public void deleteAt(int index)
        {
            if (index < 0 || index > Size && Size != 0)
            {
                throw new InvalidOperationException();
            }
            else if (index == 0)
            {
                if (First == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    DLLNode temp = First;
                    if (First.Next != null)
                    {
                        First.Next.Previous = null;
                    }
                    First = First.Next;
                    temp = null;
                    Size--;
                }
            }
            else if (index == Size - 1)
            {
                if (Last == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    DLLNode temp = Last;
                    if (Last.Previous != null)
                    {
                        Last.Previous.Next = null;
                    }
                    Last = Last.Previous;
                    temp = null;
                    Size--;
                }
            }
            else
            {
                int count = 0;
                Current = First;
                while (Current != null && count != index)
                {
                    Current = Current.Next;
                    count++;
                }
                Current.Previous.Next = Current.Next;
                Current.Next.Previous = Current.Previous;
                Size--;
            }
        }

        public double getValue(int index)
        {
            Current = First;
            int count = 0;
            while (Current != null && count != index)
            {
                Current = Current.Next;
                count++;
            }
            
            if (Current != null)
            {
                return Current.Value;
            }
            else
            {
                return 0;
            }
        }

        public int getIndex(double value)
        {
            Current = First;
            int index = 0;
            while (Current != null && Current.Value != value)
            {
                Current = Current.Next;
                index++;
            }

            if (index != Size)
            {
                return index;
            }
            else
            {
                return 0;
            }
        }
    }
}
