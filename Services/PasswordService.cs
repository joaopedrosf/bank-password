using BankPassword.Models;
using BankPassword.Repositories;
using System.Text.Json;

namespace BankPassword.Services {
    public class PasswordService : IPasswordService {
        private readonly IRedisRepository _redisRepository;

        public PasswordService(IRedisRepository redisRepository) {
            _redisRepository = redisRepository;
        }

        public async Task<PasswordSession> CreatePasswordSession() {
            var session = new PasswordSession {
                Id = Guid.NewGuid().ToString(),
                Keyboard = new Keyboard()
            };

            await _redisRepository.Set(session.Id, JsonSerializer.Serialize(session.Keyboard), TimeSpan.FromMinutes(2));
            return session;
        }

        public async Task<Keyboard> GetKeyboardFromSession(string sessionId) {
            var keyboard = JsonSerializer.Deserialize<Keyboard>(await _redisRepository.Get(sessionId));
            if (keyboard is null) {
                throw new ArgumentException("Invalid or expired session id");
            }
            return keyboard;
        }

        public async Task<bool> CheckPassword(string sessionId, List<byte> buttonSequence) {
            var examplePassword = BCrypt.Net.BCrypt.EnhancedHashPassword("87654321");
            var sessionKeyboard = await GetKeyboardFromSession(sessionId);
            var passwordPossibilities = KeyboardPossibilitiesCalculator.CalculatePasswordPossibilities(sessionKeyboard, buttonSequence);
            var isPasswordCorrect = false;
            
            ParallelOptions options = new ParallelOptions {
                MaxDegreeOfParallelism = 4
            };

            Parallel.ForEach(passwordPossibilities, options, (possibility, state) => {
                if (BCrypt.Net.BCrypt.EnhancedVerify(possibility, examplePassword)) {
                    isPasswordCorrect = true;
                    state.Stop();
                }
            });

            return isPasswordCorrect;
        }
    }
}
