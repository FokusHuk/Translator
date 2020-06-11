using System;

namespace Translator
{
    class HashTable
    {
        private readonly byte tableSize = 255;

        private DoublyLinkedList<DoublyLinkedList<HTElement>> Values;

        public HashTable()
        {
            Values = new DoublyLinkedList<DoublyLinkedList<HTElement>>();

            for (int i = 0; i < tableSize; i++)
            {
                Values.insertAt(null, 0);
            }
        }
        
        public void insert(int key, double value)
        {
            HTElement newItem = new HTElement(key, value);

            int hash = getHash(newItem.Key);

            DoublyLinkedList<HTElement> hashTableItem = Values.getValue(hash);

            if (hashTableItem != null)
            {
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

                hashTableItem.insertAt(newItem, hashTableItem.Size);
            }
            else
            {
                hashTableItem = new DoublyLinkedList<HTElement>();
                hashTableItem.insertAt(newItem, 0);
                Values.insertAt(hashTableItem, hash);
            }
        }

        public void delete(int key)
        {
            int hash = getHash(key);

            var hashTableItem = Values.getValue(hash);

            if (hashTableItem == null)
                return;

            for (int i = 0; i < hashTableItem.Size; i++)
            {
                if (hashTableItem.getValue(i).Key == key)
                {
                    hashTableItem.deleteAt(i);
                    break;
                }
            }
        }

        public double search(int key)
        {
            int hash = getHash(key);

            var hashTableItem = Values.getValue(hash);

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
            for (int i = 0; i < Values.Size; i++)
            {
                var value = Values.getValue(i);
                if (value == null || value.Size == 0) continue;

                Console.Write("[{0}]\t", i);
                for (int j = 0; j < value.Size; j++)
                {
                    Console.Write("{0}:{1}\t", value.getValue(j).Key, value.getValue(j).Value);
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
