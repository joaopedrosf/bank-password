using StackExchange.Redis;

namespace BankPassword.Repositories.Connections {
    public interface IRedisConnectionFactory {
        ConnectionMultiplexer GetConnection();
    }
}
