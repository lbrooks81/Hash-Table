using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    // Implements generic types for keys and values
    public class MyHashTable<TKey, TValue> 
    {
        private const uint DEFAULT_CAPACITY = 16;
        private List<KeyValuePair<TKey, TValue>>[] buckets;

        public int Count { get; private set; } = 0;
        public int Buckets => buckets.Length;

        // Index won't necessarily be a number, but a key
        public TValue this[TKey key]
        {
            get
            {
                int index = GetHashIndex(key);

                if (buckets[index] != null)
                {
                    // iterate through each list in buckets
                    foreach(KeyValuePair<TKey, TValue> kvp in buckets[index])
                    {
                        if(kvp.Key!.Equals(key))
                        {
                            return kvp.Value;
                        }
                    }
                }

                throw new KeyNotFoundException("Key not found in the hash table.");
            }
        }

        public MyHashTable()
            : this(DEFAULT_CAPACITY)
        {}

        public MyHashTable(uint capacity)
        { 
            buckets = new List<KeyValuePair<TKey, TValue>>[capacity];
        }

        private int GetHashIndex(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            // Somewhat random, won't always return the same hash for every key
            // Random based on runtime, so the key will always be the same for each value during runtime

            int hashCode = key.GetHashCode();

            // % makes sure that it returns something in the range of the buckets array
            // Abs to ensure the value is 0 or positive
            return Math.Abs(hashCode % buckets.Length); 
        }

        // Constant time in nearly all scenarios
        public void Add(TKey key, TValue value) 
        {
            int index = GetHashIndex(key);

            if (buckets[index] == null) 
            {
                buckets[index] = [];
            }

            // Checks for duplicate keys
            foreach(KeyValuePair<TKey, TValue> kvp in buckets[index])
            {
                if(kvp.Key!.Equals(key))
                {
                    throw new ArgumentException("Key already exists in the hash table.");
                }
            }

            buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
            Count++;
        }

        public bool Remove(TKey key) 
        {
            int index = GetHashIndex(key);

            if (buckets[index] != null)
            {
                foreach(KeyValuePair<TKey, TValue> kvp in buckets[index])
                {
                    if (kvp.Key!.Equals(key))
                    {
                        buckets[index].Remove(kvp);
                        Count--;
                        return true;
                    }

                }
            }

            return false;
        }

        public void Clear()
        {
            buckets = new List<KeyValuePair<TKey, TValue>>[buckets.Length];
            Count = 0;
        }

            public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            for(int i = 0; i < buckets.Length; i++) 
            {
                if (buckets[i] != null)
                {
                    foreach(KeyValuePair<TKey, TValue> kvp in buckets[i])
                    {
                        sb.AppendLine($"[{i}] {kvp.Key} = {kvp.Value}");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
