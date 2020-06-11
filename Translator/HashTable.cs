using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Translator
{
    class HashTable
    {
        private readonly byte tableSize = 255;

        private List<int> Hashes;
        private List<List<HTElement>> Values;

        public HashTable()
        {
            Hashes = new List<int>();
            Values = new List<List<HTElement>>();
        }
        
        public void insert(int key, double value)
        {
            HTElement newItem = new HTElement(key, value);

            int hash = getHash(newItem.Key);

            List<HTElement> hashTableItem = null;

            if (Hashes.Contains(hash))
            {
                hashTableItem = Values[Hashes.IndexOf(hash)];

                HTElement oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == newItem.Key);

                if (oldElementWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                hashTableItem.Add(newItem);
            }
            else
            {
                hashTableItem = new List<HTElement> { newItem };

                Hashes.Add(hash);
                Values.Add(hashTableItem);
            }
        }

        public void delete(int key)
        {
            int hash = getHash(key);

            if (!Hashes.Contains(hash))
            {
                return;
            }

            var hashTableItem = Values[Hashes.IndexOf(hash)];

            HTElement item = hashTableItem.SingleOrDefault(i => i.Key == key);

            if (item != null)
            {
                hashTableItem.Remove(item);

                if (hashTableItem.Count == 0)
                {
                    Values.Remove(hashTableItem);
                    Hashes.Remove(hash);
                }
            }
        }

        public double search(int key)
        {
            int hash = getHash(key);

            if (!Hashes.Contains(hash))
            {
                throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
            }

            var hashTableItem = Values[Hashes.IndexOf(hash)];

            if (hashTableItem != null)
            {
                HTElement item = hashTableItem.SingleOrDefault(i => i.Key == key);

                if (item != null)
                {
                    return item.Value;
                }
            }

            throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
        }

        public void display()
        {
            Console.WriteLine("[hash]\tkey:value\t...");
            for (int i = 0; i < Hashes.Count; i++)
            {
                Console.Write("[{0}]\t", Hashes[i]);

                for (int j = 0; j < Values[i].Count; j++)
                {
                    Console.Write("{0}:{1}\t", Values[i][j].Key, Values[i][j].Value);
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
