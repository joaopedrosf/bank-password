namespace BankPassword.Repositories {
    public interface IRedisRepository {
        Task Set(string key, string value, TimeSpan expirationTime = default);
        Task<string> Get(string key);
    }
}
