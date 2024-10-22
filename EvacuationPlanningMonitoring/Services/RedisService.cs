using EvacuationPlanningMonitoring.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace EvacuationPlanningMonitoring.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _db;
        public RedisService(IConnectionMultiplexer connectionMultiplexer) 
        { 
            _connectionMultiplexer = connectionMultiplexer;
            _db = connectionMultiplexer.GetDatabase();
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (!value.HasValue)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, serializedValue, expiration);
        }

        public async Task<bool> UpdateAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var exists = await _db.KeyExistsAsync(key);
            if (!exists)
                return false;

            await SetAsync(key, value, expiration);
            return true;
        }
    }
}
