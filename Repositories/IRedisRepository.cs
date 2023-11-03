namespace BankPassword.Repositories {
    public interface IRedisRepository {
        Task Set(string key, string value);
        Task<string> Get(string key);
    }
}
