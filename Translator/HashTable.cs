using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Translator
{
    class HashTable
    {
        private readonly byte tableSize = 255;

        private DoublyLinkedList<int> Hashes;
        private DoublyLinkedList<DoublyLinkedList<HTElement>> Values;

        public HashTable()
        {
            Hashes = new DoublyLinkedList<int>();
            Values = new DoublyLinkedList<DoublyLinkedList<HTElement>>();
        }
        
        public void insert(int key, double value)
        {
            HTElement newItem = new HTElement(key, value);

            int hash = getHash(newItem.Key);

            DoublyLinkedList<HTElement> hashTableItem = null;

            if (Hashes.contains(hash))
            {
                hashTableItem = Values.getValue(Hashes.getIndex(hash));

                bool isContkey = false;

                for (int i = 0; i < hashTableItem.Size; i++)
                {
                    if (hashTableItem.getValue(i).Key == key)
                    {
                        isContkey = true;
                        break;
                    }
                }

                if (isContkey)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                hashTableItem.insertAt(newItem, hashTableItem.Size - 1);
            }
            else
            {
                hashTableItem = new DoublyLinkedList<HTElement>();
                hashTableItem.insertAt(newItem, 0);

                Hashes.insertAt(hash, Hashes.Size);
                Values.insertAt(hashTableItem, Values.Size);
            }
        }

        public void delete(int key)
        {
            int hash = getHash(key);

            if (!Hashes.contains(hash))
            {
                return;
            }

            var hashTableItem = Values.getValue(Hashes.getIndex(hash));

            for (int i = 0; i < hashTableItem.Size; i++)
            {
                if (hashTableItem.getValue(i).Key == key)
                {
                    hashTableItem.deleteAt(i);

                    if (hashTableItem.isEmpty())
                    {
                        Values.deleteAt(Values.getIndex(hashTableItem));
                        Hashes.deleteAt(Hashes.getIndex(hash));
                    }
                    break;
                }
            }
        }

        public double search(int key)
        {
            int hash = getHash(key);

            if (!Hashes.contains(hash))
            {
                throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
            }

            var hashTableItem = Values.getValue(Hashes.getIndex(hash));

            if (hashTableItem != null)
            {
                for (int i = 0; i < hashTableItem.Size; i++)
                {
                    if (hashTableItem.getValue(i).Key == key)
                    {
                        return hashTableItem.getValue(i).Value;
                    }
                }
            }

            throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
        }

        public void display()
        {
            Console.WriteLine("[hash]\tkey:value\t...");
            for (int i = 0; i < Hashes.Size; i++)
            {
                Console.Write("[{0}]\t", Hashes.getValue(i));

                for (int j = 0; j < Values.getValue(i).Size; j++)
                {
                    Console.Write("{0}:{1}\t", Values.getValue(i).getValue(j).Key, Values.getValue(i).getValue(j).Value);
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
