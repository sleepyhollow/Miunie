using Miunie.Core.Storage;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Miunie.Core.XUnit.Tests
{
    public class DataStorageMock : IDataStorage
    {
        private Dictionary<string, object> storage;

        public DataStorageMock()
        {
            storage = new Dictionary<string, object>();
        }

        public bool KeyExists(string collection, string key)
            => storage.ContainsKey($"{collection}/{key}");

        public IEnumerable<T> RestoreCollection<T>(string collection)
            => storage.Where(p => p.Key.StartsWith($"{collection}/"))
                    .ToList()
                    .Cast<T>();

        public T RestoreObject<T>(string collection, string key)
        {
            var result = storage.TryGetValue($"{collection}/{key}", out var obj);
            if(result)
            {
                return (T) obj;
            }
            return default(T);
        }

        public void StoreObject(object obj, string collection, string key)
            => storage.TryAdd($"{collection}/{key}", obj);
    }
}
