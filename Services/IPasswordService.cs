using BankPassword.Models;

namespace BankPassword.Services {
    public interface IPasswordService {
        public Task<PasswordSession> CreatePasswordSession();
        public Task<Keyboard> GetKeyboardFromSession(string sessionId);
        public Task<bool> CheckPassword(string sessionId, List<byte> buttonSequence);
    }
}
