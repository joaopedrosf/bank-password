using StackExchange.Redis;

namespace BankPassword.Repositories.Connections {
    public class RedisConnectionFactory : IRedisConnectionFactory {
        private readonly IConfiguration _configuration;
        public RedisConnectionFactory(IConfiguration configuration) {
            _configuration = configuration;
        }

        public ConnectionMultiplexer GetConnection() {
            string host = _configuration.GetSection("redis")["host"];
            string port = _configuration.GetSection("redis")["port"];
            string password = _configuration.GetSection("redis")["password"];

            ConfigurationOptions configurationOptions = new ConfigurationOptions {
                EndPoints = { $"{host}:{port}" },
                Password = password,
                AbortOnConnectFail = false
            };

            return ConnectionMultiplexer.Connect(configurationOptions);
        }
    }
}
