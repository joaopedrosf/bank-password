using BankPassword.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankPassword.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class KeyboardController : ControllerBase {

        [HttpGet]
        public IActionResult Get() {
            var keyboard = new Keyboard();
            keyboard.Generate();
            return Ok(keyboard);
        }
    }
}