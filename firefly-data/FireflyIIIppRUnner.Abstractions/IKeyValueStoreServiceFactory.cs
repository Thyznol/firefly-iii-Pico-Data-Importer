using FireflyIIIppRunner.Abstractions.KeyValueStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions
{
    public interface IKeyValueStoreServiceFactory
    {
        public List<string> GetAvailableStores();
        public Task<IKeyValueStoreService> GetKeyValueStoreService(string store);
        public Task<(bool Success, IKeyValueStoreService KeyValueStoreService)> TryGetKeyValueStoreService(string store);
    }
}
