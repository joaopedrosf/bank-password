using BankPassword.Repositories.Connections;
using StackExchange.Redis;

namespace BankPassword.Repositories {
    public class RedisRepository : IRedisRepository {
        private readonly IDatabase _database;

        public RedisRepository(IRedisConnectionFactory redisConnectionFactory) {
            _database = redisConnectionFactory.GetConnection().GetDatabase();
        }

        public async Task Set(string key, string value, TimeSpan expirationTime = default) {
            await _database.StringSetAsync(key, value);
            if (expirationTime != default) {
                await _database.KeyExpireAsync(key, expirationTime);
            }
        }

        public async Task<string> Get(string key) {
            return (await _database.StringGetAsync(key)).ToString();
        }
    }
}
