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
            var examplePassword = BCrypt.Net.BCrypt.EnhancedHashPassword("654321");
            var sessionKeyboard = await GetKeyboardFromSession(sessionId);
            var passwordPossibilities = GeneratePasswordPossibilities(sessionKeyboard, buttonSequence);

            foreach (var possibility in passwordPossibilities) {
                if (BCrypt.Net.BCrypt.EnhancedVerify(possibility, examplePassword)) {
                    return true;
                }
            }

            return false;
        }

        private List<string> GeneratePasswordPossibilities(Keyboard keyboard, List<byte> buttonSequence) {
            var buttonsClicked = new List<KeyboardButton>();
            foreach (var button in buttonSequence) {
                buttonsClicked.Add(keyboard.Buttons[button]);
            }
            var passwordPossibilities = new List<string>();
            GeneratePossibility(buttonsClicked, 0, string.Empty, passwordPossibilities);

            return passwordPossibilities;
        }

        private void GeneratePossibility(List<KeyboardButton> buttonsClicked, int index, string currentCombination, List<string> passwordPossibilities) {
            if (index == buttonsClicked.Count) {
                passwordPossibilities.Add(currentCombination);
                return;
            }
        
            var button = buttonsClicked[index];

            GeneratePossibility(buttonsClicked, index + 1, currentCombination + button.FirstNumber, passwordPossibilities);
            GeneratePossibility(buttonsClicked, index + 1, currentCombination + button.SecondNumber, passwordPossibilities);
        }
    }
}
