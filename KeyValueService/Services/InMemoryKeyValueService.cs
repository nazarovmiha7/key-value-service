using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValueService.Services
{
    public class InMemoryKeyValueService : IKeyValueService
    {
        private static ConcurrentDictionary<string, string> _storage = new ConcurrentDictionary<string, string>();

        public Task<List<string>> GetKeysAsync()
        {
            return Task.Run(() =>
            {
                return _storage.Where(c => c.Value != null)
                    .Select(s => s.Key)
                    .ToList();
            });
        }

        public async Task<(bool, string)> GetValueAsync(string key, bool format)
        {
            string value = null;
            bool result = await Task.Run(() => _storage.TryGetValue(key, out value)).ConfigureAwait(false);

            if (format)
                value = value.SentencesFormatting();

            return (result, value);
        }

        public async Task<(bool, KeyValuePair<string, string>)> SetValueAsync(string key, string value)
        {
            bool result = true;
            bool addResult = await Task.Run(() => _storage.TryAdd(key, value)).ConfigureAwait(false);
            if (!addResult)
            {
                _storage.TryRemove(key, out string delValue);
                _storage.TryAdd(key, value);
            }
            return (result, new KeyValuePair<string, string>(key, value));
        }

        public async Task<bool> RemoveValueAsync(string key)
        {
            bool result = true;
            var exist = _storage.TryGetValue(key, out string value);
            if (exist)
            {
                result = await Task.Run(() => _storage.TryUpdate(key, null, value)).ConfigureAwait(false);
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
