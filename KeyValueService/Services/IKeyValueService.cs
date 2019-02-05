using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyValueService.Services
{
    public interface IKeyValueService
    {
        Task<List<string>> GetKeysAsync();
        Task<(bool, string)> GetValueAsync(string key, bool format);
        Task<(bool, KeyValuePair<string, string>)> SetValueAsync(string key, string value);
        Task<bool> RemoveValueAsync(string key);
    }
}
