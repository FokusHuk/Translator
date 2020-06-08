using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Translator
{
    class HashTable
    {
        private readonly byte tableSize = 255;

        private Dictionary<int, List<HTElement>> _items = null;

        public ReadOnlyCollection<KeyValuePair<int, List<HTElement>>> Items => _items?.ToList()?.AsReadOnly();

        public HashTable()
        {
            _items = new Dictionary<int, List<HTElement>>(tableSize);
        }
        
        public void insert(int key, double value)
        {
            HTElement newItem = new HTElement(key, value);

            int hash = GetHash(newItem.Key);

            List<HTElement> hashTableItem = null;

            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];

                HTElement oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == newItem.Key);

                if (oldElementWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                _items[hash].Add(newItem);
            }
            else
            {
                hashTableItem = new List<HTElement> { newItem };

                _items.Add(hash, hashTableItem);
            }
        }

        public void delete(int key)
        {
            int hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return;
            }

            var hashTableItem = _items[hash];

            HTElement item = hashTableItem.SingleOrDefault(i => i.Key == key);

            if (item != null)
            {
                hashTableItem.Remove(item);

                if (hashTableItem.Count == 0)
                    _items.Remove(hash);
            }
        }

        public double search(int key)
        {
            int hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                throw new ArgumentException($"Хеш-таблица не содержит элемент с ключом {key}.", nameof(key));
            }

            var hashTableItem = _items[hash];

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

        private int GetHash(int key)
        {
            return key % tableSize;
        }
    }
}
