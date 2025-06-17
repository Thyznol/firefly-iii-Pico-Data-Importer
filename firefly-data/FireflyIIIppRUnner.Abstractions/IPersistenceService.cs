using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions
{
    public interface IPersistenceService
    {
        public bool CollectionExists(string path);
        public void AssertCollectionExists(string path);
        public bool PathExists(string collection, string path);
        public void AssertPathExists(string collection, string path);
        public  Task<T> ReadAsync<T>(string collection, string path);
        public Task WriteAsync<T>(string collection, string path, T obj);

        public Task CreateCollectionAsync(string collection);
        public Task CreatePathAndWriteAsync<T>(string collection, string path, T obj);
    }
}
