using System;

namespace Translator.DataStructures
{
    class HashTable
    {
        private readonly byte tableSize = 255;

        private DoublyLinkedList<HTElement>[] Values;

        public HashTable()
        {
            Values = new DoublyLinkedList<HTElement>[tableSize];
        }
        
        public void insert(int key, double value)
        {
            HTElement newItem = new HTElement(key, value);

            int hash = getHash(newItem.Key);

            if (Values[hash] != null)
            {
                bool isContkey = false;

                for (int i = 0; i < Values[hash].Size; i++)
                {
                    if (Values[hash].getValue(i).Key == key)
                    {
                        isContkey = true;
                        break;
                    }
                }

                if (isContkey)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                Values[hash].insertAt(newItem, Values[hash].Size);
            }
            else
            {
                Values[hash] = new DoublyLinkedList<HTElement>();
                Values[hash].insertAt(newItem, 0);
            }
        }

        public void delete(int key)
        {
            int hash = getHash(key);

            if (Values[hash] == null)
                return;

            for (int i = 0; i < Values[hash].Size; i++)
            {
                if (Values[hash].getValue(i).Key == key)
                {
                    Values[hash].deleteAt(i);
                    break;
                }
            }
        }

        public double search(int key)
        {
            int hash = getHash(key);

            if (Values[hash] != null)
            {
                for (int i = 0; i < Values[hash].Size; i++)
                {
                    if (Values[hash].getValue(i).Key == key)
                    {
                        return Values[hash].getValue(i).Value;
                    }
                }
            }

            throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
        }

        public void display()
        {
            Console.WriteLine("[hash]\tkey:value\t...");
            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] == null || Values[i].Size == 0) continue;

                Console.Write("[{0}]\t", i);
                for (int j = 0; j < Values[i].Size; j++)
                {
                    Console.Write("{0}:{1}\t", Values[i].getValue(j).Key, Values[i].getValue(j).Value);
                }
                Console.WriteLine();
            }
        }

        private int getHash(int key)
        {
            return key % tableSize;
        }
    }
}
