using BankPassword.Models;
using BankPassword.Repositories;
using System.Text.Json;

namespace BankPassword.Services {
    public class PasswordService {
        private readonly IRedisRepository _redisRepository;

        public PasswordService(IRedisRepository redisRepository) {
            _redisRepository = redisRepository;
        }

        public async Task<PasswordSession> CreatePasswordSession() {
            var session = new PasswordSession {
                Id = Guid.NewGuid().ToString(),
                Keyboard = new Keyboard()
            };

            await _redisRepository.Set(session.Id, JsonSerializer.Serialize(session.Keyboard));
            return session;
        }

        public async Task<bool> CheckPassword(string password) {
            var passwordFromRedis = await _redisRepository.Get("password");
            return password == passwordFromRedis;
        }
    }
}
