using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValueService.Services
{
    public class PersistanceKeyValueService : IKeyValueService
    {
        public readonly ApplicationDbContext context;
        public PersistanceKeyValueService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<string>> GetKeysAsync()
        {
            var storage = await context.KeyValuePairs
                    .Where(c => c.Value != null)
                    .Select(s => s.Key)
                    .ToListAsync();
            return storage;
        }

        public async Task<(bool, string)> GetValueAsync(string key, bool format)
        {
            var pair = await context.KeyValuePairs.SingleOrDefaultAsync(c => c.Key == key).ConfigureAwait(false);

            var value = pair?.Value;
            if (value != null)
            {
                if (format)
                    value = value.SentencesFormatting();
                return (true, value);
            }
            else
                return (false, value);
        }

        public async Task<(bool, KeyValuePair<string, string>)> SetValueAsync(string key, string value)
        {
            var pair = await context.KeyValuePairs.SingleOrDefaultAsync(c => c.Key == key).ConfigureAwait(false);
            if (pair != null)
            {
                pair.Value = value;
            }
            else
            {
                var newEntity = new KeyValuePairEntity { Key = key, Value = value };
                await context.AddAsync(newEntity).ConfigureAwait(false);
            }

            int x = await context.SaveChangesAsync().ConfigureAwait(false);
            var res = new KeyValuePair<string, string>(key, value);
            if (x > 0)
                return (true, res);
            else
                return (false, res);
        }

        public async Task<bool> RemoveValueAsync(string key)
        {
            var pair = await context.KeyValuePairs.SingleOrDefaultAsync(c => c.Key == key).ConfigureAwait(false);

            if (pair == null)
                return false;

            pair.Value = null;
            int x = await context.SaveChangesAsync().ConfigureAwait(false);

            return (x > 0) ? true : false;

        }
    }
}
