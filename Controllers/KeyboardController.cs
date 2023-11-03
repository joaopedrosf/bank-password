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
    }
}