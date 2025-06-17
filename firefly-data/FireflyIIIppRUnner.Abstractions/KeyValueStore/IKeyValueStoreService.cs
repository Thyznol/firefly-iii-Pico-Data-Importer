using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireflyIIIppRunner.Abstractions.KeyValueStore
{
    /// <summary>
    /// Technically it's a two-layer lookup (many -> one -> one) but whatever
    /// </summary>
    public interface IKeyValueStoreService
    {
        public Task ReadFromStorage();
        public Task WriteToStorage();

        /// <summary>
        /// Return the default valueValue used when initializing a new value
        /// </summary>
        /// <returns></returns>
        public Task<string> GetDefaultValueValue();

        /// <summary>
        /// Return all keys that are mapped to <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<(bool Success, List<string> Keys)> GetKeys(string value);

        /// <summary>
        /// Add a new entry for <paramref name="key"/>, mapping it to <paramref name="value"/>.
        /// If <paramref name="value"/> does not already exist, it will be initializd with the default valueValue.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task AddKey(string key, string value);

        /// <summary>
        /// Delete entry for <paramref name="key"/>. If there are no other keys that map to the value that <paramref name="key"/> mapped to,
        /// then that value is also deleted.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task DeleteKey(string key);

        /// <summary>
        /// Get the value that <paramref name="key"/> maps to.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<(bool Success, string Reason, string Value)> GetKeyValue(string key);

        /// <summary>
        /// <paramref name="key"/> -> value -> valueValue.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<(bool Success, string Reason, string Value, string ValueValue)> GetKeyValueValue(string key);

        /// <summary>
        /// Get the valueValue that <paramref name="value"/> maps to.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<(bool Success, string ValueValue)> GetValueValue(string value);


        /// <summary>
        /// Update the valueValue for <paramref name="value"/>. Will throw exception if value does not already exist.
        /// New values should be created with a key using <see cref="AddKey(string, string)"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueValue"></param>
        /// <returns></returns>
        public Task UpdateValue(string value, string valueValue);
        
        /// <summary>
        /// Delete entry for <paramref name="value"/>. Also deletes all keys that map to the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task DeleteValue(string value);

        /// <summary>
        /// Autocomplete search for a partial value.
        /// </summary>
        /// <param name="partialValue"></param>
        /// <returns></returns>
        public Task<List<string>> AutocompleteValue(string partialValue);
    }
}
