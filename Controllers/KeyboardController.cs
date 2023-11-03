using BankPassword.Models;
using BankPassword.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankPassword.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class KeyboardController : ControllerBase {

        private readonly PasswordService _passwordService;

        public KeyboardController(PasswordService passwordService) {
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ActionResult<PasswordSession>> Get() {
            var session = await _passwordService.CreatePasswordSession();
            return Ok(session);
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<Keyboard>> Get(string sessionId) {
            var keyboard = await _passwordService.GetKeyboardFromSession(sessionId);
            return Ok(keyboard);
        }

        [HttpPost("check/{sessionId}")]
        public async Task<ActionResult<bool>> CheckPassword(
            string sessionId, 
            [FromBody] List<byte> buttonSequence
            ) {
            var isPasswordCorrect = await _passwordService.CheckPassword(sessionId, buttonSequence);
            return Ok(isPasswordCorrect);
        }
    }
}